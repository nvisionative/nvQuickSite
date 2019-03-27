//Copyright (c) 2016 nvisionative, Inc.

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
//along with nvQuickSite.  If not, see<http://www.gnu.org/licenses/>.

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
using Octokit;
using Ookii.Dialogs;

namespace nvQuickSite
{
    public partial class Start : MetroUserControl
    {
        public Start()
        {
            InitializeComponent();

            tabControl.SelectedIndex = 0;
            tabSiteInfo.Enabled = false;
            tabControl.TabPages.Remove(tabSiteInfo);
            tabDatabaseInfo.Enabled = false;
            tabControl.TabPages.Remove(tabDatabaseInfo);
            tabProgress.Enabled = false;
            tabControl.TabPages.Remove(tabProgress);

            //FeedParser parser = new FeedParser();
            //var releases = parser.Parse("http://dotnetnuke.codeplex.com/project/feeds/rss?ProjectRSSFeed=codeplex%3a%2f%2frelease%2fdotnetnuke", FeedType.RSS);

            var url = "http://www.nvquicksite.com/downloads/";
            WebClient client = new WebClient();
            try
            {
                string result = client.DownloadString(url + "PackageManifest.xml");

                XDocument doc = XDocument.Parse(result);

                
                AddLatestReleaseCandidate();
                var packages = from x in doc.Descendants("DNNPackage")
                               select new
                               {
                                   Name = x.Descendants("Name").First().Value,
                                   File = x.Descendants("File").First().Value
                               };

                foreach (var package in packages)
                    cboLatestReleases.Items.Add(new ComboItem(url + package.File, package.Name));

                

                //foreach (var package in packages)
                //{
                //    cboLatestReleases.Items.Add(new ComboItem(release.Link, release.Title));
                //}
                cboLatestReleases.SelectedIndex = 0;
                cboLatestReleases.SelectedIndexChanged += cboLatestReleases_SelectedIndexChanged;

            }
            catch (Exception ex)
            {
                lblLatestReleases.Text = "INTERNET CURRENTLY UNAVAILABLE: Use Local Install Package Instead";
                lblLatestReleases.CustomForeColor = true;
                lblLatestReleases.ForeColor = Color.DarkRed;
                cboLatestReleases.Enabled = false;
                btnGetLatestRelease.Enabled = false;
            }

            if (Properties.Settings.Default.RememberFieldValues)
            {
                txtSiteNamePrefix.Text = Properties.Settings.Default.SiteNamePrefixRecent;
                txtSiteNameSuffix.Text = Properties.Settings.Default.SiteNameSuffixRecent;
                chkSiteSpecificAppPool.Checked = Properties.Settings.Default.AppPoolRecent;
                chkDeleteSiteIfExists.Checked = Properties.Settings.Default.DeleteSiteInIISRecent;
                txtInstallBaseFolder.Text = Properties.Settings.Default.InstallBaseFolderRecent;

                txtDBServerName.Text = Properties.Settings.Default.DatabaseServerNameRecent;
                txtDBName.Text = Properties.Settings.Default.DatabaseNameRecent;
            }
        }


