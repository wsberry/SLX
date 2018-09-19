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
using System.Diagnostics;
using System.Management;

namespace slx.system
{
    /// <summary>
    /// Kill a process and any associate child processes.
    /// </summary>
    public static class processes
    {
        public static void kill_process_and_children(int pid)
        {
            var processSearcher = new ManagementObjectSearcher
                ("Select * From Win32_Process Where ParentProcessID=" + pid);
            var processCollection = processSearcher.Get();

            try
            {
                var proc = Process.GetProcessById(pid);
                if (!proc.HasExited) proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
                // Ignore
            }

            foreach (var o in processCollection)
            {
                var mo = (ManagementObject)o;
                kill_process_and_children(Convert.ToInt32(mo["ProcessID"]));
            }
        }
    }
}


