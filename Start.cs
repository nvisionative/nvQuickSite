using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;
using System.Diagnostics;
using MetroFramework.Controls;
using Microsoft.Web.Administration;
using Ionic.Zip;
using Ookii.Dialogs;

namespace DNNQuickSite
{
    public partial class Start : MetroUserControl
    {
        public Start()
        {
            InitializeComponent();
            InitializeBackgroundWorker();

            tabControl.SelectedIndex = 0;
            tabSiteInfo.Enabled = false;
            tabDatabaseInfo.Enabled = false;
            tabProgress.Enabled = false;

            //FeedParser parser = new FeedParser();
            //var releases = parser.Parse("http://dotnetnuke.codeplex.com/project/feeds/rss?ProjectRSSFeed=codeplex%3a%2f%2frelease%2fdotnetnuke", FeedType.RSS);

            var url = "http://www.dnnquicksite.com/downloads/";
            WebClient client = new WebClient();
            string result = client.DownloadString(url + "PackageManifest.xml");

            XDocument doc = XDocument.Parse(result);
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

        // Set up the BackgroundWorker object by 
        // attaching event handlers. 
        private void InitializeBackgroundWorker()
        {
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
        }

        #region "Install Package"
        private void cboLatestReleases_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboItem item = cboLatestReleases.SelectedItem as ComboItem;
        }

        private void btnGetLatestRelease_Click(object sender, EventArgs e)
        {
            ComboItem item = cboLatestReleases.SelectedItem as ComboItem;
            Process.Start(item.Name);
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
            tabInstallPackage.Enabled = false;
            tabSiteInfo.Enabled = true;
            tabDatabaseInfo.Enabled = false;
            tabProgress.Enabled = false;
            tabControl.SelectedIndex = 1;
        }

        #endregion

        #region "Site Info"
        private void txtLocation_Click(object sender, EventArgs e)
        {
            openFolderDiag();
        }

        private void btnLocation_Click(object sender, EventArgs e)
        {
            openFolderDiag();
        }

