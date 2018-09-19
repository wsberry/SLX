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
using System.Diagnostics;

namespace slx
{
    namespace system
    {
        /// <summary>
        /// TODO: Update for Linux/OS X
        /// </summary>
        public static class environment
        {
            /// <summary>
            /// Gets the environment variable if it is alread defined. Otherwise
            /// the environment variable is set with a default value if 'defaultValue'
            /// is not null.
            /// </summary>
            /// <param name="key">
            /// The key to set.
            /// </param>
            /// <param name="defaultValue">
            /// An option default fixup value.
            /// </param>
            /// <param name="force">
            /// Force the update even it the environment variable is already defined.
            /// </param>
            /// <returns>Key (environment var) and Value KeyValuePair.</returns>
            public static KeyValuePair get_or_set_environmen_variable(string key, string defaultValue = "", bool force = false)
            {
                var tmp = new KeyValuePair(key, get_environment_variable(key, defaultValue, force));

                if (!string.IsNullOrEmpty(tmp.Value) || null == defaultValue) return tmp;

                // Set the environment variable with a fixup.
                //
                tmp.Value = defaultValue;

                set_environment_variable(tmp.Key, tmp.Value);

                return tmp;
            }

            /// <summary>
            /// Sets an envionment variable at the process and user target levels.
            /// <see cref="EnvironmentVariableTarget.User"/>
            /// <see cref="EnvironmentVariableTarget.Process"/>
            /// </summary>
            /// <param name="key">The environment key.</param>
            /// <param name="value">The value to be assigned.</param>
            public static void set_environment_variable(string key, string value)
            {
                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return;
                var tmp = get_environment_variable(key);

                // Setting environment variables is expensive in time.
                // Therefore only do it when required.
                //
                if (tmp == value) return;
                Environment.SetEnvironmentVariable(key, value, EnvironmentVariableTarget.User);
                Environment.SetEnvironmentVariable(key, value, EnvironmentVariableTarget.Process);
            }

            /// <summary>
            /// Tries to get an environment variable. If it returns empty or null then the 
            /// fixup value is assigned to the environment variable and the assigned
            /// value is returned.
            /// </summary>
            /// <param name="key">
            /// The key of the environment variable.
            /// </param>
            /// <param name="fixup">
            /// A default value for an unassigned environment variable.
            /// </param>
            /// <param name="force">
            /// Force the fixup value to replace any previously assigned value.
            /// </param>
            /// <returns>The value assigned to environment variable.
            /// Returns 'fixup' on exception.
            /// </returns>
            public static string get_environment_variable(string key, string fixup = "", bool force = false)
            {
                try
                {
                    if (string.IsNullOrEmpty(key)) return fixup;
                    if (force) set_environment_variable(key, fixup);

                    // Try to get the value of the environment variable, first
                    // from the User level, and then the Process level.
                    //
                    var value = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.User);

                    if (string.IsNullOrEmpty(value))
                    {
                        value = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
                    }

                    if (!string.IsNullOrEmpty(value))
                    {
                        return value;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }

                // The fixup is being used if it is defined.
                //
                if (string.IsNullOrEmpty(fixup)) return fixup;

                // Use the fixup as the value of the environment variable.
                //
                set_environment_variable(key, fixup);

                return fixup;
            }
        }
    }
}