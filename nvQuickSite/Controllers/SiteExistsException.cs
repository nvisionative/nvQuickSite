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
    using System.Runtime.Serialization;

    /// <summary>
    /// Thrown when a site aleady exists and the user did not allow it's deletion.
    /// </summary>
    [Serializable]
    public class SiteExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteExistsException"/> class.
        /// </summary>
        public SiteExistsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteExistsException"/> class.
        /// </summary>
        /// <param name="message">A friendly message to display to the user.</param>
        public SiteExistsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteExistsException"/> class.
        /// </summary>
        /// <param name="message">A friendly message to display to the user.</param>
        /// <param name="innerException">The details of the exception that triggered this exception.</param>
        public SiteExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteExistsException"/> class.
        /// </summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected SiteExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
