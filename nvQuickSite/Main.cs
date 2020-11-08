//Copyright (c) 2016-2020 nvisionative, Inc.

namespace nvQuickSite
{
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
    using System.Configuration;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Forms;

    using JCS;
    using MetroFramework.Forms;
    using nvQuickSite.Controllers;
    using Segment;

    public partial class Main : MetroForm
    {
        public Main()
        {
            InitializeComponent();

            if (Properties.Settings.Default.UpdateSettings)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpdateSettings = false;
                Properties.Settings.Default.Save();
            }

            if (Properties.Settings.Default.ShareStatistics)
            {
                var userGuid = System.Guid.NewGuid().ToString("B").ToUpper();
                Analytics.Initialize("pzNi0MJVC1P9tVZdnvDOyptvUwPov9BN", new Config().SetAsync(false));
                Analytics.Client.Track(userGuid, "Started App", new Segment.Model.Properties() {
                    //{ "datetime", DateTime.Now },
                    { "dimension1", OSVersionInfo.Name + " " + OSVersionInfo.Edition + " " + OSVersionInfo.ServicePack }
                });
            }

            lblVersion.Text = "v" + Application.ProductVersion;

            var latestVersion = VersionController.GetRemoteLatestVersion();
            if (Version.Parse(latestVersion) > Version.Parse(Application.ProductVersion))
            {
                tileGetNewVersion.Visible = true;
            }

            Start control = new Start();
            control.Dock = DockStyle.Fill;
            Controls.Add(control);
        }

        private bool SettingExists(string settingName)
        {
            return Properties.Settings.Default.Properties.Cast<SettingsProperty>().Any(prop => prop.Name == settingName);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.nvisionative.com"); 
        }

        private void tileGetNewVersion_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/nvisionative/nvQuickSite/releases/latest");
        }
    }
}