        private void AddLatestReleaseCandidate()
        {
            Octokit.Release release = null;


            try
            {
                var client = new GitHubClient(new ProductHeaderValue("nvQuickSite"));
                var releases = client.Repository.Release.GetAll("dnnsoftware", "Dnn.Platform").Result;
                if (releases.Count > 0)
                {
                    release = releases[0];
                    if (release.Name.IndexOf("rc", StringComparison.OrdinalIgnoreCase) >= 0)  //is it a release candidate?
                    {
                       var asset = release.Assets.Where(a => a.BrowserDownloadUrl.IndexOf("install", StringComparison.OrdinalIgnoreCase) > -1).FirstOrDefault();
                       if (asset != null)
                       {
                           string nameForCBO = "DNN Platform ";

                           if (release.TagName != null && release.TagName[0] == 'v')
                               nameForCBO += release.TagName.Remove(0, 1);
                           else
                               nameForCBO += release.TagName;
                            
                           cboLatestReleases.Items.Add(new ComboItem(asset.BrowserDownloadUrl, nameForCBO));
                       }
                    }
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }


        #region "Tabs"

        #region "Install Package"
        private void cboLatestReleases_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboItem item = cboLatestReleases.SelectedItem as ComboItem;
            DisplayPackagePath(item);
        }

        private void DisplayPackagePath(ComboItem item)
        {
            var downloadDirectory = GetDownloadDirectory();
            var fileName = item.Name.Split('/').Last();
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
            ComboItem item = cboLatestReleases.SelectedItem as ComboItem;
            //Process.Start(item.Name);

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            var fileName = item.Name.Split('/').Last();
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
                client.DownloadFileAsync(new Uri(item.Name), downloadDirectory + fileName);
                progressBarDownload.BackColor = Color.WhiteSmoke;
                progressBarDownload.Visible = true;
            }
            else
            {
                txtLocalInstallPackage.Text = Directory.GetCurrentDirectory() + "\\Downloads\\" + Path.GetFileName(item.Name);
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
            ComboItem item = cboLatestReleases.SelectedItem as ComboItem;
            //MessageBox.Show("Download Completed", "Install Package Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtLocalInstallPackage.Text = Directory.GetCurrentDirectory() + @"\Downloads\" + Path.GetFileName(item.Name);
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
                GetOnlineVersion();
            }
            else
            {
                DialogResult result = MessageBox.Show("Click 'Yes' to use the Local Install Package. Click 'No' to attempt download of the selected Download Install Package.",
                    "Confirm: Use Local Package?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    GetOnlineVersion();
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
            bool proceed = false;

            if (txtInstallBaseFolder.Text != "" && txtInstallSubFolder.Text != "" && txtSiteNamePrefix.Text != "")
            {
                string installFolder = txtInstallBaseFolder.Text + "\\" + txtInstallSubFolder.Text;

                if (!Directory.Exists(installFolder))
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
                            Directory.CreateDirectory(installFolder);
                            proceed = true;
                        }
                        else
                        {
                            proceed = false;
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(installFolder);
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
                    if (!DirectoryEmpty(installFolder))
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
                    if (Properties.Settings.Default.RememberFieldValues)
                    {
                        Properties.Settings.Default.SiteNamePrefixRecent = txtSiteNamePrefix.Text;
                        Properties.Settings.Default.SiteNameSuffixRecent = txtSiteNameSuffix.Text;
                        Properties.Settings.Default.AppPoolRecent = chkSiteSpecificAppPool.Checked;
                        Properties.Settings.Default.DeleteSiteInIISRecent = chkDeleteSiteIfExists.Checked;
                        Properties.Settings.Default.Save();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please make sure you have entered a Site Name and Install Folder.", "Site Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool DirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
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
            if (txtDBServerName.Text != "" && txtDBName.Text != "")
            {
                if (CreateSiteInIIS())
                {
                    if (UpdateHostsFile())
                    {
                        if (CreateDirectories())
                        {
                            if (CreateDatabase())
                            {
                                if (SetDatabasePermissions())
                                {
                                    tabInstallPackage.Enabled = false;
                                    tabSiteInfo.Enabled = false;
                                    tabDatabaseInfo.Enabled = false;
                                    tabControl.TabPages.Insert(3, tabProgress);
                                    tabProgress.Enabled = true;
                                    lblProgress.Visible = true;
                                    progressBar.Visible = true;
                                    tabControl.SelectedIndex = 3;

                                    if (Properties.Settings.Default.RememberFieldValues)
                                    {
                                        Properties.Settings.Default.DatabaseServerNameRecent = txtDBServerName.Text;
                                        Properties.Settings.Default.DatabaseNameRecent = txtDBName.Text;
                                        Properties.Settings.Default.Save();
                                    }

                                    if (ReadAndExtract(txtLocalInstallPackage.Text, txtInstallBaseFolder.Text + "\\" + txtInstallSubFolder.Text + "\\Website"))
                                    {
                                        if (ModifyConfig())
                                        {
                                            btnVisitSite.Visible = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                RemoveDirectories();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please make sure you have entered a Database Server Name and a Database Name.", "Database Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool CreateSiteInIIS()
        {
            try
            {
                //Create website in IIS
                ServerManager iisManager = new ServerManager();
                var siteName = txtSiteNamePrefix.Text + txtSiteNameSuffix.Text;
                var bindingInfo = "*:80:" + siteName;
                string installFolder = txtInstallBaseFolder.Text.TrimEnd('\\') + "\\" + txtInstallSubFolder.Text;

                Boolean siteExists = SiteExists(siteName);
                if (!siteExists)
                {
                    Site mySite = iisManager.Sites.Add(siteName, "http", bindingInfo, installFolder + "\\Website");
                    mySite.TraceFailedRequestsLogging.Enabled = true;
                    mySite.TraceFailedRequestsLogging.Directory = installFolder + "\\Logs";
                    mySite.LogFile.Directory = installFolder + "\\Logs" + "\\W3svc" + mySite.Id.ToString();

                    if (chkSiteSpecificAppPool.Checked) 
                    {
                        var appPoolName = siteName + "_nvQuickSite";
                        ApplicationPool newPool = iisManager.ApplicationPools.Add(appPoolName);
                        newPool.ManagedRuntimeVersion = "v4.0";
                        mySite.ApplicationDefaults.ApplicationPoolName = appPoolName;
                    }
                    iisManager.CommitChanges();
                    //MessageBox.Show("New DNN site (" + siteName + ") added sucessfully!", "Create Site", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Site name (" + siteName + ") already exists.", "Create Site", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Create Site", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool SiteExists(string siteName)
        {
            Boolean flag = false;
            ServerManager iisManager = new ServerManager();
            SiteCollection siteCollection = iisManager.Sites;

            foreach (Site site in siteCollection)
            {
                if (site.Name == siteName.ToString())
                {
                    flag = true;
                    if (chkDeleteSiteIfExists.Checked)
                    {
                        if (site.ApplicationDefaults.ApplicationPoolName == siteName + "_nvQuickSite")
                        {
                            ApplicationPoolCollection appPools = iisManager.ApplicationPools;
                            foreach (ApplicationPool appPool in appPools)
                            {
                                if (appPool.Name == siteName + "_nvQuickSite")
                                {
                                    iisManager.ApplicationPools.Remove(appPool);
                                    break;
                                }
                            }
                        }
                        iisManager.Sites.Remove(site);
                        iisManager.CommitChanges();
                        flag = false;
                        break;
                    }
                    break;
                }
                else
                {
                    flag = false;
                }
            }
            return flag;
        }

        private bool UpdateHostsFile()
        {
            try
            {
                string hostsFile = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\drivers\etc\hosts";

                var newEntry = "\t127.0.0.1 \t" + txtSiteNamePrefix.Text + txtSiteNameSuffix.Text;
                if (!File.ReadAllLines(hostsFile).Contains(newEntry))
                {
                    if (File.ReadAllText(hostsFile).EndsWith(Environment.NewLine))
                    {
                        using (StreamWriter w = File.AppendText(hostsFile))
                        {
                            w.WriteLine(newEntry);
                        }
                    }
                    else
                    {
                        using (StreamWriter w = File.AppendText(hostsFile))
                        {
                            w.WriteLine(Environment.NewLine + newEntry);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;

                if (errorMessage.IndexOf("is denied") > 0)
                    errorMessage +=
                        "\r\r\nnvQuickSite is unable to add a new host entry to the above file. Please make sure the file is not read only. If it's not, make sure your antivirus software is not blocking changes made to the file. You can pause your antivirus software until nvQuickSite has completed its work, or add an exception for nvQuickSite in the antivirus software.";


                MessageBox.Show("Error: " + errorMessage, "Update HOSTS File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool CreateDirectories()
        {
            try
            {
                string installFolder = txtInstallBaseFolder.Text + "\\" + txtInstallSubFolder.Text;

                var websiteDir = installFolder + "\\Website";
                var logsDir = installFolder + "\\Logs";
                var databaseDir = installFolder + "\\Database";

                var appPoolName = @"IIS APPPOOL\DefaultAppPool";
                var dbServiceAccount = GetDBServiceAccount();
                var authenticatedUsers = GetAuthenticatedUsersAccount();

                if (chkSiteSpecificAppPool.Checked)
                {
                    appPoolName = @"IIS APPPOOL\" + txtSiteNamePrefix.Text + txtSiteNameSuffix.Text + "_nvQuickSite";
                }

                if (!Directory.Exists(websiteDir))
                {
                    Directory.CreateDirectory(websiteDir);
                    SetFolderPermission(appPoolName, websiteDir);
                    SetFolderPermission(authenticatedUsers, websiteDir);
                }
                else
                {
                    Directory.Delete(websiteDir, true);
                    Directory.CreateDirectory(websiteDir);
                    SetFolderPermission(appPoolName, websiteDir);
                    SetFolderPermission(authenticatedUsers, websiteDir);
                }

                if (!Directory.Exists(logsDir))
                {
                    Directory.CreateDirectory(logsDir);
                    SetFolderPermission(dbServiceAccount, logsDir);
                    SetFolderPermission(authenticatedUsers, logsDir);
                }
                else
                {
                    Directory.Delete(logsDir, true);
                    Directory.CreateDirectory(logsDir);
                    SetFolderPermission(dbServiceAccount, logsDir);
                    SetFolderPermission(authenticatedUsers, logsDir);
                }

                if (!Directory.Exists(databaseDir))
                {
                    Directory.CreateDirectory(databaseDir);
                    SetFolderPermission(dbServiceAccount, databaseDir);
                    SetFolderPermission(authenticatedUsers, databaseDir);
                }
                else
                {
                    if (!DirectoryEmpty(databaseDir))
                    {
                        var myDBFile = Directory.EnumerateFiles(databaseDir, "*.mdf").First().Split('_').First().Split('\\').Last();
                        DropDatabase(myDBFile);
                    }
                    Directory.Delete(databaseDir);
                    Directory.CreateDirectory(databaseDir);
                    SetFolderPermission(dbServiceAccount, databaseDir);
                    SetFolderPermission(authenticatedUsers, databaseDir);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Create Directories", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static string GetAuthenticatedUsersAccount()
        {
            var sid = new System.Security.Principal.SecurityIdentifier("S-1-5-11"); //Authenticated Users
            var account = sid.Translate(typeof(System.Security.Principal.NTAccount));
            return account.Value;
        }

        private string GetDBServiceAccount()
        {
            string dbServiceAccount = @"NT Service\MSSQLSERVER";
            string instanceName = txtDBServerName.Text.Trim();

            if (instanceName.IndexOf(@"\") > -1)
            {
                dbServiceAccount = @"NT Service\MSSQL$" + instanceName.Substring(instanceName.LastIndexOf(@"\") + 1).ToUpper();
            }


            return dbServiceAccount;
        }

        private static void SetFolderPermission(String accountName, String folderPath)
        {
            try
            {
                FileSystemRights Rights;
                Rights = FileSystemRights.Modify;
                bool modified;
                var none = new InheritanceFlags();
                none = InheritanceFlags.None;

                var accessRule = new FileSystemAccessRule(accountName, Rights, none, PropagationFlags.NoPropagateInherit, AccessControlType.Allow);
                var dInfo = new DirectoryInfo(folderPath);
                var dSecurity = dInfo.GetAccessControl();
                dSecurity.ModifyAccessRule(AccessControlModification.Set, accessRule, out modified);

                var iFlags = new InheritanceFlags();
                iFlags = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;

                var accessRule2 = new FileSystemAccessRule(accountName, Rights, iFlags, PropagationFlags.InheritOnly, AccessControlType.Allow);
                dSecurity.ModifyAccessRule(AccessControlModification.Add, accessRule2, out modified);
                 
                dInfo.SetAccessControl(dSecurity);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Set Folder Permissions", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveDirectories()
        {
            string installFolder = txtInstallBaseFolder.Text + "\\" + txtInstallSubFolder.Text;

            var websiteDir = installFolder + "\\Website";
            var logsDir = installFolder + "\\Logs";
            var databaseDir = installFolder + "\\Database";

            Directory.Delete(websiteDir, true);
            Directory.Delete(logsDir, true);
            Directory.Delete(databaseDir);
        }

        private bool CreateDatabase()
        {
            string myDBServerName = txtDBServerName.Text;
            string connectionStringAuthSection = "";
            string connectionTimeout = "Connection Timeout=5;";
            if (rdoWindowsAuthentication.Checked)
            {
                connectionStringAuthSection = "Integrated Security=True;";
            }
            else
            {
                connectionStringAuthSection = "User ID=" + txtDBUserName.Text + ";Password=" + txtDBPassword.Text + ";";
            }

            SqlConnection myConn = new SqlConnection("Server=" + myDBServerName + "; Initial Catalog=master;" + connectionStringAuthSection + connectionTimeout);

            string myDBName = txtDBName.Text;
            string installFolder = txtInstallBaseFolder.Text + "\\" + txtInstallSubFolder.Text;

            string str = "CREATE DATABASE [" + myDBName + "] ON PRIMARY " +
                "(NAME = [" + myDBName + "_Data], " +
                "FILENAME = '" + installFolder + "\\Database\\" + myDBName + "_Data.mdf', " +
                "SIZE = 20MB, MAXSIZE = 200MB, FILEGROWTH = 10%) " +
                "LOG ON (NAME = [" + myDBName + "_Log], " +
                "FILENAME = '" + installFolder + "\\Database\\" + myDBName + "_Log.ldf', " +
                "SIZE = 13MB, " +
                "MAXSIZE = 50MB, " +
                "FILEGROWTH = 10%)";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                //if (CanConnectToDatabase(myDBServerName))
                //{
                    myConn.Open();
                    myCommand.ExecuteNonQuery();
                    //MessageBox.Show("Database created successfully", "Create Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                //}
                //else
                //{
                //    MessageBox.Show("There was a problem connecting to the database. Please check the database name for accuracy.", "Create Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return false;
                //}
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Create Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        static bool CanConnectToDatabase(string server)
        {
            bool returnVal = false;
            try
            {
                if (server == "(local)")
                {
                    server = "127.0.0.1";
                }
                using (TcpClient tcpSocket = new TcpClient())
                {
                    AsyncCallback ConnectCallback = null;
                    IAsyncResult async = tcpSocket.BeginConnect(server, 1433, ConnectCallback, null);
                    DateTime startTime = DateTime.Now;
                    do
                    {
                        System.Threading.Thread.Sleep(500);
                        if (async.IsCompleted) break;
                    } while (DateTime.Now.Subtract(startTime).TotalSeconds < 5);
                    if (async.IsCompleted)
                    {
                        tcpSocket.EndConnect(async);
                        returnVal = true;
                    }
                    tcpSocket.Close();
                    if (!async.IsCompleted)
                    {
                        returnVal = false;
                    }
                }
                return returnVal;
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return returnVal;
            }
        }

        private bool SetDatabasePermissions()
        {
            string myDBServerName = txtDBServerName.Text;
            string connectionStringAuthSection = "";
            if (rdoWindowsAuthentication.Checked)
            {
                connectionStringAuthSection = "Integrated Security=True;";
            }
            else
            {
                connectionStringAuthSection = "User ID=" + txtDBUserName.Text + ";Password=" + txtDBPassword.Text + ";";
            }

            SqlConnection myConn = new SqlConnection("Server=" + myDBServerName + "; Initial Catalog=master;" + connectionStringAuthSection);

            string myDBName = txtDBName.Text;

            var appPoolNameFull = @"IIS APPPOOL\DefaultAppPool";
            var appPoolName = "DefaultAppPool";

            if (chkSiteSpecificAppPool.Checked)
            {
                appPoolNameFull = @"IIS APPPOOL\" + txtSiteNamePrefix.Text + txtSiteNameSuffix.Text + "_nvQuickSite";
                appPoolName = txtSiteNamePrefix.Text + txtSiteNameSuffix.Text + "_nvQuickSite";
            }

            string str1 = "USE master";
            string str2 = "sp_grantlogin '" + appPoolNameFull + "'";
            string str3 = "USE [" + txtDBName.Text + "]";
            string str4 = "sp_grantdbaccess '" + appPoolNameFull + "', '" + appPoolName + "'";
            string str5 = "sp_addrolemember 'db_owner', '" + appPoolName + "'";

            SqlCommand myCommand1 = new SqlCommand(str1, myConn);
            SqlCommand myCommand2 = new SqlCommand(str2, myConn);
            SqlCommand myCommand3 = new SqlCommand(str3, myConn);
            SqlCommand myCommand4 = new SqlCommand(str4, myConn);
            SqlCommand myCommand5 = new SqlCommand(str5, myConn);
            try
            {
                myConn.Open();
                myCommand1.ExecuteNonQuery();
                myCommand2.ExecuteNonQuery();
                myCommand3.ExecuteNonQuery();
                myCommand4.ExecuteNonQuery();
                myCommand5.ExecuteNonQuery();
                //MessageBox.Show("Database created successfully", "Set Database Permissions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Set Database Permissions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        private void DropDatabase(string myDBName)
        {
            string myDBServerName = txtDBServerName.Text;
            string connectionStringAuthSection = "";
            if (rdoWindowsAuthentication.Checked)
            {
                connectionStringAuthSection = "Integrated Security=True;";
            }
            else
            {
                connectionStringAuthSection = "User ID=" + txtDBUserName.Text + ";Password=" + txtDBPassword.Text + ";";
            }

            SqlConnection myConn = new SqlConnection("Server=" + myDBServerName + "; Initial Catalog=master;" + connectionStringAuthSection);

            string str1 = @"USE master";
            string str2 = @"IF EXISTS(SELECT name FROM sys.databases WHERE name = '" + myDBName + "')" +
                "DROP DATABASE [" + myDBName + "]";

            SqlCommand myCommand1 = new SqlCommand(str1, myConn);
            SqlCommand myCommand2 = new SqlCommand(str2, myConn);
            try
            {
                myConn.Open();
                myCommand1.ExecuteNonQuery();
                myCommand2.ExecuteNonQuery();
                //MessageBox.Show("Database created successfully", "Create Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Drop Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        int fileCount;
        long totalSize = 0, total = 0, lastVal = 0, sum = 0;

        public bool ReadAndExtract(string openPath, string savePath)
        {
            try
            {
                fileCount = 0;
                ZipFile myZip = new ZipFile();
                myZip = ZipFile.Read(openPath);
                foreach (var entry in myZip)
                {
                    fileCount++;
                    totalSize += entry.UncompressedSize;
                }
                progressBar.Maximum = (Int32)totalSize;
                myZip.ExtractProgress += new EventHandler<ExtractProgressEventArgs>(myZip_ExtractProgress);
                myZip.ExtractAll(savePath, ExtractExistingFileAction.OverwriteSilently);
                lblProgressStatus.Text = "Congratulations! Your new site is now ready to visit!";
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Read And Extract Install Package", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        void myZip_ExtractProgress(object sender, Ionic.Zip.ExtractProgressEventArgs e)
        {
            System.Windows.Forms.Application.DoEvents();
            if (total != e.TotalBytesToTransfer)
            {
                sum += total - lastVal + e.BytesTransferred;
                total = e.TotalBytesToTransfer;
                lblProgressStatus.Text = "Copying: " + e.CurrentEntry.FileName;
            }
            else
                sum += e.BytesTransferred - lastVal;

            lastVal = e.BytesTransferred;

            progressBar.Value = (Int32)sum;
        }

        private bool ModifyConfig()
        {
            try
            {
                string myDBServerName = txtDBServerName.Text;
                string connectionStringAuthSection = "";
                if (rdoWindowsAuthentication.Checked)
                {
                    connectionStringAuthSection = "Integrated Security=True;";
                }
                else
                {
                    connectionStringAuthSection = "User ID=" + txtDBUserName.Text + ";Password=" + txtDBPassword.Text +
                                                  ";";
                }

                string key = "SiteSqlServer";
                string value = @"Server=" + myDBServerName + ";Database=" + txtDBName.Text + ";" +
                               connectionStringAuthSection;
                //string providerName = "System.Data.SqlClient";

                string installFolder = txtInstallBaseFolder.Text + "\\" + txtInstallSubFolder.Text;
                string path = installFolder + @"\Website\web.config";

                var config = XDocument.Load(path);
                var targetNode = config.Root.Element("connectionStrings").Element("add").Attribute("connectionString");
                targetNode.Value = value;

                var list = from appNode in config.Descendants("appSettings").Elements()
                    where appNode.Attribute("key").Value == key
                    select appNode;

                var e = list.FirstOrDefault();
                if (e != null)
                {
                    e.Attribute("value").SetValue(value);
                }

                config.Save(path);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Modify Config", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region "Progress"

        private void btnVisitSite_Click(object sender, EventArgs e)
        {
            Process.Start("http://" + txtSiteNamePrefix.Text + txtSiteNameSuffix.Text);
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
            Process.Start("http://www.nvquick.com");
        }

        private void tileDNNDocumentationCenter_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.dnnsoftware.com/docs");
        }

        private void tileDNNAwareness_Click(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/DNNAwareness");
        }

        private void tileQuickStartGuide_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/nvisionative/nvQuickSite/wiki");
        }

        #endregion

    }
}
