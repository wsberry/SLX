// Prologue
//
// SLX - Simple Library Extensions
//
// Copyright 2000-2018 Bill Berry
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// Implements a simple data model example.
//
// endPrologue
#pragma warning disable IDE1006 // Naming styles - suppress lower case

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using slx;
using slx.mvc;
using slx.system;
using slx.system.directory;
using slx.system.windows;
using static System.IO.File;
using static System.String;

namespace source_filter
{
    /// <summary>
    /// Defines the supported file copy mechanisms in this application.
    /// TODO: Decouple from data model and move into its own module.
    /// </summary>
    public static class CopyFileMethod
    {
        public const string CS_SystemIoFileCopy = @"  System.IO.File.Copy";
        public const string CS_BufferedFileCopy = @"  C# Buffered File Copy";
        public const string CPP_BufferedFileCopy = @"  C++ Buffered File Copy";
        public const string CPP_StdFileSystemCopy = @"  C++ std::filesystem::copy";

        public static void SetIndex(this ComboBox cb, string itemText)
        {
            // This is a hack. Strongly bind the index to the 
            // string item. 
            //
            cb.SelectedIndex = new Dictionary<string, int>
            {
                {CS_SystemIoFileCopy, 0},
                {CS_BufferedFileCopy, 1},
                {CPP_BufferedFileCopy, 2},
                {CPP_StdFileSystemCopy, 3}
            }[itemText];

            SetCopyMethod(cb);
        }

        public static void SetCopyMethod(ComboBox cb)
        {
            switch (cb.SelectedIndex)
            {
                case 0:
                    Copy = dotnet_system_io_file_copy;
                    break;
                case 1:
                    Copy = dotnet_buffered_file_copy;
                    break;
                case 2:
                    Copy = cpp_buffered_file_copy;
                    break;
                case 3:
                    Copy = cpp_filesystem_file_copy;
                    break;
            }

            Debug.Assert(null != Copy, "Copy method should never be null value!");
        }

        public static Func<string, string, bool> Copy = dotnet_system_io_file_copy;

        private static bool dotnet_system_io_file_copy(string source, string destination)
        {
            return io.file.copy(source, destination, useFileCopy: true);
        }

        private static bool dotnet_buffered_file_copy(string source, string destination)
        {
            return io.file.copy(source, destination, useFileCopy: false);
        }

        private static bool cpp_filesystem_file_copy(string source, string destination)
        {
            return native.cpp_filesystem_file_copy(source, destination);
        }

        private static bool cpp_buffered_file_copy(string source, string destination)
        {
            return native.cpp_buffered_file_copy(source, destination, Units.Digital.WindowsOS.KB);
        }
    }

    /// <summary>
    /// Defines and implements the data model for
    /// the application.
    /// </summary>
    public class AppDataModel : IDataModel
    {
        public DirectoryCopyInfo DirectoryInfo = new DirectoryCopyInfo();

        public string CopyMethod = CopyFileMethod.CS_BufferedFileCopy;

        /// <summary>
        /// The application title.
        /// </summary>
        public string MainFormTitle = @"Directory Filter - SLX";

        /// <summary>
        /// The editor used to edit the configuration file.
        /// </summary>
        public string ModelEditor = AppDataModelExtensions.GetModelEditor();

        /// <summary>
        /// The name of the model. Should be a valid file name in this
        /// project.
        /// </summary>
        public string ModelName { get; set; } = ".SourceFilter";

        [JsonIgnore]
        public byte[] data
        {
            get
            {
                var json = JsonConvert.SerializeObject(this, Formatting.None);
                return json.tobytes();
            }
            set
            {
                var json = value.tostring();
                var t = JsonConvert.DeserializeObject<AppDataModel>(json);
                this.copy(t);
            }
        }
    }

    /// <summary>
    /// Algorithms and actions that may be performed on the
    /// model.
    /// </summary>
    public static class AppDataModelExtensions
    {
        public static void edit(this AppDataModel model, string path = null)
        {
            // The model load operation contains the code 
            // to open the model data store in Notepad++ or
            // Notepad.
            //
            model.load(path, edit:true);
        }

        public static void delete(this AppDataModel model, string path = null)
        {
            path = EnsureValidPath(model, path);

            if (Exists(path))
            {
                Delete(path);
            }
        }

