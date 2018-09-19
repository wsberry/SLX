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

namespace slx
{
    public static class algorithms
    {
        /// <summary>
        /// Use to create a partial GUID or full UUID
        /// </summary>
        /// <param name="size">
        /// Limits the character length of the 'UUID' returned.
        /// </param>
        /// <returns>The UUID requested.</returns>
        public static string create_uuid(int size = 8)
        {
            var t = Guid.NewGuid().ToString().Replace("-", "");
            if (size >= t.Length) size = 8;
            return t.Substring(0, size);
        }
    }
}