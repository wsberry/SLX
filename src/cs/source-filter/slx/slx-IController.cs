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

namespace slx.mvc
{
    /// <summary>
    /// An interface for a simple controller.
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Run a thread
        /// </summary>
        /// <param name="message">
        /// An optional paramterized messag.
        /// </param>
        void Run(string message);

        /// <summary>
        /// Disable user input on a collection of controls.
        /// </summary>
        void DisableUserInput();

        /// <summary>
        /// Enable user input on a collection of controls.
        /// </summary>
        void EnableUserInput();

        /// <summary>
        /// Save the data model.
        /// </summary>
        void SaveModel();

        /// <summary>
        /// Load the data model into an application's view widgets.
        /// </summary>
        void LoadModel();

        /// <summary>
        /// Connect the view objects to the various control
        /// behaviors defined in the controller.
        /// </summary>
        void ComposeViews();
    }
}