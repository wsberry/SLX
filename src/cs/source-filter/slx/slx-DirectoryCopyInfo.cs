// Prologue
//
// Experimental (i.e. code may not be optimized or correct)
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
// endPrologue
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace slx.system.directory
{
    /// <summary>
    /// Defines fields that are used to copy a source
    /// directory to another directory.
    /// </summary>
    public sealed class DirectoryCopyInfo : IValidate
    {
        /// <summary>
        /// Open the target directory in an explorer view
        /// when the copy is completed.
        /// </summary>
        public bool OpenTargetDirectoryWhenDone = false;

        /// <summary>
        /// Include all subdirectories in the copy.
        /// </summary>
        public bool IncludeSubDirectories = false;

        /// <summary>
        /// The source directory to be copied.
        /// </summary>
        public string SourceDirectory = "";

        /// <summary>
        /// The target directory to be created or overwritten.
        /// </summary>
        public string TargetDirectory = "";

        /// <summary>
        /// File extension filters (wildcards permitted).
        /// </summary>
        public List<string> FileExtensionNameFilters = new List<string>();

        /// <summary>
        /// File name filters (wildcards permitted).
        /// </summary>
        public List<string> FileNameFilters = new List<string>();

        /// <summary>
        ///  Directory name filters (wildcards NOT permitted).
        /// </summary>
        public List<string> DirectoryNameFilters = new List<string>();

        /// <summary>
        /// A method that may be called to update a view object.
        /// TODO: Move this out of here!
        /// </summary>
        /// <remarks>Implement to be thread safe.</remarks>
        [JsonIgnore]
        public Action<ProgressStatusInfo> OnFileCopyNotify = null;

        public bool IsValid()
        {
            return DirectoryCopyInfoExtensions.IsValid(this);
        }
    }

    /// <summary>
    /// Implements algorithms for  DirectoryCopyInfo types.
    /// </summary>
    public static class DirectoryCopyInfoExtensions
    {
        /// <summary>
        /// Use to check if a 'DirectoryCopyInfo' instance is in
        /// a valid state.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="overwrite">
        /// It 'overwrite' is false and the target exists
        /// then this function will return false.
        /// </param>
        /// <returns>
        /// 'true' if copy info is valid.
        /// </returns>
        public static bool IsValid(this DirectoryCopyInfo info, bool overwrite = false)
        {
            var target = info.TargetDirectory;

            var check_0 = (null != info.SourceDirectory) &&
                   Directory.Exists(info.SourceDirectory) && (null != target);

            var check_1 = false;
            try
            {
                if (null == target || Directory.Exists(target) && overwrite)
                {
                    return false;
                }

                if (!Directory.Exists(target))
                {
                    Directory.CreateDirectory(target);
                }

                check_1 = Directory.Exists(target);
            }
            catch(Exception ex)
            {
                Debug.Assert(false, ex.Message);
                // Ignored
            }

            return check_0 && check_1;
        }
    }
}