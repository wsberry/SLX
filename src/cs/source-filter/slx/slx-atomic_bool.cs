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
// This a prototype for working with atomic concepts in C#. This
// code is currently untested and may not work as intended.
// Also there may be better ways of doing these things in C#.
// Therefore use the types defined in here with care!
//
// endPrologue

using System.Threading;

namespace experimental
{
    namespace slx.atomic
    {
        /// <summary>
        /// Implements a thread safe bool value type.
        /// Why atomic? Aren't bool types atomic in the
        /// C# specification? Well its complicated:)
        /// https://www.ecma-international.org/publications/files/ECMA-ST-ARCH/ECMA-334%201st%20edition%20December%202001.pdf
        /// </summary>
        public sealed class atomic_bool
        {
            private int bool_;

            public atomic_bool()
            {
            }

            public atomic_bool(int v)
            {
                data = v >= 1;
            }

            public atomic_bool(bool v)
            {
                data = v;
            }

            public static implicit operator bool(atomic_bool v)
            {
                return v.data;
            }

            public static implicit operator atomic_bool(int v)
            {
                return new atomic_bool(v);
            }

            public static implicit operator atomic_bool(bool v)
            {
                return new atomic_bool(v);
            }

            public bool data
            {
                get => (Interlocked.CompareExchange(ref bool_, 1, 1) == 1);
                set
                {
                    var v = value ? 1 : 0;
                    var c = value ? 0 : 1;
                    Interlocked.CompareExchange(ref bool_, v, c);
                }
            }
        }

        public sealed class atomic_bool2
        {
            private volatile bool value_;

            public atomic_bool2()
            {
            }

            public atomic_bool2(bool v)
            {
                data = v;
            }

            public static implicit operator bool(atomic_bool2 v)
            {
                return v.data;
            }

            public static implicit operator atomic_bool2(bool v)
            {
                return new atomic_bool2(v);
            }

            public bool data
            {
                get => value_;
                set => value_ = value;
            }
        }

        public class atomic<T>
        {
            private T value_;

            private readonly object lk_ = new object();

            public atomic()
            {
            }

            public atomic(T v)
            {
                data = v;
            }

            public static implicit operator T(atomic<T> v)
            {
                return v.data;
            }

            public static implicit operator atomic<T>(T v)
            {
                return new atomic<T>(v);
            }

            public T data
            {
                get
                {
                    lock (lk_)
                    {
                        return value_;
                    }
                }
                set
                {
                    lock (lk_)
                    {
                        value_ = value;
                    }
                }
            }
        }

        public sealed class atomic_int : atomic<int>
        {
        }

    }
}