using System;
using System.Collections.Generic;
using System.Net;

namespace nvQuickSite.Controllers
{
    public class VersionController
    {
        public static decimal GetRemoteCurrentVersion()
        {
            WebClient client = new WebClient();
            try
            {
                var url = "https://github.com/nvisionative/nvQuickSite/blob/issue-151/data/currentVersion.json";
                string result = client.DownloadString(url);
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Version>(result);
                return res.currentVersion;
            }
            catch (Exception ex)
            {
            }
            return new decimal();
        }

    }
}
