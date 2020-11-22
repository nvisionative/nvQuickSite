namespace nvQuickSite.Controls.Settings
{
    partial class UserSettings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMessage = new MetroFramework.Controls.MetroLabel();
            this.btnSave = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.chkShowReleaseCandidates = new MetroFramework.Controls.MetroCheckBox();
            this.dialogIcon = new System.Windows.Forms.PictureBox();
            this.chkShareStatistics = new MetroFramework.Controls.MetroCheckBox();
            this.chkEnableLocalPackageInstall = new MetroFramework.Controls.MetroCheckBox();
            this.cboLoggingLevel = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.lnkViewLogs = new MetroFramework.Controls.MetroLink();
            this.logsIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dialogIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logsIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblMessage.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblMessage.Location = new System.Drawing.Point(111, 18);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(86, 25);
            this.lblMessage.Style = MetroFramework.MetroColorStyle.Blue;
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Message";
            this.lblMessage.UseStyleColors = true;
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(330, 129);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            // 
            // metroButton2
            // 
            this.metroButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.metroButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.metroButton2.Location = new System.Drawing.Point(429, 129);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(75, 23);
            this.metroButton2.TabIndex = 2;
            this.metroButton2.Text = "Cancel";
            // 
            // chkShowReleaseCandidates
            // 
            this.chkShowReleaseCandidates.AutoSize = true;
            this.chkShowReleaseCandidates.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkShowReleaseCandidates.Location = new System.Drawing.Point(118, 53);
            this.chkShowReleaseCandidates.Name = "chkShowReleaseCandidates";
            this.chkShowReleaseCandidates.Size = new System.Drawing.Size(156, 15);
            this.chkShowReleaseCandidates.TabIndex = 3;
            this.chkShowReleaseCandidates.Text = "Show Release Candidates";
            this.chkShowReleaseCandidates.UseVisualStyleBackColor = true;
            // 
            // dialogIcon
            // 
            this.dialogIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.dialogIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.dialogIcon.Image = global::nvQuickSite.Properties.Resources.user_settings;
            this.dialogIcon.Location = new System.Drawing.Point(11, 23);
            this.dialogIcon.Name = "dialogIcon";
            this.dialogIcon.Size = new System.Drawing.Size(91, 88);
            this.dialogIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.dialogIcon.TabIndex = 4;
            this.dialogIcon.TabStop = false;
            // 
            // chkShareStatistics
            // 
            this.chkShareStatistics.AutoSize = true;
            this.chkShareStatistics.Checked = true;
            this.chkShareStatistics.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShareStatistics.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkShareStatistics.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkShareStatistics.Location = new System.Drawing.Point(118, 74);
            this.chkShareStatistics.Name = "chkShareStatistics";
            this.chkShareStatistics.Size = new System.Drawing.Size(101, 15);
            this.chkShareStatistics.TabIndex = 5;
            this.chkShareStatistics.Text = "Share Statistics";
            this.chkShareStatistics.UseVisualStyleBackColor = true;
            // 
            // chkEnableLocalPackageInstall
            // 
            this.chkEnableLocalPackageInstall.AutoSize = true;
            this.chkEnableLocalPackageInstall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkEnableLocalPackageInstall.Location = new System.Drawing.Point(118, 95);
            this.chkEnableLocalPackageInstall.Name = "chkEnableLocalPackageInstall";
            this.chkEnableLocalPackageInstall.Size = new System.Drawing.Size(170, 15);
            this.chkEnableLocalPackageInstall.TabIndex = 6;
            this.chkEnableLocalPackageInstall.Text = "Enable Local Package Install";
            this.chkEnableLocalPackageInstall.UseVisualStyleBackColor = true;
            // 
            // cboLoggingLevel
            // 
            this.cboLoggingLevel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboLoggingLevel.FormattingEnabled = true;
            this.cboLoggingLevel.ItemHeight = 23;
            this.cboLoggingLevel.Location = new System.Drawing.Point(330, 82);
            this.cboLoggingLevel.Name = "cboLoggingLevel";
            this.cboLoggingLevel.Size = new System.Drawing.Size(174, 29);
            this.cboLoggingLevel.TabIndex = 7;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(330, 64);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(81, 15);
            this.metroLabel1.TabIndex = 8;
            this.metroLabel1.Text = "Logging Level";
            // 
            // lnkViewLogs
            // 
            this.lnkViewLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkViewLogs.Location = new System.Drawing.Point(420, 56);
            this.lnkViewLogs.Name = "lnkViewLogs";
            this.lnkViewLogs.Size = new System.Drawing.Size(73, 23);
            this.lnkViewLogs.Style = MetroFramework.MetroColorStyle.Blue;
            this.lnkViewLogs.TabIndex = 9;
            this.lnkViewLogs.Text = "View Logs";
            this.lnkViewLogs.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lnkViewLogs.UseStyleColors = true;
            this.lnkViewLogs.Click += new System.EventHandler(this.lnkViewLogs_Click);
            // 
            // logsIcon
            // 
            this.logsIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logsIcon.Image = global::nvQuickSite.Properties.Resources.logs_icon;
            this.logsIcon.Location = new System.Drawing.Point(493, 64);
            this.logsIcon.Name = "logsIcon";
            this.logsIcon.Size = new System.Drawing.Size(11, 15);
            this.logsIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logsIcon.TabIndex = 10;
            this.logsIcon.TabStop = false;
            this.logsIcon.Click += new System.EventHandler(this.logsIcon_Click);
            // 
            // UserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(527, 170);
            this.Controls.Add(this.logsIcon);
            this.Controls.Add(this.lnkViewLogs);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.cboLoggingLevel);
            this.Controls.Add(this.chkEnableLocalPackageInstall);
            this.Controls.Add(this.chkShareStatistics);
            this.Controls.Add(this.dialogIcon);
            this.Controls.Add(this.chkShowReleaseCandidates);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblMessage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserSettings";
            this.Resizable = false;
            ((System.ComponentModel.ISupportInitialize)(this.dialogIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logsIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel lblMessage;
        private MetroFramework.Controls.MetroButton btnSave;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroCheckBox chkShowReleaseCandidates;
        private System.Windows.Forms.PictureBox dialogIcon;
        private MetroFramework.Controls.MetroCheckBox chkShareStatistics;
        private MetroFramework.Controls.MetroCheckBox chkEnableLocalPackageInstall;
        private MetroFramework.Controls.MetroComboBox cboLoggingLevel;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLink lnkViewLogs;
        private System.Windows.Forms.PictureBox logsIcon;
    }
}