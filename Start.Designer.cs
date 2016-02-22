namespace DNNQuickSite
{
    partial class Start
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabSiteInfo = new MetroFramework.Controls.MetroTabPage();
            this.btnSiteInfoBack = new MetroFramework.Controls.MetroButton();
            this.btnInstall = new MetroFramework.Controls.MetroButton();
            this.btnLocation = new MetroFramework.Controls.MetroButton();
            this.lblLocation = new MetroFramework.Controls.MetroLabel();
            this.txtLocation = new MetroFramework.Controls.MetroTextBox();
            this.chkDeleteSiteIfExists = new MetroFramework.Controls.MetroCheckBox();
            this.chkSiteSpecificAppPool = new MetroFramework.Controls.MetroCheckBox();
            this.lblSiteName = new MetroFramework.Controls.MetroLabel();
            this.txtSiteName = new MetroFramework.Controls.MetroTextBox();
            this.tabInstallPackage = new MetroFramework.Controls.MetroTabPage();
            this.btnInstallPackageNext = new MetroFramework.Controls.MetroButton();
            this.btnLocalInstallPackage = new MetroFramework.Controls.MetroButton();
            this.txtLocalInstallPackage = new MetroFramework.Controls.MetroTextBox();
            this.lblLatestReleases = new MetroFramework.Controls.MetroLabel();
            this.lblLocalInstallPackage = new MetroFramework.Controls.MetroLabel();
            this.cboLatestReleases = new MetroFramework.Controls.MetroComboBox();
            this.btnGetLatestRelease = new MetroFramework.Controls.MetroButton();
            this.btnViewAllReleases = new MetroFramework.Controls.MetroButton();
            this.tabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabProgress = new MetroFramework.Controls.MetroTabPage();
            this.progressBar = new MetroFramework.Controls.MetroProgressBar();
            this.tileQuickStartGuide = new MetroFramework.Controls.MetroTile();
            this.tileDNNDevSpark = new MetroFramework.Controls.MetroTile();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tileDNNDocumentationCenter = new MetroFramework.Controls.MetroTile();
            this.tileDNNCommunityForums = new MetroFramework.Controls.MetroTile();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.tabSiteInfo.SuspendLayout();
            this.tabInstallPackage.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "ZIP Files|*.zip";
            // 
            // tabSiteInfo
            // 
            this.tabSiteInfo.Controls.Add(this.btnSiteInfoBack);
            this.tabSiteInfo.Controls.Add(this.btnInstall);
            this.tabSiteInfo.Controls.Add(this.btnLocation);
            this.tabSiteInfo.Controls.Add(this.lblLocation);
            this.tabSiteInfo.Controls.Add(this.txtLocation);
            this.tabSiteInfo.Controls.Add(this.chkDeleteSiteIfExists);
            this.tabSiteInfo.Controls.Add(this.chkSiteSpecificAppPool);
            this.tabSiteInfo.Controls.Add(this.lblSiteName);
            this.tabSiteInfo.Controls.Add(this.txtSiteName);
            this.tabSiteInfo.HorizontalScrollbarBarColor = true;
            this.tabSiteInfo.Location = new System.Drawing.Point(4, 35);
            this.tabSiteInfo.Name = "tabSiteInfo";
            this.tabSiteInfo.Size = new System.Drawing.Size(599, 279);
            this.tabSiteInfo.TabIndex = 1;
            this.tabSiteInfo.Text = "Site Info";
            this.tabSiteInfo.VerticalScrollbarBarColor = true;
            // 
            // btnSiteInfoBack
            // 
            this.btnSiteInfoBack.Location = new System.Drawing.Point(0, 216);
            this.btnSiteInfoBack.Name = "btnSiteInfoBack";
            this.btnSiteInfoBack.Size = new System.Drawing.Size(90, 36);
            this.btnSiteInfoBack.TabIndex = 11;
            this.btnSiteInfoBack.Text = "Back";
            this.btnSiteInfoBack.Click += new System.EventHandler(this.btnSiteInfoBack_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.Highlight = true;
            this.btnInstall.Location = new System.Drawing.Point(507, 216);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(90, 36);
            this.btnInstall.TabIndex = 10;
            this.btnInstall.Text = "Install";
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnLocation
            // 
            this.btnLocation.Highlight = true;
            this.btnLocation.Location = new System.Drawing.Point(559, 169);
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.Size = new System.Drawing.Size(38, 23);
            this.btnLocation.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnLocation.TabIndex = 8;
            this.btnLocation.Text = "...";
            this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(0, 144);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(58, 19);
            this.lblLocation.TabIndex = 7;
            this.lblLocation.Text = "Location";
            // 
            // txtLocation
            // 
            this.txtLocation.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.txtLocation.Location = new System.Drawing.Point(0, 169);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.Size = new System.Drawing.Size(540, 23);
            this.txtLocation.TabIndex = 6;
            this.txtLocation.Click += new System.EventHandler(this.txtLocation_Click);
            // 
            // chkDeleteSiteIfExists
            // 
            this.chkDeleteSiteIfExists.AutoSize = true;
            this.chkDeleteSiteIfExists.Checked = true;
            this.chkDeleteSiteIfExists.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeleteSiteIfExists.Location = new System.Drawing.Point(0, 113);
            this.chkDeleteSiteIfExists.Name = "chkDeleteSiteIfExists";
            this.chkDeleteSiteIfExists.Size = new System.Drawing.Size(155, 15);
            this.chkDeleteSiteIfExists.TabIndex = 5;
            this.chkDeleteSiteIfExists.Text = "Delete Site in IIS (if exists)";
            this.chkDeleteSiteIfExists.UseVisualStyleBackColor = true;
            // 
            // chkSiteSpecificAppPool
            // 
            this.chkSiteSpecificAppPool.AutoSize = true;
            this.chkSiteSpecificAppPool.Checked = true;
            this.chkSiteSpecificAppPool.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSiteSpecificAppPool.Location = new System.Drawing.Point(0, 82);
            this.chkSiteSpecificAppPool.Name = "chkSiteSpecificAppPool";
            this.chkSiteSpecificAppPool.Size = new System.Drawing.Size(303, 15);
            this.chkSiteSpecificAppPool.TabIndex = 4;
            this.chkSiteSpecificAppPool.Text = "Site-Specific AppPool (DefaultAppPool if unchecked)";
            this.chkSiteSpecificAppPool.UseVisualStyleBackColor = true;
            // 
            // lblSiteName
            // 
            this.lblSiteName.AutoSize = true;
            this.lblSiteName.Location = new System.Drawing.Point(0, 12);
            this.lblSiteName.Name = "lblSiteName";
            this.lblSiteName.Size = new System.Drawing.Size(105, 19);
            this.lblSiteName.TabIndex = 3;
            this.lblSiteName.Text = "Site Name (URL)";
            // 
            // txtSiteName
            // 
            this.txtSiteName.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.txtSiteName.Location = new System.Drawing.Point(0, 37);
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.PromptText = "MySite.local";
            this.txtSiteName.Size = new System.Drawing.Size(597, 23);
            this.txtSiteName.TabIndex = 2;
            this.txtSiteName.Text = "MySite.local";
            this.txtSiteName.UseStyleColors = true;
            // 
            // tabInstallPackage
            // 
            this.tabInstallPackage.Controls.Add(this.btnInstallPackageNext);
            this.tabInstallPackage.Controls.Add(this.btnLocalInstallPackage);
            this.tabInstallPackage.Controls.Add(this.txtLocalInstallPackage);
            this.tabInstallPackage.Controls.Add(this.lblLatestReleases);
            this.tabInstallPackage.Controls.Add(this.lblLocalInstallPackage);
            this.tabInstallPackage.Controls.Add(this.cboLatestReleases);
            this.tabInstallPackage.Controls.Add(this.btnGetLatestRelease);
            this.tabInstallPackage.Controls.Add(this.btnViewAllReleases);
            this.tabInstallPackage.HorizontalScrollbarBarColor = true;
            this.tabInstallPackage.Location = new System.Drawing.Point(4, 35);
            this.tabInstallPackage.Name = "tabInstallPackage";
            this.tabInstallPackage.Size = new System.Drawing.Size(599, 279);
            this.tabInstallPackage.TabIndex = 0;
            this.tabInstallPackage.Text = "Install Package";
            this.tabInstallPackage.VerticalScrollbarBarColor = true;
            // 
            // btnInstallPackageNext
            // 
            this.btnInstallPackageNext.Highlight = true;
            this.btnInstallPackageNext.Location = new System.Drawing.Point(507, 216);
            this.btnInstallPackageNext.Name = "btnInstallPackageNext";
            this.btnInstallPackageNext.Size = new System.Drawing.Size(90, 36);
            this.btnInstallPackageNext.TabIndex = 28;
            this.btnInstallPackageNext.Text = "Next";
            this.btnInstallPackageNext.Click += new System.EventHandler(this.btnInstallPackageNext_Click);
            // 
            // btnLocalInstallPackage
            // 
            this.btnLocalInstallPackage.Highlight = true;
            this.btnLocalInstallPackage.Location = new System.Drawing.Point(559, 169);
            this.btnLocalInstallPackage.Name = "btnLocalInstallPackage";
            this.btnLocalInstallPackage.Size = new System.Drawing.Size(38, 23);
            this.btnLocalInstallPackage.TabIndex = 27;
            this.btnLocalInstallPackage.Text = "...";
            this.btnLocalInstallPackage.Click += new System.EventHandler(this.btnLocalInstallPackage_Click);
            // 
            // txtLocalInstallPackage
            // 
            this.txtLocalInstallPackage.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.txtLocalInstallPackage.Location = new System.Drawing.Point(1, 169);
            this.txtLocalInstallPackage.Name = "txtLocalInstallPackage";
            this.txtLocalInstallPackage.ReadOnly = true;
            this.txtLocalInstallPackage.Size = new System.Drawing.Size(540, 23);
            this.txtLocalInstallPackage.TabIndex = 26;
            this.txtLocalInstallPackage.Click += new System.EventHandler(this.txtLocalInstallPackage_Click);
            // 
            // lblLatestReleases
            // 
            this.lblLatestReleases.AutoSize = true;
            this.lblLatestReleases.Location = new System.Drawing.Point(0, 11);
            this.lblLatestReleases.Name = "lblLatestReleases";
            this.lblLatestReleases.Size = new System.Drawing.Size(161, 19);
            this.lblLatestReleases.TabIndex = 20;
            this.lblLatestReleases.Text = "Download Install Packages";
            // 
            // lblLocalInstallPackage
            // 
            this.lblLocalInstallPackage.AutoSize = true;
            this.lblLocalInstallPackage.Location = new System.Drawing.Point(0, 144);
            this.lblLocalInstallPackage.Name = "lblLocalInstallPackage";
            this.lblLocalInstallPackage.Size = new System.Drawing.Size(127, 19);
            this.lblLocalInstallPackage.TabIndex = 25;
            this.lblLocalInstallPackage.Text = "Local Install Package";
            // 
            // cboLatestReleases
            // 
            this.cboLatestReleases.FormattingEnabled = true;
            this.cboLatestReleases.ItemHeight = 23;
            this.cboLatestReleases.Location = new System.Drawing.Point(1, 33);
            this.cboLatestReleases.Name = "cboLatestReleases";
            this.cboLatestReleases.Size = new System.Drawing.Size(540, 29);
            this.cboLatestReleases.TabIndex = 21;
            this.cboLatestReleases.SelectedIndexChanged += new System.EventHandler(this.cboLatestReleases_SelectedIndexChanged);
            // 
            // btnGetLatestRelease
            // 
            this.btnGetLatestRelease.Location = new System.Drawing.Point(559, 33);
            this.btnGetLatestRelease.Name = "btnGetLatestRelease";
            this.btnGetLatestRelease.Size = new System.Drawing.Size(38, 29);
            this.btnGetLatestRelease.TabIndex = 22;
            this.btnGetLatestRelease.Text = ">";
            this.btnGetLatestRelease.Click += new System.EventHandler(this.btnGetLatestRelease_Click);
            // 
            // btnViewAllReleases
            // 
            this.btnViewAllReleases.Location = new System.Drawing.Point(1, 81);
            this.btnViewAllReleases.Name = "btnViewAllReleases";
            this.btnViewAllReleases.Size = new System.Drawing.Size(540, 37);
            this.btnViewAllReleases.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnViewAllReleases.TabIndex = 23;
            this.btnViewAllReleases.Text = "View ALL Releases";
            this.btnViewAllReleases.Click += new System.EventHandler(this.btnViewAllReleases_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabInstallPackage);
            this.tabControl.Controls.Add(this.tabSiteInfo);
            this.tabControl.Controls.Add(this.tabProgress);
            this.tabControl.Location = new System.Drawing.Point(3, 14);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(607, 318);
            this.tabControl.TabIndex = 26;
            // 
            // tabProgress
            // 
            this.tabProgress.Controls.Add(this.progressBar);
            this.tabProgress.HorizontalScrollbarBarColor = true;
            this.tabProgress.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tabProgress.Location = new System.Drawing.Point(4, 35);
            this.tabProgress.Name = "tabProgress";
            this.tabProgress.Size = new System.Drawing.Size(599, 279);
            this.tabProgress.TabIndex = 2;
            this.tabProgress.Text = "Progress";
            this.tabProgress.VerticalScrollbarBarColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(0, 36);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(597, 23);
            this.progressBar.TabIndex = 2;
            // 
            // tileQuickStartGuide
            // 
            this.tileQuickStartGuide.Location = new System.Drawing.Point(7, 335);
            this.tileQuickStartGuide.Name = "tileQuickStartGuide";
            this.tileQuickStartGuide.Size = new System.Drawing.Size(162, 104);
            this.tileQuickStartGuide.TabIndex = 27;
            this.tileQuickStartGuide.Text = "Quick Start Guide";
            // 
            // tileDNNDevSpark
            // 
            this.tileDNNDevSpark.Location = new System.Drawing.Point(473, 334);
            this.tileDNNDevSpark.Name = "tileDNNDevSpark";
            this.tileDNNDevSpark.Size = new System.Drawing.Size(131, 50);
            this.tileDNNDevSpark.Style = MetroFramework.MetroColorStyle.Purple;
            this.tileDNNDevSpark.TabIndex = 28;
            this.tileDNNDevSpark.Text = "DNN DevSpark";
            // 
            // tileDNNDocumentationCenter
            // 
            this.tileDNNDocumentationCenter.Location = new System.Drawing.Point(176, 389);
            this.tileDNNDocumentationCenter.Name = "tileDNNDocumentationCenter";
            this.tileDNNDocumentationCenter.Size = new System.Drawing.Size(428, 50);
            this.tileDNNDocumentationCenter.Style = MetroFramework.MetroColorStyle.Orange;
            this.tileDNNDocumentationCenter.TabIndex = 29;
            this.tileDNNDocumentationCenter.Text = "DNN Documentation Center";
            this.tileDNNDocumentationCenter.Click += new System.EventHandler(this.tileDNNDocumentationCenter_Click);
            // 
            // tileDNNCommunityForums
            // 
            this.tileDNNCommunityForums.Cursor = System.Windows.Forms.Cursors.Default;
            this.tileDNNCommunityForums.Location = new System.Drawing.Point(176, 335);
            this.tileDNNCommunityForums.Name = "tileDNNCommunityForums";
            this.tileDNNCommunityForums.Size = new System.Drawing.Size(291, 50);
            this.tileDNNCommunityForums.Style = MetroFramework.MetroColorStyle.Green;
            this.tileDNNCommunityForums.TabIndex = 30;
            this.tileDNNCommunityForums.Text = "DNN Community Forums";
            this.tileDNNCommunityForums.Click += new System.EventHandler(this.tileDNNCommunityForums_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tileDNNCommunityForums);
            this.Controls.Add(this.tileDNNDocumentationCenter);
            this.Controls.Add(this.tileDNNDevSpark);
            this.Controls.Add(this.tileQuickStartGuide);
            this.Controls.Add(this.tabControl);
            this.Name = "Start";
            this.Size = new System.Drawing.Size(610, 447);
            this.tabSiteInfo.ResumeLayout(false);
            this.tabSiteInfo.PerformLayout();
            this.tabInstallPackage.ResumeLayout(false);
            this.tabInstallPackage.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabProgress.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private MetroFramework.Controls.MetroTabPage tabSiteInfo;
        private MetroFramework.Controls.MetroTabPage tabInstallPackage;
        private MetroFramework.Controls.MetroTextBox txtLocalInstallPackage;
        private MetroFramework.Controls.MetroLabel lblLocalInstallPackage;
        private MetroFramework.Controls.MetroLabel lblLatestReleases;
        private MetroFramework.Controls.MetroButton btnViewAllReleases;
        private MetroFramework.Controls.MetroComboBox cboLatestReleases;
        private MetroFramework.Controls.MetroButton btnGetLatestRelease;
        private MetroFramework.Controls.MetroTabControl tabControl;
        private MetroFramework.Controls.MetroTile tileQuickStartGuide;
        private MetroFramework.Controls.MetroTile tileDNNDevSpark;
        private MetroFramework.Controls.MetroTextBox txtSiteName;
        private MetroFramework.Controls.MetroLabel lblSiteName;
        private MetroFramework.Controls.MetroButton btnLocation;
        private MetroFramework.Controls.MetroLabel lblLocation;
        private MetroFramework.Controls.MetroTextBox txtLocation;
        private MetroFramework.Controls.MetroCheckBox chkDeleteSiteIfExists;
        private MetroFramework.Controls.MetroCheckBox chkSiteSpecificAppPool;
        private MetroFramework.Controls.MetroButton btnLocalInstallPackage;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private MetroFramework.Controls.MetroButton btnInstall;
        private MetroFramework.Controls.MetroTile tileDNNDocumentationCenter;
        private MetroFramework.Controls.MetroTile tileDNNCommunityForums;
        private MetroFramework.Controls.MetroButton btnInstallPackageNext;
        private MetroFramework.Controls.MetroButton btnSiteInfoBack;
        private MetroFramework.Controls.MetroTabPage tabProgress;
        private MetroFramework.Controls.MetroProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}
