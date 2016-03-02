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
            this.tabSiteInfo = new MetroFramework.Controls.MetroTabPage();
            this.btnSiteInfoBack = new MetroFramework.Controls.MetroButton();
            this.btnSiteInfoNext = new MetroFramework.Controls.MetroButton();
            this.btnLocation = new MetroFramework.Controls.MetroButton();
            this.lblLocation = new MetroFramework.Controls.MetroLabel();
            this.txtLocation = new MetroFramework.Controls.MetroTextBox();
            this.chkDeleteSiteIfExists = new MetroFramework.Controls.MetroCheckBox();
            this.chkSiteSpecificAppPool = new MetroFramework.Controls.MetroCheckBox();
            this.lblSiteName = new MetroFramework.Controls.MetroLabel();
            this.txtSiteName = new MetroFramework.Controls.MetroTextBox();
            this.toggleSiteInfoRemember = new MetroFramework.Controls.MetroToggle();
            this.tabInstallPackage = new MetroFramework.Controls.MetroTabPage();
            this.progressBarDownload = new MetroFramework.Controls.MetroProgressBar();
            this.btnInstallPackageNext = new MetroFramework.Controls.MetroButton();
            this.btnLocalInstallPackage = new MetroFramework.Controls.MetroButton();
            this.txtLocalInstallPackage = new MetroFramework.Controls.MetroTextBox();
            this.lblLatestReleases = new MetroFramework.Controls.MetroLabel();
            this.lblLocalInstallPackage = new MetroFramework.Controls.MetroLabel();
            this.cboLatestReleases = new MetroFramework.Controls.MetroComboBox();
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
            this.tileQuickStartGuide = new MetroFramework.Controls.MetroTile();
            this.tileDNNDevSpark = new MetroFramework.Controls.MetroTile();
            this.tileDNNDocumentationCenter = new MetroFramework.Controls.MetroTile();
            this.tileDNNCommunityForums = new MetroFramework.Controls.MetroTile();
            this.lblRemember = new MetroFramework.Controls.MetroLabel();
            this.tabSiteInfo.SuspendLayout();
            this.tabInstallPackage.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabDatabaseInfo.SuspendLayout();
            this.tabProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSiteInfo
            // 
            this.tabSiteInfo.Controls.Add(this.btnSiteInfoBack);
            this.tabSiteInfo.Controls.Add(this.btnSiteInfoNext);
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
            this.tabSiteInfo.Size = new System.Drawing.Size(599, 255);
            this.tabSiteInfo.TabIndex = 1;
            this.tabSiteInfo.Text = "Site Info";
            this.tabSiteInfo.VerticalScrollbarBarColor = true;
            // 
            // btnSiteInfoBack
            // 
            this.btnSiteInfoBack.Location = new System.Drawing.Point(0, 215);
            this.btnSiteInfoBack.Name = "btnSiteInfoBack";
            this.btnSiteInfoBack.Size = new System.Drawing.Size(90, 36);
            this.btnSiteInfoBack.TabIndex = 11;
            this.btnSiteInfoBack.Text = "Back";
            this.btnSiteInfoBack.Click += new System.EventHandler(this.btnSiteInfoBack_Click);
            // 
            // btnSiteInfoNext
            // 
            this.btnSiteInfoNext.Highlight = true;
            this.btnSiteInfoNext.Location = new System.Drawing.Point(507, 216);
            this.btnSiteInfoNext.Name = "btnSiteInfoNext";
            this.btnSiteInfoNext.Size = new System.Drawing.Size(90, 36);
            this.btnSiteInfoNext.TabIndex = 10;
            this.btnSiteInfoNext.Text = "Next";
            this.btnSiteInfoNext.Click += new System.EventHandler(this.btnSiteInfoNext_Click);
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
            this.txtSiteName.Size = new System.Drawing.Size(540, 23);
            this.txtSiteName.TabIndex = 2;
            this.txtSiteName.Text = "MySite.local";
            this.txtSiteName.UseStyleColors = true;
            // 
            // toggleSiteInfoRemember
            // 
            this.toggleSiteInfoRemember.AutoSize = true;
            this.toggleSiteInfoRemember.Checked = true;
            this.toggleSiteInfoRemember.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toggleSiteInfoRemember.Location = new System.Drawing.Point(4, 452);
            this.toggleSiteInfoRemember.Name = "toggleSiteInfoRemember";
            this.toggleSiteInfoRemember.Size = new System.Drawing.Size(80, 17);
            this.toggleSiteInfoRemember.TabIndex = 12;
            this.toggleSiteInfoRemember.Text = "On";
            this.toggleSiteInfoRemember.Theme = MetroFramework.MetroThemeStyle.Light;
            this.toggleSiteInfoRemember.UseVisualStyleBackColor = true;
            this.toggleSiteInfoRemember.Visible = false;
            this.toggleSiteInfoRemember.CheckedChanged += new System.EventHandler(this.toggleSiteInfoRemember_CheckedChanged);
            // 
            // tabInstallPackage
            // 
            this.tabInstallPackage.Controls.Add(this.progressBarDownload);
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
            this.tabInstallPackage.Size = new System.Drawing.Size(599, 255);
            this.tabInstallPackage.TabIndex = 0;
            this.tabInstallPackage.Text = "Install Package Info";
            this.tabInstallPackage.VerticalScrollbarBarColor = true;
            // 
            // progressBarDownload
            // 
            this.progressBarDownload.HideProgressText = false;
            this.progressBarDownload.Location = new System.Drawing.Point(1, 66);
            this.progressBarDownload.Name = "progressBarDownload";
            this.progressBarDownload.Size = new System.Drawing.Size(539, 23);
            this.progressBarDownload.Style = MetroFramework.MetroColorStyle.Blue;
            this.progressBarDownload.TabIndex = 29;
            this.progressBarDownload.Visible = false;
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
            this.txtLocalInstallPackage.Size = new System.Drawing.Size(539, 23);
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
            this.cboLatestReleases.Size = new System.Drawing.Size(539, 29);
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
            this.btnViewAllReleases.Location = new System.Drawing.Point(0, 95);
            this.btnViewAllReleases.Name = "btnViewAllReleases";
            this.btnViewAllReleases.Size = new System.Drawing.Size(540, 37);
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
            this.tabControl.Location = new System.Drawing.Point(3, 14);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 3;
            this.tabControl.Size = new System.Drawing.Size(607, 294);
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
            this.tabDatabaseInfo.Location = new System.Drawing.Point(4, 35);
            this.tabDatabaseInfo.Name = "tabDatabaseInfo";
            this.tabDatabaseInfo.Size = new System.Drawing.Size(599, 255);
            this.tabDatabaseInfo.TabIndex = 3;
            this.tabDatabaseInfo.Text = "Database Info";
            this.tabDatabaseInfo.VerticalScrollbarBarColor = true;
            // 
            // btnDatabaseInfoBack
            // 
            this.btnDatabaseInfoBack.Location = new System.Drawing.Point(0, 216);
            this.btnDatabaseInfoBack.Name = "btnDatabaseInfoBack";
            this.btnDatabaseInfoBack.Size = new System.Drawing.Size(90, 36);
            this.btnDatabaseInfoBack.TabIndex = 13;
            this.btnDatabaseInfoBack.Text = "Back";
            this.btnDatabaseInfoBack.Click += new System.EventHandler(this.btnDatabaseInfoBack_Click);
            // 
            // btnDatabaseInfoNext
            // 
            this.btnDatabaseInfoNext.Highlight = true;
            this.btnDatabaseInfoNext.Location = new System.Drawing.Point(507, 216);
            this.btnDatabaseInfoNext.Name = "btnDatabaseInfoNext";
            this.btnDatabaseInfoNext.Size = new System.Drawing.Size(90, 36);
            this.btnDatabaseInfoNext.TabIndex = 12;
            this.btnDatabaseInfoNext.Text = "Next";
            this.btnDatabaseInfoNext.Click += new System.EventHandler(this.btnDatabaseInfoNext_Click);
            // 
            // lblDBName
            // 
            this.lblDBName.AutoSize = true;
            this.lblDBName.Location = new System.Drawing.Point(0, 110);
            this.lblDBName.Name = "lblDBName";
            this.lblDBName.Size = new System.Drawing.Size(103, 19);
            this.lblDBName.TabIndex = 11;
            this.lblDBName.Text = "Database Name";
            // 
            // txtDBName
            // 
            this.txtDBName.Location = new System.Drawing.Point(0, 134);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(270, 23);
            this.txtDBName.TabIndex = 3;
            this.txtDBName.Text = "MySite";
            this.txtDBName.UseStyleColors = true;
            // 
            // txtDBPassword
            // 
            this.txtDBPassword.Enabled = false;
            this.txtDBPassword.Location = new System.Drawing.Point(340, 134);
            this.txtDBPassword.Name = "txtDBPassword";
            this.txtDBPassword.Size = new System.Drawing.Size(257, 23);
            this.txtDBPassword.TabIndex = 9;
            // 
            // lblDBPassword
            // 
            this.lblDBPassword.AutoSize = true;
            this.lblDBPassword.Enabled = false;
            this.lblDBPassword.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblDBPassword.Location = new System.Drawing.Point(340, 111);
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
            this.lblDBUserName.Location = new System.Drawing.Point(340, 63);
            this.lblDBUserName.Name = "lblDBUserName";
            this.lblDBUserName.Size = new System.Drawing.Size(64, 15);
            this.lblDBUserName.TabIndex = 7;
            this.lblDBUserName.Text = "User Name";
            // 
            // txtDBUserName
            // 
            this.txtDBUserName.Enabled = false;
            this.txtDBUserName.Location = new System.Drawing.Point(340, 85);
            this.txtDBUserName.Name = "txtDBUserName";
            this.txtDBUserName.Size = new System.Drawing.Size(257, 23);
            this.txtDBUserName.TabIndex = 6;
            // 
            // rdoSQLServerAuthentication
            // 
            this.rdoSQLServerAuthentication.AutoSize = true;
            this.rdoSQLServerAuthentication.Location = new System.Drawing.Point(321, 45);
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
            this.rdoWindowsAuthentication.Location = new System.Drawing.Point(321, 23);
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
            this.lblDBServerName.Location = new System.Drawing.Point(0, 12);
            this.lblDBServerName.Name = "lblDBServerName";
            this.lblDBServerName.Size = new System.Drawing.Size(145, 19);
            this.lblDBServerName.TabIndex = 3;
            this.lblDBServerName.Text = "Database Server Name";
            // 
            // txtDBServerName
            // 
            this.txtDBServerName.Location = new System.Drawing.Point(0, 37);
            this.txtDBServerName.Name = "txtDBServerName";
            this.txtDBServerName.Size = new System.Drawing.Size(270, 23);
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
            this.tabProgress.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tabProgress.Location = new System.Drawing.Point(4, 35);
            this.tabProgress.Name = "tabProgress";
            this.tabProgress.Size = new System.Drawing.Size(599, 255);
            this.tabProgress.TabIndex = 2;
            this.tabProgress.Text = "Progress";
            this.tabProgress.VerticalScrollbarBarColor = true;
            // 
            // btnVisitSite
            // 
            this.btnVisitSite.Highlight = true;
            this.btnVisitSite.Location = new System.Drawing.Point(506, 216);
            this.btnVisitSite.Name = "btnVisitSite";
            this.btnVisitSite.Size = new System.Drawing.Size(90, 36);
            this.btnVisitSite.TabIndex = 5;
            this.btnVisitSite.Text = "Visit Site";
            this.btnVisitSite.Visible = false;
            this.btnVisitSite.Click += new System.EventHandler(this.btnVisitSite_Click);
            // 
            // lblProgressStatus
            // 
            this.lblProgressStatus.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblProgressStatus.Location = new System.Drawing.Point(0, 62);
            this.lblProgressStatus.Name = "lblProgressStatus";
            this.lblProgressStatus.Size = new System.Drawing.Size(597, 19);
            this.lblProgressStatus.TabIndex = 4;
            this.lblProgressStatus.UseStyleColors = true;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(0, 11);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(60, 19);
            this.lblProgress.TabIndex = 3;
            this.lblProgress.Text = "Progress";
            this.lblProgress.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.HideProgressText = false;
            this.progressBar.Location = new System.Drawing.Point(0, 36);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(597, 23);
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;
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
            // lblRemember
            // 
            this.lblRemember.AutoSize = true;
            this.lblRemember.Location = new System.Drawing.Point(90, 450);
            this.lblRemember.Name = "lblRemember";
            this.lblRemember.Size = new System.Drawing.Size(146, 19);
            this.lblRemember.TabIndex = 13;
            this.lblRemember.Text = "Remember Field Values";
            this.lblRemember.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRemember.Visible = false;
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblRemember);
            this.Controls.Add(this.tileDNNCommunityForums);
            this.Controls.Add(this.tileDNNDocumentationCenter);
            this.Controls.Add(this.tileDNNDevSpark);
            this.Controls.Add(this.toggleSiteInfoRemember);
            this.Controls.Add(this.tileQuickStartGuide);
            this.Controls.Add(this.tabControl);
            this.Name = "Start";
            this.Size = new System.Drawing.Size(610, 470);
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
        private MetroFramework.Controls.MetroButton btnSiteInfoNext;
        private MetroFramework.Controls.MetroTile tileDNNDocumentationCenter;
        private MetroFramework.Controls.MetroTile tileDNNCommunityForums;
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
    }
}
