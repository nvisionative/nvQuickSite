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
    using MetroFramework.Forms;

    /// <summary>
    /// Implementes the user specific settings form.
    /// </summary>
    public partial class UserSettings : MetroForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSettings"/> class.
        /// </summary>
        public UserSettings()
        {
            this.InitializeComponent();
            this.lblMessage.Text = "User Settings";
            this.chkShowReleaseCandidates.Checked = Properties.Settings.Default.ShowReleaseCandidates;
            this.chkShareStatistics.Checked = Properties.Settings.Default.ShareStatistics;
        }

        /// <summary>
        /// Gets a value indicating whether the user wants to display release candidates.
        /// </summary>
        public bool ShowReleaseCandidates => this.chkShowReleaseCandidates.Checked;

        /// <summary>
        /// Gets a value indicating whether the user wants to share statistics.
        /// </summary>
        public bool ShareStatistics => this.chkShareStatistics.Checked;
    }
}
