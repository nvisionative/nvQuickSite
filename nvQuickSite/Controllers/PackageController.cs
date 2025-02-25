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

namespace nvQuickSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;

    using nvQuickSite.Models;
    using Octokit;
    using Serilog;

    /// <summary>
    /// Manages packages.
    /// </summary>
    public static class PackageController
    {
        /// <summary>
        /// Gets a value indicating whether the system can access the GitHub repository.
        /// </summary>
        internal static bool IsOnline
        {
            get
            {
                bool canRead;
                try
                {
                    using (var client = new WebClient())
                    {
                        canRead = client.OpenRead("https://github.com/nvisionative/nvQuickSite").CanRead;
                        Log.Logger.Information("Internet appears to be " + (canRead ? "online" : "offline"));
                        return canRead;
                    }
                }
                catch (WebException ex)
                {
                    Log.Logger.Information("Internet appears offline");
                    Log.Logger.Error(ex, "Unexpected error occurred while checking internet access to GitHub repository");
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the list of packages available.
        /// </summary>
        /// <returns>An enumeration of packages.</returns>
        public static IEnumerable<Package> GetPackageList()
        {
            var localPackages = GetLocalPackages();
            var packages = localPackages.ToList();
            if (PackageController.IsOnline)
            {
                var remotePackages = GetRemotePackages();
                if (remotePackages.Any())
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
                if (ghPackages.Any())
                {
                    // Deduplicate exising packages to be safe and fix users that already have duplicates in packages.json
                    packages = packages.GroupBy(p => p.version).Select(p => p.First()).ToList();

                    // Remove the RCs from the local packages as they may be out of RC by now.
                    // Github will include them or not right after this clearing.
                    // Also we may publish multiple RCs and should never have more than one published at any time so we need only the latest shown.
                    packages.RemoveAll(p => p.did == "dnn-platform-rc");

                    foreach (var ghPackage in ghPackages)
                    {
                        // Only add the github packages if they are not yet in the local packages.json file.
                        if (!packages.Any(p => p.version == ghPackage.version))
                        {
                            packages.Add(ghPackage);
                        }
                    }
                }
            }

            // Remove RCs if they already have a non-RC version.
            packages.RemoveAll(p =>
                p.did == "dnn-platform-rc" &&
                packages.Any(p2 =>
                    p2.version.Major == p.version.Major &&
                    p2.version.Minor == p2.version.Minor &&
                    p2.version.Build == p2.version.Build &&
                    p2.did != "dnn-platform-rc"));

            SaveLocalPackagesFile(packages);
            Log.Logger.Information("Saved local packages file");
            Log.Logger.Debug("Saved packages to local packages file: {@packages}", packages);
            return packages;
        }

        private static IEnumerable<Package> GetLocalPackages()
        {
            var localPackages = new List<Package>();
            var packagesFile = Directory.GetCurrentDirectory() + @"\Downloads\packages.json";
            if (File.Exists(packagesFile))
            {
                using (var sr = new StreamReader(packagesFile))
                {
                    var content = sr.ReadToEnd();
                    localPackages = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Package>>(content);
                }
            }

            Log.Logger.Information("Loaded local packages");
            Log.Logger.Debug("Loaded local packages: {@localPackages}", localPackages);
            return localPackages;
        }

        private static void SaveLocalPackagesFile(IEnumerable<Package> packages)
        {
            var downloadDirectory = GetDownloadDirectory();
            if (!Directory.Exists(downloadDirectory))
            {
                Directory.CreateDirectory(downloadDirectory);
            }

            var pfile = Directory.GetCurrentDirectory() + @"\Downloads\packages.json";
            using (var sw = new StreamWriter(pfile))
            {
                sw.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(packages, Newtonsoft.Json.Formatting.Indented));
            }
        }

        private static IEnumerable<Package> GetRemotePackages()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    var url = "https://raw.githubusercontent.com/nvisionative/nvQuickSite/main/nvQuickSite/data/packages.json";
                    string result = client.DownloadString(url);
                    var packages = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Package>>(result);
                    Log.Logger.Information("Loaded remote packages");
                    Log.Logger.Debug("Loaded remote packages: {@packages}", packages);
                    return packages;
                }
                catch (WebException ex)
                {
                    Log.Logger.Error(ex, "Unexpected error occurred retrieving remote packages.");
                    return new List<Package>();
                }
            }
        }

        private static string GetDownloadDirectory()
        {
            return Directory.GetCurrentDirectory() + @"\Downloads\";
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design",
            "CA1031:Do not catch general exception types",
            Justification = "On purpose so any exception here does not prevent using local packages.")]
        private static IEnumerable<Package> GetGitHubPackages()
        {
            var packages = new List<Package>();
            try
            {
                var client = new GitHubClient(new ProductHeaderValue("nvQuickSite"));
                var releases = client.Repository.Release.GetAll("dnnsoftware", "Dnn.Platform").Result;

                if (releases.Count > 0)
                {
                    var index = 0;
                    foreach (Release release in releases)
                    {
                        var installPackage = release.Assets
                            .Where(a =>
                                a.BrowserDownloadUrl.IndexOf("install", StringComparison.OrdinalIgnoreCase) > -1 &&
                                a.BrowserDownloadUrl.IndexOf("dnn_platform", StringComparison.OrdinalIgnoreCase) > -1)
                            .FirstOrDefault();

                        var upgradePackage = release.Assets
                            .Where(a =>
                                a.BrowserDownloadUrl.IndexOf("upgrade", StringComparison.OrdinalIgnoreCase) > -1 &&
                                a.BrowserDownloadUrl.IndexOf("dnn_platform", StringComparison.OrdinalIgnoreCase) > -1)
                            .FirstOrDefault();

                        var ghPackage = new Package();

                        ghPackage.version = new System.Version(release.TagName.TrimStart('v').Split('-')[0]);

                        if (release.Name.IndexOf("rc", StringComparison.OrdinalIgnoreCase) >= 0 &&
                            Properties.Settings.Default.ShowReleaseCandidates &&
                            installPackage != null)
                        {
                            ghPackage.did = "dnn-platform-rc";
                            ghPackage.name = "DNN Platform Release Candidate";
                            ghPackage.url = installPackage.BrowserDownloadUrl;
                            ghPackage.upgradeurl = upgradePackage.BrowserDownloadUrl;

                            var preReleaseTag = release.TagName.TrimStart('v').Split('-')[1];
                            var rcSuffix = preReleaseTag.Replace("rc", string.Empty);
                            if (int.TryParse(rcSuffix, out int preReleaseNumber))
                            {
                                ghPackage.version = new System.Version(
                                    ghPackage.version.Major,
                                    ghPackage.version.Minor,
                                    ghPackage.version.Build,
                                    preReleaseNumber);
                            }

                            packages.Add(ghPackage);
                        }
                        else if (!release.Name.ToUpperInvariant().Contains("RC") &&
                            installPackage != null)
                        {
                            ghPackage.did = "dnn-platform-" + ghPackage.version.Major.ToString();
                            ghPackage.name = "DNN Platform " + ghPackage.version.Major.ToString();
                            ghPackage.url = installPackage.BrowserDownloadUrl;
                            ghPackage.upgradeurl = upgradePackage.BrowserDownloadUrl;
                            packages.Add(ghPackage);
                        }

                        index++;
                    }
                }

                Log.Logger.Information("Retrieved DNN packages from GitHub");
                Log.Logger.Debug("Retrieved DNN packages from GitHub: {@packages}", packages);
                return packages;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Unexpected error occurred retrieving DNN packages from GitHub");
                return packages;
            }
        }
    }
}
