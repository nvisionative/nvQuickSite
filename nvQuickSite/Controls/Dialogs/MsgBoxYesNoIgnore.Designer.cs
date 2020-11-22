namespace nvQuickSite.Controls.Dialogs
{
    partial class MsgBoxYesNoIgnore
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
            this.lblMessage = new MetroFramework.Controls.MetroLabel();
            this.btnYes = new MetroFramework.Controls.MetroButton();
            this.btnNo = new MetroFramework.Controls.MetroButton();
            this.chkDoNotWarnAgain = new MetroFramework.Controls.MetroCheckBox();
            this.dialogIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dialogIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblTitle.Location = new System.Drawing.Point(77, 23);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(67, 19);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Message";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(77, 43);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(60, 19);
            this.lblMessage.TabIndex = 5;
            this.lblMessage.Text = "Message";
            // 
            // btnYes
            // 
            this.btnYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnYes.Location = new System.Drawing.Point(330, 129);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 1;
            this.btnYes.Text = "Yes";
            // 
            // btnNo
            // 
            this.btnNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnNo.Location = new System.Drawing.Point(429, 129);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 23);
            this.btnNo.TabIndex = 2;
            this.btnNo.Text = "No";
            // 
            // chkDoNotWarnAgain
            // 
            this.chkDoNotWarnAgain.AutoSize = true;
            this.chkDoNotWarnAgain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkDoNotWarnAgain.Location = new System.Drawing.Point(17, 129);
            this.chkDoNotWarnAgain.Name = "chkDoNotWarnAgain";
            this.chkDoNotWarnAgain.Size = new System.Drawing.Size(140, 15);
            this.chkDoNotWarnAgain.TabIndex = 3;
            this.chkDoNotWarnAgain.Text = "Do not warn me again";
            this.chkDoNotWarnAgain.UseVisualStyleBackColor = true;
            // 
            // dialogIcon
            // 
            this.dialogIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dialogIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.dialogIcon.Location = new System.Drawing.Point(4, 11);
            this.dialogIcon.Name = "dialogIcon";
            this.dialogIcon.Size = new System.Drawing.Size(77, 77);
            this.dialogIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.dialogIcon.TabIndex = 4;
            this.dialogIcon.TabStop = false;
            // 
            // MsgBoxYesNoIgnore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(527, 170);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dialogIcon);
            this.Controls.Add(this.chkDoNotWarnAgain);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.lblMessage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MsgBoxYesNoIgnore";
            this.Resizable = false;
            ((System.ComponentModel.ISupportInitialize)(this.dialogIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel lblTitle;
        private MetroFramework.Controls.MetroLabel lblMessage;
        private MetroFramework.Controls.MetroButton btnYes;
        private MetroFramework.Controls.MetroButton btnNo;
        private MetroFramework.Controls.MetroCheckBox chkDoNotWarnAgain;
        private System.Windows.Forms.PictureBox dialogIcon;
    }
}