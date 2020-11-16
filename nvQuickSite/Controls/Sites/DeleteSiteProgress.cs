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

namespace nvQuickSite.Controls.Sites
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using MetroFramework.Forms;
    using Microsoft.Web.Administration;
    using nvQuickSite.Controllers;
    using nvQuickSite.Controllers.Exceptions;

    /// <summary>
    /// Implementes the user specific settings form.
    /// </summary>
    public partial class DeleteSiteProgress : MetroForm
    {
        private readonly Site site;
        private readonly string sitePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSiteProgress"/> class.
        /// </summary>
        /// <param name="site">The site to delete.</param>
        public DeleteSiteProgress(Site site)
        {
            Contract.Requires(site != null);

            this.site = site;
            this.sitePath = site.Applications[0].VirtualDirectories[0].PhysicalPath;

            this.InitializeComponent();

            this.InitProgressBars();
            this.DeleteSite();
        }

        private void InitProgressBars()
        {
            var max = FileSystemController.CountFilesAndDirectories(this.sitePath);
            if (max > 0)
            {
                this.progressDeleteFiles.Maximum = max;
            }

            this.UpdateTotalProgress();
        }

        private async void DeleteSite()
        {
            using (var iisManager = new ServerManager())
            {
                // Stop Site
                var stopSiteProgress = new Progress<int>(percent =>
                {
                    this.progressStopSite.Value = percent;
                    this.UpdateTotalProgress();
                });
                await Task.Run(() => this.StopSite(iisManager, stopSiteProgress)).ConfigureAwait(true);

                // Stop AppPool
                var stopAppPoolProgress = new Progress<int>(percent =>
                {
                    this.progressStopAppPool.Value = percent;
                    this.UpdateTotalProgress();
                });
                await Task.Run(() => this.StopAppPool(iisManager, stopAppPoolProgress)).ConfigureAwait(true);

                // Delete Database
                var deleteDatabaseProgress = new Progress<int>(percent =>
                {
                    this.progressDeleteDatabae.Value = percent;
                    this.UpdateTotalProgress();
                });
                await Task.Run(() => this.DeleteDatabase(deleteDatabaseProgress)).ConfigureAwait(true);

                // Delete files
                var deleteFilesProgress = new Progress<string>(name =>
                {
                    if (this.progressDeleteFiles.Value < this.progressDeleteFiles.Maximum)
                    {
                        this.progressDeleteFiles.Value++;
                        this.UpdateTotalProgress();
                    }
                });
                await Task.Run(() => FileSystemController.DeleteDirectory(this.sitePath, deleteFilesProgress)).ConfigureAwait(true);
                this.progressDeleteFiles.Value = this.progressDeleteFiles.Maximum;
                this.UpdateTotalProgress();

                // Delete entry from HOSTS file
                var deleteHostEntryProgress = new Progress<int>(percent =>
                {
                    this.progressRemoveHostEntry.Value = percent;
                    this.UpdateTotalProgress();
                });
                await Task.Run(() => FileSystemController.RemoveHostEntry(this.site.Name, deleteHostEntryProgress)).ConfigureAwait(true);

                // Delete Site
                var deleteSiteProgress = new Progress<int>(percent =>
                {
                    this.progressDeletingSite.Value = percent;
                    this.UpdateTotalProgress();
                });
                await Task.Run(() => IISController.DeleteSite(this.site.Id, deleteSiteProgress)).ConfigureAwait(true);

                // Try to delete AppPool if possible
                var deleteAppPoolProgress = new Progress<int>(percent =>
                {
                    this.progressDeleteAppPool.Value = percent;
                    this.UpdateTotalProgress();
                });
                await Task.Run(() => IISController.DeleteAppPool(this.site.ApplicationDefaults.ApplicationPoolName, deleteAppPoolProgress)).ConfigureAwait(true);
            }

            this.progressTotal.Value = this.progressTotal.Maximum;

            DialogController.ShowMessage(
                "Site Deleted",
                $"{this.site.Name} has been deleted successfully.",
                SystemIcons.Information,
                DialogController.DialogButtons.OK);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void UpdateTotalProgress()
        {
            this.progressTotal.Maximum =
                this.progressStopAppPool.Maximum +
                this.progressStopSite.Maximum +
                this.progressDeleteDatabae.Maximum +
                this.progressDeleteFiles.Maximum +
                this.progressRemoveHostEntry.Maximum +
                this.progressDeletingSite.Maximum +
                this.progressDeleteAppPool.Maximum;

            var totalValue =
                this.progressStopAppPool.Value +
                this.progressStopSite.Value +
                this.progressDeleteDatabae.Value +
                this.progressDeleteFiles.Value +
                this.progressRemoveHostEntry.Value +
                this.progressDeleteAppPool.Value;

            if (this.progressTotal.Value < this.progressTotal.Maximum)
            {
                this.progressTotal.Value = totalValue;
            }
        }

        private void StopAppPool(ServerManager iisManager, IProgress<int> progress)
        {
            progress?.Report(25);
            var appPool = iisManager.ApplicationPools.FirstOrDefault(a =>
                a.Name == this.site.ApplicationDefaults.ApplicationPoolName);

            if (appPool == null)
            {
                this.Abort(
                    "AppPool Not Found",
                    "There was a problem finding the application pool.\nAborting the site deletion.");
            }

            var totalSitesUsingAppPool = iisManager.Sites.Count(s => s.ApplicationDefaults.ApplicationPoolName == appPool.Name);
            if (totalSitesUsingAppPool > 1)
            {
                this.Abort(
                    "AppPool Not Specific",
                    "More than one site uses this application pool.\nAborting the site deletion.");
            }

            progress?.Report(50);

            try
            {
                appPool.AutoStart = false;
                if (appPool.State != ObjectState.Stopped)
                {
                    var state = appPool.Stop();
                    if (state != ObjectState.Stopped)
                    {
                        this.Abort(
                            "AppPool not Stopped",
                            "There was a problem stopping the application pool.\nAborting the site deletion.");
                    }
                }
            }
            catch (COMException)
            {
                this.Abort(
                    "AppPool Error",
                    "There was a problem stopping the application pool.\nAborting the site deletion.");
            }

            progress?.Report(100);
        }

        private void StopSite(ServerManager iisManager, IProgress<int> progress)
        {
            progress?.Report(25);
            var site = iisManager.Sites.FirstOrDefault(s => s.Id == this.site.Id);
            site.ServerAutoStart = false;

            progress.Report(50);
            if (site.State != ObjectState.Stopped)
            {
                try
                {
                    var state = site.Stop();
                    if (state != ObjectState.Stopped)
                    {
                        this.Abort(
                            "Stop Site Error",
                            "There was a problem stopping the site.\nAborting the site deletion.");
                    }
                }
                catch (COMException)
                {
                    this.Abort(
                        "Stop Site Error",
                        "There was a problem stopping the site.\nAborting the site deletion.");
                }
            }

            progress?.Report(100);
        }

        private void DeleteDatabase(IProgress<int> progress)
        {
            progress?.Report(25);
            var installFolder = Directory.GetParent(this.sitePath);
            var databaseController = new DatabaseController(
                this.site.Name.Split('.')[0],
                Properties.Settings.Default.DatabaseServerNameRecent,
                true,
                string.Empty,
                string.Empty,
                installFolder.FullName,
                true,
                this.site.Name);
            try
            {
                progress?.Report(75);
                databaseController.DropDatabase();
            }
            catch (DatabaseControllerException ex)
            {
                this.Abort(ex.Source, ex.Message);
            }

            progress?.Report(100);
        }

        private void Abort(string errorTitle, string errorDescription)
        {
            DialogController.ShowMessage(
                errorTitle,
                errorDescription,
                SystemIcons.Error,
                DialogController.DialogButtons.OK);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
