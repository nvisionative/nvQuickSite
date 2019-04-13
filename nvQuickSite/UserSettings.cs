using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace nvQuickSite
{
    public partial class UserSettings : MetroForm
    {
        public UserSettings()
        {
            InitializeComponent();
            lblMessage.Text = "User Settings";
            chkShowReleaseCandidates.Checked = Properties.Settings.Default.ShowReleaseCandidates;
            chkShareStatistics.Checked = Properties.Settings.Default.ShareStatistics;
        }

        public bool ShowReleaseCandidates
        {
            get { return chkShowReleaseCandidates.Checked; }
        }

        public bool ShareStatistics
        {
            get { return chkShareStatistics.Checked; }
        }

        //public string DialogMessage { get; set; }

        //public Image DialogIcon { get; set; }
    }
}
