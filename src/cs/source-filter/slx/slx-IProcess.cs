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
using System.Collections.Generic;

namespace slx
{
    /// <summary>
    /// An interface to define an interface that works
    /// on data.
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// Use to start a background thread that does work.
        /// </summary>
        void DoWork();

        /// <summary>
        /// Pause the work in the thread if needed.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resume the work in the thread after pausing it.
        /// </summary>
        void Resume();

        /// <summary>
        /// Get a list of errors that have been reported.
        /// </summary>
        List<string> Errors { get; set; }

        /// <summary>
        /// Use to update the progress of the work being accomplished
        /// in a view object. The implementation should be thread safe.
        /// e.g. Use 'InvokeRequired' and the following delegate:
        ///      'delegate void UpdateProgressDelegate(int value);'
        /// </summary>
        /// <param name="ps">
        /// A counter or percent complete value and a message.
        /// </param>
        void OnUpdateProgressView(ProgressStatusInfo ps);

        /// <summary>
        /// Implement something that handles errors.
        /// </summary>
        /// <param name="errorContent"></param>
        void OnHandleError(string errorContent);

        /// <summary>
        /// Handle conditions when work is completed.
        /// </summary>
        /// <param name="any"></param>
        void OnWorkComplete(string any);
  
    }
}