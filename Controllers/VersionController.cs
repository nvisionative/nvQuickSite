using System;
using System.Collections.Generic;
using System.Net;

namespace nvQuickSite.Controllers
{
    public class VersionController
    {
        public static string GetRemoteLatestVersion()
        {
            WebClient client = new WebClient();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var url = "https://raw.githubusercontent.com/nvisionative/nvQuickSite/issue-151/data/latestVersion.json";
                string result = client.DownloadString(url);
                Models.Version res = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Version>(result);
                return res.latestVersion;
            }
            catch (Exception ex)
            {
            }
            return "";
        }

    }
}
