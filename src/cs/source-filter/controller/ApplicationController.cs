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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using slx;
using slx.mvc;
using slx.system;

// TODO: Error Handling has not been implemented.
namespace source_filter
{
   /// <summary>
   /// Implements a simple controller example. The controller composes
   /// the view objects and 'binds' the view objects to the model code. 
   /// Note that WinForms binding could have been used here also.
   /// </summary>
   public sealed class ApplicationController : IController
   {

      private volatile bool initializing_views_;

      /// <summary>
      /// Constructor - Inject Form view instance.
      /// </summary>
      /// <param name="form"></param>
      public ApplicationController(Form form)
      {
         FormView = form;
         ComposeViews();
      }

      #region Data Fields

      private ApplicationLogicCore applicationLogicCore_;

      /// <summary>
      /// The application data model. Contains the configuration
      /// settings of the application. Serialize in JSON.
      /// </summary>
      public static AppDataModel AppDataModel;

      /// <summary>
      /// The main application view control.
      /// </summary>
      public readonly Form FormView;

      public readonly view.MainFormView mainFormView_ = new view.MainFormView();
      public readonly view.source_filter_options applicationSettingsView_ = new view.source_filter_options();

      #endregion

      #region View Widgets
      // <summary>
      // A map of the application's view controls.
      // </summary>
      private readonly Dictionary<string, Control> controlsMap_ = new Dictionary<string, Control>();

      // Aliases to simplify access to the controls map.
      // For complex UI's you would not want to do this but instead
      // use the control map directly for maintenance reasons.
      //
      public ComboBox ComboBoxCopyMethods => (ComboBox)controlsMap_["comboBoxCopyMethods"];
      public Panel PanelOptions => (Panel)controlsMap_["panelOptions"];
      public LinkLabel LinkLabelOption => (LinkLabel)controlsMap_["linkLabelOptions"];
      public LinkLabel LinkLabelSLX => (LinkLabel)controlsMap_["linkLabelSLX"];
      public Button ButtonCloseOptions => (Button)controlsMap_["buttonCloseOptions"];
      public Button ButtonEditFilters => (Button)controlsMap_["buttonEditFilters"];
      public Button ButtonDone => (Button)controlsMap_["buttonDone"];
      public Button ButtonRun => (Button)controlsMap_["buttonRun"];
      public Button ButtonSourceDirectory => (Button)controlsMap_["buttonSelectSourceDirectory"];
      public Button ButtonTargetDirectory => (Button)controlsMap_["buttonTargetDirectory"];
      public TextBox TextBoxSourceDirectory => (TextBox)controlsMap_["textBoxSourceDirectory"];
      public TextBox TextBoxTargetDirectory => (TextBox)controlsMap_["textBoxTargetDirectory"];
      public TextBox TextBoxStatus => (TextBox)controlsMap_["textBoxStatus"];
      public ProgressBar ProgressBar => (ProgressBar)controlsMap_["progressBar"];
      public CheckBox CheckBoxOpenTargetDirectory => (CheckBox)controlsMap_["checkBoxOpenTargetDirectory"];
      public CheckBox CheckBoxBoxIncludeSubdirectories => (CheckBox)controlsMap_["checkBoxIncludeSubdirectories"];
      #endregion

      #region IController overrides

      private bool sourceDirectoryIsBeingSelected_;

      /// <inheritdoc/>
      public void SaveModel()
      {
         AppDataModel.DirectoryInfo.IncludeSubDirectories = CheckBoxBoxIncludeSubdirectories.Checked;
         AppDataModel.DirectoryInfo.OpenTargetDirectoryWhenDone = CheckBoxOpenTargetDirectory.Checked;
         AppDataModel.DirectoryInfo.SourceDirectory = TextBoxSourceDirectory.Text;
         AppDataModel.DirectoryInfo.TargetDirectory = TextBoxTargetDirectory.Text;
         AppDataModel.save();
      }

      /// <inheritdoc />
      public void LoadModel()
      {
         AppDataModel.load();
      }

