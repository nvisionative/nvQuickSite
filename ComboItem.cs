using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNNQuickSite
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
