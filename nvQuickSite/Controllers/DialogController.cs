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

namespace nvQuickSite.Controllers
{
    using System.Drawing;
    using System.Windows.Forms;

    using nvQuickSite.Controls.Dialogs;

    /// <summary>
    /// Manages versions.
    /// </summary>
    public static class DialogController
    {
        /// <summary>
        /// Enumeration for the type of dialog buttons for the custom message box.
        /// </summary>
        internal enum DialogButtons
        {
            /// <summary>
            /// An OK button will be displayed.
            /// </summary>
            OK = 1,

            /// <summary>
            /// A Yes and No button will be displayed.
            /// </summary>
            YesNo = 2,

            /// <summary>
            /// A Yes and No button will be displayed, along with an Ignore checkbox.
            /// </summary>
            YesNoIgnore = 3,
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not to warn again.
        /// </summary>
        public static bool DoNotWarnAgain { get; set; }

        /// <summary>
        /// Shows a custom message box dialog.
        /// </summary>
        /// <param name="title">Title of the custom message box.</param>
        /// <param name="message">Message for the custom message box.</param>
        /// <param name="icon">Icon for the bitmap image (e.g., SystemIcons.Error, SystemIcons.Warning, SystemIcons.Information).</param>
        /// <param name="dialogButtons">Type of buttons to use for the custom message box.</param>
        /// <param name="doNotWarnAgain">User preference for not warning again with a dialog (i.e., no longer show the dialog).</param>
        /// <returns>DialogResult.</returns>
        internal static DialogResult ShowMessage(string title, string message, Icon icon, DialogButtons dialogButtons, bool doNotWarnAgain = false)
        {
            var dialogTitle = title;
            var dialogMessage = message;
            var dialogIcon = icon.ToBitmap();
            DialogResult result;
            switch (dialogButtons)
            {
                case DialogButtons.OK:
                    using (var messageBox = new MsgBoxOk(dialogTitle, dialogMessage, dialogIcon))
                    {
                        result = messageBox.ShowDialog();
                    }

                    break;
                case DialogButtons.YesNo:
                    using (var messageBox = new MsgBoxYesNo(dialogTitle, dialogMessage, dialogIcon))
                    {
                        result = messageBox.ShowDialog();
                    }

                    break;
                case DialogButtons.YesNoIgnore:
                    using (var messageBox = new MsgBoxYesNoIgnore(doNotWarnAgain, dialogTitle, dialogMessage, dialogIcon))
                    {
                        result = messageBox.ShowDialog();
                        DoNotWarnAgain = messageBox.DoNotWarnAgain;
                    }

                    break;
                default:
                    using (var messageBox = new MsgBoxOk(dialogTitle, dialogMessage, dialogIcon))
                    {
                        result = messageBox.ShowDialog();
                    }

                    break;
            }

            return result;
        }
    }
}
