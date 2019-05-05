//Copyright (c) 2016-2019 nvisionative, Inc.

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
                var url = "https://raw.githubusercontent.com/nvisionative/nvQuickSite/master/nvQuickSite/data/latestVersion.json";
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