        /// <summary>
        /// Saves the data model associated with this application.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="path">An optional path for saving the mode to.
        /// If one is not provided then the path directory will be
        /// 'Environment.SpecialFolder.ApplicationData' and its file name
        /// will be 'AppDataModel.ModelName'.</param>
        public static void save(this AppDataModel model, string path = null)
        {
            // Assign a default path name for saving the AppDataModel
            // if one is not provided.
            //
            path = EnsureValidPath(model, path);

            var json = JsonConvert.SerializeObject(model, Formatting.Indented);

            WriteAllText(path, json);
        }

        /// <summary>
        /// Loads the data model into the application or an editor
        /// to edit the default settings of the model.
        /// </summary>
        /// <param name="model">The model to be loaded.</param>
        /// <param name="path">The path to the model.</param>
        /// <param name="edit">Set true to open model in the designated editor.</param>
        public static void load(this AppDataModel model, string path = null, bool edit = false)
        {
            // Assign a default path name for saving the AppDataModel
            // if one is not provided. To force a default path just
            // pass in null or string.empty().
            //
            path = EnsureValidPath(model, path);

            if (Exists(path))
            {
                if (edit)
                {
                    Process.Start(model.ModelEditor, path);
                    return;
                }

                var json = ReadAllText(path);
                try
                {
                    var tmp = JsonConvert.DeserializeObject<AppDataModel>(json);
                    copy(model, tmp);
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    //
                    // TODO: We will overwrite the user's configuration
                    //       file if there is an exception from their
                    //       configuration. This is by no means a robust
                    //       way to handle such a condition. This
                    //       said the tool suite my needs as is.
                }
            }
   
            CreateDefaultModelData(model);

            save(model);
        }

        public static void copy(this AppDataModel rhv, AppDataModel lhv)
        {
            rhv.DirectoryInfo = lhv.DirectoryInfo;
            rhv.MainFormTitle = lhv.MainFormTitle;
            rhv.ModelName = lhv.ModelName;
            rhv.ModelEditor = lhv.ModelEditor;
            rhv.CopyMethod = lhv.CopyMethod;
        }

        /// <summary>
        /// Uses 'Notepad++' as the default configuration editor
        /// if found, otherwise 'Notepad' is used.
        /// </summary>
        /// <returns>Path to the editor.</returns>
        public static string GetModelEditor()
        {
            const string notepad = "Notepad";

            // See if 'Notepad++' is present...
            //
            var p = Environment.GetEnvironmentVariable("PATH");
            if (null != p && p.Contains("Notepad++")) return "Notepad++";
            return notepad;
        }

        public static string GetConfigurationPath(this AppDataModel model)
        {
            return EnsureValidPath(model, null);
        }

        /// <summary>
        /// Create default model data for the user.
        /// </summary>
        /// <param name="model"></param>
        private static void CreateDefaultModelData(AppDataModel model)
        {
            // Create default settings for the user
            //
            model.DirectoryInfo.DirectoryNameFilters.Add(".vs");
            model.DirectoryInfo.DirectoryNameFilters.Add("_build");
            model.DirectoryInfo.DirectoryNameFilters.Add("obj");
            model.DirectoryInfo.DirectoryNameFilters.Add("bin");
            model.DirectoryInfo.DirectoryNameFilters.Add("Debug");
            model.DirectoryInfo.DirectoryNameFilters.Add("Release");
            model.DirectoryInfo.DirectoryNameFilters.Add("x64");
            model.DirectoryInfo.DirectoryNameFilters.Add("*.usr");
            model.DirectoryInfo.DirectoryNameFilters.Add("*.db");
            model.DirectoryInfo.SourceDirectory = "";
            model.DirectoryInfo.TargetDirectory = "";
            model.DirectoryInfo.OpenTargetDirectoryWhenDone = false;
            model.DirectoryInfo.IncludeSubDirectories = true;
        }

        private static string EnsureValidPath(AppDataModel model, string path)
        {
            var fixupPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            fixupPath = Path.Combine(fixupPath, model.ModelName);

            if (IsNullOrEmpty(path))
            {
                return fixupPath;
            }

            try
            {
                // Invalid path should throw exception.
                // 
                if (Exists(path)) return path;
            }
            catch
            {
                // ignored
            }

            return fixupPath;
        }
    }
}
