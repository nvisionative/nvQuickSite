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

using MetroFramework.Forms;

namespace nvQuickSite
{
    public partial class UserSettings : MetroForm
    {
        public UserSettings()
        {
            InitializeComponent();
            lblMessage.Text = "User Settings";
            chkShowReleaseCandidates.Checked = Properties.Settings.Default.ShowReleaseCandidates;
            chkShareStatistics.Checked = Properties.Settings.Default.ShareStatistics;
        }

        public bool ShowReleaseCandidates
        {
            get { return chkShowReleaseCandidates.Checked; }
        }

        public bool ShareStatistics
        {
            get { return chkShareStatistics.Checked; }
        }

        //public string DialogMessage { get; set; }

        //public Image DialogIcon { get; set; }
    }
}
