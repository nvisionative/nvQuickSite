//Copyright (c) 2016 nvisionative, Inc.

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
//along with Foobar.  If not, see<http://www.gnu.org/licenses/>.

namespace nvQuickSite
{
    class ComboItem
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public ComboItem(string name, string value)
        {
            Name = name; Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
