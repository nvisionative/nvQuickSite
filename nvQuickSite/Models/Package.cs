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

namespace nvQuickSite.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Represents one package.
    /// </summary>
    public class Package
    {
        /// <summary>
        /// Gets or sets the package distribution id.
        /// </summary>
        public string did { get; set; }

        /// <summary>
        /// Gets or sets the package name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the package version.
        /// </summary>
        [JsonConverter(typeof(VersionConverter))]
        public System.Version version { get; set; }

        /// <summary>
        /// Gets or sets the url to download the install package.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design",
            "CA1056:URI-like properties should not be strings",
            Justification = "Needs to be a simple string for serialization.")]
        public string url { get; set; }

        /// <summary>
        /// Gets or sets the url to download the upgrade package.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design",
            "CA1056:URI-like properties should not be strings",
            Justification = "Needs to be a simple string for serialization.")]
        public string upgradeurl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to keep this package.
        /// </summary>
        public bool keep { get; set; }
    }
}