      /// <inheritdoc />
      public void DisableUserInput()
      {
         ButtonSourceDirectory.Enabled = false;
         ButtonTargetDirectory.Enabled = false;
         TextBoxSourceDirectory.ReadOnly = true;
         TextBoxTargetDirectory.ReadOnly = true;
         ButtonRun.Text = @"Cancel";
      }

      /// <inheritdoc />
      public void EnableUserInput()
      {
         ButtonSourceDirectory.Enabled = true;
         ButtonTargetDirectory.Enabled = true;
         TextBoxSourceDirectory.ReadOnly = false;
         TextBoxTargetDirectory.ReadOnly = false;
         ProgressBar.Value = 0;
         ProgressBar.Visible = false;
         TextBoxStatus.Text = "";
         ButtonRun.Text = @"Run";
      }

      /// <inheritdoc />
      public void Run(string target = null)
      {
         DisableUserInput();
         applicationLogicCore_ = new ApplicationLogicCore(this);
         applicationLogicCore_.DoWork();
      }

      /// <summary>
      /// Composes the views of the form and binds them to the
      /// data model.
      /// </summary>
      public void ComposeViews()
      {
         // This should only be called once.
         //
         if (null != AppDataModel) return;

         initializing_views_ = true;

         try
         {

            AppDataModel = new AppDataModel();

            // Link main view controls with this controller.
            //

            // Note: Order matters here for the following to user controls.
            //       In terms of the workflow for the user the MainFormView
            //       should be presented first.
            //
            FormView.Controls.Add(mainFormView_);
            FormView.Controls.Add(applicationSettingsView_);

            AddControleRecursively((Form.ControlCollection)FormView.Controls);

            LinkLabelOption.LinkClicked += linkLabelOptions_LinkClicked;
            LinkLabelSLX.LinkClicked += linkLabelSLX_LinkClicked;

            ButtonCloseOptions.Click += buttonCloseOptions_Click;
            ButtonRun.Click += buttonRun_Click;
            ButtonDone.Click += buttonDone_Click;
            ButtonSourceDirectory.Click += buttonSelectSourceDirectory_Click;
            ButtonTargetDirectory.Click += buttonTargetDirectory_Click;
            ButtonEditFilters.Click += buttonEditFilters_Click;
            ComboBoxCopyMethods.SelectedIndexChanged += comboBoxCopyMethods_SelectedIndexChanged;
            ComboBoxCopyMethods.Items.Add(CopyFileMethod.CS_SystemIoFileCopy);
            ComboBoxCopyMethods.Items.Add(CopyFileMethod.CS_BufferedFileCopy);
            ComboBoxCopyMethods.Items.Add(CopyFileMethod.CPP_BufferedFileCopy);
            ComboBoxCopyMethods.Items.Add(CopyFileMethod.CPP_StdFileSystemCopy);

            TextBoxSourceDirectory.AllowDrop = true;
            TextBoxSourceDirectory.DragDrop += textBoxSourceDirectory_DragDrop;
            TextBoxSourceDirectory.DragEnter += textBoxSourceDirectory_DragEnter;
            TextBoxSourceDirectory.TextChanged += textBoxSourceDirectory_TextChanged;
            TextBoxTargetDirectory.TextChanged += textBoxTargetDirectory_TextChanged;

            CheckBoxBoxIncludeSubdirectories.CheckedChanged += checkBoxIncludeSubdirectories_CheckedChanged;
            CheckBoxOpenTargetDirectory.CheckedChanged += checkBoxOpenTargetDirectory_CheckedChanged;

            // Must be called prior to the code below.
            //
            LoadModel();

            CheckBoxBoxIncludeSubdirectories.Checked = AppDataModel.DirectoryInfo.IncludeSubDirectories;
            CheckBoxOpenTargetDirectory.Checked = AppDataModel.DirectoryInfo.OpenTargetDirectoryWhenDone;
            TextBoxSourceDirectory.Text = AppDataModel.DirectoryInfo.SourceDirectory;
            TextBoxTargetDirectory.Text = AppDataModel.DirectoryInfo.TargetDirectory;

            // Check source and target directories...
            //
            var t0 = io.directory_format_is_correct(TextBoxSourceDirectory.Text);
            var t1 = io.directory_format_is_correct(TextBoxTargetDirectory.Text);

            if (t0 && t1)
            {
               ButtonRun.Enabled = true;
            }
         }
         finally
         {
            initializing_views_ = false;
            ComboBoxCopyMethods.SetIndex(AppDataModel?.CopyMethod);
         }
      }

