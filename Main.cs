using System;
using System.Windows.Forms;
using System.Diagnostics;
using MetroFramework.Forms;

namespace nvQuickSite
{
    public partial class Main : MetroForm
    {
        public Main()
        {
            InitializeComponent();
            //this.Icon = nvQuickSite.Properties.Resources.DNN;

            Start control = new Start();
            control.Dock = DockStyle.Fill;
            Controls.Add(control);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.nvisionative.com"); 
        }
    }
}
