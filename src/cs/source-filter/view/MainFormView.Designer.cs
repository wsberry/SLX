namespace source_filter.view
{
    partial class MainFormView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
         this.linkLabelOptions = new System.Windows.Forms.LinkLabel();
         this.textBoxStatus = new System.Windows.Forms.TextBox();
         this.buttonDone = new System.Windows.Forms.Button();
         this.buttonRun = new System.Windows.Forms.Button();
         this.buttonTargetDirectory = new System.Windows.Forms.Button();
         this.textBoxTargetDirectory = new System.Windows.Forms.TextBox();
         this.labelTarget = new System.Windows.Forms.Label();
         this.buttonSelectSourceDirectory = new System.Windows.Forms.Button();
         this.textBoxSourceDirectory = new System.Windows.Forms.TextBox();
         this.labelSourceDirectory = new System.Windows.Forms.Label();
         this.progressBar = new System.Windows.Forms.ProgressBar();
         this.labelAbout = new System.Windows.Forms.Label();
         this.linkLabelSLX = new System.Windows.Forms.LinkLabel();
         this.SuspendLayout();
         // 
         // linkLabelOptions
         // 
         this.linkLabelOptions.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
         this.linkLabelOptions.AutoSize = true;
         this.linkLabelOptions.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
         this.linkLabelOptions.Location = new System.Drawing.Point(68, 114);
         this.linkLabelOptions.Name = "linkLabelOptions";
         this.linkLabelOptions.Size = new System.Drawing.Size(57, 20);
         this.linkLabelOptions.TabIndex = 35;
         this.linkLabelOptions.TabStop = true;
         this.linkLabelOptions.Text = "Options";
         // 
         // textBoxStatus
         // 
         this.textBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.textBoxStatus.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.textBoxStatus.Location = new System.Drawing.Point(72, 148);
         this.textBoxStatus.Name = "textBoxStatus";
         this.textBoxStatus.ReadOnly = true;
         this.textBoxStatus.Size = new System.Drawing.Size(561, 20);
         this.textBoxStatus.TabIndex = 34;
         // 
         // buttonDone
         // 
         this.buttonDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.buttonDone.Location = new System.Drawing.Point(569, 107);
         this.buttonDone.Name = "buttonDone";
         this.buttonDone.Size = new System.Drawing.Size(64, 34);
         this.buttonDone.TabIndex = 33;
         this.buttonDone.Text = "Done";
         this.buttonDone.UseVisualStyleBackColor = true;
         // 
         // buttonRun
         // 
         this.buttonRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.buttonRun.Location = new System.Drawing.Point(499, 107);
         this.buttonRun.Name = "buttonRun";
         this.buttonRun.Size = new System.Drawing.Size(64, 34);
         this.buttonRun.TabIndex = 32;
         this.buttonRun.Text = "Run";
         this.buttonRun.UseVisualStyleBackColor = true;
         // 
         // buttonTargetDirectory
         // 
         this.buttonTargetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.buttonTargetDirectory.Location = new System.Drawing.Point(578, 68);
         this.buttonTargetDirectory.Name = "buttonTargetDirectory";
         this.buttonTargetDirectory.Size = new System.Drawing.Size(48, 27);
         this.buttonTargetDirectory.TabIndex = 30;
         this.buttonTargetDirectory.Text = ". . .";
         this.buttonTargetDirectory.UseVisualStyleBackColor = true;
         // 
         // textBoxTargetDirectory
         // 
         this.textBoxTargetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.textBoxTargetDirectory.Location = new System.Drawing.Point(72, 68);
         this.textBoxTargetDirectory.Name = "textBoxTargetDirectory";
         this.textBoxTargetDirectory.Size = new System.Drawing.Size(499, 27);
         this.textBoxTargetDirectory.TabIndex = 29;
         // 
         // labelTarget
         // 
         this.labelTarget.AutoSize = true;
         this.labelTarget.Location = new System.Drawing.Point(10, 71);
         this.labelTarget.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.labelTarget.Name = "labelTarget";
         this.labelTarget.Size = new System.Drawing.Size(50, 20);
         this.labelTarget.TabIndex = 28;
         this.labelTarget.Text = "Target:";
         // 
         // buttonSelectSourceDirectory
         // 
         this.buttonSelectSourceDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.buttonSelectSourceDirectory.Location = new System.Drawing.Point(578, 25);
         this.buttonSelectSourceDirectory.Name = "buttonSelectSourceDirectory";
         this.buttonSelectSourceDirectory.Size = new System.Drawing.Size(48, 27);
         this.buttonSelectSourceDirectory.TabIndex = 27;
         this.buttonSelectSourceDirectory.Text = ". . .";
         this.buttonSelectSourceDirectory.UseVisualStyleBackColor = true;
         // 
         // textBoxSourceDirectory
         // 
         this.textBoxSourceDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.textBoxSourceDirectory.Location = new System.Drawing.Point(72, 25);
         this.textBoxSourceDirectory.Name = "textBoxSourceDirectory";
         this.textBoxSourceDirectory.Size = new System.Drawing.Size(499, 27);
         this.textBoxSourceDirectory.TabIndex = 26;
         // 
         // labelSourceDirectory
         // 
         this.labelSourceDirectory.AutoSize = true;
         this.labelSourceDirectory.Location = new System.Drawing.Point(10, 28);
         this.labelSourceDirectory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.labelSourceDirectory.Name = "labelSourceDirectory";
         this.labelSourceDirectory.Size = new System.Drawing.Size(55, 20);
         this.labelSourceDirectory.TabIndex = 25;
         this.labelSourceDirectory.Text = "Source:";
         // 
         // progressBar
         // 
         this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.progressBar.Location = new System.Drawing.Point(131, 114);
         this.progressBar.Name = "progressBar";
         this.progressBar.Size = new System.Drawing.Size(362, 23);
         this.progressBar.Step = 1;
         this.progressBar.TabIndex = 31;
         this.progressBar.Visible = false;
         // 
         // labelAbout
         // 
         this.labelAbout.AutoSize = true;
         this.labelAbout.Location = new System.Drawing.Point(68, 147);
         this.labelAbout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.labelAbout.Name = "labelAbout";
         this.labelAbout.Size = new System.Drawing.Size(49, 20);
         this.labelAbout.TabIndex = 37;
         this.labelAbout.Text = "About:";
         // 
         // linkLabelSLX
         // 
         this.linkLabelSLX.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
         this.linkLabelSLX.AutoSize = true;
         this.linkLabelSLX.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
         this.linkLabelSLX.Location = new System.Drawing.Point(124, 148);
         this.linkLabelSLX.Name = "linkLabelSLX";
         this.linkLabelSLX.Size = new System.Drawing.Size(176, 20);
         this.linkLabelSLX.TabIndex = 36;
         this.linkLabelSLX.TabStop = true;
         this.linkLabelSLX.Text = "https://github.com/wsberry";
         this.linkLabelSLX.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
         // 
         // MainFormView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
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
         this.Font = new System.Drawing.Font("Segoe UI Light", 11.25F);
         this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.Name = "MainFormView";
         this.Size = new System.Drawing.Size(642, 186);
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabelOptions;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonTargetDirectory;
        private System.Windows.Forms.TextBox textBoxTargetDirectory;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.Button buttonSelectSourceDirectory;
        private System.Windows.Forms.TextBox textBoxSourceDirectory;
        private System.Windows.Forms.Label labelSourceDirectory;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelAbout;
        private System.Windows.Forms.LinkLabel linkLabelSLX;
    }
}
