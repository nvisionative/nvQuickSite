using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using MetroFramework.Controls;
using Microsoft.Web.Administration;

namespace DNNQuickSite
{
    public partial class Start : MetroUserControl
    {
        public Start()
        {
            InitializeComponent();

            tabControl.SelectedIndex = 0;
            tabSiteInfo.Enabled = false;
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
            int size = -1;
            DialogResult result = openFileDialog.ShowDialog(); // Show dialog
            if (result == DialogResult.OK) // Test result
            {
                txtLocalInstallPackage.Text = openFileDialog.FileName;
                //string file = openFileDialog.FileName;
                //try
                //{
                //    string text = File.ReadAllText(file);
                //    size = text.Length;
                //    txtLocalInstallPackage.Text = file;
                //}
                //catch (IOException)
                //{
                //}
            }
        }

        private void btnInstallPackageNext_Click(object sender, EventArgs e)
        {
            tabInstallPackage.Enabled = false;
            tabSiteInfo.Enabled = true;
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
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtLocation.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnSiteInfoBack_Click(object sender, EventArgs e)
        {
            tabSiteInfo.Enabled = false;
            tabInstallPackage.Enabled = true;
            tabControl.SelectedIndex = 0;
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("All files and folders at this location will be deleted prior to installation of the new DNN instance. Do you wish to proceed?",
                                     "Confirm Installation",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                tabProgress.Enabled = true;
                tabSiteInfo.Enabled = false;
                tabControl.SelectedIndex = 2;

                //backgroundWorker.RunWorkerAsync();

                CreateSite();
                UpdateHostsFile();
            }
            else
            {
                // If 'No', do something here.
            }
        }

        private void CreateSite()
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
                    Site mySite = iisManager.Sites.Add(siteName, "http", bindingInfo, txtLocation.Text);
                    mySite.TraceFailedRequestsLogging.Enabled = true;
                    mySite.TraceFailedRequestsLogging.Directory = txtLocation.Text + "\\Logs";

                    if (chkSiteSpecificAppPool.Checked)
                    {
                        var appPoolName = siteName + "_DNNQuickSite";
                        iisManager.ApplicationPools.Add(appPoolName);
                        mySite.ApplicationDefaults.ApplicationPoolName = appPoolName;
                    }
                    iisManager.CommitChanges();
                    MessageBox.Show("New DNN site (" + siteName + ") added sucessfully!", "Success", MessageBoxButtons.OK);
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

        #endregion


        #region "Progress"

        private void Calculate(int i)
        {
            double pow = Math.Pow(i, i);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            for (int j = 0; j < 100000; j++)
            {
                Calculate(j);
                backgroundWorker.ReportProgress((j * 100) / 100000);
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


    }
}
