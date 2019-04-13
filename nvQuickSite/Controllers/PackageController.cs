using nvQuickSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

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
            SaveLocalPackagesFile(packages);
            return packages;
        }
        private static void SaveLocalPackagesFile(IEnumerable<Package> packages)
        {
            var pfile = System.IO.Directory.GetCurrentDirectory() + @"\Downloads\packages.json";
            using (var sw = new System.IO.StreamWriter(pfile))
            {
                sw.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(packages, Newtonsoft.Json.Formatting.Indented));
            }
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

    }
}