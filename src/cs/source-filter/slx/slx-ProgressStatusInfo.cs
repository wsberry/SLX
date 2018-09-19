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
// endPrologue
using Newtonsoft.Json;

namespace slx
{
    /// <summary>
    /// Simple ProgressBar type status info message.
    /// </summary>
    public sealed class ProgressStatusInfo
    {
        public int ProgressValue;
        public string Message;
        public int Max = 100;
        public int Min = 0;
    }

    /// <summary>
    /// Implements JSON serialization methods for <see cref="ProgressStatusInfo"/>
    /// </summary>
    public static class ProgressStatusExtensions
    {
        public static string to_string(this ProgressStatusInfo ps)
        {
            return JsonConvert.SerializeObject(ps, Formatting.None);
        }
        public static ProgressStatusInfo to_object(this string json)
        {
            return JsonConvert.DeserializeObject<ProgressStatusInfo>(json);
        }
        public static void to_object(this ProgressStatusInfo ps, string json)
        {
            var t = JsonConvert.DeserializeObject<ProgressStatusInfo>(json);
            ps.ProgressValue = t.ProgressValue;
            ps.Message = t.Message;
        }
    }
}