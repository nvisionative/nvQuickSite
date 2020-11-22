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
    using System.Security.Principal;
    using System.Windows.Forms;

    using Serilog;
    using Serilog.Core;

    /// <summary>
    /// Implements the program entry point.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var loggingLevelSwitch = new LoggingLevelSwitch();
            var log = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(loggingLevelSwitch)
                .WriteTo.File(
                    path: "Logs\\log-.txt",
                    rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .WriteTo.Debug()
                .CreateLogger();
            log.Information("Application Started v{version}", Application.ProductVersion);
            Log.Logger = log;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            log.Debug("Current user is {@principal}", principal.Identity.Name);
            bool administrativeMode = principal.IsInRole(WindowsBuiltInRole.Administrator);

            if (!administrativeMode)
            {
                log.Information("Starting as an administrator.");
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.Verb = "runas";
                startInfo.FileName = Application.ExecutablePath;
                try
                {
                    Process.Start(startInfo);
                }
                catch
                {
                    throw;
                }

                return;
            }

            using (var main = new Main(loggingLevelSwitch))
            {
                Application.Run(main);
            }
        }
    }
}
