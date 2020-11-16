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

namespace nvQuickSite.Controls.Sites
{
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;

    using MetroFramework.Forms;
    using Microsoft.Web.Administration;
    using nvQuickSite.Controllers;

    /// <summary>
    /// Implementes the user specific settings form.
    /// </summary>
    public partial class ViewExistingSites : MetroForm
    {
        private IOrderedEnumerable<Site> sites;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewExistingSites"/> class.
        /// </summary>
        public ViewExistingSites()
        {
            this.InitializeComponent();
            this.BindViewSitesDataGrid();
        }

        private void BindViewSitesDataGrid()
        {
            this.sites = IISController.GetSites(true).OrderBy(s => s.Name);
            using (var deleteButton = new DataGridViewButtonColumn())
            {
                deleteButton.Text = "Delete";
                deleteButton.Name = "Delete";
                deleteButton.HeaderText = string.Empty;

                this.dataGridViewSites.AllowUserToDeleteRows = true;
                this.dataGridViewSites.Columns.Clear();
                this.dataGridViewSites.Columns.Add(deleteButton);
                this.dataGridViewSites.Columns.Add("Id", "ID");
                this.dataGridViewSites.Columns.Add(new DataGridViewLinkColumn() { Name = "SiteName", HeaderText = "Site Name" });
                this.dataGridViewSites.Columns.Add("ApplicationPool", "Application Pool");
                this.dataGridViewSites.Columns.Add("State", "State");
                this.dataGridViewSites.Columns.Add("Path", "Path");

                this.dataGridViewSites.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                this.dataGridViewSites.Columns["Id"].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridViewSites.Columns["Path"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            this.dataGridViewSites.Rows.Clear();
            foreach (var site in this.sites)
            {
                this.dataGridViewSites.Rows.Add(
                    "Delete",
                    site.Id,
                    site.Name,
                    site.ApplicationDefaults.ApplicationPoolName,
                    site.State.ToString(),
                    site.Applications["/"].VirtualDirectories["/"].PhysicalPath);
            }

            this.dataGridViewSites.AutoResizeColumns();
            this.dataGridViewSites.CellContentClick += this.DataGridViewSites_CellContentClick;
        }

        private void DataGridViewSites_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var row = this.dataGridViewSites.Rows[e.RowIndex];
            var column = this.dataGridViewSites.Columns[e.ColumnIndex];
            var id = int.Parse(row.Cells["Id"].Value.ToString(), CultureInfo.InvariantCulture);
            var site = this.sites.FirstOrDefault(s => s.Id == id);

            if (column.Name == "Delete")
            {
                var result = DialogController.ShowMessage(
                    "Delete Site",
                    $"Are you sure you want to delete the following site?\n{site.Name}",
                    SystemIcons.Question,
                    DialogController.DialogButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    using (var deleteSiteProgress = new DeleteSiteProgress(site))
                    {
                        this.dataGridViewSites.CellContentClick -= this.DataGridViewSites_CellContentClick;
                        var deleteSiteResult = deleteSiteProgress.ShowDialog();
                        if (deleteSiteResult == DialogResult.OK)
                        {
                            this.BindViewSitesDataGrid();
                        }
                    }
                }
            }

            if (column.Name == "SiteName")
            {
                Process.Start($"{site.Bindings[0].Protocol}://{site.Bindings[0].Host}");
            }
        }
    }
}
