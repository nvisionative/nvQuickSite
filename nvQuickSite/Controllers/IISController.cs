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
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;

    using Microsoft.Web.Administration;
    using nvQuickSite.Controllers.Exceptions;
    using Serilog;

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
            Log.Logger.Information("Creating site {siteName} in {installFolder}", siteName, installFolder);
            if (SiteExists(siteName, deleteSiteIfExists))
            {
                Log.Logger.Error("Site {siteName} already exists, aborting operation", siteName);
                throw new SiteExistsException("Site name (" + siteName + ") already exists.") { Source = "Create Site: Site Exists" };
            }

            try
            {
                var bindingInfo = "*:80:" + siteName;

                using (ServerManager iisManager = new ServerManager())
                {
                    Site site = iisManager.Sites.Add(siteName, "http", bindingInfo, installFolder + "\\Website");
                    site.TraceFailedRequestsLogging.Enabled = true;
                    site.TraceFailedRequestsLogging.Directory = installFolder + "\\Logs";
                    site.LogFile.Directory = installFolder + "\\Logs" + "\\W3svc" + site.Id.ToString(CultureInfo.InvariantCulture);

                    if (useSiteSpecificAppPool)
                    {
                        var appPoolName = siteName + "_nvQuickSite";
                        ApplicationPool newPool = iisManager.ApplicationPools.Add(appPoolName);
                        newPool.ManagedRuntimeVersion = "v4.0";
                        site.ApplicationDefaults.ApplicationPoolName = appPoolName;
                    }

                    iisManager.CommitChanges();
                    Log.Logger.Information("Created site {siteName}", siteName);
                    Log.Logger.Debug("Created site {@site}", site);
                }
            }
            catch (Exception ex)
            {
                var message = "Error creating site in IIS";
                Log.Logger.Error(ex, message);
                throw new IISControllerException(message, ex) { Source = "Create Site" };
            }
        }

        /// <summary>
        /// Attempts to delete a site in IIS, optionnally reporting progress.
        /// </summary>
        /// <param name="siteId">The id of the site to delete.</param>
        /// <param name="progress">The progress reporter.</param>
        /// <exception cref="ArgumentException"> is thrown if the site cannot be deleted.</exception>
        internal static void DeleteSite(long siteId, IProgress<int> progress = null)
        {
            using (var iisManager = new ServerManager())
            {
                try
                {
                    var site = iisManager.Sites.FirstOrDefault(s => s.Id == siteId);

                    if (site == null)
                    {
                        Log.Logger.Error("Site with id {siteId} does not exist", siteId);
                        progress?.Report(100);
                        return;
                    }

                    iisManager.Sites.Remove(site);
                    iisManager.CommitChanges();
                    progress?.Report(100);
                    Log.Logger.Information("Site {siteName} deleted", site.Name);
                }
                catch (COMException ex)
                {
                    progress?.Report(100);
                    var message = $"The site with id {siteId} could not be deleted";
                    Log.Logger.Error(message, ex);
                    throw new ArgumentException(message, ex) { Source = "Site Deletion Error" };
                }
            }
        }

        /// <summary>
        /// Attempts to delete an application pool.
        /// </summary>
        /// <param name="appPoolName">The name of the application pool.</param>
        /// <param name="progress">The progress reporter.</param>
        /// <exception cref="ArgumentException"> is thrown when the application pool cannot be deleted.</exception>
        internal static void DeleteAppPool(string appPoolName, IProgress<int> progress)
        {
            Log.Logger.Information("Deleting application pool {appPoolName}", appPoolName);
            try
            {
                using (var iisManager = new ServerManager())
                {
                    var appPool = iisManager.ApplicationPools.FirstOrDefault(a => a.Name == appPoolName);
                    if (appPool == null)
                    {
                        Log.Logger.Information("Application pool {appPoolName} does not exist", appPoolName);
                        progress?.Report(100);
                        return;
                    }

                    iisManager.ApplicationPools.Remove(appPool);
                    iisManager.CommitChanges();
                    Log.Logger.Information("Application pool {appPoolName} deleted", appPoolName);
                    progress?.Report(100);
                }
            }
            catch (COMException ex)
            {
                progress?.Report(100);
                var message = $"There was an error deleting the application pool {appPoolName}";
                Log.Logger.Error(message, ex);
                throw new ArgumentException(message, ex) { Source = "Delete AppPool Error" };
            }
        }

        /// <summary>
        /// Gets a list of IIS sites.
        /// </summary>
        /// <param name="createdByThisToolOnly">When true, will filter the results to only show sites created by this tool.</param>
        /// <returns>An enumeration of sites.</returns>
        internal static IEnumerable<Site> GetSites(bool createdByThisToolOnly = false)
        {
            Log.Logger.Information("Getting the list of sites");
            List<Site> sites;
            using (ServerManager iisManager = new ServerManager())
            {
                sites = iisManager.Sites.ToList();
                Log.Logger.Debug("Found the following sites {@sites}", sites);
                if (!createdByThisToolOnly)
                {
                    return sites;
                }

                sites = sites
                    .Where(s =>
                        s.ApplicationDefaults.ApplicationPoolName.EndsWith(
                            "_nvQuickSite",
                            StringComparison.Ordinal))
                    .ToList();

                Log.Logger.Debug("Found the following sites created by this tool {@sites}", sites);
                return sites;
            }
        }

        /// <summary>
        /// Stops an IIS website.
        /// </summary>
        /// <param name="siteId">The id of the site to stop.</param>
        /// <param name="progress">An optional progress reporter.</param>
        /// <exception cref="ArgumentException"> is thrown when the site cannot be stopped.</exception>
        internal static void StopSite(long siteId, IProgress<int> progress)
        {
            Log.Logger.Debug("Deleting site with id {siteId}", siteId);
            using (var iisManager = new ServerManager())
            {
                progress?.Report(25);
                var site = iisManager.Sites.FirstOrDefault(s => s.Id == siteId);
                if (site == null)
                {
                    Log.Logger.Information("Site with id {siteId} does not exist", siteId);
                }

                site.ServerAutoStart = false;
                progress.Report(50);
                if (site.State != ObjectState.Stopped)
                {
                    try
                    {
                        var state = site.Stop();
                        Log.Logger.Information("Site {siteName} is {state}", site.Name, state);
                    }
                    catch (COMException ex)
                    {
                        var message = "There was a problem stopping the site.\nAborting the site deletion.";
                        Log.Logger.Error(message, ex);
                        throw new ArgumentException(message, ex) { Source = "Stop Site Error" };
                    }
                }

                Log.Logger.Information("Site {siteName} was already stopped", site.Name);
                progress?.Report(100);
            }
        }

        /// <summary>
        /// Attempts to stop an application pool.
        /// </summary>
        /// <param name="siteId">The id of the site for which to stop the application pool.</param>
        /// <param name="progress">An optional progress reporter.</param>
        internal static void StopAppPool(long siteId, IProgress<int> progress)
        {
            progress?.Report(25);
            using (var iisManager = new ServerManager())
            {
                var site = iisManager.Sites.FirstOrDefault(s => s.Id == siteId);
                if (site == null)
                {
                    Log.Logger.Error("The site with id {siteId} could not be found", siteId);
                    throw new ArgumentException("The site could not be found, aborting.") { Source = "Site Not Found" };
                }

                var appPool = iisManager.ApplicationPools.FirstOrDefault(a =>
                    a.Name == site.ApplicationDefaults.ApplicationPoolName);

                if (appPool == null)
                {
                    var message = "There was a problem finding the application pool.\nAborting the site deletion.";
                    Log.Logger.Error(message);
                    throw new ArgumentException(message) { Source = "AppPool Not Found" };
                }

                var totalSitesUsingAppPool = iisManager.Sites.Count(s => s.ApplicationDefaults.ApplicationPoolName == appPool.Name);
                if (totalSitesUsingAppPool > 1)
                {
                    var message = "More than one site uses this application pool.\nAborting the site deletion.";
                    Log.Logger.Error(message);
                    throw new ArgumentException(message) { Source = "AppPool Not Specific" };
                }

                progress?.Report(50);

                try
                {
                    appPool.AutoStart = false;
                    if (appPool.State != ObjectState.Stopped)
                    {
                        var state = appPool.Stop();
                        Log.Logger.Information("Application Pool {appPoolName} is {state}", appPool.Name, state);
                    }
                }
                catch (COMException ex)
                {
                    var message = "There was a problem stopping the application pool.\nAborting the site deletion.";
                    Log.Logger.Error(message, ex);
                    throw new ArgumentException(message, ex) { Source = "AppPool Error" };
                }
            }

            progress?.Report(100);
        }

        private static bool SiteExists(string siteName, bool deleteSiteIfExists)
        {
            Log.Logger.Verbose("Checking if site {siteName} exists", siteName);
            bool exists = false;
            using (ServerManager iisManager = new ServerManager())
            {
                var sites = iisManager.Sites;

                foreach (Site site in sites)
                {
                    if (site.Name == siteName.ToString())
                    {
                        exists = true;
                        Log.Logger.Verbose("Site {siteName} exists", siteName);
                        if (deleteSiteIfExists)
                        {
                            Log.Logger.Verbose("The user requested for the existing site {siteName} to be deleted", siteName);
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
                            Log.Logger.Information("Site {siteName} was deleted", siteName);
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
