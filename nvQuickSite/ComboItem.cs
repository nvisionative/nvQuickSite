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
    /// <summary>
    /// Represents a combo box item.
    /// </summary>
    public class ComboItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComboItem"/> class.
        /// </summary>
        /// <param name="name">The item name.</param>
        /// <param name="value">The item value.</param>
        public ComboItem(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the item name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the item value.
        /// </summary>
        public string Value { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
