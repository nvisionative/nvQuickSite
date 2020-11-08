//Copyright (c) 2016-2020 nvisionative, Inc.

//This file is part of nvQuickSite.

//nvQuickSite is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//nvQuickSite is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with nvQuickSite.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;
using System.Net.Sockets;
using System.Diagnostics;
using MetroFramework.Controls;
using Microsoft.Web.Administration;
using Ionic.Zip;
using Ookii.Dialogs;
using nvQuickSite.Controllers;
using nvQuickSite.Models;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace nvQuickSite
{
    public partial class Start : MetroUserControl
    {

        private IEnumerable<Package> Packages { get; set; }

        protected string currentVersion;
        protected string currentUrl;
        private string installFolder {
            get
            {
                return Path.Combine(txtInstallBaseFolder.Text, txtInstallSubFolder.Text);
            }
        }
        private string siteName
        {
            get
            {
                return txtSiteNamePrefix.Text + txtSiteNameSuffix.Text;

            }
        }
        internal static bool isOnline
        {
            get
            {
                return new WebClient().OpenRead("https://github.com/nvisionative/nvQuickSite").CanRead;
            }
        }

        public Start()
        {
            InitializeComponent();

            InitializeTabs();

            LoadPackages();
            ReadUserSettings();
        }

        private void LoadPackages()
        {
            try
            {
                cboProductName.Items.Clear();
                cboProductVersion.Items.Clear();

                Packages = PackageController.GetPackageList();
                foreach (var did in Packages.OrderByDescending(p => p.version).Select(p => p.did).Distinct())
                {
                    if (did == "dnn-platform-rc")
                    {
                        if (Properties.Settings.Default.ShowReleaseCandidates)
                        {
                            cboProductName.Items.Add(new ComboItem(Packages.First(p => p.did == did).name, did));
                        }
                    } else
                    {
                        cboProductName.Items.Add(new ComboItem(Packages.First(p => p.did == did).name, did));
                    }
                }
                if (cboProductName.Items.Count > 0)
                {
                    cboProductName.SelectedIndex = 0;
                    LoadPackageVersions(((ComboItem)cboProductName.SelectedItem).Value);
                }
            }
            catch (WebException ex)
            {
                lblLatestReleases.Text = "It appears you have no internet, but you can still use Local Install Packages.";
                lblLatestReleases.CustomForeColor = true;
                lblLatestReleases.ForeColor = Color.DarkRed;
                cboProductName.Enabled = false;
                cboProductVersion.Enabled = false;
                btnGetLatestRelease.Enabled = false;
            }
        }

        private void ReadUserSettings()
        {
            if (Properties.Settings.Default.RememberFieldValues)
            {
                txtSiteNamePrefix.Text = Properties.Settings.Default.SiteNamePrefixRecent;
                txtSiteNameSuffix.Text = Properties.Settings.Default.SiteNameSuffixRecent;
                chkSiteSpecificAppPool.Checked = Properties.Settings.Default.AppPoolRecent;
                chkDeleteSiteIfExists.Checked = Properties.Settings.Default.DeleteSiteInIISRecent;
                txtInstallBaseFolder.Text = Properties.Settings.Default.InstallBaseFolderRecent;

                txtDBServerName.Text = Properties.Settings.Default.DatabaseServerNameRecent;
                txtDBName.Text = Properties.Settings.Default.DatabaseNameRecent;
                txtDBUserName.Text = Properties.Settings.Default.DatabaseUserNameRecent;
            }
        }

        private void SaveUserSettings()
        {
            if (Properties.Settings.Default.RememberFieldValues)
            {
                Properties.Settings.Default.SiteNamePrefixRecent = txtSiteNamePrefix.Text;
                Properties.Settings.Default.SiteNameSuffixRecent = txtSiteNameSuffix.Text;
                Properties.Settings.Default.AppPoolRecent = chkSiteSpecificAppPool.Checked;
                Properties.Settings.Default.DeleteSiteInIISRecent = chkDeleteSiteIfExists.Checked;
                Properties.Settings.Default.InstallBaseFolderRecent = txtInstallBaseFolder.Text;

                Properties.Settings.Default.DatabaseServerNameRecent = txtDBServerName.Text;
                Properties.Settings.Default.DatabaseNameRecent = txtDBName.Text;
                Properties.Settings.Default.DatabaseUserNameRecent = txtDBUserName.Text;

                Properties.Settings.Default.Save();
            }
        }

        #region "Tabs"

        private void InitializeTabs()
        {
            tabControl.SelectedIndex = 0;
            tabSiteInfo.Enabled = false;
            tabControl.TabPages.Remove(tabSiteInfo);
            tabDatabaseInfo.Enabled = false;
            tabControl.TabPages.Remove(tabDatabaseInfo);
            tabProgress.Enabled = false;
            tabControl.TabPages.Remove(tabProgress);
        }

        #region "Install Package"
        private void LoadPackageVersions(string packageId)
        {
            cboProductVersion.Items.Clear();
            foreach (var package in Packages.Where(p => p.did == packageId).OrderByDescending(p => p.version))
            {
                cboProductVersion.Items.Add(new ComboItem(package.version, package.version));
            }
            if (cboProductVersion.Items.Count > 0)
            {
                cboProductVersion.SelectedIndex = 0;
            }
        }

        private void cboProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var did = ((ComboItem)cboProductName.SelectedItem).Value;
            LoadPackageVersions(did);
            DisplayPackagePath();
        }

        private void cboProductVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            var did = ((ComboItem)cboProductVersion.SelectedItem).Value;
            DisplayPackagePath();
        }

        private void DisplayPackagePath()
        {
            if (cboProductName.SelectedItem == null || cboProductVersion.SelectedItem == null) { return; }
            Models.Package package;
            var url = "";
            var fileName = "";

            package = Packages.FirstOrDefault(p => p.did == ((ComboItem)cboProductName.SelectedItem).Value && p.version == ((ComboItem)cboProductVersion.SelectedItem).Value);
            fileName = package.url.Split('/').Last();

            var downloadDirectory = GetDownloadDirectory();
            var packageFullpath = downloadDirectory + fileName;

            if (File.Exists(packageFullpath))
                txtLocalInstallPackage.Text = packageFullpath;
            else
                txtLocalInstallPackage.Text = null;
        }

        private void btnGetLatestRelease_Click(object sender, EventArgs e)
        {
            GetOnlineVersion();
        }

        private string GetDownloadDirectory()
        {
            return Directory.GetCurrentDirectory() + @"\Downloads\";
        }

        private void GetOnlineVersion()
        {
            if (cboProductName.SelectedItem == null || cboProductVersion.SelectedItem == null) { return; }
            Models.Package package;
            package = Packages.FirstOrDefault(p => p.did == ((ComboItem)cboProductName.SelectedItem).Value && p.version == ((ComboItem)cboProductVersion.SelectedItem).Value);
            var url = package.url;
            var fileName = package.url.Split('/').Last();

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

            var downloadDirectory = GetDownloadDirectory();
            if (!Directory.Exists(downloadDirectory))
            {
                Directory.CreateDirectory(downloadDirectory);
            }

            var dlContinue = true;
            if (File.Exists(downloadDirectory + fileName))
            {
                DialogResult result = MessageBox.Show("Install Package is already downloaded. Would you like to download it again? This will replace the existing download.",
                    "Download Install Package", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    dlContinue = false;
                }
            }

            if (dlContinue)
            {
                client.DownloadFileAsync(new Uri(url), downloadDirectory + fileName);
                progressBarDownload.BackColor = Color.WhiteSmoke;
                progressBarDownload.Visible = true;
            }
            else
            {
                txtLocalInstallPackage.Text = Directory.GetCurrentDirectory() + "\\Downloads\\" + Path.GetFileName(url);
                Properties.Settings.Default.LocalInstallPackageRecent = downloadDirectory;
                Properties.Settings.Default.Save();
                ValidateInstallPackage();
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            progressBarDownload.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Models.Package package;
            var url = "";
            var fileName = "";

            package = Packages.FirstOrDefault(p => p.did == ((ComboItem)cboProductName.SelectedItem).Value && p.version == ((ComboItem)cboProductVersion.SelectedItem).Value);
            fileName = package.url.Split('/').Last();

            txtLocalInstallPackage.Text = Directory.GetCurrentDirectory() + @"\Downloads\" + fileName;
            Properties.Settings.Default.LocalInstallPackageRecent = Directory.GetCurrentDirectory() + @"\Downloads\";
            Properties.Settings.Default.Save();
            ValidateInstallPackage();
        }

        private void btnViewAllReleases_Click(object sender, EventArgs e)
        {
            Process.Start("https://dotnetnuke.codeplex.com/");
        }

        private void txtLocalInstallPackage_Click(object sender, EventArgs e)
        {
            openFileDiag();
        }

        private void btnLocalInstallPackage_Click(object sender, EventArgs e)
        {
            openFileDiag();
        }

        private void openFileDiag()
        {
            OpenFileDialog fileDiag = new OpenFileDialog();
            fileDiag.Filter = "ZIP Files|*.zip";
            fileDiag.InitialDirectory = Properties.Settings.Default.LocalInstallPackageRecent;
            DialogResult result = fileDiag.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtLocalInstallPackage.Text = fileDiag.FileName;
                Properties.Settings.Default.LocalInstallPackageRecent = Path.GetDirectoryName(fileDiag.FileName);
                Properties.Settings.Default.Save();
            }
        }

        private void btnInstallPackageNext_Click(object sender, EventArgs e)
        {
            if (txtLocalInstallPackage.Text == "")
            {
                if (isOnline)
                {
                    GetOnlineVersion();
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Click 'Yes' to use the Local Install Package. Click 'No' to attempt download of the selected Download Install Package.",
                    "Confirm: Use Local Package?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    if (isOnline)
                    {
                        GetOnlineVersion();
                    }
                }
                else
                {
                    progressBarDownload.Visible = false;
                    ValidateInstallPackage();
                }
            }
        }

        private void ValidateInstallPackage()
        {
            //if (Package.Validate(txtLocalInstallPackage.Text))
            //{
                tabInstallPackage.Enabled = false;
                tabControl.TabPages.Insert(1, tabSiteInfo);
                tabSiteInfo.Enabled = true;
                tabDatabaseInfo.Enabled = false;
                tabProgress.Enabled = false;
                tabControl.SelectedIndex = 1;
            //}
            //else
            //{
            //    MessageBox.Show("You must first Download or select a valid Local Install Package.", "Install Package", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        #endregion

        #region "Site Info"
        //private void txtLocation_Click(object sender, EventArgs e)
        //{
        //    openFolderDiag();
        //}

        private void btnLocation_Click(object sender, EventArgs e)
        {
            openFolderDiag();
        }

        private void openFolderDiag()
        {
            VistaFolderBrowserDialog diag = new VistaFolderBrowserDialog();
            diag.RootFolder = Environment.SpecialFolder.MyComputer;
            diag.SelectedPath = Properties.Settings.Default.InstallBaseFolderRecent;
            DialogResult result = diag.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtInstallBaseFolder.Text = diag.SelectedPath;
                Properties.Settings.Default.InstallBaseFolderRecent = diag.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void btnSiteInfoBack_Click(object sender, EventArgs e)
        {
            tabSiteInfo.Enabled = false;
            tabControl.TabPages.Remove(tabSiteInfo);
            tabInstallPackage.Enabled = true;
            tabControl.SelectedIndex = 0;
        }

        private void txtSiteNamePrefix_TextChanged(object sender, EventArgs e)
        {
            txtInstallSubFolder.Text = txtSiteNamePrefix.Text;
            txtDBName.Text = txtSiteNamePrefix.Text;
        }

        private void btnSiteInfoNext_Click(object sender, EventArgs e)
        {
            bool proceed;

            if (String.IsNullOrWhiteSpace(txtInstallBaseFolder.Text) || String.IsNullOrWhiteSpace(txtInstallSubFolder.Text) || String.IsNullOrWhiteSpace(txtSiteNamePrefix.Text))
            {
                MessageBox.Show("Please make sure you have entered a Site Name and Install Folder.", "Site Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(this.installFolder))
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
                        Directory.CreateDirectory(this.installFolder);
                        proceed = true;
                    }
                    else
                    {
                        proceed = false;
                    }
                }
                else
                {
                    Directory.CreateDirectory(this.installFolder);
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
                if (!FileSystemController.DirectoryEmpty(this.installFolder))
                {
                    var confirmResult = MessageBox.Show("All files and folders at this location will be deleted prior to installation of the new DNN instance. Do you wish to proceed?",
                                                "Confirm Installation",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (confirmResult == DialogResult.No)
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
                tabInstallPackage.Enabled = false;
                tabSiteInfo.Enabled = false;
                tabControl.TabPages.Insert(2, tabDatabaseInfo);
                tabDatabaseInfo.Enabled = true;
                tabProgress.Enabled = false;
                tabControl.SelectedIndex = 2;
                SaveUserSettings();
            }
        }

        #endregion

        #region "Database Info"

        private void rdoWindowsAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            lblDBUserName.Enabled = false;
            txtDBUserName.Enabled = false;
            txtDBUserName.UseStyleColors = true;
            lblDBPassword.Enabled = false;
            txtDBPassword.Enabled = false;
            txtDBPassword.UseStyleColors = true;
        }

        private void rdoSQLServerAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            lblDBUserName.Enabled = true;
            txtDBUserName.Enabled = true;
            txtDBUserName.UseStyleColors = false;
            lblDBPassword.Enabled = true;
            txtDBPassword.Enabled = true;
            txtDBPassword.UseStyleColors = false;
        }

        private void btnDatabaseInfoBack_Click(object sender, EventArgs e)
        {
            tabInstallPackage.Enabled = false;
            tabSiteInfo.Enabled = true;
            tabDatabaseInfo.Enabled = false;
            tabControl.TabPages.Remove(tabDatabaseInfo);
            tabControl.SelectedIndex = 1;
        }

        private void btnDatabaseInfoNext_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtDBServerName.Text) || String.IsNullOrWhiteSpace(txtDBName.Text))
            {
                MessageBox.Show("Please make sure you have entered a Database Server Name and a Database Name.", "Database Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                IISController.CreateSite(
                    this.siteName, 
                    this.installFolder, 
                    chkSiteSpecificAppPool.Checked, 
                    chkDeleteSiteIfExists.Checked);
                
                FileSystemController.UpdateHostsFile(
                    this.siteName);

                FileSystemController.CreateDirectories(
                    this.installFolder, 
                    this.siteName, 
                    chkSiteSpecificAppPool.Checked, 
                    txtDBServerName.Text.Trim(), 
                    txtDBServerName.Text, 
                    rdoWindowsAuthentication.Checked, 
                    txtDBUserName.Text, 
                    txtDBPassword.Text);

                var databaseController = new DatabaseController(
                    txtDBName.Text, 
                    txtDBServerName.Text, 
                    rdoWindowsAuthentication.Checked, 
                    txtDBUserName.Text, 
                    txtDBPassword.Text, 
                    this.installFolder, 
                    chkSiteSpecificAppPool.Checked, 
                    this.siteName);
                databaseController.CreateDatabase();
                databaseController.SetDatabasePermissions();

                tabInstallPackage.Enabled = false;
                tabSiteInfo.Enabled = false;
                tabDatabaseInfo.Enabled = false;
                tabControl.TabPages.Insert(3, tabProgress);
                tabProgress.Enabled = true;
                lblProgress.Visible = true;
                progressBar.Visible = true;
                tabControl.SelectedIndex = 3;

                SaveUserSettings();

                this.ReadAndExtract(txtLocalInstallPackage.Text, Path.Combine(txtInstallBaseFolder.Text, txtInstallSubFolder.Text, "Website"));
                FileSystemController.ModifyConfig(
                    txtDBServerName.Text, 
                    rdoWindowsAuthentication.Checked, 
                    txtDBUserName.Text, 
                    txtDBPassword.Text, 
                    txtDBName.Text, 
                    this.installFolder);

                btnVisitSite.Visible = true;
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
            }
        }

        private int fileCount;
        private long totalSize = 0, total = 0, lastVal = 0, sum = 0;

        private void ReadAndExtract(string openPath, string savePath)
        {
            try
            {
                this.fileCount = 0;
                ZipFile myZip = new ZipFile();
                myZip = ZipFile.Read(openPath);
                foreach (var entry in myZip)
                {
                    this.fileCount++;
                    this.totalSize += entry.UncompressedSize;
                }
                progressBar.Maximum = (Int32)this.totalSize;
                myZip.ExtractProgress += new EventHandler<ExtractProgressEventArgs>(myZip_ExtractProgress);
                myZip.ExtractAll(savePath, ExtractExistingFileAction.OverwriteSilently);
                lblProgressStatus.Text = "Congratulations! Your new site is now ready to visit!";
            }
            catch (Exception ex)
            {
                throw new ReadAndExtractException("There was an error attempting to read and extract the package", ex) { Source = "Read And Extract Package" };
            }
        }

        void myZip_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            System.Windows.Forms.Application.DoEvents();
            if (this.total != e.TotalBytesToTransfer)
            {
                this.sum += this.total - this.lastVal + e.BytesTransferred;
                this.total = e.TotalBytesToTransfer;
                lblProgressStatus.Text = "Copying: " + e.CurrentEntry.FileName;
            }
            else
            {
                this.sum += e.BytesTransferred - this.lastVal;
            }

            this.lastVal = e.BytesTransferred;

            progressBar.Value = (Int32)sum;
        }

        #endregion

        #region "Progress"

        private void btnVisitSite_Click(object sender, EventArgs e)
        {
            Process.Start("http://" + this.siteName);
            Main.ActiveForm.Close();
        }

        #endregion

        #endregion

        #region "Tiles"

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
            var userSettings = new UserSettings();
            var result = userSettings.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.ShowReleaseCandidates = userSettings.ShowReleaseCandidates;
                Properties.Settings.Default.ShareStatistics = userSettings.ShareStatistics;
                Properties.Settings.Default.Save();
                LoadPackages();
            }
        }

        #endregion

    }

    [Serializable]
    internal class ReadAndExtractException : Exception
    {
        public ReadAndExtractException()
        {
        }

        public ReadAndExtractException(string message) : base(message)
        {
        }

        public ReadAndExtractException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ReadAndExtractException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
