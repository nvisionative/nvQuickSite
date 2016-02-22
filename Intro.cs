using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace DNNQuickSite
{
    public partial class Intro : MetroUserControl
    {
        public Intro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start control = new Start();
            control.Dock = DockStyle.Fill;
            this.Controls.Clear();
            this.Controls.Add(control);
        }
    }
}
