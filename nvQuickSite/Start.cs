// Copyright (c) 2016-2020 nvisionative, Inc.
//
// This file is part of nvQuickSite.
//
// nvQuickSite is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// nvQuickSite is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with nvQuickSite.  If not, see <http://www.gnu.org/licenses/>.

namespace nvQuickSite
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Windows.Forms;

    using Ionic.Zip;
    using MetroFramework.Controls;
    using nvQuickSite.Controllers;
    using nvQuickSite.Controllers.Exceptions;
    using nvQuickSite.Controls.Settings;
    using nvQuickSite.Controls.Sites;
    using nvQuickSite.Exceptions;
    using nvQuickSite.Models;
    using Ookii.Dialogs;
    using Serilog;
    using Serilog.Core;

    /// <summary>
    /// Implementes the UI tabs and tiles logic.
    /// </summary>
    public partial class Start : MetroUserControl
    {
        private readonly LoggingLevelSwitch loggingLevelSwitch;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "No performance concerns")]
        private readonly Timer downloadProgressLogTimer;
        private long totalSize;
        private long total;
        private long lastVal;
        private long sum;

        /// <summary>
        /// Initializes a new instance of the <see cref="Start"/> class.
        /// </summary>
        /// <param name="loggingLevelSwitch">An object that can be used to dynamically change the logging level at runtime.</param>
        public Start(Serilog.Core.LoggingLevelSwitch loggingLevelSwitch)
        {
            this.InitializeComponent();
            this.InitializeTabs();
            this.LoadPackages();
            this.ReadUserSettings();
            this.loggingLevelSwitch = loggingLevelSwitch;
            this.downloadProgressLogTimer = new Timer() { Interval = 2000 };
            this.downloadProgressLogTimer.Tick += this.DownloadProgressLogTimer_Tick;
        }

        private IEnumerable<Package> Packages { get; set; }

        private string InstallFolder => Path.Combine(this.txtInstallBaseFolder.Text, this.txtInstallSubFolder.Text);

        private string SiteName => this.txtSiteNamePrefix.Text + this.txtSiteNameSuffix.Text;

        private void LoadPackages()
        {
            try
            {
                this.cboProductName.Items.Clear();
                this.cboProductVersion.Items.Clear();

                this.Packages = PackageController.GetPackageList();
                foreach (var did in this.Packages.OrderByDescending(p => p.version).Select(p => p.did).Distinct())
                {
                    if (did == "dnn-platform-rc")
                    {
                        if (Properties.Settings.Default.ShowReleaseCandidates)
                        {
                            this.cboProductName.Items.Add(new ComboItem(this.Packages.First(p => p.did == did).name, did));
                        }
                    }
                    else
                    {
                        this.cboProductName.Items.Add(new ComboItem(this.Packages.First(p => p.did == did).name, did));
                    }
                }

                if (this.cboProductName.Items.Count > 0)
                {
                    this.cboProductName.SelectedIndex = 0;
                    this.LoadPackageVersions(((ComboItem)this.cboProductName.SelectedItem).Value);
                }
            }
            catch (WebException)
            {
                this.lblLatestReleases.Text = "It appears you have no internet, but you can still use Local Install Packages.";
                this.lblLatestReleases.CustomForeColor = true;
                this.lblLatestReleases.ForeColor = Color.DarkRed;
                this.cboProductName.Enabled = false;
                this.cboProductVersion.Enabled = false;
            }
        }

        private void ReadUserSettings()
        {
            Log.Logger.Information("Reading current user settings");
            Log.Logger.Debug("Current user settings: {@userSettings}", Properties.Settings.Default);
            if (Properties.Settings.Default.RememberFieldValues)
            {
                var enableLocalInstallPackage = Properties.Settings.Default.EnableLocalPackageInstall;
                this.lblLocalInstallPackage.Visible = enableLocalInstallPackage;
                this.txtLocalInstallPackage.Visible = enableLocalInstallPackage;
                this.btnLocalInstallPackage.Visible = enableLocalInstallPackage;

                this.txtSiteNamePrefix.Text = Properties.Settings.Default.SiteNamePrefixRecent;
                this.txtSiteNameSuffix.Text = Properties.Settings.Default.SiteNameSuffixRecent;
                this.chkSiteSpecificAppPool.Checked = Properties.Settings.Default.AppPoolRecent;
                this.chkDeleteSiteIfExists.Checked = Properties.Settings.Default.DeleteSiteInIISRecent;
                this.txtInstallBaseFolder.Text = Properties.Settings.Default.InstallBaseFolderRecent;

                this.txtDBServerName.Text = Properties.Settings.Default.DatabaseServerNameRecent;
                this.txtDBName.Text = Properties.Settings.Default.DatabaseNameRecent;
                this.txtDBUserName.Text = Properties.Settings.Default.DatabaseUserNameRecent;
            }
        }

        private void SaveUserSettings()
        {
            if (Properties.Settings.Default.RememberFieldValues)
            {
                Properties.Settings.Default.SiteNamePrefixRecent = this.txtSiteNamePrefix.Text;
                Properties.Settings.Default.SiteNameSuffixRecent = this.txtSiteNameSuffix.Text;
                Properties.Settings.Default.AppPoolRecent = this.chkSiteSpecificAppPool.Checked;
                Properties.Settings.Default.DeleteSiteInIISRecent = this.chkDeleteSiteIfExists.Checked;
                Properties.Settings.Default.InstallBaseFolderRecent = this.txtInstallBaseFolder.Text;

                Properties.Settings.Default.DatabaseServerNameRecent = this.txtDBServerName.Text;
                Properties.Settings.Default.DatabaseNameRecent = this.txtDBName.Text;
                Properties.Settings.Default.DatabaseUserNameRecent = this.txtDBUserName.Text;

                Properties.Settings.Default.Save();
                Log.Logger.Information("Saved user settings");
                Log.Logger.Debug("Saved user settings: {@userSettings}", Properties.Settings.Default);
                return;
            }

            Log.Logger.Information("User settings changed");
            Log.Logger.Debug("User settings changed: {@userSettings}", Properties.Settings.Default);
        }

        private void InitializeTabs()
        {
            this.tabControl.SelectedIndex = 0;
            this.tabSiteInfo.Enabled = false;
            this.tabControl.TabPages.Remove(this.tabSiteInfo);
            this.tabDatabaseInfo.Enabled = false;
            this.tabControl.TabPages.Remove(this.tabDatabaseInfo);
            this.tabProgress.Enabled = false;
            this.tabControl.TabPages.Remove(this.tabProgress);
        }

        private void LoadPackageVersions(string packageId)
        {
            this.cboProductVersion.Items.Clear();
            foreach (var package in this.Packages.Where(p => p.did == packageId).OrderByDescending(p => p.version))
            {
                this.cboProductVersion.Items.Add(new ComboItem(package.version, package.version));
            }

            if (this.cboProductVersion.Items.Count > 0)
            {
                this.cboProductVersion.SelectedIndex = 0;
            }
        }

        private void cboProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var did = ((ComboItem)this.cboProductName.SelectedItem).Value;
            this.LoadPackageVersions(did);
            this.DisplayPackagePath();
        }

        private void cboProductVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            var did = ((ComboItem)this.cboProductVersion.SelectedItem).Value;
            this.DisplayPackagePath();
        }

        private void DisplayPackagePath()
        {
            if (this.cboProductName.SelectedItem == null || this.cboProductVersion.SelectedItem == null)
            {
                return;
            }

            Package package;
            var fileName = string.Empty;

            package = this.Packages.FirstOrDefault(p => p.did == ((ComboItem)this.cboProductName.SelectedItem).Value && p.version == ((ComboItem)this.cboProductVersion.SelectedItem).Value);
            fileName = package.url.Split('/').Last();

            var downloadDirectory = FileSystemController.GetDownloadDirectory();
            var packageFullpath = downloadDirectory + fileName;

            if (File.Exists(packageFullpath))
            {
                this.txtLocalInstallPackage.Text = packageFullpath;
            }
            else
            {
                this.txtLocalInstallPackage.Text = null;
            }
        }

        private void btnGetLatestRelease_Click(object sender, EventArgs e)
        {
            this.GetOnlineVersion();
        }

        private void GetOnlineVersion()
        {
            if (this.cboProductName.SelectedItem == null || this.cboProductVersion.SelectedItem == null)
            {
                return;
            }

            Package package;
            package = this.Packages.FirstOrDefault(p => p.did == ((ComboItem)this.cboProductName.SelectedItem).Value && p.version == ((ComboItem)this.cboProductVersion.SelectedItem).Value);
            var url = package.url;
            var fileName = package.url.Split('/').Last();

            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(this.client_DownloadFileCompleted);
                var downloadDirectory = FileSystemController.GetDownloadDirectory();
                if (!Directory.Exists(downloadDirectory))
                {
                    Directory.CreateDirectory(downloadDirectory);
                }

                var dlContinue = true;
                if (File.Exists(downloadDirectory + fileName))
                {
                    var result = DialogController.ShowMessage(
                        "Get Online Version",
                        "Install Package is already downloaded. Would you like to download\nit again? This will replace the existing download.",
                        SystemIcons.Warning,
                        DialogController.DialogButtons.YesNo);

                    if (result == DialogResult.No)
                    {
                        dlContinue = false;
                    }
                }

                if (dlContinue)
                {
                    Log.Logger.Information("Downloading package from {url}", url);
                    this.downloadProgressLogTimer.Start();
                    client.DownloadFileAsync(new Uri(url), downloadDirectory + fileName);
                    this.progressBarDownload.BackColor = Color.WhiteSmoke;
                    this.progressBarDownload.Visible = true;
                }
                else
                {
                    this.txtLocalInstallPackage.Text = Directory.GetCurrentDirectory() + "\\Downloads\\" + Path.GetFileName(url);
                    Properties.Settings.Default.LocalInstallPackageRecent = downloadDirectory;
                    Properties.Settings.Default.Save();
                    Log.Logger.Information("Using local install package {filePath}", this.txtLocalInstallPackage.Text);
                    this.ValidateInstallPackage();
                }
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = Convert.ToDouble(e.BytesReceived);
            double totalBytes = Convert.ToDouble(e.TotalBytesToReceive);
            double percentage = bytesIn / totalBytes * 100;
            this.progressBarDownload.Value = Convert.ToInt32(percentage);
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Package package;
            var fileName = string.Empty;

            package = this.Packages.FirstOrDefault(p => p.did == ((ComboItem)this.cboProductName.SelectedItem).Value && p.version == ((ComboItem)this.cboProductVersion.SelectedItem).Value);
            fileName = package.url.Split('/').Last();
            this.downloadProgressLogTimer.Stop();
            Log.Logger.Information("Completed download of {fileName}", fileName);

            this.txtLocalInstallPackage.Text = Directory.GetCurrentDirectory() + @"\Downloads\" + fileName;
            Log.Logger.Information("Saved {filePath}", this.txtLocalInstallPackage.Text);
            Properties.Settings.Default.LocalInstallPackageRecent = Directory.GetCurrentDirectory() + @"\Downloads\";
            Properties.Settings.Default.Save();
            this.ValidateInstallPackage();
        }

        private void btnViewAllReleases_Click(object sender, EventArgs e)
        {
            Process.Start("https://dotnetnuke.codeplex.com/");
        }

        private void txtLocalInstallPackage_Click(object sender, EventArgs e)
        {
            this.openFileDiag();
        }

        private void btnLocalInstallPackage_Click(object sender, EventArgs e)
        {
            this.openFileDiag();
        }

        private void openFileDiag()
        {
            using (OpenFileDialog fileDiag = new OpenFileDialog())
            {
                fileDiag.Filter = "ZIP Files|*.zip";
                fileDiag.InitialDirectory = Properties.Settings.Default.LocalInstallPackageRecent;
                DialogResult result = fileDiag.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.txtLocalInstallPackage.Text = fileDiag.FileName;
                    Properties.Settings.Default.LocalInstallPackageRecent = Path.GetDirectoryName(fileDiag.FileName);
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void btnInstallPackageNext_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(this.txtLocalInstallPackage.Text) || !Properties.Settings.Default.EnableLocalPackageInstall) && PackageController.IsOnline)
            {
                this.GetOnlineVersion();
                return;
            }

            var result = DialogController.ShowMessage(
                "Confirm: Use Local Package?",
                "Click 'Yes' to use the Local Install Package. Click 'No' to attempt\ndownload of the selected Download Install Package.",
                SystemIcons.Question,
                DialogController.DialogButtons.YesNo);

            if (result == DialogResult.No && PackageController.IsOnline)
            {
                this.GetOnlineVersion();
                return;
            }

            this.progressBarDownload.Visible = false;
            this.ValidateInstallPackage();
        }

        private void ValidateInstallPackage()
        {
            this.tabInstallPackage.Enabled = false;
            this.tabControl.TabPages.Insert(1, this.tabSiteInfo);
            this.tabSiteInfo.Enabled = true;
            this.tabDatabaseInfo.Enabled = false;
            this.tabProgress.Enabled = false;
            this.tabControl.SelectedIndex = 1;
        }

        private void btnLocation_Click(object sender, EventArgs e)
        {
            this.openFolderDiag();
        }

        private void openFolderDiag()
        {
            using (VistaFolderBrowserDialog diag = new VistaFolderBrowserDialog())
            {
                diag.RootFolder = Environment.SpecialFolder.MyComputer;
                diag.SelectedPath = Properties.Settings.Default.InstallBaseFolderRecent;
                DialogResult result = diag.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.txtInstallBaseFolder.Text = diag.SelectedPath;
                    Properties.Settings.Default.InstallBaseFolderRecent = diag.SelectedPath;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void btnSiteInfoBack_Click(object sender, EventArgs e)
        {
            this.tabSiteInfo.Enabled = false;
            this.tabControl.TabPages.Remove(this.tabSiteInfo);
            this.tabInstallPackage.Enabled = true;
            this.tabControl.SelectedIndex = 0;
        }

        private void txtSiteNamePrefix_TextChanged(object sender, EventArgs e)
        {
            this.txtInstallSubFolder.Text = this.txtSiteNamePrefix.Text;
            this.txtDBName.Text = this.txtSiteNamePrefix.Text;
        }

        private void btnSiteInfoNext_Click(object sender, EventArgs e)
        {
            bool proceed;

            if (string.IsNullOrWhiteSpace(this.txtInstallBaseFolder.Text) || string.IsNullOrWhiteSpace(this.txtInstallSubFolder.Text) || string.IsNullOrWhiteSpace(this.txtSiteNamePrefix.Text))
            {
                DialogController.ShowMessage(
                    "Site Info",
                    "Please make sure you have entered a Site Name and Install Folder",
                    SystemIcons.Warning,
                    DialogController.DialogButtons.OK);

                return;
            }

            if (!Directory.Exists(this.InstallFolder))
            {
                var doNotWarnAgain = Properties.Settings.Default.LocationDoNotWarnAgain;

                if (!doNotWarnAgain)
                {
                    var result = DialogController.ShowMessage(
                        "Site Info",
                        "The entered location does not exist. Do you wish to create it?",
                        SystemIcons.Warning,
                        DialogController.DialogButtons.YesNoIgnore,
                        Properties.Settings.Default.LocationDoNotWarnAgain);

                    if (result == DialogResult.Yes)
                    {
                        Directory.CreateDirectory(this.InstallFolder);
                        proceed = true;
                    }
                    else
                    {
                        proceed = false;
                    }

                    Properties.Settings.Default.LocationDoNotWarnAgain = DialogController.DoNotWarnAgain;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Directory.CreateDirectory(this.InstallFolder);
                    proceed = true;
                }
            }
            else
            {
                proceed = true;
            }

            if (proceed)
            {
                if (!FileSystemController.DirectoryEmpty(this.InstallFolder))
                {
                    var result = DialogController.ShowMessage(
                        "Confirm Installation",
                        "All files and folders at this location will be deleted prior to installation of\nthe new DNN instance. Do you wish to proceed?",
                        SystemIcons.Information,
                        DialogController.DialogButtons.YesNo);

                    if (result == DialogResult.No)
                    {
                        proceed = false;
                    }
                    else
                    {
                        proceed = true;
                    }
                }
                else
                {
                    proceed = true;
                }
            }

            if (proceed)
            {
                this.tabInstallPackage.Enabled = false;
                this.tabSiteInfo.Enabled = false;
                this.tabControl.TabPages.Insert(2, this.tabDatabaseInfo);
                this.tabDatabaseInfo.Enabled = true;
                this.tabProgress.Enabled = false;
                this.tabControl.SelectedIndex = 2;
                this.SaveUserSettings();
            }
        }

        private void rdoWindowsAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            this.lblDBUserName.Enabled = false;
            this.txtDBUserName.Enabled = false;
            this.txtDBUserName.UseStyleColors = true;
            this.lblDBPassword.Enabled = false;
            this.txtDBPassword.Enabled = false;
            this.txtDBPassword.UseStyleColors = true;
        }

        private void rdoSQLServerAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            this.lblDBUserName.Enabled = true;
            this.txtDBUserName.Enabled = true;
            this.txtDBUserName.UseStyleColors = false;
            this.lblDBPassword.Enabled = true;
            this.txtDBPassword.Enabled = true;
            this.txtDBPassword.UseStyleColors = false;
        }

        private void btnDatabaseInfoBack_Click(object sender, EventArgs e)
        {
            this.tabInstallPackage.Enabled = false;
            this.tabSiteInfo.Enabled = true;
            this.tabDatabaseInfo.Enabled = false;
            this.tabControl.TabPages.Remove(this.tabDatabaseInfo);
            this.tabControl.SelectedIndex = 1;
        }

        private void btnDatabaseInfoNext_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtDBServerName.Text) || string.IsNullOrWhiteSpace(this.txtDBName.Text))
            {
                DialogController.ShowMessage(
                    "Database Info",
                    "Please make sure you have entered a Database Server Name and\na Database Name.",
                    SystemIcons.Warning,
                    DialogController.DialogButtons.OK);

                return;
            }

            try
            {
                IISController.CreateSite(
                    this.SiteName,
                    this.InstallFolder,
                    this.chkSiteSpecificAppPool.Checked,
                    this.chkDeleteSiteIfExists.Checked);

                FileSystemController.UpdateHostsFile(this.SiteName);

                FileSystemController.CreateDirectories(
                    this.InstallFolder,
                    this.SiteName,
                    this.chkSiteSpecificAppPool.Checked,
                    this.txtDBServerName.Text.Trim(),
                    this.txtDBServerName.Text,
                    this.rdoWindowsAuthentication.Checked,
                    this.txtDBUserName.Text,
                    this.txtDBPassword.Text);

                var databaseController = new DatabaseController(
                    this.txtDBName.Text,
                    this.txtDBServerName.Text,
                    this.rdoWindowsAuthentication.Checked,
                    this.txtDBUserName.Text,
                    this.txtDBPassword.Text,
                    this.InstallFolder,
                    this.chkSiteSpecificAppPool.Checked,
                    this.SiteName);
                databaseController.CreateDatabase();
                databaseController.SetDatabasePermissions();

                this.tabInstallPackage.Enabled = false;
                this.tabSiteInfo.Enabled = false;
                this.tabDatabaseInfo.Enabled = false;
                this.tabControl.TabPages.Insert(3, this.tabProgress);
                this.tabProgress.Enabled = true;
                this.lblProgress.Visible = true;
                this.progressBar.Visible = true;
                this.tabControl.SelectedIndex = 3;

                this.SaveUserSettings();

                this.ReadAndExtract(this.txtLocalInstallPackage.Text, Path.Combine(this.txtInstallBaseFolder.Text, this.txtInstallSubFolder.Text, "Website"));
                FileSystemController.ModifyConfig(
                    this.txtDBServerName.Text,
                    this.rdoWindowsAuthentication.Checked,
                    this.txtDBUserName.Text,
                    this.txtDBPassword.Text,
                    this.txtDBName.Text,
                    this.InstallFolder);

                this.btnVisitSite.Visible = true;
                Log.Logger.Information("Site {siteName} ready to visit", this.SiteName);
            }
            catch (SiteExistsException ex)
            {
                DialogController.ShowMessage(ex.Source, ex.Message, SystemIcons.Warning, DialogController.DialogButtons.OK);
            }
            catch (IISControllerException ex)
            {
                DialogController.ShowMessage(ex.Source, ex.Message, SystemIcons.Error, DialogController.DialogButtons.OK);
            }
            catch (FileSystemControllerException ex)
            {
                DialogController.ShowMessage(ex.Source, ex.Message, SystemIcons.Error, DialogController.DialogButtons.OK);
            }
            catch (DatabaseControllerException ex)
            {
                DialogController.ShowMessage(ex.Source, ex.Message, SystemIcons.Error, DialogController.DialogButtons.OK);
            }
            catch (Exception ex)
            {
                DialogController.ShowMessage("Database Info Next", ex.Message, SystemIcons.Error, DialogController.DialogButtons.OK);
                throw;
            }
        }

        private void ReadAndExtract(string openPath, string savePath)
        {
            try
            {
                Log.Logger.Information("Extracting package");
                var myZip = ZipFile.Read(openPath);
                foreach (var entry in myZip)
                {
                    this.totalSize += entry.UncompressedSize;
                }

                this.progressBar.Maximum = (int)this.totalSize;
                myZip.ExtractProgress += new EventHandler<ExtractProgressEventArgs>(this.myZip_ExtractProgress);
                myZip.ExtractAll(savePath, ExtractExistingFileAction.OverwriteSilently);
                this.lblProgressStatus.Text = "Congratulations! Your new site is now ready to visit!";
            }
            catch (Exception ex)
            {
                var message = "Error attempting to read and extract the package";
                Log.Error(ex, message);
                throw new ReadAndExtractException(message, ex) { Source = "Read And Extract Package" };
            }

            Log.Logger.Information("Extracted package from {openPath} to {savePath}", openPath, savePath);
        }

        private void myZip_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            System.Windows.Forms.Application.DoEvents();
            if (this.total != e.TotalBytesToTransfer)
            {
                this.sum += this.total - this.lastVal + e.BytesTransferred;
                this.total = e.TotalBytesToTransfer;
                this.lblProgressStatus.Text = "Copying: " + e.CurrentEntry.FileName;
            }
            else
            {
                this.sum += e.BytesTransferred - this.lastVal;
            }

            this.lastVal = e.BytesTransferred;

            this.progressBar.Value = (int)this.sum;
        }

        private void btnVisitSite_Click(object sender, EventArgs e)
        {
            var url = "http://" + this.SiteName;
            Log.Logger.Information("Visiting {url}", url);
            Process.Start(url);
            Log.Logger.Information("Closing application");
            Main.ActiveForm.Close();
        }

        private void toggleSiteInfoRemember_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RememberFieldValues = !Properties.Settings.Default.RememberFieldValues;
        }

        private void tileMorenvQuickProducts_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/nvisionative/nvQuickSite/wiki");
        }

        private void tileDNNCommunity_Click(object sender, EventArgs e)
        {
            Process.Start("https://dnncommunity.org");
        }

        private void tileDNNDocs_Click(object sender, EventArgs e)
        {
            Process.Start("https://dnndocs.com");
        }

        private void tileDNNAwareness_Click(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/DNNAwareness");
        }

        private void tileQuickSettings_Click(object sender, EventArgs e)
        {
            using (var userSettings = new UserSettings(this.loggingLevelSwitch))
            {
                var result = userSettings.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Properties.Settings.Default.ShowReleaseCandidates = userSettings.ShowReleaseCandidates;
                    Properties.Settings.Default.ShareStatistics = userSettings.ShareStatistics;
                    Properties.Settings.Default.EnableLocalPackageInstall = userSettings.EnableLocalPackageInstall;
                    Properties.Settings.Default.Save();
                    this.ReadUserSettings();
                    this.LoadPackages();
                }
            }
        }

        private void btnViewExistingSites_Click(object sender, EventArgs e)
        {
            using (var viewExistingSites = new ViewExistingSites())
            {
                viewExistingSites.ShowDialog();
            }
        }

        private void DownloadProgressLogTimer_Tick(object sender, EventArgs e)
        {
            Log.Logger.Information("Download progress {progress}%", this.progressBarDownload.Value);
        }
    }
}
