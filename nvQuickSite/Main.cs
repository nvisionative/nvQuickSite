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

namespace nvQuickSite
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    using JCS;
    using MetroFramework.Forms;
    using nvQuickSite.Controllers;
    using nvQuickSite.Controllers.Exceptions;
    using Segment;
    using Serilog;
    using Serilog.Core;

    /// <summary>
    /// Implements the logic of the main form.
    /// </summary>
    public partial class Main : MetroForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class.
        /// </summary>
        /// <param name="loggingLevelSwitch">An object that can be used to dynamically change the logging level at runtime.</param>
        public Main(LoggingLevelSwitch loggingLevelSwitch)
        {
            this.InitializeComponent();

            if (Properties.Settings.Default.UpdateSettings)
            {
                Log.Logger.Information("Updating user settings per new release.");
                Log.Logger.Information("Old User Settings {@settings}", Properties.Settings.Default);
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpdateSettings = false;
                Properties.Settings.Default.Save();
                Log.Logger.Information("New User Settings {@settings}", Properties.Settings.Default);
            }

            var osInfo = $"{OSVersionInfo.Name} {OSVersionInfo.Edition} {OSVersionInfo.ServicePack}";
            var versionString = OSVersionInfo.VersionString;
            Log.Logger.Information("Operating System Information {osInfo} {versionString}", osInfo, versionString);
            if (Properties.Settings.Default.ShareStatistics)
            {
                var userGuid = Guid.NewGuid().ToString("B").ToUpper(CultureInfo.InvariantCulture);
                Analytics.Initialize("pzNi0MJVC1P9tVZdnvDOyptvUwPov9BN", new Config().SetAsync(false));
                Analytics.Client.Track(
                    userGuid,
                    "Started App",
                    new Segment.Model.Properties()
                    {
                        {
                            "dimension1",
                            osInfo
                        },
                    });
            }

            this.lblVersion.Text = "v" + Application.ProductVersion;

            try
            {
                var latestVersion = VersionController.GetRemoteLatestVersion();
                if (Version.Parse(latestVersion) > Version.Parse(Application.ProductVersion))
                {
                    Log.Information("Version {version} is available.", latestVersion);
                    this.tileGetNewVersion.Visible = true;
                }
            }
            catch (VersionControllerException ex)
            {
                DialogController.ShowMessage(ex.Source, ex.Message, SystemIcons.Error, DialogController.DialogButtons.OK);
            }

            Start control = new Start(loggingLevelSwitch);
            control.Dock = DockStyle.Fill;
            this.Controls.Add(control);
        }

        private void footerImageLink_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.nvisionative.com");
        }

        private void tileGetNewVersion_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/nvisionative/nvQuickSite/releases/latest");
        }
    }
}
