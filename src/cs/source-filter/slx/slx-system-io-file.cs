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
// Initial prototype.
//
// endPrologue

using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace slx.system
{
    namespace windows
    {
        public static class winforms
        {
            public static void make_topmost(this Form form)
            {
                // First time does not always bring the form to the top.
                //
                form.TopMost = true;
                form.TopMost = true;
            }

            public static void MakeForeground(this Form form)
            {
                make_topmost(form);
                form.TopMost = false;
            }

            /// <summary>
            /// Use this class to 
            /// </summary>
            public sealed class winform_adapter : IWin32Window
            {
                // http://ryanfarley.com/blog/archive/2004/03/23/465.aspx

                /// <summary>
                /// E.g. var windowToCenterOn = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
                /// </summary>
                /// <param name="handle"></param>
                public winform_adapter(IntPtr handle)
                {
                    Handle = handle;
                }

                public winform_adapter()
                {
                    Handle = Application.OpenForms[0].Handle;
                }

                public IntPtr Handle { get; }

                public static FormCollection application_forms()
                {
                    return Application.OpenForms;
                }
            }

            public static void ShowMessage(string caption, string message,
                MessageBoxIcon icon = MessageBoxIcon.Information)
            {
                var center = new winform_adapter(Process.GetCurrentProcess().MainWindowHandle);

                MessageBox.Show
                (
                    center,
                    message,
                    caption,
                    MessageBoxButtons.OK,
                    icon
                );
            }

            public static DialogResult ShowMessage(string caption, string message,
                MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
            {
                var center = new winform_adapter(Process.GetCurrentProcess().MainWindowHandle);

                return MessageBox.Show
                (
                    center,
                    message,
                    caption,
                    buttons,
                    icon
                );
            }

            public static void FileAccessDenied(string caption, string body)
            {
                var center = new winform_adapter(Process.GetCurrentProcess().MainWindowHandle);

                MessageBox.Show
                (
                    center,
                    body,
                    caption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
    }

    public static class threads
    {
        public static void thread_yield()
        {
            Application.DoEvents();
            Thread.Sleep(0);
        }

        public static void wait(int milliseconds)
        {
            for (var i = 0; i < milliseconds; ++i)
            {
                thread_yield();
                Thread.Sleep(i);
            }
        }
    }

    public sealed partial class io
    {
        public static class directory
        {
            /// <summary>
            /// Creates the directory tree if it does not exist.
            /// </summary>
            /// <param name="path">A directory or directory tree.</param>
            /// <param name="values">A list of directories to create.</param>
            /// <returns>A directory path.</returns>
            public static string make_directory(string path, params string[] values)
            {
                try
                {
                    path = values.Aggregate(path, Path.Combine);
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                }
                catch
                {
                    // Ignore
                }
                return path;
            }

            public static IEnumerable<string> find_file_by_name(string fileSearchPattern, string rootFolderPath, string foldersToExclude= "Windows")
            {
                var pending = new Queue<string>();

                pending.Enqueue(rootFolderPath);

                var excludes = foldersToExclude.ToLower();

                while (pending.Count > 0)
                {
                    threads.thread_yield();

                    rootFolderPath = pending.Dequeue();

                    if (excludes.Contains(rootFolderPath.ToLower())) continue;
 
                    string[] tmp;

                    try
                    {
                        tmp = Directory.GetFiles(rootFolderPath, fileSearchPattern);
                    }
                    catch
                    {
                        continue;
                    }

                    foreach (var t in tmp) yield return t;

                    tmp = Directory.GetDirectories(rootFolderPath);

                    foreach (var t in tmp) pending.Enqueue(t);
                }
            }      
        }

        public static class file
        {
            /// <summary>
            /// copy source file to destination;
            /// </summary>
            /// <param name="source">Source</param>
            /// <param name="destination">Destination</param>
            /// <param name="useFileCopy">
            /// Set 'true' to use System.File.Copy; Historically this has
            /// been significantly slower.
            /// </param>
            /// <returns></returns>
            public static bool copy(string source, string destination, bool useFileCopy = false)
            {
                // Pre-Conditions:
                //
                Debug.Assert(File.Exists(source));
                Debug.Assert(!string.IsNullOrEmpty(destination));

                // Why this choice? At one time the following code
                // was significantly slower as compared to the
                // using the buffered copy implementation below.
                // This has not been tested in newer versions of
                // .NET. 
                //
                if (useFileCopy)
                {
                    try
                    {
                        // The directory must exist:
                        //
                        var d = Path.GetDirectoryName(destination);
                        if (!Directory.Exists(d))
                        {
                          Debug.Assert(d != null, nameof(d) + " != null");
                          Directory.CreateDirectory(d);
                        }

                        File.Copy(source, destination);
                    }
                    catch
                    {
                        return false;
                    }

                    return true;
                }

                // TODO: Profile to determine optimal size/performance.
                //       Note this seems to be really good so far.
                //
                var bufferSize = 2 * Units.Digital.WindowsOS.KB;

                FileStream fsSource = null;
                FileStream fsDestination = null;

                try
                {
                    // Create destination directory if required:
                    //
                    var dst = Path.GetDirectoryName(destination);
                    if (!string.IsNullOrEmpty(dst) && !Directory.Exists(dst))
                    {
                        Directory.CreateDirectory(dst);
                    }

                    fsSource = File.OpenRead(source);

                    fsDestination = new FileStream(destination, FileMode.Create, FileAccess.Write, FileShare.Write,
                        bufferSize);

                    if (fsSource.Length < bufferSize)
                    {
                        bufferSize = (int) fsSource.Length;
                    }

                    var buffer = new byte[bufferSize];

                    int bytesRead;

                    while (0 != (bytesRead = fsSource.Read(buffer, 0, bufferSize)))
                    {
                        fsDestination.Write(buffer, 0, bytesRead);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return false;
                }
                finally
                {
                    fsSource?.Close();
                    fsDestination?.Close();
                }

                return true;
            }

            /// <summary>
            /// Sometimes a file is locked and cannot be moved.
            /// This method will copy the file in those cases.
            /// </summary>
            /// <param name="source"></param>
            /// <param name="destination"></param>
            /// <returns></returns>
            public static bool safe_move(string source, string destination)
            {
                Debug.Assert(File.Exists(source), @"The 'source' file should exist!");

                if (
                    string.IsNullOrEmpty(source) ||
                    string.IsNullOrEmpty(destination) ||
                    //
                    // Source file must exist.
                    //
                    !File.Exists(source)
                ) return false;


                if (File.Exists(destination)) File.Delete(destination);

                try
                {
                    File.Move(source, destination);
                    return true;
                }
                catch
                {
                    return copy(source, destination);
                }
            }

            /// <summary>
            /// Create a temporary file name with the given extension.
            /// </summary>
            /// <param name="extension"></param>
            /// <returns>A unique file name.</returns>
            public static string create_temp_path(string extension)
            {
                var t = Path.GetTempFileName();
                if (!extension.Contains(".")) extension = extension.Insert(0, ".");
                var fn = Path.GetFileNameWithoutExtension(t) + extension;
                var d = Path.GetDirectoryName(t);
                return Path.Combine(d, fn);
            }

            public static bool try_delete(string path, ref string error)
            {
                try
                {
                    if (!File.Exists(path)) return false;
                    File.Delete(path);
                    return true;
                }
                catch (Exception ex)
                {
                    error += error + "\n" + ex.Message;
                }
                return false;
            }
        }
    }
}