      /// <summary>
      /// TODO: Move to a reuse library. Add argument to
      ///       populate a controls map.
      /// </summary>
      /// <param name="controls"></param>
      private void AddControleRecursively(IEnumerable controls)
      {
         if (null == controls) return;

         // Attach the view controls to this controller
         //
         foreach (Control c in controls)
         {
            controlsMap_.Add(c.Name, c);
            AddControleRecursively(c.Controls);
         }
      }

      #endregion

      #region View Event Handlers

      private void comboBoxCopyMethods_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (initializing_views_) return;
         CopyFileMethod.SetCopyMethod((ComboBox)sender);
         AppDataModel.CopyMethod = ((Control)sender).Text;
      }

      private void buttonCloseOptions_Click(object sender, EventArgs e)
      {
         PanelOptions.Visible = false;
         mainFormView_.Visible = !PanelOptions.Visible;
      }

      // Event handlers each of the main view controls.
      //
      private void linkLabelOptions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         PanelOptions.Visible = !PanelOptions.Visible;
         mainFormView_.Visible = !PanelOptions.Visible;

      }

      private void linkLabelSLX_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start(LinkLabelSLX.Text);
      }

      private void checkBoxIncludeSubdirectories_CheckedChanged(object sender, EventArgs e)
      {
         AppDataModel.DirectoryInfo.IncludeSubDirectories = CheckBoxBoxIncludeSubdirectories.Checked;
      }

      private void checkBoxOpenTargetDirectory_CheckedChanged(object sender, EventArgs e)
      {
         AppDataModel.DirectoryInfo.OpenTargetDirectoryWhenDone = CheckBoxBoxIncludeSubdirectories.Checked;
      }

      private void textBoxTargetDirectory_TextChanged(object sender, EventArgs e)
      {
         var targetDirectory = TextBoxTargetDirectory.Text;

         if (string.IsNullOrEmpty(targetDirectory) || !io.directory_format_is_correct(targetDirectory))
         {
            ButtonRun.Enabled = false;

            // The user is most likely typing in the source 
            // directory...therefore it is not yet completed.
            //
            return;
         }

         ButtonRun.Enabled = true;
      }

      private void textBoxSourceDirectory_DragDrop(object sender, DragEventArgs e)
      {
         var textBox = (TextBox)sender;
         textBox.DropFolder(e);
      }

      private void textBoxSourceDirectory_DragEnter(object sender, DragEventArgs e)
      {
         // The following changes the cursor shown in the text box that
         // is being updated.
         //
         e.Effect = DragDropEffects.Copy;
      }

      private void textBoxSourceDirectory_TextChanged(object sender, EventArgs e)
      {
         if (sourceDirectoryIsBeingSelected_) return;

         var restore = AppDataModel.DirectoryInfo.SourceDirectory;
         AppDataModel.DirectoryInfo.SourceDirectory = TextBoxSourceDirectory.Text;
         var sd = AppDataModel.DirectoryInfo.SourceDirectory;
         if (string.IsNullOrEmpty(sd) || !Directory.Exists(sd))
         {
            AppDataModel.DirectoryInfo.SourceDirectory = restore;

            ButtonRun.Enabled = false;

            // The user is most likely typing in the source 
            // directory...therefore it is not yet completed.
            //
            return;
         }

         ButtonRun.Enabled = true;

         CreateTargetDirectory();

         SaveModel();
      }

      private void buttonRun_Click(object sender, EventArgs e)
      {
         if (ButtonRun.Text == @"Cancel")
         {
            applicationLogicCore_.Stop();
            return;
         }

         if (TextBoxTargetDirectory.Text == TextBoxSourceDirectory.Text)
         {
            var caption = AppDataModel.MainFormTitle + " - Error";
            const string message = @"Source Directory cannot be the same as the Target Directory!";
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
         }
         // Save the model just in case something goes wrong 
         // with the application. This way data recovery is
         // more likely to happen.
         //
         SaveModel();

         Run();
      }

      private void buttonDone_Click(object sender, EventArgs e)
      {
         SaveModel();
         FormView.Close();
      }

      public void buttonSelectSourceDirectory_Click(object sender, EventArgs e)
      {
         try
         {
            sourceDirectoryIsBeingSelected_ = true;

            using (var fbd = new FolderBrowserDialog())
            {
               fbd.Description = @"Source directory to process:";

               fbd.SelectedPath = AppDataModel.DirectoryInfo.SourceDirectory;

               var result = fbd.ShowDialog();

               if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
               {
                  AppDataModel.DirectoryInfo.SourceDirectory = fbd.SelectedPath;
                  CreateTargetDirectory();
                  TextBoxSourceDirectory.Text = fbd.SelectedPath;
                  SaveModel();
               }
            }

            TextBoxTargetDirectory.Enabled = !string.IsNullOrEmpty(TextBoxSourceDirectory.Text) &&
                                             Directory.Exists(AppDataModel.DirectoryInfo.SourceDirectory);

         }
         finally
         {
            sourceDirectoryIsBeingSelected_ = false;
         }
      }

      private void buttonTargetDirectory_Click(object sender, EventArgs e)
      {
         using (var fbd = new FolderBrowserDialog())
         {
            fbd.Description = @"Target directory to write to:";
            //
            // Why Microsoft why does this have to be?
            // fbd.RootFolder = Environment.SpecialFolder.UserProfile;
            //
            fbd.SelectedPath = AppDataModel.DirectoryInfo.TargetDirectory;

            var result = fbd.ShowDialog();

            if (result != DialogResult.OK || string.IsNullOrWhiteSpace(fbd.SelectedPath)) return;

            AppDataModel.DirectoryInfo.TargetDirectory = TextBoxTargetDirectory.Text = fbd.SelectedPath;
         }
      }

      private void buttonEditFilters_Click(object sender, EventArgs e)
      {
         io.exec_as_admin(AppDataModel.ModelEditor, AppDataModel.GetConfigurationPath());
      }
      #endregion

      #region Directory Processing
      // <summary>
      // Creates a unique target directory.
      // </summary>
      private void CreateTargetDirectory()
      {
         TextBoxTargetDirectory.Text = EnsureTargetDirectoryIsValid();
         AppDataModel.DirectoryInfo.TargetDirectory = TextBoxTargetDirectory.Text;
      }

      // <summary>
      // The target directory should always be unique.
      // A previously used target directory must be explicitly
      // deleted by a user.
      // </summary>
      // <returns>A unique target directory.</returns>
      private static string EnsureTargetDirectoryIsValid()
      {
         var destFolderName = Path.GetDirectoryName(AppDataModel.DirectoryInfo.SourceDirectory);
         var destTargetName = Path.GetFileName(AppDataModel.DirectoryInfo.SourceDirectory);

         // 'destFolderName' and 'destTargetName' should never be null or
         // empty.
         //
         if (string.IsNullOrEmpty(destFolderName))
         {
            destTargetName = Path.Combine("SLX", AppDataModel.ModelName);
         }

         destTargetName = string.IsNullOrEmpty(destTargetName) ? "target" : destTargetName;

         if (null == destFolderName)
         {
            destFolderName = Environment.UserName;
         }

         var destTarget = Path.Combine(destFolderName, destTargetName);

         for (var i = 0; !io.is_directory_empty(destTarget); ++i)
         {
            destTarget = Path.Combine(destFolderName, $"{destTargetName}-{i}");
         }

         return destTarget;

      }

      #endregion
   }
}