using nvQuickSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using Octokit;

namespace nvQuickSite.Controllers
{
    public class PackageController
    {
        public static IEnumerable<Package> GetPackageList()
        {
            var localPackages = GetLocalPackages();
            var packages = localPackages.ToList();
            var remotePackages = GetRemotePackages();
            if (remotePackages.Count() > 0)
            {
                packages = localPackages.Where(p => p.keep == true).ToList();
                foreach (var package in remotePackages)
                {
                    if (packages.SingleOrDefault(p => p.did == package.did && p.version == package.version) == null)
                    {
                        packages.Add(package);
                    }
                }
            }
            var ghPackages = GetGitHubPackages();
            if (ghPackages.Count() > 0)
            {
                packages = packages.Union(ghPackages).ToList();
            }
            SaveLocalPackagesFile(packages);
            return packages;
        }

        private static IEnumerable<Package> GetLocalPackages()
        {
            var res = new List<Package>();
            var pfile = System.IO.Directory.GetCurrentDirectory() + @"\Downloads\packages.json";
            if (System.IO.File.Exists(pfile))
            {
                using (var sr = new System.IO.StreamReader(pfile))
                {
                    var content = sr.ReadToEnd();
                    res = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Package>>(content);
                }
            }
            return res;
        }

        private static void SaveLocalPackagesFile(IEnumerable<Package> packages)
        {
            var downloadDirectory = GetDownloadDirectory();
            if (!Directory.Exists(downloadDirectory))
            {
                Directory.CreateDirectory(downloadDirectory);
            }

            var pfile = System.IO.Directory.GetCurrentDirectory() + @"\Downloads\packages.json";
            using (var sw = new System.IO.StreamWriter(pfile))
            {
                sw.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(packages, Newtonsoft.Json.Formatting.Indented));
            }
        }

        private static IEnumerable<Package> GetRemotePackages()
        {
            WebClient client = new WebClient();
            try
            {
                var url = "https://github.com/nvisionative/nvQuickSite/raw/master/nvQuickSite/data/packages.json";
                string result = client.DownloadString(url);
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Package>>(result);
                return res;
            }
            catch (Exception ex)
            {
            }
            return new List<Package>();
        }

        private static string GetDownloadDirectory()
        {
            return Directory.GetCurrentDirectory() + @"\Downloads\";
        }

        private static IEnumerable<Package> GetGitHubPackages()
        {
            var res = new List<Package>();
            try
            {
                var client = new GitHubClient(new ProductHeaderValue("nvQuickSite"));
                var releases = client.Repository.Release.GetAll("dnnsoftware", "Dnn.Platform").Result;

                if (releases.Count > 0)
                {
                    var index = 0;
                    foreach (Release release in releases)
                    {
                        var installPackage = release.Assets.Where(a => a.BrowserDownloadUrl.IndexOf("install", StringComparison.OrdinalIgnoreCase) > -1 && a.BrowserDownloadUrl.IndexOf("dnn_platform", StringComparison.OrdinalIgnoreCase) > -1).FirstOrDefault();
                        var upgradePackage = release.Assets.Where(a => a.BrowserDownloadUrl.IndexOf("upgrade", StringComparison.OrdinalIgnoreCase) > -1 && a.BrowserDownloadUrl.IndexOf("dnn_platform", StringComparison.OrdinalIgnoreCase) > -1).FirstOrDefault();
                        var ghPackage = new Package();

                        ghPackage.version = TrimTagName(release);

                        if (index == 0 && 
                            release.Name.IndexOf("rc", StringComparison.OrdinalIgnoreCase) >= 0 && 
                            Properties.Settings.Default.ShowReleaseCandidates && 
                            installPackage != null)
                        {
                            ghPackage.did = "dnn-platform-rc";
                            ghPackage.name = "DNN Platform Release Candidate";
                            ghPackage.url = installPackage.BrowserDownloadUrl;
                            ghPackage.upgradeurl = upgradePackage.BrowserDownloadUrl;
                            res.Add(ghPackage);
                        }
                        else if (!release.Name.ToLower().Contains("rc") &&
                            installPackage != null)
                        {
                            ghPackage.did = "dnn-platform-" + ghPackage.version.Substring(0, 1);
                            ghPackage.name = "DNN Platform " + ghPackage.version.Substring(0, 1);
                            ghPackage.url = installPackage.BrowserDownloadUrl;
                            ghPackage.upgradeurl = upgradePackage.BrowserDownloadUrl;
                            res.Add(ghPackage);
                        }
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return res;
        }

        private static string TrimTagName(Release release)
        {
            if (release.TagName != null && release.TagName[0] == 'v')
                return release.TagName.Remove(0, 1);
            else
                return release.TagName;
        }

    }
}