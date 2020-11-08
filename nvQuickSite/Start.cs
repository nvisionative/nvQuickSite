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
    using nvQuickSite.Models;
    using Ookii.Dialogs;

    /// <summary>
    /// Implementes the UI tabs and tiles logic.
    /// </summary>
    public partial class Start : MetroUserControl
    {
        private long totalSize;
        private long total;
        private long lastVal;
        private long sum;

        /// <summary>
        /// Initializes a new instance of the <see cref="Start"/> class.
        /// </summary>
        public Start()
        {
            this.InitializeComponent();
            this.InitializeTabs();
            this.LoadPackages();
            this.ReadUserSettings();
        }

        /// <summary>
        /// Gets a value indicating whether the system can access the github.
        /// </summary>
        internal static bool isOnline
        {
            get
            {
                bool canRead;
                using (var client = new WebClient())
                {
                    canRead = client.OpenRead("https://github.com/nvisionative/nvQuickSite").CanRead;
                }

                return canRead;
            }
        }

        private IEnumerable<Package> Packages { get; set; }

        private string InstallFolder => Path.Combine(this.txtInstallBaseFolder.Text, this.txtInstallSubFolder.Text);

        private string SiteName => this.txtSiteNamePrefix.Text + this.txtSiteNameSuffix.Text;

        private static string GetDownloadDirectory()
        {
            return Directory.GetCurrentDirectory() + @"\Downloads\";
        }

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
                this.btnGetLatestRelease.Enabled = false;
            }
        }

        private void ReadUserSettings()
        {
            if (Properties.Settings.Default.RememberFieldValues)
            {
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
            }
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

            Models.Package package;
            var fileName = string.Empty;

            package = this.Packages.FirstOrDefault(p => p.did == ((ComboItem)this.cboProductName.SelectedItem).Value && p.version == ((ComboItem)this.cboProductVersion.SelectedItem).Value);
            fileName = package.url.Split('/').Last();

            var downloadDirectory = GetDownloadDirectory();
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

            Models.Package package;
            package = this.Packages.FirstOrDefault(p => p.did == ((ComboItem)this.cboProductName.SelectedItem).Value && p.version == ((ComboItem)this.cboProductVersion.SelectedItem).Value);
            var url = package.url;
            var fileName = package.url.Split('/').Last();

            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(this.client_DownloadFileCompleted);
                var downloadDirectory = GetDownloadDirectory();
                if (!Directory.Exists(downloadDirectory))
                {
                    Directory.CreateDirectory(downloadDirectory);
                }

                var dlContinue = true;
                if (File.Exists(downloadDirectory + fileName))
                {
                    var dialogTitle = "Get Online Version";
                    var dialogMessage = "Install Package is already downloaded. Would you like to download\nit again? This will replace the existing download.";
                    var dialogIcon = SystemIcons.Warning.ToBitmap();
                    var msgBoxYesNo = new MsgBoxYesNo(dialogTitle, dialogMessage, dialogIcon);
                    var result = msgBoxYesNo.ShowDialog();
                    if (result == DialogResult.No)
                    {
                        dlContinue = false;
                    }

                    msgBoxYesNo.Dispose();
                }

                if (dlContinue)
                {
                    client.DownloadFileAsync(new Uri(url), downloadDirectory + fileName);
                    this.progressBarDownload.BackColor = Color.WhiteSmoke;
                    this.progressBarDownload.Visible = true;
                }
                else
                {
                    this.txtLocalInstallPackage.Text = Directory.GetCurrentDirectory() + "\\Downloads\\" + Path.GetFileName(url);
                    Properties.Settings.Default.LocalInstallPackageRecent = downloadDirectory;
                    Properties.Settings.Default.Save();
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
            Models.Package package;
            var fileName = string.Empty;

            package = this.Packages.FirstOrDefault(p => p.did == ((ComboItem)this.cboProductName.SelectedItem).Value && p.version == ((ComboItem)this.cboProductVersion.SelectedItem).Value);
            fileName = package.url.Split('/').Last();

            this.txtLocalInstallPackage.Text = Directory.GetCurrentDirectory() + @"\Downloads\" + fileName;
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
            if (string.IsNullOrWhiteSpace(this.txtLocalInstallPackage.Text))
            {
                this.GetOnlineVersion();
            }
            else
            {
                var dialogTitle = "Confirm: Use Local Package?";
                var dialogMessage = "Click 'Yes' to use the Local Install Package. Click 'No' to attempt\ndownload of the selected Download Install Package.";
                var dialogIcon = SystemIcons.Question.ToBitmap();
                var msgBoxYesNo = new MsgBoxYesNo(dialogTitle, dialogMessage, dialogIcon);
                var result = msgBoxYesNo.ShowDialog();
                if (result == DialogResult.No)
                {
                    if (isOnline)
                    {
                        this.GetOnlineVersion();
                    }
                }
                else
                {
                    this.progressBarDownload.Visible = false;
                    this.ValidateInstallPackage();
                }

                msgBoxYesNo.Dispose();
            }
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
                MessageBox.Show("Please make sure you have entered a Site Name and Install Folder.", "Site Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(this.InstallFolder))
            {
                var dialogMessage = "The entered location does not exist. Do you wish to create it?";
                var dialogIcon = SystemIcons.Warning.ToBitmap();
                var doNotWarnAgain = Properties.Settings.Default.LocationDoNotWarnAgain;
                var msgBoxYesNoIgnore = new MsgBoxYesNoIgnore(doNotWarnAgain, dialogMessage, dialogIcon);
                if (!doNotWarnAgain)
                {
                    var result = msgBoxYesNoIgnore.ShowDialog();
                    if (result == DialogResult.Yes)
                    {
                        Directory.CreateDirectory(this.InstallFolder);
                        proceed = true;
                    }
                    else
                    {
                        proceed = false;
                    }
                }
                else
                {
                    Directory.CreateDirectory(this.InstallFolder);
                    proceed = true;
                }

                Properties.Settings.Default.LocationDoNotWarnAgain = msgBoxYesNoIgnore.DoNotWarnAgain;
                Properties.Settings.Default.Save();
            }
            else
            {
                proceed = true;
            }

            if (proceed)
            {
                if (!FileSystemController.DirectoryEmpty(this.InstallFolder))
                {
                    var dialogTitle = "Confirm Installation";
                    var dialogMessage = "All files and folders at this location will be deleted prior to installation of\nthe new DNN instance. Do you wish to proceed?";
                    var dialogIcon = SystemIcons.Information.ToBitmap();
                    var msgBoxYesNo = new MsgBoxYesNo(dialogTitle, dialogMessage, dialogIcon);
                    var result = msgBoxYesNo.ShowDialog();
                    if (result == DialogResult.No)
                    {
                        proceed = false;
                    }
                    else
                    {
                        proceed = true;
                    }

                    msgBoxYesNo.Dispose();
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
                MessageBox.Show("Please make sure you have entered a Database Server Name and a Database Name.", "Database Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                IISController.CreateSite(
                    this.SiteName,
                    this.InstallFolder,
                    this.chkSiteSpecificAppPool.Checked,
                    this.chkDeleteSiteIfExists.Checked);

                FileSystemController.UpdateHostsFile(
                    this.SiteName);

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
            }
            catch (SiteExistsException ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (IISControllerException ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FileSystemControllerException ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DatabaseControllerException ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Info Next", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void ReadAndExtract(string openPath, string savePath)
        {
            try
            {
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
                throw new ReadAndExtractException("There was an error attempting to read and extract the package", ex) { Source = "Read And Extract Package" };
            }
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
            Process.Start("http://" + this.SiteName);
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
            using (var userSettings = new UserSettings())
            {
                var result = userSettings.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Properties.Settings.Default.ShowReleaseCandidates = userSettings.ShowReleaseCandidates;
                    Properties.Settings.Default.ShareStatistics = userSettings.ShareStatistics;
                    Properties.Settings.Default.Save();
                    this.LoadPackages();
                }
            }
        }
    }
}
