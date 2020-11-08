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
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.Serialization;

    /// <summary>
    /// Thrown when a database related error occurs.
    /// </summary>
    [Serializable]
    public class DatabaseControllerException : Exception
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
