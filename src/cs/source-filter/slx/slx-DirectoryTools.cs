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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace slx.system.directory
{
    /// <summary>
    /// Prototyoe that contains methods to filter and copy
    /// directory contents based on directory names, file
    /// names, or file extension names.
    /// </summary>
    public static class DirectoryTools
    {
        /// <summary>
        /// Creates a copy of a source directory 
        /// </summary>
        /// <param name="info">DirectoryCopyInfo item instance.</param>
        /// <returns>Number of files copied.</returns>
        public static int CreateFilteredDirectoryCopy(this DirectoryCopyInfo info)
        {
            Debug.Assert(null != info.OnFileCopyNotify, "You should notify the user of the changes being made.");

            var sourceDirectory  = info.SourceDirectory;
            var targetDirectory  = info.TargetDirectory;
            var onFileCopyNotify = info.OnFileCopyNotify;

            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            Debug.Assert(null != onFileCopyNotify);

            var filteredDirectoryList = GetFilteredListOfDirectories(sourceDirectory, info);
            var filteredFiles = GetFilteredDirectoryFilesList(filteredDirectoryList);
         
            // Filter out files to be ignored based on name
            // or extension.
            //
            // Note: Passing by ref to gain a slight performance
            //       improve by not copying the file names.
            //       (i.e. 1 copy vs three).
            //
            var ps = new ProgressStatusInfo {Max = filteredFiles.Count};

            ps.Max = filteredFiles.Count;

            foreach (var t in filteredFiles)
            {
                var filePath = t;
                if (IgnoreExtension(ref filePath, info)) continue;
                if (IgnoreFile(ref filePath, info)) continue;
                try
                {
                    io.file.copy(t, CreateDestinationPath(sourceDirectory,t, targetDirectory));
                }
                catch 
                {
                   // TODO: Implement error handling here.
                }
                ++ps.ProgressValue; // keeps count of files copied.
                ps.Message = Path.GetFileName(t);
                onFileCopyNotify(ps);
               
                // Process all message in que in order to update view.
                // This results in a small performance hit. 
                //
                Application.DoEvents();
            }

            // 'ps.ProgressValue' is the total number of files copied.
            //
            return ps.ProgressValue;
        }

        /// <summary>
        /// Acquires a list of files from a list of directories.
        /// </summary>
        /// <param name="directoriesList">The directories list to search.</param>
        /// <returns>A list of the files found with fully qualified named paths.</returns>
        public static List<string> GetFilteredDirectoryFilesList(IEnumerable<string> directoriesList)
        {
            var filesToCopy = new List<string>();

            foreach (var dir in directoriesList)
            {
                var files = Directory.GetFiles(dir, "*.*", SearchOption.TopDirectoryOnly);

                if (files.Length > 0)
                {
                    filesToCopy.AddRange(files);
                }
            }

            return filesToCopy;
        }

        //<summary>
        // Check for user specified extensions to ignore.
        // </summary>
        // <param name="filePath">
        // The file path to be checked.
        // </param>
        // <returns>
        // 'true' if file should be ignored (i.e. not copied).
        // </returns>
        public static bool IgnoreExtension(ref string filePath, DirectoryCopyInfo info)
        {
            // This should never happen.
            //
            Debug.Assert(!string.IsNullOrEmpty(filePath));

            if (string.IsNullOrEmpty(filePath)) return true;

            var extFilters = info.FileExtensionNameFilters;
    
            var ext = Path.GetExtension(filePath);

            return ShouldIgnore(extFilters, ext); 
        }

        //<summary>
        // Check for user specified extensions to ignore.
        // </summary>
        // <param name="filePath">
        // The file path to be checked.
        // </param>
        // <returns>
        // 'true' if file should be ignored (i.e. not copied).
        // </returns>
        public static bool IgnoreFile(ref string filePath, DirectoryCopyInfo info)
        {
            // This should never happen.
            //
            Debug.Assert(!string.IsNullOrEmpty(filePath));

            if (string.IsNullOrEmpty(filePath)) return true;

            var fileFilters = info.FileNameFilters;

            var fileName = Path.GetFileName(filePath);

            return ShouldIgnore(fileFilters, fileName);
        }

        public static bool IgnoreDirectory(ref string directoryName, DirectoryCopyInfo info)
        {
            // This should never happen.
            //
            Debug.Assert(!string.IsNullOrEmpty(directoryName));

            if (string.IsNullOrEmpty(directoryName)) return true;

            var directoryFilters = info.DirectoryNameFilters;

            var dirName = new DirectoryInfo(directoryName).Name;

            foreach (var filter in directoryFilters)
            {
                if (dirName.iequals(filter))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool ShouldIgnore(IEnumerable<string> filters, string nameToCheck)
        {
            // Prefer 'foreach' over LINQ for readability.
            //
            // i.e.   return filters.Select(fileFilter 
            //               => new wildcard(fileFilter, RegexOptions.IgnoreCase))
            //                  .Any(wildcard => wildcard.IsMatch(nameToCheck));
            //
            //        compared to:
            //
            foreach (var f in filters)
            {
                // Processes regular expressions.
                //
                // E.g.
                //
                //  "*_i*.c" or "*.d*oc", and so forth.
                //
                var wildcard = new io.wildcard(f, RegexOptions.IgnoreCase);

                // Print all matching files
                //
                if (wildcard.IsMatch(nameToCheck))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Acquires a filtered list of directories.
        /// </summary>
        /// <param name="source">The 'root' directory to start the search from.</param>
        /// <param name="info"></param>
        /// <returns>A list of directories found.</returns>
        public static IEnumerable<string> GetFilteredListOfDirectories(string source, DirectoryCopyInfo info)
        {
            var option = SearchOption.AllDirectories;

            if (!info.IncludeSubDirectories)
            {
                option = SearchOption.TopDirectoryOnly;
            }

            var directories = new List<string> {source};
            var tmp = Directory.EnumerateDirectories(source, "*.*", option).ToList();
            directories.AddRange(tmp);
            var filteredDirectories = new List<string>();
          
            foreach (var d in directories)
            {
                var dir = d;
                if (IgnoreDirectory(ref dir, info)) continue;
                filteredDirectories.Add(dir);
            }
            return filteredDirectories;
        }

        /// <summary>
        /// Creates a destination path based on a source path.
        /// </summary>
        /// <param name="sourceRoot">
        /// The source root path. This is replace by the
        /// destFolder path.</param>
        /// <param name="sourcePath">
        /// The source path of the file being copied.
        /// </param>
        /// <param name="destFolder">
        /// The 'top-level' destination folder.
        /// </param>
        /// <returns>
        /// Given the following example:
        ///    sourceRoot=\\Mac\Home\Desktop\Test
        ///    sourcePath=\\Mac\Home\Desktop\Test\glfw-3.2.1\CMake\amd64-mingw32msvc.cmake
        ///    destFolder=C:\Test
        ///    returns a path of 'C:\Test\glfw-3.2.1\CMake\amd64-mingw32msvc.cmake
        /// </returns>
        public static string CreateDestinationPath(string sourceRoot, string sourcePath, string destFolder)
        {
            // Preconditions:
            //
            Debug.Assert(!string.IsNullOrEmpty(sourceRoot), "Argument, 'sourceRoot' should not be null or empty!");
            Debug.Assert(!string.IsNullOrEmpty(sourcePath), "Argument, 'sourcePath' should not be null or empty!");
            Debug.Assert(Directory.Exists(sourceRoot), "Directory should Exist!");
            Debug.Assert(File.Exists(sourcePath), "File should Exist!");

            // No check on 'destFolder'...assuming it is well formed.
            //
            return sourcePath.Replace(sourceRoot, destFolder);
        }
    }
}