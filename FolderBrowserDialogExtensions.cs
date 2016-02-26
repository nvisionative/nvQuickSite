using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DNNQuickSite
{
    public static class FolderBrowserDialogExtensions
    {
        public static DialogResult ShowDialog(this FolderBrowserDialog dialog, ApplicationSettingsBase settings, string key)
        {
            return dialog.ShowDialog(settings[key].ToString(),
                                     path => {
                                         settings[key] = dialog.SelectedPath;
                                         settings.Save();
                                     });
        }

        public static DialogResult ShowDialog(this FolderBrowserDialog dialog, string defaultValue, Action<string> onAccept)
        {
            dialog.SelectedPath = defaultValue;
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                // check for null, if needed
                onAccept(dialog.SelectedPath);
            }
            return result;
        }
    }
}
