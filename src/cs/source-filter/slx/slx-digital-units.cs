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

using System;

namespace slx.system
{
    /// <summary>
    /// Defines units of measure for digital
    /// information.
    /// </summary>
    public static class Units
    {
        public static class Digital
        {
            public static class WindowsOS
            {
                /// <summary>
                /// int kilobyte = 1024 bytes
                /// </summary>
                public const int Kilobyte = 1024;

                /// <summary>
                /// int megabyte = Kilobyte^2
                /// </summary>
                public const int Megabyte = Kilobyte ^ 2;

                /// <summary>
                /// Int64 gigabyte = Kilobyte^3
                /// </summary>
                public const Int64 Gigabyte = Kilobyte ^ 3;

                /// <summary>
                /// Int64 terabyte =  Kilobyte^4
                /// </summary>
                public const Int64 Terabyte = Kilobyte ^ 4;

                /// <summary>
                /// Int64 petabyte =  Kilobyte^5
                /// </summary>
                public const Int64 Petabyte = Kilobyte ^ 5;

                public const int KB = Kilobyte;
                public const int MB = Megabyte;
                public const Int64 GB = Gigabyte;
                public const Int64 TB = Terabyte;
                public const Int64 PB = Petabyte;
            }

            public static class International
            {
                /// <summary>
                /// int kilobyte = 1000 bytes
                /// </summary>
                public const int Kilobyte = 1000;

                /// <summary>
                /// int megabyte = Kilobyte^2
                /// </summary>
                public const int Megabyte = Kilobyte ^ 2;

                /// <summary>
                /// Int64 gigabyte = Kilobyte^3
                /// </summary>
                public const Int64 Gigabyte = Kilobyte ^ 3;

                /// <summary>
                /// Int64 terabyte =  Kilobyte^4
                /// </summary>
                public const Int64 Terabyte = Kilobyte ^ 4;

                /// <summary>
                /// Int64 petabyte =  Kilobyte^5
                /// </summary>
                public const Int64 Petabyte = Kilobyte ^ 5;

                public const int kB = Kilobyte;
                public const int MB = Megabyte;
                public const Int64 GB = Gigabyte;
                public const Int64 TB = Terabyte;
                public const Int64 PB = Petabyte;
            }
        }
    }
}