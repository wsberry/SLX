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

// TODO: Refactor/re-write.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using experimental.slx.atomic;
using slx;
using slx.system.directory;

namespace source_filter
{
    /// <summary>
    /// Implements an example of something that does work.
    /// In this example we are copying a directory and filtering
    /// out (ignoring) specified directories, files, or
    /// file names based on their extension type.
    /// </summary>
    /// <remarks>Experimental</remarks>
    public class ApplicationLogicCore : IProcess
    {
        private Stopwatch stopwatch_ = new Stopwatch();
        /// <summary>
        /// Default constructor used for dependency injection.
        /// </summary>
        /// <param name="controller"></param>
        public ApplicationLogicCore(ApplicationController controller)
        {
            Controller = controller;

            // TODO: Refactor. 'OnFileCopyNotify' should not be part of the
            //       application data model. It was added to it out of
            //       convenience but really should be limited to the
            //       application controller and the working thread.
            //       
            ApplicationController.AppDataModel.DirectoryInfo.OnFileCopyNotify = OnUpdateProgressView;
        }

        #region Data
        private delegate void UpdateProgressDelegate(ProgressStatusInfo value);
        private delegate void OnWorkCompleteDelegate(string any);

        private readonly List<string> errorsList_ = new List<string>();

        // We should also be able to use:
        // private static volatile bool running_ = false;
        // 
        private static atomic_bool running_ = false;
        private static atomic_bool pause_ = false;

        /// <summary>
        /// Use this field to inject the application controller.
        /// </summary>
        public ApplicationController Controller;
        #endregion

        #region IProcess Overrides
        /// <summary>
        /// A thread/task that does the work.
        /// </summary>
        /// <param name="model">The data model being worked on.</param>
        private void DoWorkThread(AppDataModel model)
        {
            stopwatch_.Start();
            model.DirectoryInfo.CreateFilteredDirectoryCopy();

            running_ = false;

            OnWorkComplete("");
        }

        /// <inheritdoc />
        public void DoWork()
        {
            if (running_)
            {
                // Cancel the work in progress.
                //
                running_ = false;
                
                return;
            }
            running_ = true;

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                DoWorkThread(ApplicationController.AppDataModel);
            }).Start();

            //
            // Task.Factory.StartNew(() => DoWorkThread(ApplicationController.AppDataModel));
        }

        /// <inheritdoc />
        public void Pause()
        {
            pause_ = true;
        }

        /// <inheritdoc />
        public void Resume()
        {
            pause_ = false;
        }

        /// <inheritdoc />
        /// <summary>
        /// Note: This routine should be thread safe.
        /// </summary>
        /// <param name="ps">A <see cref="T:slx.mvc.ProgressStatusInfo" /></param>
        public void OnUpdateProgressView(ProgressStatusInfo ps)
        {
            if (null == Controller.ProgressBar) return;

            if (Controller.ProgressBar.InvokeRequired)
            {
                Controller.ProgressBar.BeginInvoke(
                    new UpdateProgressDelegate(OnUpdateProgressView), 
                    ps);
            }
            else
            {
                if (ps.ProgressValue <= 1)
                {
                    // Initializes the progress of the view.
                    //
                    Controller.ProgressBar.Maximum = ps.Max;
                    Controller.ProgressBar.Minimum = ps.Min;
                    Controller.ProgressBar.Visible = true;

                    // Change to a wait cursor on the main
                    // view window.
                    //
                    Controller.FormView.Cursor = Cursors.WaitCursor;
                }
                Controller.ProgressBar.Value = ps.ProgressValue;
                Controller.TextBoxStatus.Text =
                    $@"Copying '{ps.Message}'...{ps.ProgressValue} of {ps.Max} files.";
            }
        }

        /// <inheritdoc />
        public void OnHandleError(string errorContent)
        {
            // TODO: Should we allow duplicates?
            //      
            foreach (var e in errorsList_)
            {
                if (e == errorContent) return;
            }
           
            errorsList_.Add(errorContent);
        }

        /// <inheritdoc />
        public void OnWorkComplete(string any)
        {
            var cb = Controller.CheckBoxOpenTargetDirectory;

            if (null == cb) return;

            if (cb.InvokeRequired)
            {
                cb.BeginInvoke(new OnWorkCompleteDelegate(OnWorkComplete), any);
            }
            else
            {
                stopwatch_.Stop();

                var itemsProcessed = Controller.ProgressBar.Value;
                var itemsTotal = Controller.ProgressBar.Maximum;

                Controller.FormView.Cursor = Cursors.Default;
                Controller.EnableUserInput();
                Controller.SaveModel();
                var elapsedTime = Math.Round(stopwatch_.Elapsed.TotalMinutes, 4).ToString(CultureInfo.InvariantCulture);
                Controller.TextBoxStatus.Text = $@"Copied {itemsProcessed} of {itemsTotal} items! (Total Time: {elapsedTime} minutes)";

                if (cb.Checked)
                {
                    Process.Start("Explorer.exe", Controller.TextBoxTargetDirectory.Text);
                }
            }
        }

        /// <inheritdoc />
        public List<string> Errors { get; set; }
        #endregion
    }
}