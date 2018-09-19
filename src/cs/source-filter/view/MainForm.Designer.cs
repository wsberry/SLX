namespace source_filter
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region WindowsOS Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.labelSourceDirectory = new System.Windows.Forms.Label();
            this.buttonSelectSourceDirectory = new System.Windows.Forms.Button();
            this.textBoxSourceDirectory = new System.Windows.Forms.TextBox();
            this.buttonTargetDirectory = new System.Windows.Forms.Button();
            this.textBoxTargetDirectory = new System.Windows.Forms.TextBox();
            this.labelTarget = new System.Windows.Forms.Label();
            this.toolTipMainForm = new System.Windows.Forms.ToolTip(this.components);
            this.linkLabelOptions = new System.Windows.Forms.LinkLabel();
            this.textBoxHints = new System.Windows.Forms.TextBox();
            this.buttonEditFilters = new System.Windows.Forms.Button();
            this.checkBoxIncludeSubdirectories = new System.Windows.Forms.CheckBox();
            this.linkLabelSLX = new System.Windows.Forms.LinkLabel();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonDone = new System.Windows.Forms.Button();
            this.panelOptions = new System.Windows.Forms.Panel();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.buttonCloseOptions = new System.Windows.Forms.Button();
            this.checkBoxOpenTargetDirectory = new System.Windows.Forms.CheckBox();
            this.labelAbout = new System.Windows.Forms.Label();
            this.panelOptions.SuspendLayout();
            this.groupBoxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelSourceDirectory
            // 
            this.labelSourceDirectory.AutoSize = true;
            this.labelSourceDirectory.Location = new System.Drawing.Point(7, 20);
            this.labelSourceDirectory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSourceDirectory.Name = "labelSourceDirectory";
            this.labelSourceDirectory.Size = new System.Drawing.Size(55, 20);
            this.labelSourceDirectory.TabIndex = 0;
            this.labelSourceDirectory.Text = "Source:";
            // 
            // buttonSelectSourceDirectory
            // 
            this.buttonSelectSourceDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectSourceDirectory.Location = new System.Drawing.Point(575, 17);
            this.buttonSelectSourceDirectory.Name = "buttonSelectSourceDirectory";
            this.buttonSelectSourceDirectory.Size = new System.Drawing.Size(48, 27);
            this.buttonSelectSourceDirectory.TabIndex = 4;
            this.buttonSelectSourceDirectory.Text = ". . .";
            this.buttonSelectSourceDirectory.UseVisualStyleBackColor = true;
            // 
            // textBoxSourceDirectory
            // 
            this.textBoxSourceDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSourceDirectory.Location = new System.Drawing.Point(69, 17);
            this.textBoxSourceDirectory.Name = "textBoxSourceDirectory";
            this.textBoxSourceDirectory.Size = new System.Drawing.Size(499, 27);
            this.textBoxSourceDirectory.TabIndex = 3;
            this.toolTipMainForm.SetToolTip(this.textBoxSourceDirectory, "The Source Folder.");
            // 
            // buttonTargetDirectory
            // 
            this.buttonTargetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTargetDirectory.Location = new System.Drawing.Point(575, 60);
            this.buttonTargetDirectory.Name = "buttonTargetDirectory";
            this.buttonTargetDirectory.Size = new System.Drawing.Size(48, 27);
            this.buttonTargetDirectory.TabIndex = 7;
            this.buttonTargetDirectory.Text = ". . .";
            this.buttonTargetDirectory.UseVisualStyleBackColor = true;
            // 
            // textBoxTargetDirectory
            // 
            this.textBoxTargetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTargetDirectory.Location = new System.Drawing.Point(69, 60);
            this.textBoxTargetDirectory.Name = "textBoxTargetDirectory";
            this.textBoxTargetDirectory.Size = new System.Drawing.Size(499, 27);
            this.textBoxTargetDirectory.TabIndex = 6;
            this.toolTipMainForm.SetToolTip(this.textBoxTargetDirectory, "The Target Folder.");
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Location = new System.Drawing.Point(7, 63);
            this.labelTarget.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(50, 20);
            this.labelTarget.TabIndex = 5;
            this.labelTarget.Text = "Target:";
            // 
            // linkLabelOptions
            // 
            this.linkLabelOptions.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.linkLabelOptions.AutoSize = true;
            this.linkLabelOptions.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.linkLabelOptions.Location = new System.Drawing.Point(65, 106);
            this.linkLabelOptions.Name = "linkLabelOptions";
            this.linkLabelOptions.Size = new System.Drawing.Size(57, 20);
            this.linkLabelOptions.TabIndex = 21;
            this.linkLabelOptions.TabStop = true;
            this.linkLabelOptions.Text = "Options";
            this.toolTipMainForm.SetToolTip(this.linkLabelOptions, "Select configuration settings.");
            // 
            // textBoxHints
            // 
            this.textBoxHints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHints.BackColor = System.Drawing.Color.White;
            this.textBoxHints.Location = new System.Drawing.Point(6, 140);
            this.textBoxHints.Name = "textBoxHints";
            this.textBoxHints.ReadOnly = true;
            this.textBoxHints.Size = new System.Drawing.Size(336, 27);
            this.textBoxHints.TabIndex = 23;
            this.textBoxHints.Text = " Note: Empty directories will not be copied!";
            this.toolTipMainForm.SetToolTip(this.textBoxHints, "Note: Empty directories will not be copied!");
            // 
            // buttonEditFilters
            // 
            this.buttonEditFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditFilters.Location = new System.Drawing.Point(6, 31);
            this.buttonEditFilters.Name = "buttonEditFilters";
            this.buttonEditFilters.Size = new System.Drawing.Size(165, 34);
            this.buttonEditFilters.TabIndex = 17;
            this.buttonEditFilters.Text = "Filtering Rules . . .";
            this.toolTipMainForm.SetToolTip(this.buttonEditFilters, "Modify filtering rules.");
            this.buttonEditFilters.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeSubdirectories
            // 
            this.checkBoxIncludeSubdirectories.AutoSize = true;
            this.checkBoxIncludeSubdirectories.Location = new System.Drawing.Point(6, 74);
            this.checkBoxIncludeSubdirectories.Name = "checkBoxIncludeSubdirectories";
            this.checkBoxIncludeSubdirectories.Size = new System.Drawing.Size(165, 24);
            this.checkBoxIncludeSubdirectories.TabIndex = 18;
            this.checkBoxIncludeSubdirectories.Text = "Include Subdirectories";
            this.toolTipMainForm.SetToolTip(this.checkBoxIncludeSubdirectories, "Open the created target folder when processing has completed.");
            this.checkBoxIncludeSubdirectories.UseVisualStyleBackColor = true;
            // 
            // linkLabelSLX
            // 
            this.linkLabelSLX.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.linkLabelSLX.AutoSize = true;
            this.linkLabelSLX.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.linkLabelSLX.Location = new System.Drawing.Point(112, 140);
            this.linkLabelSLX.Name = "linkLabelSLX";
            this.linkLabelSLX.Size = new System.Drawing.Size(176, 20);
            this.linkLabelSLX.TabIndex = 23;
            this.linkLabelSLX.TabStop = true;
            this.linkLabelSLX.Text = "https://github.com/wsberry";
            this.toolTipMainForm.SetToolTip(this.linkLabelSLX, "SLX - GitHub");
            this.linkLabelSLX.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStatus.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxStatus.Location = new System.Drawing.Point(128, 106);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ReadOnly = true;
            this.textBoxStatus.Size = new System.Drawing.Size(362, 20);
            this.textBoxStatus.TabIndex = 14;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(69, 139);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(561, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 8;
            this.progressBar.Visible = false;
            // 
            // buttonRun
            // 
            this.buttonRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRun.Location = new System.Drawing.Point(496, 99);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(64, 34);
            this.buttonRun.TabIndex = 9;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            // 
            // buttonDone
            // 
            this.buttonDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDone.Location = new System.Drawing.Point(566, 99);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(64, 34);
            this.buttonDone.TabIndex = 11;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            // 
            // panelOptions
            // 
            this.panelOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOptions.Controls.Add(this.groupBoxOptions);
            this.panelOptions.Location = new System.Drawing.Point(205, 4);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Padding = new System.Windows.Forms.Padding(5, 1, 5, 5);
            this.panelOptions.Size = new System.Drawing.Size(435, 185);
            this.panelOptions.TabIndex = 22;
            this.panelOptions.Visible = false;
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.textBoxHints);
            this.groupBoxOptions.Controls.Add(this.buttonCloseOptions);
            this.groupBoxOptions.Controls.Add(this.buttonEditFilters);
            this.groupBoxOptions.Controls.Add(this.checkBoxIncludeSubdirectories);
            this.groupBoxOptions.Controls.Add(this.checkBoxOpenTargetDirectory);
            this.groupBoxOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxOptions.Location = new System.Drawing.Point(5, 1);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(423, 177);
            this.groupBoxOptions.TabIndex = 0;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // buttonCloseOptions
            // 
            this.buttonCloseOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCloseOptions.Location = new System.Drawing.Point(348, 137);
            this.buttonCloseOptions.Name = "buttonCloseOptions";
            this.buttonCloseOptions.Size = new System.Drawing.Size(64, 34);
            this.buttonCloseOptions.TabIndex = 22;
            this.buttonCloseOptions.Text = "Done";
            this.buttonCloseOptions.UseVisualStyleBackColor = true;
            // 
            // checkBoxOpenTargetDirectory
            // 
            this.checkBoxOpenTargetDirectory.AutoSize = true;
            this.checkBoxOpenTargetDirectory.Location = new System.Drawing.Point(6, 107);
            this.checkBoxOpenTargetDirectory.Name = "checkBoxOpenTargetDirectory";
            this.checkBoxOpenTargetDirectory.Size = new System.Drawing.Size(215, 24);
            this.checkBoxOpenTargetDirectory.TabIndex = 16;
            this.checkBoxOpenTargetDirectory.Text = "Open Target when Completed";
            this.checkBoxOpenTargetDirectory.UseVisualStyleBackColor = true;
            // 
            // labelAbout
            // 
            this.labelAbout.AutoSize = true;
            this.labelAbout.Location = new System.Drawing.Point(65, 140);
            this.labelAbout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(49, 20);
            this.labelAbout.TabIndex = 24;
            this.labelAbout.Text = "About:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(642, 194);
            this.Controls.Add(this.panelOptions);
            this.Controls.Add(this.linkLabelOptions);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.buttonTargetDirectory);
            this.Controls.Add(this.textBoxTargetDirectory);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.buttonSelectSourceDirectory);
            this.Controls.Add(this.textBoxSourceDirectory);
            this.Controls.Add(this.labelSourceDirectory);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.labelAbout);
            this.Controls.Add(this.linkLabelSLX);
            this.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Directory Transmorgrifier - SLX";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelOptions.ResumeLayout(false);
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSourceDirectory;
        private System.Windows.Forms.Button buttonSelectSourceDirectory;
        private System.Windows.Forms.TextBox textBoxSourceDirectory;
        private System.Windows.Forms.Button buttonTargetDirectory;
        private System.Windows.Forms.TextBox textBoxTargetDirectory;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.ToolTip toolTipMainForm;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.LinkLabel linkLabelOptions;
        private System.Windows.Forms.Panel panelOptions;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.TextBox textBoxHints;
        private System.Windows.Forms.Button buttonCloseOptions;
        private System.Windows.Forms.Button buttonEditFilters;
        private System.Windows.Forms.CheckBox checkBoxIncludeSubdirectories;
        private System.Windows.Forms.CheckBox checkBoxOpenTargetDirectory;
        private System.Windows.Forms.LinkLabel linkLabelSLX;
        private System.Windows.Forms.Label labelAbout;
    }
}

