// Prologue
//
// SLX - MVC Experimentation code using WinForms.
//       Operating Systems - Windows Only
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
using System.ComponentModel;
using System.Windows.Forms;

#warning @"In order for the forms to scale correctly across"
#warning @"a variety of display scales be sure to use the"
#warning @"following settings for the 'AutoScale' attributes:"
#warning @" - this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);"
#warning @" - this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;"

namespace source_filter
{
    public partial class MainForm : Form
    {
        // For applications that need to be adaptable
        // it is best to inject the controller.
        //
        private ApplicationController controller_;

        public MainForm() => InitializeComponent();

        private void MainForm_Load(object sender, EventArgs e)
        {
            // View behaviors are defined in the controller.
            // This is why the Form instance, MainForm, is passed  
            // into the controller. The controller then enumerates
            // the controls of the MainForm widgets (e.g. Buttons,
            // Panels, TextBoxs) and binds each control to various
            // notification handlers.
            //
            controller_ = new ApplicationController(this);
        }

        // The only control behavior implemented in the MainForm 
        // is related to the 'OnClosing' event handler. This is 
        // an override method on the form itself and must be 
        // implemented here.
        //
        protected override void OnClosing(CancelEventArgs e)
        {
            controller_.SaveModel();
            base.OnClosing(e);
        }
    }
}
