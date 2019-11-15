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
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using slx;
using slx.mvc;
using slx.system;
using slx.system.directory;
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

    public static IIgnoreData DataSource { get; set; }

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


    public static bool UpdateNamespaces = false;

    /// <summary>
    /// A hack to update namespaces in some source files...
    /// </summary>
    public static void FindAndReplaceContent(string destination)
    {
      Debug.Assert(!IsNullOrEmpty(destination));
      Debug.Assert(null != DataSource);

      if (IsNullOrEmpty(destination) || null == DataSource) return;

      if (!UpdateNamespaces) return;

      if (DataSource.IgnoreFileTypeForSearchAndReplace(destination)) return;
      
      var file_text = ReadAllText(destination);

      var updated_text = file_text;

      foreach (var ns in DataSource.ContentReplace)
      {
        updated_text = updated_text.Replace(ns.Key, ns.Value);
      }

      WriteAllText(destination, updated_text);
    }

    public static Func<string, string, bool> Copy = dotnet_system_io_file_copy;
    //public static Func<string, string, bool> Copy = dotnet_buffered_file_copy;

    private static bool dotnet_system_io_file_copy(string source, string destination)
    {
      var r = io.file.copy(source, destination, useFileCopy: true);

      if (r) FindAndReplaceContent(destination);

      return r;
    }

    private static bool dotnet_buffered_file_copy(string source, string destination)
    {
      var r = io.file.copy(source, destination, useFileCopy: false);

      if (r) FindAndReplaceContent(destination);

      return r;
    }

    private static bool cpp_filesystem_file_copy(string source, string destination)
    {
      var r = native.filesystem_copy(source, destination);

      if (r) FindAndReplaceContent(destination);

      return r;
    }

    private static bool cpp_buffered_file_copy(string source, string destination)
    {
      var r = native.buffered_copy(source, destination);

      if (r) FindAndReplaceContent(destination);

      return r;
    }
  }


  /// <summary>
  /// Defines and implements the data model for
  /// the application.
  /// </summary>
  public class AppDataModel : IDataModel
  {
    public DirectoryCopyInfo DirectoryInfo = new DirectoryCopyInfo();

    public Dictionary<string, string> FindAndReplace = new Dictionary<string, string>();

    public string CopyMethod = CopyFileMethod.CS_BufferedFileCopy;

    /// <summary>
    /// The application title.
    /// </summary>
    public string MainFormTitle = @"Directory Filter - SLX";

    /// <summary>
    /// The editor used to edit the configuration file.
    /// </summary>
    public string ModelEditor = AppDataModelExtensions.GetModelEditor();

    [JsonIgnore]
    private IIgnoreData dataToIgnore;

    /// <summary>
    /// The name of the model. Should be a valid file name in this
    /// project.
    /// </summary>
    // TODO: Fix this hack...initialization order is a problem here.
    // TODO: Modify this class to handle dependency injection.
    //
#if BUILD_CUSTOM
    public string ModelName { get; set; } = CUSTOM_Ignore.DATA_MODEL_NAME;
#else
    public string ModelName { get; set; } = Default_Ignore.DATA_MODEL_NAME;
#endif

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
        this.Copy(t);
      }
    }

    /// <summary>
    /// Use property injection to assign a data filter.
    /// </summary>
    [JsonIgnore]
    public IIgnoreData DataToIgnore
    {
      get { return dataToIgnore; }
      set
      {
        // TODO: Fix this relationship. This is a poor relationship to have
        //       and is being used for expediency.
        //
        CopyFileMethod.DataSource = value;
        dataToIgnore = value;
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
      model.load(path, edit: true);
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
          Copy(model, tmp);
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

    public static void Copy(this AppDataModel rhv, AppDataModel lhv)
    {
      rhv.CopyMethod = lhv.CopyMethod;
      rhv.DataToIgnore = lhv.DataToIgnore;
      rhv.DirectoryInfo = lhv.DirectoryInfo;
      rhv.FindAndReplace = lhv.FindAndReplace;
      rhv.MainFormTitle = lhv.MainFormTitle;
      rhv.ModelEditor = lhv.ModelEditor;
      rhv.ModelName = lhv.ModelName;
      CopyFileMethod.DataSource = new Default_Ignore();
      CopyFileMethod.DataSource.ContentReplace = lhv.FindAndReplace;
      CopyFileMethod.DataSource.DataFilters[Ignore.File] = rhv.DirectoryInfo.FileNameFilters;
      CopyFileMethod.DataSource.DataFilters[Ignore.Directory] = rhv.DirectoryInfo.DirectoryNameFilters;
      CopyFileMethod.DataSource.DataFilters[Ignore.Extensions] = rhv.DirectoryInfo.FileExtensionNameFilters;

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
      // TODO: Injection code should go here:
      //
#if BUILD_CUSTOM
      var data_to_ignore = model.DataToIgnore = new CUSTOM_Ignore();
#else
      var data_to_ignore = model.DataToIgnore = new Default_Ignore();
#endif

      model.DirectoryInfo.SourceDirectory = "";
      model.DirectoryInfo.TargetDirectory = "";

      model.DirectoryInfo.FindAndReplaceContent = false;
      model.DirectoryInfo.IncludeSubDirectories = true;
      model.DirectoryInfo.OpenTargetDirectoryWhenDone = false;

      model.FindAndReplace = data_to_ignore.ContentReplace;
      model.DirectoryInfo.FileNameFilters.AddRange(data_to_ignore.DataFilters[Ignore.File]);
      model.DirectoryInfo.DirectoryNameFilters.AddRange(data_to_ignore.DataFilters[Ignore.Directory]);
      model.DirectoryInfo.FileExtensionNameFilters.AddRange(data_to_ignore.DataFilters[Ignore.Extensions]);

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
