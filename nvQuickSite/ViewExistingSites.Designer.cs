namespace nvQuickSite
{
    partial class ViewExistingSites
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblMessage = new MetroFramework.Controls.MetroLabel();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.dataGridViewSites = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSites)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMessage.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblMessage.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblMessage.Location = new System.Drawing.Point(20, 30);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(124, 25);
            this.lblMessage.Style = MetroFramework.MetroColorStyle.Blue;
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Existing Sites";
            this.lblMessage.UseStyleColors = true;
            // 
            // metroButton2
            // 
            this.metroButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.metroButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.metroButton2.Dock = System.Windows.Forms.DockStyle.Right;
            this.metroButton2.Location = new System.Drawing.Point(752, 0);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(70, 32);
            this.metroButton2.TabIndex = 2;
            this.metroButton2.Text = "Cancel";
            // 
            // dataGridViewSites
            // 
            this.dataGridViewSites.AllowUserToAddRows = false;
            this.dataGridViewSites.AllowUserToDeleteRows = false;
            this.dataGridViewSites.AllowUserToOrderColumns = true;
            this.dataGridViewSites.AllowUserToResizeRows = false;
            this.dataGridViewSites.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewSites.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewSites.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSites.Location = new System.Drawing.Point(0, 10);
            this.dataGridViewSites.Margin = new System.Windows.Forms.Padding(30, 120, 30, 50);
            this.dataGridViewSites.MultiSelect = false;
            this.dataGridViewSites.Name = "dataGridViewSites";
            this.dataGridViewSites.ReadOnly = true;
            this.dataGridViewSites.RowHeadersVisible = false;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5);
            this.dataGridViewSites.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewSites.RowTemplate.DividerHeight = 3;
            this.dataGridViewSites.RowTemplate.Height = 32;
            this.dataGridViewSites.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSites.ShowCellErrors = false;
            this.dataGridViewSites.ShowCellToolTips = false;
            this.dataGridViewSites.ShowEditingIcon = false;
            this.dataGridViewSites.Size = new System.Drawing.Size(822, 471);
            this.dataGridViewSites.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.metroButton2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 546);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(822, 32);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridViewSites);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(20, 55);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.panel2.Size = new System.Drawing.Size(822, 491);
            this.panel2.TabIndex = 5;
            // 
            // ViewExistingSites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(862, 598);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblMessage);
            this.DisplayHeader = false;
            this.Name = "ViewExistingSites";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSites)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel lblMessage;
        private MetroFramework.Controls.MetroButton metroButton2;
        private System.Windows.Forms.DataGridView dataGridViewSites;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}