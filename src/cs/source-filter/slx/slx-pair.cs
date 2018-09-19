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
#pragma warning disable IDE1006 // Naming Styles

using System;

namespace slx
{
    /// <summary>
    /// A standard KeyValuePair.
    /// </summary>
    public sealed class KeyValuePair
    {
        public string Key;
        public string Value;

        /// <summary>
        /// initializes
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public KeyValuePair(string key = "", string value = "")
        {
            Key = key;
            Value = value;
        }

        public static implicit operator KeyValuePair(Tuple<string,string> t)
        {
            return new KeyValuePair
            {
                Key = t.Item1,
                Value = t.Item2
            };
        }

        public static implicit operator Tuple<string, string>(KeyValuePair p)
        {
            return new Tuple<string, string>(p.Key, p.Value);
        }
    }
}