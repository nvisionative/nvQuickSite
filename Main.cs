using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework.Components;

namespace DNNQuickSite
{
    public partial class Main : MetroForm
    {
        public Main()
        {
            InitializeComponent();
            //this.Icon = DNNQuickSite.Properties.Resources.DNN;

            Start control = new Start();
            control.Dock = DockStyle.Fill;
            this.Controls.Add(control);
        }
    }
}
