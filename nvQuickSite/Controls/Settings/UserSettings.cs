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

namespace nvQuickSite.Controls.Settings
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using MetroFramework.Controls;
    using MetroFramework.Forms;
    using Serilog;
    using Serilog.Core;
    using Serilog.Events;

    /// <summary>
    /// Implementes the user specific settings form.
    /// </summary>
    public partial class UserSettings : MetroForm
    {
        private readonly LoggingLevelSwitch loggingLevelSwitch;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSettings"/> class.
        /// </summary>
        /// <param name="loggingLevelSwitch">An object used to change logging level at runtime.</param>
        public UserSettings(Serilog.Core.LoggingLevelSwitch loggingLevelSwitch)
        {
            this.InitializeComponent();
            this.lblMessage.Text = "User Settings";
            this.chkShowReleaseCandidates.Checked = Properties.Settings.Default.ShowReleaseCandidates;
            this.chkShareStatistics.Checked = Properties.Settings.Default.ShareStatistics;
            this.chkEnableLocalPackageInstall.Checked = Properties.Settings.Default.EnableLocalPackageInstall;
            this.loggingLevelSwitch = loggingLevelSwitch;
            this.PopulateLoggingLevelSwitchCombo();
        }

        /// <summary>
        /// Gets a value indicating whether the user wants to display release candidates.
        /// </summary>
        public bool ShowReleaseCandidates => this.chkShowReleaseCandidates.Checked;

        /// <summary>
        /// Gets a value indicating whether the user wants to share statistics.
        /// </summary>
        public bool ShareStatistics => this.chkShareStatistics.Checked;

        /// <summary>
        /// Gets a value indicating whether the user wants to enable local package install.
        /// </summary>
        public bool EnableLocalPackageInstall => this.chkEnableLocalPackageInstall.Checked;

        private void PopulateLoggingLevelSwitchCombo()
        {
            this.cboLoggingLevel.Items.Clear();
            var levels = Enum.GetValues(typeof(LogEventLevel)).Cast<LogEventLevel>();
            foreach (var level in levels)
            {
                this.cboLoggingLevel.Items.Add(level);
            }

            this.cboLoggingLevel.SelectedItem = this.loggingLevelSwitch.MinimumLevel;
            this.cboLoggingLevel.SelectedValueChanged += this.CboLogginLevel_SelectedValueChanged;
        }

        private void CboLogginLevel_SelectedValueChanged(object sender, EventArgs e)
        {
            var cbo = (MetroComboBox)sender;
            var newLevel = (LogEventLevel)cbo.SelectedItem;

            // To log information about the level change we must:
            // 1. Force the level to Information (in case the level was set lower and it would not log information)
            // 2. Log information about the level change
            // 3. Change the level.
            this.loggingLevelSwitch.MinimumLevel = LogEventLevel.Information;
            Log.Logger.Information("Logging level changed to {newLevel}", newLevel);
            this.loggingLevelSwitch.MinimumLevel = newLevel;
        }

        private void lnkViewLogs_Click(object sender, EventArgs e)
        {
            var logsPath = Path.Combine(Environment.CurrentDirectory, "Logs");
            Process.Start("explorer.exe", logsPath);
        }

        private void logsIcon_Click(object sender, EventArgs e)
        {
            var logsPath = Path.Combine(Environment.CurrentDirectory, "Logs");
            Process.Start("explorer.exe", logsPath);
        }
    }
}
