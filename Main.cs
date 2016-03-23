using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Configuration;
using MetroFramework.Forms;
using Segment;
using JCS;

namespace nvQuickSite
{
    public partial class Main : MetroForm
    {
        public Main()
        {
            InitializeComponent();

            var userGuid = System.Guid.NewGuid().ToString("B").ToUpper();
            var operatingSystem = Environment.OSVersion.ToString();
            Analytics.Initialize("pzNi0MJVC1P9tVZdnvDOyptvUwPov9BN", new Config().SetAsync(false));
            Analytics.Client.Track(userGuid, "Started App", new Segment.Model.Properties() {
                { "datetime", DateTime.Now },
                { "os", OSVersionInfo.Name + " " + OSVersionInfo.Edition + " " + OSVersionInfo.ServicePack }
            });
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
