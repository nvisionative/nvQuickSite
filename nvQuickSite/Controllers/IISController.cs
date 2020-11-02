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

using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace nvQuickSite.Controllers
{
    public static class IISController
    {
        internal static bool SiteExists(string siteName, bool deleteSiteIfExists)
        {
            Boolean exists = false;
            ServerManager iisManager = new ServerManager();
            SiteCollection siteCollection = iisManager.Sites;

            foreach (Site site in siteCollection)
            {
                if (site.Name == siteName.ToString())
                {
                    exists = true;
                    if (deleteSiteIfExists)
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
                        exists = false;
                        break;
                    }
                    break;
                }
                else
                {
                    exists = false;
                }
            }
            return exists;
        }

        internal static void CreateSite(string siteName, string installFolder, bool useSiteSpecificAppPool)
        {
            try
            {
                var bindingInfo = "*:80:" + siteName;

                ServerManager iisManager = new ServerManager();

                Site mySite = iisManager.Sites.Add(siteName, "http", bindingInfo, installFolder + "\\Website");
                mySite.TraceFailedRequestsLogging.Enabled = true;
                mySite.TraceFailedRequestsLogging.Directory = installFolder + "\\Logs";
                mySite.LogFile.Directory = installFolder + "\\Logs" + "\\W3svc" + mySite.Id.ToString();

                if (useSiteSpecificAppPool)
                {
                    var appPoolName = siteName + "_nvQuickSite";
                    ApplicationPool newPool = iisManager.ApplicationPools.Add(appPoolName);
                    newPool.ManagedRuntimeVersion = "v4.0";
                    mySite.ApplicationDefaults.ApplicationPoolName = appPoolName;
                }
                iisManager.CommitChanges();
            }
            catch (Exception ex)
            {
                throw new IISException("Something went wrong creating the site in IIS: ", ex);
            }

        }
    }

    [Serializable]
    internal class IISException : Exception
    {
        public IISException()
        {
        }

        public IISException(string message) : base(message)
        {
        }

        public IISException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IISException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
