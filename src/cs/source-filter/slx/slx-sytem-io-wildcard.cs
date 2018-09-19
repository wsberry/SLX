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
using System.Text.RegularExpressions;

namespace slx.system
{
    public sealed partial class io
    {
        /// <inheritdoc />
        ///  <summary>
        ///  Use to create a wild card search for files or directories using
        ///  the .NET regular expression library.
        ///  <see cref="N:System.Text.RegularExpressions" /> engine.
        ///  </summary>
        ///  <example>
        ///  // Get a list of files from the 'My Documents' folder
        ///  //
        ///  string[] files = System.IO.Directory.GetFiles(
        ///                   System.Environment.GetFolderPath(
        ///                   Environment.SpecialFolder.Personal));
        ///  // Create a new wildcard to search for all
        ///  // .txt files, regardless of case
        ///  wildcard wildcard = new wildcard("*.txt", RegexOptions.IgnoreCase);
        ///  // Print all matching files
        ///  //
        ///  foreach(string file in files)
        ///  {
        ///      if(wildcard.IsMatch(file))
        ///      {
        ///          Console.WriteLine(file);
        ///      }
        ///  }
        ///  </example>
        ///  <references>
        ///  http://www.codeproject.com/csharp/wildcardtoregex.asp
        ///  </references>
        [Serializable]
        public sealed class wildcard : Regex
        {
            /// <inheritdoc />
            /// <summary>
            /// Initializes a wildcard with the given search pattern.
            /// </summary>
            /// <param name="pattern">The wildcard pattern to match.</param>
            public wildcard(string pattern)
                : base(wildcard_to_regex(pattern))
            {
            }

            /// <inheritdoc />
            /// <summary>
            /// Initializes a wildcard with the given search pattern and options.
            /// </summary>
            /// <param name="pattern">The wildcard pattern to match.</param>
            /// <param name="options">A combination of one or more
            /// see cref="System.Text.RegexOptions".</param>
            public wildcard(string pattern, RegexOptions options)
                : base(wildcard_to_regex(pattern), options)
            {
            }

            /// <summary>
            /// Converts a wildcard to a regex.
            /// </summary>
            /// <param name="pattern">The wildcard pattern to convert.</param>
            /// <returns>A regex equivalent of the given wildcard.</returns>
            public static string wildcard_to_regex(string pattern)
            {
                var s = pattern;
                try
                {
                    s = "^" + Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".") + "$";
                }
                catch (Exception)
                {
                    // ignored
                }
                return s;
            }
        }



    }
}


