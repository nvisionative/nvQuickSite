﻿// Copyright (c) 2016-2020 nvisionative, Inc.
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

namespace nvQuickSite.Controls.Dialogs
{
    using System;
    using System.Drawing;

    using MetroFramework.Forms;

    /// <summary>
    /// A custom messagebox with yes, no and ignore.
    /// </summary>
    public partial class MsgBoxOk : MetroForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MsgBoxOk"/> class.
        /// </summary>
        /// <param name="dialogTitle">The title to show in the dialog.</param>
        /// <param name="dialogMessage">The message to show in the dialog.</param>
        /// <param name="dialogIconImage">The image to use as the dialog icon.</param>
        /// <param name="exception">Whether or not the dialog is an exception.</param>
        public MsgBoxOk(string dialogTitle, string dialogMessage, Image dialogIconImage, bool exception = false)
        {
            this.InitializeComponent();
            this.lblTitle.Text = dialogTitle;
            this.lblMessage.Text = dialogMessage;
            this.dialogIcon.Image = dialogIconImage;
            this.btnGetHelp.Visible = exception;
            this.btnGetHelp.Click += (sender, e) =>
            {
                this.linkLabelReportIssue.Visible = true;
                System.Diagnostics.Process.Start($"https://github.com/nvisionative/nvQuickSite/issues?q=is%3Aissue+{dialogTitle}+{dialogMessage}");
            };
            this.linkLabelReportIssue.Click += (sender, e) =>
            {
                System.Diagnostics.Process.Start($"https://github.com/nvisionative/nvQuickSite/issues/new?title=[In-App%20Issue]{dialogTitle}&body={dialogMessage}");
            };
        }
    }
}
