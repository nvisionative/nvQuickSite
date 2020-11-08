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
    using System.IO;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Xml.Linq;

    /// <summary>
    /// Controls file system operations.
    /// </summary>
    public static class FileSystemController
    {
        /// <summary>
        /// Checks if a directory is empty.
        /// </summary>
        /// <param name="path">The path to the directory to check.</param>
        /// <returns>A value indicating whether the directory is empty.</returns>
        internal static bool DirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        /// <summary>
        /// Adds the site alias to the operating system hosts file.
        /// </summary>
        /// <param name="siteName">The alias of the site.</param>
        internal static void UpdateHostsFile(string siteName)
        {
            try
            {
                string hostsFile = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\drivers\etc\hosts";

                var newEntry = "\t127.0.0.1 \t" + siteName;
                if (!File.ReadAllLines(hostsFile).Contains(newEntry))
                {
                    if (File.ReadAllText(hostsFile).EndsWith(Environment.NewLine, StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamWriter w = File.AppendText(hostsFile))
                        {
                            w.WriteLine(newEntry);
                        }
                    }
                    else
                    {
                        using (StreamWriter w = File.AppendText(hostsFile))
                        {
                            w.WriteLine(Environment.NewLine + newEntry);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                if (errorMessage.IndexOf("is denied", StringComparison.OrdinalIgnoreCase) > 0)
                {
                    errorMessage +=
                        "\r\r\nnvQuickSite is unable to add a new host entry to the above file. Please make sure the file is not read only. If it's not, make sure your antivirus software is not blocking changes made to the file. You can pause your antivirus software until nvQuickSite has completed its work, or add an exception for nvQuickSite in the antivirus software.";
                }

                throw new FileSystemControllerException(errorMessage, ex) { Source = "Update Hosts File" };
            }
        }

        /// <summary>
        /// Creates the directories needed to host the site.
        /// </summary>
        /// <param name="installFolder">The path to the installation folder.</param>
        /// <param name="siteName">The name of the website.</param>
        /// <param name="useSiteSpecificAppPool">A value indicating whether to use a site specific App Pool.</param>
        /// <param name="dbInstanceName">The name of the database instance.</param>
        /// <param name="dbServerName">The name of the database server.</param>
        /// <param name="usesWindowsAuthentication">A value indicating whether to use windows authentication for the databas access.</param>
        /// <param name="dbUserName">The database username.</param>
        /// <param name="dbPassword">The database password.</param>
        internal static void CreateDirectories(string installFolder, string siteName, bool useSiteSpecificAppPool, string dbInstanceName, string dbServerName, bool usesWindowsAuthentication, string dbUserName, string dbPassword)
        {
            var websiteDir = installFolder + "\\Website";
            var logsDir = installFolder + "\\Logs";
            var databaseDir = installFolder + "\\Database";

            var appPoolName = @"IIS APPPOOL\DefaultAppPool";
            var dbServiceAccount = GetDBServiceAccount(dbInstanceName);
            var authenticatedUsers = GetAuthenticatedUsersAccount();

            if (useSiteSpecificAppPool)
            {
                appPoolName = @"IIS APPPOOL\" + siteName + "_nvQuickSite";
            }

            if (!Directory.Exists(websiteDir))
            {
                Directory.CreateDirectory(websiteDir);
                SetFolderPermission(appPoolName, websiteDir);
                SetFolderPermission(authenticatedUsers, websiteDir);
            }
            else
            {
                DeleteDirectory(websiteDir);
                Directory.CreateDirectory(websiteDir);
                SetFolderPermission(appPoolName, websiteDir);
                SetFolderPermission(authenticatedUsers, websiteDir);
            }

            if (!Directory.Exists(logsDir))
            {
                Directory.CreateDirectory(logsDir);
                SetFolderPermission(dbServiceAccount, logsDir);
                SetFolderPermission(authenticatedUsers, logsDir);
            }
            else
            {
                DeleteDirectory(logsDir);
                Directory.CreateDirectory(logsDir);
                SetFolderPermission(dbServiceAccount, logsDir);
                SetFolderPermission(authenticatedUsers, logsDir);
            }

            if (!Directory.Exists(databaseDir))
            {
                Directory.CreateDirectory(databaseDir);
                SetFolderPermission(dbServiceAccount, databaseDir);
                SetFolderPermission(authenticatedUsers, databaseDir);
            }
            else
            {
                if (!DirectoryEmpty(databaseDir))
                {
                    var myDBFile = Directory.EnumerateFiles(databaseDir, "*.mdf").First().Split('_').First().Split('\\').Last();
                    DatabaseController databaseController = new DatabaseController(myDBFile, dbServerName, usesWindowsAuthentication, dbUserName, dbPassword, installFolder, useSiteSpecificAppPool, siteName);
                    databaseController.DropDatabase();
                }

                DeleteDirectory(databaseDir);
                Directory.CreateDirectory(databaseDir);
                SetFolderPermission(dbServiceAccount, databaseDir);
                SetFolderPermission(authenticatedUsers, databaseDir);
            }
        }

        /// <summary>
        /// Removes hosting directories for a site.
        /// </summary>
        /// <param name="installFolder">The path to the install folder.</param>
        internal static void RemoveDirectories(string installFolder)
        {
            var websiteDir = installFolder + "\\Website";
            var logsDir = installFolder + "\\Logs";
            var databaseDir = installFolder + "\\Database";

            DeleteDirectory(websiteDir);
            DeleteDirectory(logsDir);
            DeleteDirectory(databaseDir);
        }

        /// <summary>
        /// Modifies the website web.config file as needed.
        /// </summary>
        /// <param name="dbServerName">The name of the database server.</param>
        /// <param name="usesWindowsAuthentication">A value indicating whether to use windows authentication.</param>
        /// <param name="dbUserName">The database username.</param>
        /// <param name="dbPassword">The database password.</param>
        /// <param name="dbName">The database name.</param>
        /// <param name="installFolder">The path to the website hosting folder.</param>
        internal static void ModifyConfig(string dbServerName, bool usesWindowsAuthentication, string dbUserName, string dbPassword, string dbName, string installFolder)
        {
            try
            {
                string myDBServerName = dbServerName;
                string connectionStringAuthSection = string.Empty;
                if (usesWindowsAuthentication)
                {
                    connectionStringAuthSection = "Integrated Security=True;";
                }
                else
                {
                    connectionStringAuthSection = $"User ID={dbUserName};Password={dbPassword};";
                }

                string key = "SiteSqlServer";
                string value = $"Server={dbServerName};Database={dbName};{connectionStringAuthSection}";

                string path = Path.Combine(installFolder, "Website", "web.config");

                var config = XDocument.Load(path);
                var targetNode = config.Root.Element("connectionStrings").Element("add").Attribute("connectionString");
                targetNode.Value = value;

                var list = from appNode in config.Descendants("appSettings").Elements()
                           where appNode.Attribute("key").Value == key
                           select appNode;

                var e = list.FirstOrDefault();
                if (e != null)
                {
                    e.Attribute("value").SetValue(value);
                }

                config.Save(path);
            }
            catch (Exception ex)
            {
                throw new FileSystemControllerException("There was an error attempting to modify the web.config file", ex) { Source = "Modify Config" };
            }
        }

        private static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        private static string GetDBServiceAccount(string dbInstanceName)
        {
            string dbServiceAccount = @"NT Service\MSSQLSERVER";

            if (dbInstanceName.IndexOf(@"\", StringComparison.OrdinalIgnoreCase) > -1)
            {
                dbServiceAccount = @"NT Service\MSSQL$" + dbInstanceName.Substring(dbInstanceName.LastIndexOf(@"\", StringComparison.OrdinalIgnoreCase) + 1).ToUpperInvariant();
            }

            return dbServiceAccount;
        }

        private static string GetAuthenticatedUsersAccount()
        {
            var sid = new System.Security.Principal.SecurityIdentifier("S-1-5-11"); // Authenticated Users
            var account = sid.Translate(typeof(System.Security.Principal.NTAccount));
            return account.Value;
        }

        private static void SetFolderPermission(string accountName, string folderPath)
        {
            try
            {
                FileSystemRights rights;
                rights = FileSystemRights.Modify;
                bool modified;

                var accessRule = new FileSystemAccessRule(
                    accountName,
                    rights,
                    InheritanceFlags.None,
                    PropagationFlags.NoPropagateInherit,
                    AccessControlType.Allow);

                var dInfo = new DirectoryInfo(folderPath);
                var dSecurity = dInfo.GetAccessControl();
                dSecurity.ModifyAccessRule(AccessControlModification.Set, accessRule, out modified);

                var accessRule2 = new FileSystemAccessRule(
                    accountName,
                    rights,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.InheritOnly,
                    AccessControlType.Allow);

                dSecurity.ModifyAccessRule(AccessControlModification.Add, accessRule2, out modified);

                dInfo.SetAccessControl(dSecurity);
            }
            catch (Exception ex)
            {
                throw new FileSystemControllerException("There was a problem setting the folder permissions for folder path: " + folderPath, ex) { Source = "Set Folder Permission" };
            }
        }
    }
}
