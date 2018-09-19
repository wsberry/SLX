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
// Initial prototype.
//
// endPrologue

using System;
using System.Runtime.InteropServices;

namespace slx.system.windows
{
    /// <summary>
    /// Call 'NativeMethods' by convention.
    /// </summary>
    public static class NativeMethods
    {
        #region Native Windows OS imports
        public const int VK_RETURN = 0x0D;
        public const int WM_KEYDOWN = 0x0100;

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(
            IntPtr hwndParent,
            IntPtr hwndChildAfter,
            string lpszClass,
            string lpszWindows);

        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(
            IntPtr hWnd,
            int msg,
            IntPtr wParam,
            IntPtr lParam);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = false)]
        public static extern IntPtr GetDesktopWindow();

        // Source: https://stackoverflow.com/questions/755574/how-to-quickly-check-if-folder-is-empty-net
        //         Modified.
        //
        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WIN32_FIND_DATA
        {
            private readonly uint dwFileAttributes;
            private readonly System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
            private readonly System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
            private readonly System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
            private readonly uint nFileSizeHigh;
            private readonly uint nFileSizeLow;
            private readonly uint dwReserved0;
            private readonly uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public readonly string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            private readonly string cAlternateFileName;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindFirstFile(string lpFileName, out WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool FindNextFile(IntPtr hFindFile, out WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool FindClose(IntPtr hFindFile);

        /// <summary>
        /// Verifies that a path is a valid directory.
        /// This does not actually work as expected all the time
        /// therefore use this Windows APi with some caution.
        /// Sometime an invalid directory will be treated as a valid
        /// path.
        /// </summary>
        /// <param name="pszPath">
        /// A pointer to a null-terminated string of maximum length
        /// MAX_PATH that contains the path to verify.
        /// </param>
        /// <returns>
        /// Returns (BOOL)FILE_ATTRIBUTE_DIRECTORY if the path is a
        /// valid directory; otherwise, FALSE.
        /// </returns>
        [DllImport("shlwapi.dll", EntryPoint = "PathIsDirectoryW",
            SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool path_is_directory([MarshalAs(UnmanagedType.LPTStr)]string pszPath);
        #endregion
    }
}
