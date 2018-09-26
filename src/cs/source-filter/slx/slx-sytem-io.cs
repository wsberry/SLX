using System;
using System.Diagnostics;
using System.IO;

namespace slx.system
{
    /// <summary>
    /// TODO: Cleanup and rename.
    /// </summary>
    public sealed partial class io
    {
        /// <summary>
        /// Verifies that a path is a valid directory.
        /// </summary>
        /// <param name="directory">The directory to check.</param>
        /// <returns>'true' if the string value is a valid directory.</returns>
        public static bool directory_format_is_correct(string directory)
        {
            // This implementation is heavy, but works across most if not
            // all directory formats (Linux and Windows OS). Using other APIs 
            // and checking against invalid characters and directory syntax
            // proved that there are inconsistencies in the provided .NET
            // libraries.
            //
            try
            {
                if (Directory.Exists(directory)) return true;
                Directory.CreateDirectory(directory);
            }
            catch
            {
                // Ignore
                return false;
            }

            if (!Directory.Exists(directory)) return false;

            Directory.Delete(directory);

            return true;
        }

        /// <summary>
        /// Check quickly if a directory contains files.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>'true' if directory is empty or does not exist.</returns>
        public static bool is_directory_empty(string path)
        {
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
            {
                return true;
            }

            if (path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                path += "*";
            else
                path += Path.DirectorySeparatorChar + "*";

            var findHandle = windows_api.native.FindFirstFile(path, out var findData);

            if (findHandle == windows_api.native.INVALID_HANDLE_VALUE) return true;
            //
            //Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
            //
            var empty = true;
            try
            {
                do
                {
                    if (findData.cFileName != "." && findData.cFileName != "..")
                    {
                        empty = false;
                        break;
                    }
                } while (windows_api.native.FindNextFile(findHandle, out findData));
            }
            finally
            {
                windows_api.native.FindClose(findHandle);
            }

            return empty;

            // assume empty...
            //
            //throw new Exception("Failed to get directory first file",
            //    Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));
        }

        /// <summary>
        /// Use to check if a path pointing to a directory vs pointing
        /// to a file.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>
        /// 'true' if the path is pointing to a directory.
        /// </returns>
        public static bool is_directory(string path)
        {
            return !string.IsNullOrEmpty(path) && File.GetAttributes(path).HasFlag(FileAttributes.Directory);
        }

        /// <summary>
        /// Use to check if a path pointing to a file vs pointing
        /// to a directory.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>
        /// 'true' if the path is pointing to a file.
        /// </returns>
        public static bool is_file(string path)
        {
            return !string.IsNullOrEmpty(path) && !File.GetAttributes(path).HasFlag(FileAttributes.Directory);
        }

        /// <summary>
        /// Acquires the users 'My Documents'
        /// folder.
        /// </summary>
        public static string MyDocumentsFolder => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <summary>
        /// Acquires LocalApplicationData directory.
        /// </summary>
        public static string LocalApplicationData => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>
        /// Exec application with admin priviledges on Windows.
        /// </summary>
        /// <param name="appName">
        /// The application to run.</param>
        /// <param name="arguments">
        /// Any arguments the application may require.
        /// </param>
        public static void exec_as_admin(string appName, string arguments)
        {
            Debug.Assert(!string.IsNullOrEmpty(appName));
          
            var proc = new Process
            {
                StartInfo =
                {
                    FileName = appName,
                    Arguments = arguments,
                    UseShellExecute = true,
                    Verb = "runas"
                }
            };
            proc.Start();
        }

        /// <summary>
        /// Ensures that the given folder exists. If it does not
        /// exist then it is created. It creates all directories 
        /// and subdirectories as specified by the folder path 
        /// designated.
        /// 
        /// This routine will take a full path (a path that
        /// contains a file name) and do the correct thing.
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static bool EnsureFolder(string folder)
        {
            Debug.Assert(!string.IsNullOrEmpty(folder));

            var folderExists = false;

            try
            {
                if (!string.IsNullOrEmpty(folder) && is_file_path_grammar_correct(folder))
                {
                    if (!(folderExists = Directory.Exists(folder)))
                    {
                        var path = Path.GetDirectoryName(folder);
                        if (path != null) Directory.CreateDirectory(path);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
            }

            // Post-Conditions:
            //
            Debug.Assert(folderExists, "The folder should exist!");

            return Directory.Exists(folder);
        }

        /// <summary>
        /// Use to check if the given directory is well
        /// formed (i.e. does not contain invalid directory
        /// characters).
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static bool IsWellFormedDirectory(string dir)
        {
            Debug.Assert(!string.IsNullOrEmpty(dir));
            if (string.IsNullOrEmpty(dir)) return false;
            var invalidPathChars = get_invalid_path_chars();
            return -1 == dir.IndexOfAny(invalidPathChars);
        }

        /// <summary>
        /// Use to check if the given path is well
        /// formed (i.e. does not contain invalid path
        /// characters).
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool is_file_path_grammar_correct(string path)
        {
            return (is_file_name_grammar_correct(path) &&
                    IsWellFormedDirectory(path));
        }

        /// <summary>
        /// Use to check if the given file name is well
        /// formed (i.e. does not contain invalid file
        /// name characters).
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool is_file_name_grammar_correct(string fileName)
        {
            Debug.Assert(!string.IsNullOrEmpty(fileName));
            if (string.IsNullOrEmpty(fileName)) return false;
            var invalidFileChars = get_invalid_file_name_chars();
            return (-1 == fileName.IndexOfAny(invalidFileChars));
        }

        /// <summary>
        /// Gets an array of characters not allowed in a 
        /// file name.
        /// </summary>
        /// <returns></returns>
        public static char[] get_invalid_file_name_chars()
        {
            return Path.GetInvalidFileNameChars();
        }

        /// <summary>
        /// Gets an array of characters not allowed in a 
        /// file path.
        /// </summary>
        /// <returns></returns>
        public static char[] get_invalid_path_chars()
        {
            // char[] invalid_path_chars = System.IO.Path.get_invalid_path_chars();
            //
            char[] t = { '<', '>', '|', '*', '?' };
            return t;
        }



    }
}