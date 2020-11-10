namespace nvQuickSite
{
    partial class DeleteSiteProgress
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
            this.lblMessage = new MetroFramework.Controls.MetroLabel();
            this.progressTotal = new MetroFramework.Controls.MetroProgressBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.progressStopAppPool = new MetroFramework.Controls.MetroProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.progressStopSite = new MetroFramework.Controls.MetroProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.progressDeleteDatabae = new MetroFramework.Controls.MetroProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.progressDeleteFiles = new MetroFramework.Controls.MetroProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.progressRemoveHostEntry = new MetroFramework.Controls.MetroProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.progressDeletingSite = new MetroFramework.Controls.MetroProgressBar();
            this.label7 = new System.Windows.Forms.Label();
            this.progressDeleteAppPool = new MetroFramework.Controls.MetroProgressBar();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.lblMessage.Size = new System.Drawing.Size(121, 25);
            this.lblMessage.Style = MetroFramework.MetroColorStyle.Blue;
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Deleting Site";
            this.lblMessage.UseStyleColors = true;
            // 
            // progressTotal
            // 
            this.progressTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressTotal.HideProgressText = false;
            this.progressTotal.Location = new System.Drawing.Point(20, 279);
            this.progressTotal.Name = "progressTotal";
            this.progressTotal.Size = new System.Drawing.Size(530, 23);
            this.progressTotal.Style = MetroFramework.MetroColorStyle.Blue;
            this.progressTotal.TabIndex = 1;
            this.progressTotal.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.progressDeleteAppPool, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.progressDeletingSite, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.progressRemoveHostEntry, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.progressDeleteFiles, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.progressDeleteDatabae, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.progressStopSite, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.progressStopAppPool, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 55);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(530, 224);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stopping AppPool";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressStopAppPool
            // 
            this.progressStopAppPool.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressStopAppPool.Location = new System.Drawing.Point(116, 32);
            this.progressStopAppPool.Name = "progressStopAppPool";
            this.progressStopAppPool.Size = new System.Drawing.Size(418, 23);
            this.progressStopAppPool.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Stopping Site";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressStopSite
            // 
            this.progressStopSite.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressStopSite.Location = new System.Drawing.Point(116, 3);
            this.progressStopSite.Name = "progressStopSite";
            this.progressStopSite.Size = new System.Drawing.Size(418, 23);
            this.progressStopSite.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(3, 66);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Deleting Database";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressDeleteDatabae
            // 
            this.progressDeleteDatabae.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressDeleteDatabae.Location = new System.Drawing.Point(116, 61);
            this.progressDeleteDatabae.Name = "progressDeleteDatabae";
            this.progressDeleteDatabae.Size = new System.Drawing.Size(418, 23);
            this.progressDeleteDatabae.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(3, 95);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Deleting Files";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressDeleteFiles
            // 
            this.progressDeleteFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressDeleteFiles.Location = new System.Drawing.Point(116, 90);
            this.progressDeleteFiles.Name = "progressDeleteFiles";
            this.progressDeleteFiles.Size = new System.Drawing.Size(418, 23);
            this.progressDeleteFiles.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(3, 124);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Removing Host Entry";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressRemoveHostEntry
            // 
            this.progressRemoveHostEntry.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressRemoveHostEntry.Location = new System.Drawing.Point(116, 119);
            this.progressRemoveHostEntry.Name = "progressRemoveHostEntry";
            this.progressRemoveHostEntry.Size = new System.Drawing.Size(418, 23);
            this.progressRemoveHostEntry.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(3, 153);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Deleting Site";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressDeletingSite
            // 
            this.progressDeletingSite.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressDeletingSite.Location = new System.Drawing.Point(116, 148);
            this.progressDeletingSite.Name = "progressDeletingSite";
            this.progressDeletingSite.Size = new System.Drawing.Size(418, 23);
            this.progressDeletingSite.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(3, 182);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Deleting AppPool";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressDeleteAppPool
            // 
            this.progressDeleteAppPool.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressDeleteAppPool.Location = new System.Drawing.Point(116, 177);
            this.progressDeleteAppPool.Name = "progressDeleteAppPool";
            this.progressDeleteAppPool.Size = new System.Drawing.Size(418, 23);
            this.progressDeleteAppPool.TabIndex = 13;
            // 
            // DeleteSiteProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(570, 322);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.progressTotal);
            this.Controls.Add(this.lblMessage);
            this.DisplayHeader = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeleteSiteProgress";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Resizable = false;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel lblMessage;
        private MetroFramework.Controls.MetroProgressBar progressTotal;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroProgressBar progressStopAppPool;
        private MetroFramework.Controls.MetroProgressBar progressStopSite;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroProgressBar progressDeleteDatabae;
        private System.Windows.Forms.Label label3;
        private MetroFramework.Controls.MetroProgressBar progressDeleteFiles;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroProgressBar progressRemoveHostEntry;
        private System.Windows.Forms.Label label5;
        private MetroFramework.Controls.MetroProgressBar progressDeletingSite;
        private System.Windows.Forms.Label label6;
        private MetroFramework.Controls.MetroProgressBar progressDeleteAppPool;
        private System.Windows.Forms.Label label7;
    }
}