        private void openFolderDiag()
        {
            VistaFolderBrowserDialog diag = new VistaFolderBrowserDialog();
            diag.RootFolder = Environment.SpecialFolder.MyComputer;
            diag.SelectedPath = Properties.Settings.Default.LocationRecent;
            DialogResult result = diag.ShowDialog();

            //folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            //folderBrowserDialog.SelectedPath = Properties.Settings.Default.RecentFolder;
            //folderBrowserDialog.ShowNewFolderButton = false;
            ////SendKeys.Send("{TAB}{TAB}{RIGHT}");
            //DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtLocation.Text = diag.SelectedPath;
                Properties.Settings.Default.LocationRecent = diag.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void btnSiteInfoBack_Click(object sender, EventArgs e)
        {
            tabSiteInfo.Enabled = false;
            tabInstallPackage.Enabled = true;
            tabControl.SelectedIndex = 0;
        }

        private void btnSiteInfoNext_Click(object sender, EventArgs e)
        {
            bool proceed = false;

            if (!DirectoryEmpty(txtLocation.Text))
            {
                var confirmResult = MessageBox.Show("All files and folders at this location will be deleted prior to installation of the new DNN instance. Do you wish to proceed?",
                                         "Confirm Installation",
                                         MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    proceed = true;
                }
            }
            else
            {
                proceed = true;
            }

            if (proceed)
            {
                tabInstallPackage.Enabled = false;
                tabSiteInfo.Enabled = false;
                tabDatabaseInfo.Enabled = true;
                tabProgress.Enabled = false;
                tabControl.SelectedIndex = 2;

                //backgroundWorker.RunWorkerAsync();

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
            tabControl.SelectedIndex = 1;
        }

        private void btnDatabaseInfoNext_Click(object sender, EventArgs e)
        {
            tabInstallPackage.Enabled = false;
            tabSiteInfo.Enabled = false;
            tabDatabaseInfo.Enabled = false;
            tabProgress.Enabled = true;
            tabControl.SelectedIndex = 3;

            CreateSiteInIIS();
            UpdateHostsFile();
            CreateDirectories();
            CreateDatabase();
            ReadAndExtract(txtLocalInstallPackage.Text, txtLocation.Text + "\\Website");
        }

        private void CreateDirectories()
        {
            var websiteDir = txtLocation.Text + "\\Website";
            var logsDir = txtLocation.Text + "\\Logs";
            var databaseDir = txtLocation.Text + "\\Database";

            var appPoolName = @"IIS APPPOOL\DefaultAppPool";
            var dbServiceAccount = @"NT Service\MSSQLSERVER";

            if (chkSiteSpecificAppPool.Checked)
            {
                appPoolName = @"IIS APPPOOL\" + txtSiteName.Text + "_DNNQuickSite";
            }

            if (!Directory.Exists(websiteDir))
            {
                Directory.CreateDirectory(websiteDir);
                SetFolderPermission(appPoolName, websiteDir);
            }
            else
            {
                Directory.Delete(websiteDir, true);
                Directory.CreateDirectory(websiteDir);
                SetFolderPermission(appPoolName, websiteDir);
            }

            if (!Directory.Exists(logsDir))
            {
                Directory.CreateDirectory(logsDir);
            }
            else
            {
                Directory.Delete(logsDir);
                Directory.CreateDirectory(logsDir);
                SetFolderPermission(dbServiceAccount, logsDir);
            }

            if (!Directory.Exists(databaseDir))
            {
                Directory.CreateDirectory(databaseDir);
            }
            else
            {
                Directory.Delete(databaseDir);
                Directory.CreateDirectory(databaseDir);
                SetFolderPermission(dbServiceAccount, databaseDir);
            }
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
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void CreateSiteInIIS()
        {
            try
            {
                //Create website in IIS
                ServerManager iisManager = new ServerManager();
                var siteName = txtSiteName.Text;
                var bindingInfo = "*:80:" + siteName;

                Boolean siteExists = SiteExists(siteName);
                if (!siteExists)
                {
                    Site mySite = iisManager.Sites.Add(siteName, "http", bindingInfo, txtLocation.Text + "\\Website");
                    mySite.TraceFailedRequestsLogging.Enabled = true;
                    mySite.TraceFailedRequestsLogging.Directory = txtLocation.Text + "\\Logs";

                    if (chkSiteSpecificAppPool.Checked)
                    {
                        var appPoolName = siteName + "_DNNQuickSite";
                        iisManager.ApplicationPools.Add(appPoolName);
                        mySite.ApplicationDefaults.ApplicationPoolName = appPoolName;
                    }
                    iisManager.CommitChanges();
                    //MessageBox.Show("New DNN site (" + siteName + ") added sucessfully!", "Success", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Site name (" + siteName + ") already exists.", "Alert", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
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
                        if (site.ApplicationDefaults.ApplicationPoolName == siteName + "_DNNQuickSite")
                        {
                            ApplicationPoolCollection appPools = iisManager.ApplicationPools;
                            foreach (ApplicationPool appPool in appPools)
                            {
                                if (appPool.Name == siteName + "_DNNQuickSite")
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

        private void UpdateHostsFile()
        {
            using (StreamWriter w = File.AppendText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts")))
            {
                w.WriteLine("\t127.0.0.1 \t" + txtSiteName.Text);
            }
        }

        private void UnZipPackage()
        {
            using (ZipFile zip = ZipFile.Read(txtLocalInstallPackage.Text))
            {
                zip.ExtractProgress +=
                   new EventHandler<ExtractProgressEventArgs>(zipExtractProgress);
                zip.ExtractAll(txtLocation.Text + "/Website", ExtractExistingFileAction.OverwriteSilently);
            }
        }

        private void zipExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (e.TotalBytesToTransfer > 0)
            {
                progressBar.Value = Convert.ToInt32(100 * e.BytesTransferred / e.TotalBytesToTransfer);
            }
        }

        int fileCount = 0;
        long totalSize = 0, total = 0, lastVal = 0, sum = 0;

        public void ReadAndExtract(string openPath, string savePath)
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
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        void myZip_ExtractProgress(object sender, Ionic.Zip.ExtractProgressEventArgs e)
        {

            System.Windows.Forms.Application.DoEvents();
            if (total != e.TotalBytesToTransfer)
            {
                sum += total - lastVal + e.BytesTransferred;
                total = e.TotalBytesToTransfer;
            }
            else
                sum += e.BytesTransferred - lastVal;

            lastVal = e.BytesTransferred;

            progressBar.Value = (Int32)sum;
        }

        #endregion

        #region "Progress"

        private void Calculate(int i)
        {
            double pow = Math.Pow(i, i);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 1; i <= 10; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(500);
                    worker.ReportProgress(i * 10);
                }
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // TODO: do something with final calculation.
        }

        #endregion

        #region "Tiles"

        private void tileDNNCommunityForums_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.dnnsoftware.com/forums");
        }

        private void tileDNNDocumentationCenter_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.dnnsoftware.com/docs");
        }

        #endregion

        #region "Create Database"

        private void CreateDatabase()
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

            string str = "CREATE DATABASE [" + myDBName + "] ON PRIMARY " +
                "(NAME = [" + myDBName + "_Data], " +
                "FILENAME = '" + txtLocation.Text + "\\Database\\" + myDBName + "_Data.mdf', " +
                "SIZE = 4MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
                "LOG ON (NAME = [" + myDBName + "_Log], " +
                "FILENAME = '" + txtLocation.Text + "\\Database\\" + myDBName + "_Log.ldf', " +
                "SIZE = 1MB, " +
                "MAXSIZE = 5MB, " +
                "FILEGROWTH = 10%)";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try 
            {
                myConn.Open();
	            myCommand.ExecuteNonQuery();
	            //MessageBox.Show("Database created successfully", "DNN QuickSite", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
                {
    	            MessageBox.Show(ex.ToString(), "DNN QuickSite", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            finally
            {
	            if (myConn.State == ConnectionState.Open)
	            {
	                myConn.Close();
	            }
            }
        }

        #endregion

    }
}
