//Copyright (c) 2016-2020 nvisionative, Inc.

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
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace nvQuickSite.Controllers
{
    public class DatabaseController
    {
        private readonly string dbName;
        private readonly string dbServerName;
        private readonly bool usesWindowsAuthentication;
        private readonly string dbUserName;
        private readonly string dbPassword;
        private readonly string installFolder;
        private readonly bool usesSiteSpecificAppPool;
        private readonly object siteName;

        public DatabaseController(
            string dbName,
            string dbServerName,
            bool usesWindowsAuthentication,
            string dbUserName,
            string dbPassword,
            string installFolder,
            bool usesSiteSpecificAppPool,
            string siteName)
        {
            this.dbName = dbName;
            this.dbServerName = dbServerName;
            this.usesWindowsAuthentication = usesWindowsAuthentication;
            this.dbUserName = dbUserName;
            this.dbPassword = dbPassword;
            this.installFolder = installFolder;
            this.usesSiteSpecificAppPool = usesSiteSpecificAppPool;
            this.siteName = siteName;
        }

        public void DropDatabase()
        {
            string myDBServerName = this.dbServerName;
            string connectionStringAuthSection = "";
            if (this.usesWindowsAuthentication)
            {
                connectionStringAuthSection = "Integrated Security=True;";
            }
            else
            {
                connectionStringAuthSection = "User ID=" + this.dbUserName + ";Password=" + this.dbPassword + ";";
            }

            SqlConnection myConn = new SqlConnection("Server=" + myDBServerName + "; Initial Catalog=master;" + connectionStringAuthSection);

            string str1 = @"USE master";
            string str2 = @"IF EXISTS(SELECT name FROM sys.databases WHERE name = '" + this.dbName + "')" +
                "DROP DATABASE [" + this.dbName + "]";

            SqlCommand myCommand1 = new SqlCommand(str1, myConn);
            SqlCommand myCommand2 = new SqlCommand(str2, myConn);
            try
            {
                myConn.Open();
                myCommand1.ExecuteNonQuery();
                myCommand2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DatabaseControllerException("Something when wrong while attempting to drop the database " + this.dbName, ex);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        public bool CreateDatabase()
        {
            string connectionStringAuthSection = "";
            string connectionTimeout = "Connection Timeout=5;";
            if (this.usesWindowsAuthentication)
            {
                connectionStringAuthSection = "Integrated Security=True;";
            }
            else
            {
                connectionStringAuthSection = $"USER ID={this.dbUserName};{this.dbPassword};";
            }

            SqlConnection myConn = new SqlConnection($"Server={this.dbServerName}; Initial Catalog=master;{connectionStringAuthSection}{connectionTimeout}");


            string str = "CREATE DATABASE [" + this.dbName + "] ON PRIMARY " +
                "(NAME = [" + this.dbName + "_Data], " +
                "FILENAME = '" + this.installFolder + "\\Database\\" + this.dbName + "_Data.mdf', " +
                "SIZE = 20MB, MAXSIZE = 200MB, FILEGROWTH = 10%) " +
                "LOG ON (NAME = [" + this.dbName + "_Log], " +
                "FILENAME = '" + this.installFolder + "\\Database\\" + this.dbName + "_Log.ldf', " +
                "SIZE = 13MB, " +
                "MAXSIZE = 50MB, " +
                "FILEGROWTH = 10%)";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new DatabaseControllerException($"Error creating database {this.dbName}", ex);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        internal void SetDatabasePermissions()
        {
            string connectionStringAuthSection = "";
            if (this.usesWindowsAuthentication)
            {
                connectionStringAuthSection = "Integrated Security=True;";
            }
            else
            {
                connectionStringAuthSection = $"User ID={this.dbUserName};Password={this.dbPassword};";
            }

            SqlConnection myConn = new SqlConnection($"Server={this.dbServerName}; Initial Catalog=master;{connectionStringAuthSection}");

            var appPoolNameFull = @"IIS APPPOOL\DefaultAppPool";
            var appPoolName = "DefaultAppPool";

            if (this.usesSiteSpecificAppPool)
            {
                appPoolNameFull = $@"IIS APPPOOL\{this.siteName}_nvQuickSite";
                appPoolName = $"{this.siteName}_nvQuickSite";
            }

            string str1 = "USE master";
            string str2 = "sp_grantlogin '" + appPoolNameFull + "'";
            string str3 = "USE [" + this.dbName + "]";
            string str4 = "sp_grantdbaccess '" + appPoolNameFull + "', '" + appPoolName + "'";
            string str5 = "sp_addrolemember 'db_owner', '" + appPoolName + "'";

            SqlCommand myCommand1 = new SqlCommand(str1, myConn);
            SqlCommand myCommand2 = new SqlCommand(str2, myConn);
            SqlCommand myCommand3 = new SqlCommand(str3, myConn);
            SqlCommand myCommand4 = new SqlCommand(str4, myConn);
            SqlCommand myCommand5 = new SqlCommand(str5, myConn);
            try
            {
                myConn.Open();
                myCommand1.ExecuteNonQuery();
                myCommand2.ExecuteNonQuery();
                myCommand3.ExecuteNonQuery();
                myCommand4.ExecuteNonQuery();
                myCommand5.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                throw new DatabaseControllerException($"Error setting database permissions for database {this.dbName}", ex);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }
    }

    [Serializable]
    internal class DatabaseControllerException : Exception
    {
        public DatabaseControllerException()
        {
        }

        public DatabaseControllerException(string message) : base(message)
        {
        }

        public DatabaseControllerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DatabaseControllerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
