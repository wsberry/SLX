namespace source_filter.view
{
    partial class source_filter_options
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
      this.panelOptions = new System.Windows.Forms.Panel();
      this.groupBoxOptions = new System.Windows.Forms.GroupBox();
      this.buttonRestoreDefault = new System.Windows.Forms.Button();
      this.comboBoxCopyMethods = new System.Windows.Forms.ComboBox();
      this.textBoxHints = new System.Windows.Forms.TextBox();
      this.buttonCloseOptions = new System.Windows.Forms.Button();
      this.buttonEditFilters = new System.Windows.Forms.Button();
      this.checkBoxIncludeSubdirectories = new System.Windows.Forms.CheckBox();
      this.checkBoxOpenTargetDirectory = new System.Windows.Forms.CheckBox();
      this.checkBoxUpdateNamespaces = new System.Windows.Forms.CheckBox();
      this.panelOptions.SuspendLayout();
      this.groupBoxOptions.SuspendLayout();
      this.SuspendLayout();
      // 
      // panelOptions
      // 
      this.panelOptions.Controls.Add(this.groupBoxOptions);
      this.panelOptions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelOptions.Font = new System.Drawing.Font("Segoe UI Light", 11.25F);
      this.panelOptions.Location = new System.Drawing.Point(0, 0);
      this.panelOptions.Name = "panelOptions";
      this.panelOptions.Padding = new System.Windows.Forms.Padding(5, 1, 5, 5);
      this.panelOptions.Size = new System.Drawing.Size(642, 194);
      this.panelOptions.TabIndex = 23;
      this.panelOptions.Visible = false;
      // 
      // groupBoxOptions
      // 
      this.groupBoxOptions.Controls.Add(this.checkBoxUpdateNamespaces);
      this.groupBoxOptions.Controls.Add(this.buttonRestoreDefault);
      this.groupBoxOptions.Controls.Add(this.comboBoxCopyMethods);
      this.groupBoxOptions.Controls.Add(this.textBoxHints);
      this.groupBoxOptions.Controls.Add(this.buttonCloseOptions);
      this.groupBoxOptions.Controls.Add(this.buttonEditFilters);
      this.groupBoxOptions.Controls.Add(this.checkBoxIncludeSubdirectories);
      this.groupBoxOptions.Controls.Add(this.checkBoxOpenTargetDirectory);
      this.groupBoxOptions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBoxOptions.Location = new System.Drawing.Point(5, 1);
      this.groupBoxOptions.Name = "groupBoxOptions";
      this.groupBoxOptions.Size = new System.Drawing.Size(632, 188);
      this.groupBoxOptions.TabIndex = 1;
      this.groupBoxOptions.TabStop = false;
      this.groupBoxOptions.Text = "Options";
      // 
      // buttonRestoreDefault
      // 
      this.buttonRestoreDefault.Location = new System.Drawing.Point(187, 24);
      this.buttonRestoreDefault.Name = "buttonRestoreDefault";
      this.buttonRestoreDefault.Size = new System.Drawing.Size(140, 34);
      this.buttonRestoreDefault.TabIndex = 25;
      this.buttonRestoreDefault.Text = "Restore Defaults";
      this.buttonRestoreDefault.UseVisualStyleBackColor = true;
      // 
      // comboBoxCopyMethods
      // 
      this.comboBoxCopyMethods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.comboBoxCopyMethods.FormattingEnabled = true;
      this.comboBoxCopyMethods.Location = new System.Drawing.Point(386, 30);
      this.comboBoxCopyMethods.Name = "comboBoxCopyMethods";
      this.comboBoxCopyMethods.Size = new System.Drawing.Size(235, 28);
      this.comboBoxCopyMethods.TabIndex = 24;
      // 
      // textBoxHints
      // 
      this.textBoxHints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxHints.BackColor = System.Drawing.Color.White;
      this.textBoxHints.Location = new System.Drawing.Point(16, 140);
      this.textBoxHints.Name = "textBoxHints";
      this.textBoxHints.ReadOnly = true;
      this.textBoxHints.Size = new System.Drawing.Size(535, 27);
      this.textBoxHints.TabIndex = 23;
      this.textBoxHints.Text = " Note: Empty directories will not be copied!";
      // 
      // buttonCloseOptions
      // 
      this.buttonCloseOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCloseOptions.Location = new System.Drawing.Point(557, 137);
      this.buttonCloseOptions.Name = "buttonCloseOptions";
      this.buttonCloseOptions.Size = new System.Drawing.Size(64, 34);
      this.buttonCloseOptions.TabIndex = 22;
      this.buttonCloseOptions.Text = "OK";
      this.buttonCloseOptions.UseVisualStyleBackColor = true;
      // 
      // buttonEditFilters
      // 
      this.buttonEditFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonEditFilters.Location = new System.Drawing.Point(16, 24);
      this.buttonEditFilters.Name = "buttonEditFilters";
      this.buttonEditFilters.Size = new System.Drawing.Size(165, 34);
      this.buttonEditFilters.TabIndex = 17;
      this.buttonEditFilters.Text = "Filtering Rules . . .";
      this.buttonEditFilters.UseVisualStyleBackColor = true;
      // 
      // checkBoxIncludeSubdirectories
      // 
      this.checkBoxIncludeSubdirectories.AutoSize = true;
      this.checkBoxIncludeSubdirectories.Location = new System.Drawing.Point(16, 64);
      this.checkBoxIncludeSubdirectories.Name = "checkBoxIncludeSubdirectories";
      this.checkBoxIncludeSubdirectories.Size = new System.Drawing.Size(165, 24);
      this.checkBoxIncludeSubdirectories.TabIndex = 18;
      this.checkBoxIncludeSubdirectories.Text = "Include Subdirectories";
      this.checkBoxIncludeSubdirectories.UseVisualStyleBackColor = true;
      // 
      // checkBoxOpenTargetDirectory
      // 
      this.checkBoxOpenTargetDirectory.AutoSize = true;
      this.checkBoxOpenTargetDirectory.Location = new System.Drawing.Point(16, 110);
      this.checkBoxOpenTargetDirectory.Name = "checkBoxOpenTargetDirectory";
      this.checkBoxOpenTargetDirectory.Size = new System.Drawing.Size(215, 24);
      this.checkBoxOpenTargetDirectory.TabIndex = 16;
      this.checkBoxOpenTargetDirectory.Text = "Open Target when Completed";
      this.checkBoxOpenTargetDirectory.UseVisualStyleBackColor = true;
      // 
      // checkBoxUpdateNamespaces
      // 
      this.checkBoxUpdateNamespaces.AutoSize = true;
      this.checkBoxUpdateNamespaces.Location = new System.Drawing.Point(16, 87);
      this.checkBoxUpdateNamespaces.Name = "checkBoxUpdateNamespaces";
      this.checkBoxUpdateNamespaces.Size = new System.Drawing.Size(157, 24);
      this.checkBoxUpdateNamespaces.TabIndex = 26;
      this.checkBoxUpdateNamespaces.Text = "Update Namespaces";
      this.checkBoxUpdateNamespaces.UseVisualStyleBackColor = true;
      // 
      // source_filter_options
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.panelOptions);
      this.Name = "source_filter_options";
      this.Size = new System.Drawing.Size(642, 194);
      this.panelOptions.ResumeLayout(false);
      this.groupBoxOptions.ResumeLayout(false);
      this.groupBoxOptions.PerformLayout();
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelOptions;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.ComboBox comboBoxCopyMethods;
        private System.Windows.Forms.TextBox textBoxHints;
        private System.Windows.Forms.Button buttonCloseOptions;
        private System.Windows.Forms.Button buttonEditFilters;
        private System.Windows.Forms.CheckBox checkBoxIncludeSubdirectories;
        private System.Windows.Forms.CheckBox checkBoxOpenTargetDirectory;
    private System.Windows.Forms.Button buttonRestoreDefault;
    private System.Windows.Forms.CheckBox checkBoxUpdateNamespaces;
  }
}
