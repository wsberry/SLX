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
using System.Text;

namespace slx
{
    /// <summary>
    /// Extension methods for strings.
    /// </summary>
    public static class strings
    {
        public static bool icontains(this string s, string value,
            StringComparison sc = StringComparison.CurrentCultureIgnoreCase)
        {
            return s.IndexOf(value, sc) >= 0;
        }

        public static bool iequals(this string s, string value)
        {
            return s.Equals(value, StringComparison.CurrentCultureIgnoreCase);
        }

        public static byte[] tobytes(this string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }

        public static string tostring(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}