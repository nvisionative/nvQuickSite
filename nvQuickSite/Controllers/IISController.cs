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
    using System.Globalization;

    using Microsoft.Web.Administration;

    /// <summary>
    /// Controls IIS operations.
    /// </summary>
    public static class IISController
    {
        /// <summary>
        /// Creates a site in IIS.
        /// </summary>
        /// <param name="siteName">The name of the site.</param>
        /// <param name="installFolder">The path to the hosting folder for the site.</param>
        /// <param name="useSiteSpecificAppPool">A value indicating whether to use a site specific App Pool.</param>
        /// <param name="deleteSiteIfExists">If true will delete and recreate the site.</param>
        internal static void CreateSite(string siteName, string installFolder, bool useSiteSpecificAppPool, bool deleteSiteIfExists)
        {
            if (SiteExists(siteName, deleteSiteIfExists))
            {
                throw new SiteExistsException("Site name (" + siteName + ") already exists.") { Source = "Create Site: Site Exists" };
            }

            try
            {
                var bindingInfo = "*:80:" + siteName;

                using (ServerManager iisManager = new ServerManager())
                {
                    Site mySite = iisManager.Sites.Add(siteName, "http", bindingInfo, installFolder + "\\Website");
                    mySite.TraceFailedRequestsLogging.Enabled = true;
                    mySite.TraceFailedRequestsLogging.Directory = installFolder + "\\Logs";
                    mySite.LogFile.Directory = installFolder + "\\Logs" + "\\W3svc" + mySite.Id.ToString(CultureInfo.InvariantCulture);

                    if (useSiteSpecificAppPool)
                    {
                        var appPoolName = siteName + "_nvQuickSite";
                        ApplicationPool newPool = iisManager.ApplicationPools.Add(appPoolName);
                        newPool.ManagedRuntimeVersion = "v4.0";
                        mySite.ApplicationDefaults.ApplicationPoolName = appPoolName;
                    }

                    iisManager.CommitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new IISControllerException("Something went wrong creating the site in IIS: ", ex) { Source = "Create Site" };
            }
        }

        private static bool SiteExists(string siteName, bool deleteSiteIfExists)
        {
            bool exists = false;
            using (ServerManager iisManager = new ServerManager())
            {
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
            }

            return exists;
        }
    }
}
