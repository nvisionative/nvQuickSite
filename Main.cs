//Copyright (c) 2016 nvisionative, Inc.

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
//along with nvQuickSite.  If not, see<http://www.gnu.org/licenses/>.

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Configuration;
using MetroFramework.Forms;
using Segment;
using JCS;

namespace nvQuickSite
{
    public partial class Main : MetroForm
    {
        public Main()
        {
            InitializeComponent();

            var userGuid = System.Guid.NewGuid().ToString("B").ToUpper();
            Analytics.Initialize("pzNi0MJVC1P9tVZdnvDOyptvUwPov9BN", new Config().SetAsync(false));
            Analytics.Client.Track(userGuid, "Started App", new Segment.Model.Properties() {
            //    { "datetime", DateTime.Now },
                { "dimension1", OSVersionInfo.Name + " " + OSVersionInfo.Edition + " " + OSVersionInfo.ServicePack }
            });

            Start control = new Start();
            control.Dock = DockStyle.Fill;
            Controls.Add(control);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.nvisionative.com"); 
        }
    }
}
