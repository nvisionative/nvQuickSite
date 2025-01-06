namespace nvQuickSite.Controls.Dialogs
{
    partial class MsgBoxOk
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
            this.lblTitle = new MetroFramework.Controls.MetroLabel();
            this.btnOk = new MetroFramework.Controls.MetroButton();
            this.dialogIcon = new System.Windows.Forms.PictureBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnGetHelp = new MetroFramework.Controls.MetroButton();
            this.linkLabelReportIssue = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dialogIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblTitle.Location = new System.Drawing.Point(116, 35);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(67, 19);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Message";
            // 
            // btnOk
            // 
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnOk.Location = new System.Drawing.Point(644, 198);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(112, 35);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            // 
            // dialogIcon
            // 
            this.dialogIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dialogIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.dialogIcon.Location = new System.Drawing.Point(6, 17);
            this.dialogIcon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dialogIcon.Name = "dialogIcon";
            this.dialogIcon.Size = new System.Drawing.Size(116, 118);
            this.dialogIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.dialogIcon.TabIndex = 4;
            this.dialogIcon.TabStop = false;
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(116, 66);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(626, 109);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Message";
            // 
            // btnGetHelp
            // 
            this.btnGetHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetHelp.Highlight = true;
            this.btnGetHelp.Location = new System.Drawing.Point(524, 198);
            this.btnGetHelp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGetHelp.Name = "btnGetHelp";
            this.btnGetHelp.Size = new System.Drawing.Size(112, 35);
            this.btnGetHelp.Style = MetroFramework.MetroColorStyle.Orange;
            this.btnGetHelp.TabIndex = 5;
            this.btnGetHelp.Text = "Get Help";
            // 
            // linkLabelReportIssue
            // 
            this.linkLabelReportIssue.AutoSize = true;
            this.linkLabelReportIssue.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.linkLabelReportIssue.Location = new System.Drawing.Point(416, 211);
            this.linkLabelReportIssue.Name = "linkLabelReportIssue";
            this.linkLabelReportIssue.Size = new System.Drawing.Size(101, 20);
            this.linkLabelReportIssue.TabIndex = 6;
            this.linkLabelReportIssue.TabStop = true;
            this.linkLabelReportIssue.Text = "Report Issue";
            this.linkLabelReportIssue.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.linkLabelReportIssue.Visible = false;
            this.linkLabelReportIssue.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            // 
            // MsgBoxOk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(790, 262);
            this.Controls.Add(this.linkLabelReportIssue);
            this.Controls.Add(this.btnGetHelp);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.dialogIcon);
            this.Controls.Add(this.btnOk);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MsgBoxOk";
            this.Padding = new System.Windows.Forms.Padding(30, 92, 30, 31);
            this.Resizable = false;
            ((System.ComponentModel.ISupportInitialize)(this.dialogIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel lblTitle;
        private MetroFramework.Controls.MetroButton btnOk;
        private System.Windows.Forms.PictureBox dialogIcon;
        private System.Windows.Forms.Label lblMessage;
        private MetroFramework.Controls.MetroButton btnGetHelp;
        private System.Windows.Forms.LinkLabel linkLabelReportIssue;
    }
}