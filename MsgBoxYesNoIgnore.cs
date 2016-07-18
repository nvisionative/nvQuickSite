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
    public partial class MsgBoxYesNoIgnore : MetroForm
    {
        public MsgBoxYesNoIgnore(bool doNotWarnAgain, string dialogMessage, Image dialogIconImage)
        {
            InitializeComponent();
            chkDoNotWarnAgain.Checked = doNotWarnAgain;
            lblMessage.Text = dialogMessage;
            dialogIcon.Image = dialogIconImage;
        }

        public bool DoNotWarnAgain
        {
            get { return chkDoNotWarnAgain.Checked; }
        }

        //public string DialogMessage { get; set; }

        //public Image DialogIcon { get; set; }
    }
}
