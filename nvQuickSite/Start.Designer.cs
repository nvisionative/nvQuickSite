namespace nvQuickSite
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
            this.tabSiteInfo = new MetroFramework.Controls.MetroTabPage();
            this.lblInstallSubFolder = new MetroFramework.Controls.MetroLabel();
            this.txtInstallSubFolder = new MetroFramework.Controls.MetroTextBox();
            this.txtSiteNameSuffix = new MetroFramework.Controls.MetroTextBox();
            this.btnSiteInfoBack = new MetroFramework.Controls.MetroButton();
            this.btnSiteInfoNext = new MetroFramework.Controls.MetroButton();
            this.btnLocation = new MetroFramework.Controls.MetroButton();
            this.lblInstallBaseFolder = new MetroFramework.Controls.MetroLabel();
            this.txtInstallBaseFolder = new MetroFramework.Controls.MetroTextBox();
            this.chkDeleteSiteIfExists = new MetroFramework.Controls.MetroCheckBox();
            this.chkSiteSpecificAppPool = new MetroFramework.Controls.MetroCheckBox();
            this.lblSiteName = new MetroFramework.Controls.MetroLabel();
            this.txtSiteNamePrefix = new MetroFramework.Controls.MetroTextBox();
            this.toggleSiteInfoRemember = new MetroFramework.Controls.MetroToggle();
            this.tabInstallPackage = new MetroFramework.Controls.MetroTabPage();
            this.cboProductVersion = new MetroFramework.Controls.MetroComboBox();
            this.progressBarDownload = new MetroFramework.Controls.MetroProgressBar();
            this.btnInstallPackageNext = new MetroFramework.Controls.MetroButton();
            this.btnLocalInstallPackage = new MetroFramework.Controls.MetroButton();
            this.txtLocalInstallPackage = new MetroFramework.Controls.MetroTextBox();
            this.lblLatestReleases = new MetroFramework.Controls.MetroLabel();
            this.lblLocalInstallPackage = new MetroFramework.Controls.MetroLabel();
            this.cboProductName = new MetroFramework.Controls.MetroComboBox();
            this.btnGetLatestRelease = new MetroFramework.Controls.MetroButton();
            this.btnViewAllReleases = new MetroFramework.Controls.MetroButton();
            this.tabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabDatabaseInfo = new MetroFramework.Controls.MetroTabPage();
            this.btnDatabaseInfoBack = new MetroFramework.Controls.MetroButton();
            this.btnDatabaseInfoNext = new MetroFramework.Controls.MetroButton();
            this.lblDBName = new MetroFramework.Controls.MetroLabel();
            this.txtDBName = new MetroFramework.Controls.MetroTextBox();
            this.txtDBPassword = new MetroFramework.Controls.MetroTextBox();
            this.lblDBPassword = new MetroFramework.Controls.MetroLabel();
            this.lblDBUserName = new MetroFramework.Controls.MetroLabel();
            this.txtDBUserName = new MetroFramework.Controls.MetroTextBox();
            this.rdoSQLServerAuthentication = new MetroFramework.Controls.MetroRadioButton();
            this.rdoWindowsAuthentication = new MetroFramework.Controls.MetroRadioButton();
            this.lblDBServerName = new MetroFramework.Controls.MetroLabel();
            this.txtDBServerName = new MetroFramework.Controls.MetroTextBox();
            this.tabProgress = new MetroFramework.Controls.MetroTabPage();
            this.btnVisitSite = new MetroFramework.Controls.MetroButton();
            this.lblProgressStatus = new MetroFramework.Controls.MetroLabel();
            this.lblProgress = new MetroFramework.Controls.MetroLabel();
            this.progressBar = new MetroFramework.Controls.MetroProgressBar();
            this.tileDNNAwareness = new MetroFramework.Controls.MetroTile();
            this.tileDNNDocs = new MetroFramework.Controls.MetroTile();
            this.tileMorenvQuickProducts = new MetroFramework.Controls.MetroTile();
            this.lblRemember = new MetroFramework.Controls.MetroLabel();
            this.tileQuickSettings = new MetroFramework.Controls.MetroTile();
            this.tileDNNCommunity = new MetroFramework.Controls.MetroTile();
            this.tabSiteInfo.SuspendLayout();
            this.tabInstallPackage.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabDatabaseInfo.SuspendLayout();
            this.tabProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSiteInfo
            // 
            this.tabSiteInfo.Controls.Add(this.lblInstallSubFolder);
            this.tabSiteInfo.Controls.Add(this.txtInstallSubFolder);
            this.tabSiteInfo.Controls.Add(this.txtSiteNameSuffix);
            this.tabSiteInfo.Controls.Add(this.btnSiteInfoBack);
            this.tabSiteInfo.Controls.Add(this.btnSiteInfoNext);
            this.tabSiteInfo.Controls.Add(this.btnLocation);
            this.tabSiteInfo.Controls.Add(this.lblInstallBaseFolder);
            this.tabSiteInfo.Controls.Add(this.txtInstallBaseFolder);
            this.tabSiteInfo.Controls.Add(this.chkDeleteSiteIfExists);
            this.tabSiteInfo.Controls.Add(this.chkSiteSpecificAppPool);
            this.tabSiteInfo.Controls.Add(this.lblSiteName);
            this.tabSiteInfo.Controls.Add(this.txtSiteNamePrefix);
            this.tabSiteInfo.HorizontalScrollbarBarColor = true;
            this.tabSiteInfo.HorizontalScrollbarSize = 19;
            this.tabSiteInfo.Location = new System.Drawing.Point(8, 42);
            this.tabSiteInfo.Margin = new System.Windows.Forms.Padding(6);
            this.tabSiteInfo.Name = "tabSiteInfo";
            this.tabSiteInfo.Size = new System.Drawing.Size(1198, 515);
            this.tabSiteInfo.TabIndex = 1;
            this.tabSiteInfo.Text = "Site Info";
            this.tabSiteInfo.VerticalScrollbarBarColor = true;
            this.tabSiteInfo.VerticalScrollbarSize = 20;
            // 
            // lblInstallSubFolder
            // 
            this.lblInstallSubFolder.AutoSize = true;
            this.lblInstallSubFolder.Location = new System.Drawing.Point(646, 277);
            this.lblInstallSubFolder.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblInstallSubFolder.Name = "lblInstallSubFolder";
            this.lblInstallSubFolder.Size = new System.Drawing.Size(109, 19);
            this.lblInstallSubFolder.TabIndex = 14;
            this.lblInstallSubFolder.Text = "Install Sub Folder";
            // 
            // txtInstallSubFolder
            // 
            this.txtInstallSubFolder.Location = new System.Drawing.Point(646, 325);
            this.txtInstallSubFolder.Margin = new System.Windows.Forms.Padding(6);
            this.txtInstallSubFolder.Name = "txtInstallSubFolder";
            this.txtInstallSubFolder.Size = new System.Drawing.Size(434, 44);
            this.txtInstallSubFolder.TabIndex = 13;
            // 
            // txtSiteNameSuffix
            // 
            this.txtSiteNameSuffix.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.txtSiteNameSuffix.Location = new System.Drawing.Point(646, 71);
            this.txtSiteNameSuffix.Margin = new System.Windows.Forms.Padding(6);
            this.txtSiteNameSuffix.Name = "txtSiteNameSuffix";
            this.txtSiteNameSuffix.Size = new System.Drawing.Size(434, 44);
            this.txtSiteNameSuffix.TabIndex = 12;
            this.txtSiteNameSuffix.Text = ".dnndev.me";
            // 
            // btnSiteInfoBack
            // 
            this.btnSiteInfoBack.Location = new System.Drawing.Point(0, 413);
            this.btnSiteInfoBack.Margin = new System.Windows.Forms.Padding(6);
            this.btnSiteInfoBack.Name = "btnSiteInfoBack";
            this.btnSiteInfoBack.Size = new System.Drawing.Size(180, 69);
            this.btnSiteInfoBack.TabIndex = 11;
            this.btnSiteInfoBack.Text = "&Back";
            this.btnSiteInfoBack.Click += new System.EventHandler(this.btnSiteInfoBack_Click);
            // 
            // btnSiteInfoNext
            // 
            this.btnSiteInfoNext.Highlight = true;
            this.btnSiteInfoNext.Location = new System.Drawing.Point(1014, 415);
            this.btnSiteInfoNext.Margin = new System.Windows.Forms.Padding(6);
            this.btnSiteInfoNext.Name = "btnSiteInfoNext";
            this.btnSiteInfoNext.Size = new System.Drawing.Size(180, 69);
            this.btnSiteInfoNext.TabIndex = 10;
            this.btnSiteInfoNext.Text = "&Next";
            this.btnSiteInfoNext.Click += new System.EventHandler(this.btnSiteInfoNext_Click);
            // 
            // btnLocation
            // 
            this.btnLocation.Highlight = true;
            this.btnLocation.Location = new System.Drawing.Point(556, 325);
            this.btnLocation.Margin = new System.Windows.Forms.Padding(6);
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.Size = new System.Drawing.Size(76, 44);
            this.btnLocation.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnLocation.TabIndex = 8;
            this.btnLocation.Text = "...";
            this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // lblInstallBaseFolder
            // 
            this.lblInstallBaseFolder.AutoSize = true;
            this.lblInstallBaseFolder.Location = new System.Drawing.Point(0, 277);
            this.lblInstallBaseFolder.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblInstallBaseFolder.Name = "lblInstallBaseFolder";
            this.lblInstallBaseFolder.Size = new System.Drawing.Size(114, 19);
            this.lblInstallBaseFolder.TabIndex = 7;
            this.lblInstallBaseFolder.Text = "Install Base Folder";
            // 
            // txtInstallBaseFolder
            // 
            this.txtInstallBaseFolder.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.txtInstallBaseFolder.Location = new System.Drawing.Point(0, 325);
            this.txtInstallBaseFolder.Margin = new System.Windows.Forms.Padding(6);
            this.txtInstallBaseFolder.Name = "txtInstallBaseFolder";
            this.txtInstallBaseFolder.Size = new System.Drawing.Size(544, 44);
            this.txtInstallBaseFolder.TabIndex = 6;
            // 
            // chkDeleteSiteIfExists
            // 
            this.chkDeleteSiteIfExists.AutoSize = true;
            this.chkDeleteSiteIfExists.Checked = true;
            this.chkDeleteSiteIfExists.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeleteSiteIfExists.Location = new System.Drawing.Point(0, 217);
            this.chkDeleteSiteIfExists.Margin = new System.Windows.Forms.Padding(6);
            this.chkDeleteSiteIfExists.Name = "chkDeleteSiteIfExists";
            this.chkDeleteSiteIfExists.Size = new System.Drawing.Size(156, 15);
            this.chkDeleteSiteIfExists.TabIndex = 5;
            this.chkDeleteSiteIfExists.Text = "Delete Site in IIS (if exists)";
            this.chkDeleteSiteIfExists.UseVisualStyleBackColor = true;
            // 
            // chkSiteSpecificAppPool
            // 
            this.chkSiteSpecificAppPool.AutoSize = true;
            this.chkSiteSpecificAppPool.Checked = true;
            this.chkSiteSpecificAppPool.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSiteSpecificAppPool.Location = new System.Drawing.Point(0, 158);
            this.chkSiteSpecificAppPool.Margin = new System.Windows.Forms.Padding(6);
            this.chkSiteSpecificAppPool.Name = "chkSiteSpecificAppPool";
            this.chkSiteSpecificAppPool.Size = new System.Drawing.Size(303, 15);
            this.chkSiteSpecificAppPool.TabIndex = 4;
            this.chkSiteSpecificAppPool.Text = "Site-Specific AppPool (DefaultAppPool if unchecked)";
            this.chkSiteSpecificAppPool.UseVisualStyleBackColor = true;
            // 
            // lblSiteName
            // 
            this.lblSiteName.AutoSize = true;
            this.lblSiteName.Location = new System.Drawing.Point(0, 23);
            this.lblSiteName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSiteName.Name = "lblSiteName";
            this.lblSiteName.Size = new System.Drawing.Size(105, 19);
            this.lblSiteName.TabIndex = 3;
            this.lblSiteName.Text = "Site Name (URL)";
            // 
            // txtSiteNamePrefix
            // 
            this.txtSiteNamePrefix.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.txtSiteNamePrefix.Location = new System.Drawing.Point(0, 71);
            this.txtSiteNamePrefix.Margin = new System.Windows.Forms.Padding(6);
            this.txtSiteNamePrefix.Name = "txtSiteNamePrefix";
            this.txtSiteNamePrefix.Size = new System.Drawing.Size(632, 44);
            this.txtSiteNamePrefix.TabIndex = 2;
            this.txtSiteNamePrefix.Text = "MySite";
            this.txtSiteNamePrefix.UseStyleColors = true;
            this.txtSiteNamePrefix.TextChanged += new System.EventHandler(this.txtSiteNamePrefix_TextChanged);
            // 
            // toggleSiteInfoRemember
            // 
            this.toggleSiteInfoRemember.AutoSize = true;
            this.toggleSiteInfoRemember.Checked = true;
            this.toggleSiteInfoRemember.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toggleSiteInfoRemember.Location = new System.Drawing.Point(8, 869);
            this.toggleSiteInfoRemember.Margin = new System.Windows.Forms.Padding(6);
            this.toggleSiteInfoRemember.Name = "toggleSiteInfoRemember";
            this.toggleSiteInfoRemember.Size = new System.Drawing.Size(80, 29);
            this.toggleSiteInfoRemember.TabIndex = 12;
            this.toggleSiteInfoRemember.Text = "On";
            this.toggleSiteInfoRemember.Theme = MetroFramework.MetroThemeStyle.Light;
            this.toggleSiteInfoRemember.UseVisualStyleBackColor = true;
            this.toggleSiteInfoRemember.Visible = false;
            this.toggleSiteInfoRemember.CheckedChanged += new System.EventHandler(this.toggleSiteInfoRemember_CheckedChanged);
            // 
            // tabInstallPackage
            // 
            this.tabInstallPackage.Controls.Add(this.cboProductVersion);
            this.tabInstallPackage.Controls.Add(this.progressBarDownload);
            this.tabInstallPackage.Controls.Add(this.btnInstallPackageNext);
            this.tabInstallPackage.Controls.Add(this.btnLocalInstallPackage);
            this.tabInstallPackage.Controls.Add(this.txtLocalInstallPackage);
            this.tabInstallPackage.Controls.Add(this.lblLatestReleases);
            this.tabInstallPackage.Controls.Add(this.lblLocalInstallPackage);
            this.tabInstallPackage.Controls.Add(this.cboProductName);
            this.tabInstallPackage.Controls.Add(this.btnGetLatestRelease);
            this.tabInstallPackage.Controls.Add(this.btnViewAllReleases);
            this.tabInstallPackage.HorizontalScrollbarBarColor = true;
            this.tabInstallPackage.HorizontalScrollbarSize = 19;
            this.tabInstallPackage.Location = new System.Drawing.Point(8, 42);
            this.tabInstallPackage.Margin = new System.Windows.Forms.Padding(6);
            this.tabInstallPackage.Name = "tabInstallPackage";
            this.tabInstallPackage.Size = new System.Drawing.Size(1198, 515);
            this.tabInstallPackage.TabIndex = 0;
            this.tabInstallPackage.Text = "Install Package Info";
            this.tabInstallPackage.VerticalScrollbarBarColor = true;
            this.tabInstallPackage.VerticalScrollbarSize = 20;
            // 
            // cboProductVersion
            // 
            this.cboProductVersion.FormattingEnabled = true;
            this.cboProductVersion.ItemHeight = 23;
            this.cboProductVersion.Location = new System.Drawing.Point(834, 63);
            this.cboProductVersion.Margin = new System.Windows.Forms.Padding(6);
            this.cboProductVersion.Name = "cboProductVersion";
            this.cboProductVersion.Size = new System.Drawing.Size(242, 29);
            this.cboProductVersion.TabIndex = 30;
            this.cboProductVersion.SelectedIndexChanged += new System.EventHandler(this.cboProductVersion_SelectedIndexChanged);
            // 
            // progressBarDownload
            // 
            this.progressBarDownload.HideProgressText = false;
            this.progressBarDownload.Location = new System.Drawing.Point(2, 127);
            this.progressBarDownload.Margin = new System.Windows.Forms.Padding(6);
            this.progressBarDownload.Name = "progressBarDownload";
            this.progressBarDownload.Size = new System.Drawing.Size(1078, 44);
            this.progressBarDownload.Style = MetroFramework.MetroColorStyle.Blue;
            this.progressBarDownload.TabIndex = 29;
            this.progressBarDownload.Visible = false;
            // 
            // btnInstallPackageNext
            // 
            this.btnInstallPackageNext.Highlight = true;
            this.btnInstallPackageNext.Location = new System.Drawing.Point(1014, 415);
            this.btnInstallPackageNext.Margin = new System.Windows.Forms.Padding(6);
            this.btnInstallPackageNext.Name = "btnInstallPackageNext";
            this.btnInstallPackageNext.Size = new System.Drawing.Size(180, 69);
            this.btnInstallPackageNext.TabIndex = 28;
            this.btnInstallPackageNext.Text = "&Next";
            this.btnInstallPackageNext.Click += new System.EventHandler(this.btnInstallPackageNext_Click);
            // 
            // btnLocalInstallPackage
            // 
            this.btnLocalInstallPackage.Highlight = true;
            this.btnLocalInstallPackage.Location = new System.Drawing.Point(1118, 325);
            this.btnLocalInstallPackage.Margin = new System.Windows.Forms.Padding(6);
            this.btnLocalInstallPackage.Name = "btnLocalInstallPackage";
            this.btnLocalInstallPackage.Size = new System.Drawing.Size(76, 44);
            this.btnLocalInstallPackage.TabIndex = 27;
            this.btnLocalInstallPackage.Text = "...";
            this.btnLocalInstallPackage.Click += new System.EventHandler(this.btnLocalInstallPackage_Click);
            // 
            // txtLocalInstallPackage
            // 
            this.txtLocalInstallPackage.FontWeight = MetroFramework.MetroTextBoxWeight.Light;
            this.txtLocalInstallPackage.Location = new System.Drawing.Point(2, 325);
            this.txtLocalInstallPackage.Margin = new System.Windows.Forms.Padding(6);
            this.txtLocalInstallPackage.Name = "txtLocalInstallPackage";
            this.txtLocalInstallPackage.ReadOnly = true;
            this.txtLocalInstallPackage.Size = new System.Drawing.Size(1078, 44);
            this.txtLocalInstallPackage.TabIndex = 26;
            this.txtLocalInstallPackage.Click += new System.EventHandler(this.txtLocalInstallPackage_Click);
            // 
            // lblLatestReleases
            // 
            this.lblLatestReleases.AutoSize = true;
            this.lblLatestReleases.Location = new System.Drawing.Point(0, 21);
            this.lblLatestReleases.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblLatestReleases.Name = "lblLatestReleases";
            this.lblLatestReleases.Size = new System.Drawing.Size(161, 19);
            this.lblLatestReleases.TabIndex = 20;
            this.lblLatestReleases.Text = "Download Install Packages";
            // 
            // lblLocalInstallPackage
            // 
            this.lblLocalInstallPackage.AutoSize = true;
            this.lblLocalInstallPackage.Location = new System.Drawing.Point(0, 277);
            this.lblLocalInstallPackage.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblLocalInstallPackage.Name = "lblLocalInstallPackage";
            this.lblLocalInstallPackage.Size = new System.Drawing.Size(127, 19);
            this.lblLocalInstallPackage.TabIndex = 25;
            this.lblLocalInstallPackage.Text = "Local Install Package";
            // 
            // cboProductName
            // 
            this.cboProductName.FormattingEnabled = true;
            this.cboProductName.ItemHeight = 23;
            this.cboProductName.Location = new System.Drawing.Point(2, 63);
            this.cboProductName.Margin = new System.Windows.Forms.Padding(6);
            this.cboProductName.Name = "cboProductName";
            this.cboProductName.Size = new System.Drawing.Size(816, 29);
            this.cboProductName.TabIndex = 31;
            this.cboProductName.SelectedIndexChanged += new System.EventHandler(this.cboProductName_SelectedIndexChanged);
            // 
            // btnGetLatestRelease
            // 
            this.btnGetLatestRelease.Location = new System.Drawing.Point(1118, 63);
            this.btnGetLatestRelease.Margin = new System.Windows.Forms.Padding(6);
            this.btnGetLatestRelease.Name = "btnGetLatestRelease";
            this.btnGetLatestRelease.Size = new System.Drawing.Size(76, 56);
            this.btnGetLatestRelease.TabIndex = 22;
            this.btnGetLatestRelease.Text = ">";
            this.btnGetLatestRelease.Visible = false;
            this.btnGetLatestRelease.Click += new System.EventHandler(this.btnGetLatestRelease_Click);
            // 
            // btnViewAllReleases
            // 
            this.btnViewAllReleases.Location = new System.Drawing.Point(0, 183);
            this.btnViewAllReleases.Margin = new System.Windows.Forms.Padding(6);
            this.btnViewAllReleases.Name = "btnViewAllReleases";
            this.btnViewAllReleases.Size = new System.Drawing.Size(1080, 71);
            this.btnViewAllReleases.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnViewAllReleases.TabIndex = 23;
            this.btnViewAllReleases.Text = "View ALL Releases";
            this.btnViewAllReleases.Visible = false;
            this.btnViewAllReleases.Click += new System.EventHandler(this.btnViewAllReleases_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabInstallPackage);
            this.tabControl.Controls.Add(this.tabSiteInfo);
            this.tabControl.Controls.Add(this.tabDatabaseInfo);
            this.tabControl.Controls.Add(this.tabProgress);
            this.tabControl.Location = new System.Drawing.Point(6, 27);
            this.tabControl.Margin = new System.Windows.Forms.Padding(6);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1214, 565);
            this.tabControl.TabIndex = 26;
            // 
            // tabDatabaseInfo
            // 
            this.tabDatabaseInfo.Controls.Add(this.btnDatabaseInfoBack);
            this.tabDatabaseInfo.Controls.Add(this.btnDatabaseInfoNext);
            this.tabDatabaseInfo.Controls.Add(this.lblDBName);
            this.tabDatabaseInfo.Controls.Add(this.txtDBName);
            this.tabDatabaseInfo.Controls.Add(this.txtDBPassword);
            this.tabDatabaseInfo.Controls.Add(this.lblDBPassword);
            this.tabDatabaseInfo.Controls.Add(this.lblDBUserName);
            this.tabDatabaseInfo.Controls.Add(this.txtDBUserName);
            this.tabDatabaseInfo.Controls.Add(this.rdoSQLServerAuthentication);
            this.tabDatabaseInfo.Controls.Add(this.rdoWindowsAuthentication);
            this.tabDatabaseInfo.Controls.Add(this.lblDBServerName);
            this.tabDatabaseInfo.Controls.Add(this.txtDBServerName);
            this.tabDatabaseInfo.HorizontalScrollbarBarColor = true;
            this.tabDatabaseInfo.HorizontalScrollbarSize = 19;
            this.tabDatabaseInfo.Location = new System.Drawing.Point(8, 42);
            this.tabDatabaseInfo.Margin = new System.Windows.Forms.Padding(6);
            this.tabDatabaseInfo.Name = "tabDatabaseInfo";
            this.tabDatabaseInfo.Size = new System.Drawing.Size(1198, 515);
            this.tabDatabaseInfo.TabIndex = 3;
            this.tabDatabaseInfo.Text = "Database Info";
            this.tabDatabaseInfo.VerticalScrollbarBarColor = true;
            this.tabDatabaseInfo.VerticalScrollbarSize = 20;
            // 
            // btnDatabaseInfoBack
            // 
            this.btnDatabaseInfoBack.Location = new System.Drawing.Point(0, 415);
            this.btnDatabaseInfoBack.Margin = new System.Windows.Forms.Padding(6);
            this.btnDatabaseInfoBack.Name = "btnDatabaseInfoBack";
            this.btnDatabaseInfoBack.Size = new System.Drawing.Size(180, 69);
            this.btnDatabaseInfoBack.TabIndex = 13;
            this.btnDatabaseInfoBack.Text = "&Back";
            this.btnDatabaseInfoBack.Click += new System.EventHandler(this.btnDatabaseInfoBack_Click);
            // 
            // btnDatabaseInfoNext
            // 
            this.btnDatabaseInfoNext.Highlight = true;
            this.btnDatabaseInfoNext.Location = new System.Drawing.Point(1014, 415);
            this.btnDatabaseInfoNext.Margin = new System.Windows.Forms.Padding(6);
            this.btnDatabaseInfoNext.Name = "btnDatabaseInfoNext";
            this.btnDatabaseInfoNext.Size = new System.Drawing.Size(180, 69);
            this.btnDatabaseInfoNext.TabIndex = 12;
            this.btnDatabaseInfoNext.Text = "&Next";
            this.btnDatabaseInfoNext.Click += new System.EventHandler(this.btnDatabaseInfoNext_Click);
            // 
            // lblDBName
            // 
            this.lblDBName.AutoSize = true;
            this.lblDBName.Location = new System.Drawing.Point(0, 212);
            this.lblDBName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDBName.Name = "lblDBName";
            this.lblDBName.Size = new System.Drawing.Size(103, 19);
            this.lblDBName.TabIndex = 11;
            this.lblDBName.Text = "Database Name";
            // 
            // txtDBName
            // 
            this.txtDBName.Location = new System.Drawing.Point(0, 258);
            this.txtDBName.Margin = new System.Windows.Forms.Padding(6);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(540, 44);
            this.txtDBName.TabIndex = 3;
            this.txtDBName.Text = "MySite";
            this.txtDBName.UseStyleColors = true;
            // 
            // txtDBPassword
            // 
            this.txtDBPassword.Enabled = false;
            this.txtDBPassword.Location = new System.Drawing.Point(680, 258);
            this.txtDBPassword.Margin = new System.Windows.Forms.Padding(6);
            this.txtDBPassword.Name = "txtDBPassword";
            this.txtDBPassword.Size = new System.Drawing.Size(514, 44);
            this.txtDBPassword.TabIndex = 9;
            // 
            // lblDBPassword
            // 
            this.lblDBPassword.AutoSize = true;
            this.lblDBPassword.Enabled = false;
            this.lblDBPassword.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblDBPassword.Location = new System.Drawing.Point(680, 213);
            this.lblDBPassword.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDBPassword.Name = "lblDBPassword";
            this.lblDBPassword.Size = new System.Drawing.Size(55, 15);
            this.lblDBPassword.TabIndex = 8;
            this.lblDBPassword.Text = "Password";
            // 
            // lblDBUserName
            // 
            this.lblDBUserName.AutoSize = true;
            this.lblDBUserName.Enabled = false;
            this.lblDBUserName.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblDBUserName.Location = new System.Drawing.Point(680, 121);
            this.lblDBUserName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDBUserName.Name = "lblDBUserName";
            this.lblDBUserName.Size = new System.Drawing.Size(64, 15);
            this.lblDBUserName.TabIndex = 7;
            this.lblDBUserName.Text = "User Name";
            // 
            // txtDBUserName
            // 
            this.txtDBUserName.Enabled = false;
            this.txtDBUserName.Location = new System.Drawing.Point(680, 163);
            this.txtDBUserName.Margin = new System.Windows.Forms.Padding(6);
            this.txtDBUserName.Name = "txtDBUserName";
            this.txtDBUserName.Size = new System.Drawing.Size(514, 44);
            this.txtDBUserName.TabIndex = 6;
            // 
            // rdoSQLServerAuthentication
            // 
            this.rdoSQLServerAuthentication.AutoSize = true;
            this.rdoSQLServerAuthentication.Location = new System.Drawing.Point(642, 87);
            this.rdoSQLServerAuthentication.Margin = new System.Windows.Forms.Padding(6);
            this.rdoSQLServerAuthentication.Name = "rdoSQLServerAuthentication";
            this.rdoSQLServerAuthentication.Size = new System.Drawing.Size(161, 15);
            this.rdoSQLServerAuthentication.TabIndex = 5;
            this.rdoSQLServerAuthentication.Text = "SQL Server Authentication";
            this.rdoSQLServerAuthentication.UseVisualStyleBackColor = true;
            this.rdoSQLServerAuthentication.CheckedChanged += new System.EventHandler(this.rdoSQLServerAuthentication_CheckedChanged);
            // 
            // rdoWindowsAuthentication
            // 
            this.rdoWindowsAuthentication.AutoSize = true;
            this.rdoWindowsAuthentication.Checked = true;
            this.rdoWindowsAuthentication.Location = new System.Drawing.Point(642, 44);
            this.rdoWindowsAuthentication.Margin = new System.Windows.Forms.Padding(6);
            this.rdoWindowsAuthentication.Name = "rdoWindowsAuthentication";
            this.rdoWindowsAuthentication.Size = new System.Drawing.Size(154, 15);
            this.rdoWindowsAuthentication.TabIndex = 4;
            this.rdoWindowsAuthentication.TabStop = true;
            this.rdoWindowsAuthentication.Text = "Windows Authentication";
            this.rdoWindowsAuthentication.UseVisualStyleBackColor = true;
            this.rdoWindowsAuthentication.CheckedChanged += new System.EventHandler(this.rdoWindowsAuthentication_CheckedChanged);
            // 
            // lblDBServerName
            // 
            this.lblDBServerName.AutoSize = true;
            this.lblDBServerName.Location = new System.Drawing.Point(0, 23);
            this.lblDBServerName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDBServerName.Name = "lblDBServerName";
            this.lblDBServerName.Size = new System.Drawing.Size(145, 19);
            this.lblDBServerName.TabIndex = 3;
            this.lblDBServerName.Text = "Database Server Name";
            // 
            // txtDBServerName
            // 
            this.txtDBServerName.Location = new System.Drawing.Point(0, 71);
            this.txtDBServerName.Margin = new System.Windows.Forms.Padding(6);
            this.txtDBServerName.Name = "txtDBServerName";
            this.txtDBServerName.Size = new System.Drawing.Size(540, 44);
            this.txtDBServerName.TabIndex = 2;
            this.txtDBServerName.Text = "(local)";
            this.txtDBServerName.UseStyleColors = true;
            // 
            // tabProgress
            // 
            this.tabProgress.Controls.Add(this.btnVisitSite);
            this.tabProgress.Controls.Add(this.lblProgressStatus);
            this.tabProgress.Controls.Add(this.lblProgress);
            this.tabProgress.Controls.Add(this.progressBar);
            this.tabProgress.HorizontalScrollbarBarColor = true;
            this.tabProgress.HorizontalScrollbarSize = 19;
            this.tabProgress.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tabProgress.Location = new System.Drawing.Point(8, 42);
            this.tabProgress.Margin = new System.Windows.Forms.Padding(6);
            this.tabProgress.Name = "tabProgress";
            this.tabProgress.Size = new System.Drawing.Size(1198, 515);
            this.tabProgress.TabIndex = 2;
            this.tabProgress.Text = "Progress";
            this.tabProgress.VerticalScrollbarBarColor = true;
            this.tabProgress.VerticalScrollbarSize = 20;
            // 
            // btnVisitSite
            // 
            this.btnVisitSite.Highlight = true;
            this.btnVisitSite.Location = new System.Drawing.Point(6, 369);
            this.btnVisitSite.Margin = new System.Windows.Forms.Padding(6);
            this.btnVisitSite.Name = "btnVisitSite";
            this.btnVisitSite.Size = new System.Drawing.Size(1186, 115);
            this.btnVisitSite.Style = MetroFramework.MetroColorStyle.Purple;
            this.btnVisitSite.TabIndex = 5;
            this.btnVisitSite.Text = "&Visit Site";
            this.btnVisitSite.Visible = false;
            this.btnVisitSite.Click += new System.EventHandler(this.btnVisitSite_Click);
            // 
            // lblProgressStatus
            // 
            this.lblProgressStatus.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblProgressStatus.Location = new System.Drawing.Point(0, 119);
            this.lblProgressStatus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblProgressStatus.Name = "lblProgressStatus";
            this.lblProgressStatus.Size = new System.Drawing.Size(1194, 37);
            this.lblProgressStatus.TabIndex = 4;
            this.lblProgressStatus.UseStyleColors = true;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(0, 21);
            this.lblProgress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(60, 19);
            this.lblProgress.TabIndex = 3;
            this.lblProgress.Text = "Progress";
            this.lblProgress.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.HideProgressText = false;
            this.progressBar.Location = new System.Drawing.Point(0, 69);
            this.progressBar.Margin = new System.Windows.Forms.Padding(6);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1194, 44);
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;
            // 
            // tileDNNAwareness
            // 
            this.tileDNNAwareness.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tileDNNAwareness.Location = new System.Drawing.Point(946, 646);
            this.tileDNNAwareness.Margin = new System.Windows.Forms.Padding(6);
            this.tileDNNAwareness.Name = "tileDNNAwareness";
            this.tileDNNAwareness.Size = new System.Drawing.Size(262, 96);
            this.tileDNNAwareness.Style = MetroFramework.MetroColorStyle.Blue;
            this.tileDNNAwareness.TabIndex = 28;
            this.tileDNNAwareness.Text = "@DNNAwareness";
            this.tileDNNAwareness.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.tileDNNAwareness.Click += new System.EventHandler(this.tileDNNAwareness_Click);
            // 
            // tileDNNDocs
            // 
            this.tileDNNDocs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tileDNNDocs.Location = new System.Drawing.Point(352, 748);
            this.tileDNNDocs.Margin = new System.Windows.Forms.Padding(6);
            this.tileDNNDocs.Name = "tileDNNDocs";
            this.tileDNNDocs.Size = new System.Drawing.Size(288, 96);
            this.tileDNNDocs.Style = MetroFramework.MetroColorStyle.Lime;
            this.tileDNNDocs.TabIndex = 29;
            this.tileDNNDocs.Text = "DNN Docs";
            this.tileDNNDocs.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.tileDNNDocs.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.tileDNNDocs.Click += new System.EventHandler(this.tileDNNDocs_Click);
            // 
            // tileMorenvQuickProducts
            // 
            this.tileMorenvQuickProducts.BackColor = System.Drawing.SystemColors.Control;
            this.tileMorenvQuickProducts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tileMorenvQuickProducts.Location = new System.Drawing.Point(352, 644);
            this.tileMorenvQuickProducts.Margin = new System.Windows.Forms.Padding(6);
            this.tileMorenvQuickProducts.Name = "tileMorenvQuickProducts";
            this.tileMorenvQuickProducts.Size = new System.Drawing.Size(582, 96);
            this.tileMorenvQuickProducts.Style = MetroFramework.MetroColorStyle.Yellow;
            this.tileMorenvQuickProducts.TabIndex = 30;
            this.tileMorenvQuickProducts.Text = "nvQuickSite Wiki";
            this.tileMorenvQuickProducts.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.tileMorenvQuickProducts.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.tileMorenvQuickProducts.Click += new System.EventHandler(this.tileMorenvQuickProducts_Click);
            // 
            // lblRemember
            // 
            this.lblRemember.AutoSize = true;
            this.lblRemember.Location = new System.Drawing.Point(180, 865);
            this.lblRemember.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblRemember.Name = "lblRemember";
            this.lblRemember.Size = new System.Drawing.Size(146, 19);
            this.lblRemember.TabIndex = 13;
            this.lblRemember.Text = "Remember Field Values";
            this.lblRemember.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRemember.Visible = false;
            // 
            // tileQuickSettings
            // 
            this.tileQuickSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tileQuickSettings.Location = new System.Drawing.Point(14, 644);
            this.tileQuickSettings.Margin = new System.Windows.Forms.Padding(6);
            this.tileQuickSettings.Name = "tileQuickSettings";
            this.tileQuickSettings.Size = new System.Drawing.Size(324, 200);
            this.tileQuickSettings.Style = MetroFramework.MetroColorStyle.Orange;
            this.tileQuickSettings.TabIndex = 27;
            this.tileQuickSettings.Text = "nvQuickSite Settings";
            this.tileQuickSettings.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tileQuickSettings.TileImage = global::nvQuickSite.Properties.Resources.user_settings;
            this.tileQuickSettings.TileImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tileQuickSettings.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.tileQuickSettings.UseTileImage = true;
            this.tileQuickSettings.Click += new System.EventHandler(this.tileQuickSettings_Click);
            // 
            // tileDNNCommunity
            // 
            this.tileDNNCommunity.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tileDNNCommunity.Location = new System.Drawing.Point(652, 748);
            this.tileDNNCommunity.Margin = new System.Windows.Forms.Padding(6);
            this.tileDNNCommunity.Name = "tileDNNCommunity";
            this.tileDNNCommunity.Size = new System.Drawing.Size(556, 96);
            this.tileDNNCommunity.Style = MetroFramework.MetroColorStyle.Purple;
            this.tileDNNCommunity.TabIndex = 31;
            this.tileDNNCommunity.Text = "DNN Community";
            this.tileDNNCommunity.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.tileDNNCommunity.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.tileDNNCommunity.Click += new System.EventHandler(this.tileDNNCommunity_Click);
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tileDNNCommunity);
            this.Controls.Add(this.lblRemember);
            this.Controls.Add(this.tileMorenvQuickProducts);
            this.Controls.Add(this.tileDNNDocs);
            this.Controls.Add(this.tileDNNAwareness);
            this.Controls.Add(this.toggleSiteInfoRemember);
            this.Controls.Add(this.tileQuickSettings);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Start";
            this.Size = new System.Drawing.Size(1220, 904);
            this.tabSiteInfo.ResumeLayout(false);
            this.tabSiteInfo.PerformLayout();
            this.tabInstallPackage.ResumeLayout(false);
            this.tabInstallPackage.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabDatabaseInfo.ResumeLayout(false);
            this.tabDatabaseInfo.PerformLayout();
            this.tabProgress.ResumeLayout(false);
            this.tabProgress.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroTabPage tabSiteInfo;
        private MetroFramework.Controls.MetroTabPage tabInstallPackage;
        private MetroFramework.Controls.MetroTextBox txtLocalInstallPackage;
        private MetroFramework.Controls.MetroLabel lblLocalInstallPackage;
        private MetroFramework.Controls.MetroLabel lblLatestReleases;
        private MetroFramework.Controls.MetroButton btnViewAllReleases;
        //private MetroFramework.Controls.MetroComboBox cboLatestReleases;
        private MetroFramework.Controls.MetroComboBox cboProductName;
        private MetroFramework.Controls.MetroButton btnGetLatestRelease;
        private MetroFramework.Controls.MetroTabControl tabControl;
        private MetroFramework.Controls.MetroTile tileQuickSettings;
        private MetroFramework.Controls.MetroTile tileDNNAwareness;
        private MetroFramework.Controls.MetroTextBox txtSiteNamePrefix;
        private MetroFramework.Controls.MetroLabel lblSiteName;
        private MetroFramework.Controls.MetroButton btnLocation;
        private MetroFramework.Controls.MetroLabel lblInstallBaseFolder;
        private MetroFramework.Controls.MetroTextBox txtInstallBaseFolder;
        private MetroFramework.Controls.MetroCheckBox chkDeleteSiteIfExists;
        private MetroFramework.Controls.MetroCheckBox chkSiteSpecificAppPool;
        private MetroFramework.Controls.MetroButton btnLocalInstallPackage;
        private MetroFramework.Controls.MetroButton btnSiteInfoNext;
        private MetroFramework.Controls.MetroTile tileDNNDocs;
        private MetroFramework.Controls.MetroTile tileMorenvQuickProducts;
        private MetroFramework.Controls.MetroButton btnInstallPackageNext;
        private MetroFramework.Controls.MetroButton btnSiteInfoBack;
        private MetroFramework.Controls.MetroTabPage tabProgress;
        private MetroFramework.Controls.MetroProgressBar progressBar;
        private MetroFramework.Controls.MetroTabPage tabDatabaseInfo;
        private MetroFramework.Controls.MetroLabel lblDBServerName;
        private MetroFramework.Controls.MetroTextBox txtDBServerName;
        private MetroFramework.Controls.MetroLabel lblDBName;
        private MetroFramework.Controls.MetroTextBox txtDBName;
        private MetroFramework.Controls.MetroTextBox txtDBPassword;
        private MetroFramework.Controls.MetroLabel lblDBPassword;
        private MetroFramework.Controls.MetroLabel lblDBUserName;
        private MetroFramework.Controls.MetroTextBox txtDBUserName;
        private MetroFramework.Controls.MetroRadioButton rdoSQLServerAuthentication;
        private MetroFramework.Controls.MetroRadioButton rdoWindowsAuthentication;
        private MetroFramework.Controls.MetroButton btnDatabaseInfoNext;
        private MetroFramework.Controls.MetroButton btnDatabaseInfoBack;
        private MetroFramework.Controls.MetroLabel lblProgressStatus;
        private MetroFramework.Controls.MetroLabel lblProgress;
        private MetroFramework.Controls.MetroButton btnVisitSite;
        private MetroFramework.Controls.MetroProgressBar progressBarDownload;
        private MetroFramework.Controls.MetroToggle toggleSiteInfoRemember;
        private MetroFramework.Controls.MetroLabel lblRemember;
        private MetroFramework.Controls.MetroTextBox txtSiteNameSuffix;
        private MetroFramework.Controls.MetroLabel lblInstallSubFolder;
        private MetroFramework.Controls.MetroTextBox txtInstallSubFolder;
        private MetroFramework.Controls.MetroComboBox cboProductVersion;
        private MetroFramework.Controls.MetroTile tileDNNCommunity;
    }
}
