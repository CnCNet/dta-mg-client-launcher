namespace DTALauncherStub
{
    partial class XNAFrameworkMissingMessageForm
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
            this.btnExit = new System.Windows.Forms.Button();
            this.lblXNALink = new System.Windows.Forms.LinkLabel();
            this.lblXNADescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(186, 130);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(114, 27);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // lblXNALink
            // 
            this.lblXNALink.AutoSize = true;
            this.lblXNALink.Location = new System.Drawing.Point(12, 93);
            this.lblXNALink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblXNALink.Name = "lblXNALink";
            this.lblXNALink.Size = new System.Drawing.Size(367, 15);
            this.lblXNALink.TabIndex = 1;
            this.lblXNALink.TabStop = true;
            this.lblXNALink.Text = "https://www.microsoft.com/en-us/download/details.aspx?id=20914";
            this.lblXNALink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LblLink_LinkClicked);
            // 
            // lblXNADescription
            // 
            this.lblXNADescription.AutoSize = true;
            this.lblXNADescription.Location = new System.Drawing.Point(12, 14);
            this.lblXNADescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblXNADescription.Name = "lblXNADescription";
            this.lblXNADescription.Size = new System.Drawing.Size(415, 60);
            this.lblXNADescription.TabIndex = 0;
            this.lblXNADescription.Text = "Your system is missing Microsoft XNA Framework 4.0 Refresh.\r\n\r\nTo run the XNA cli" +
    "ent, you need to have XNA Framework 4.0 Refresh installed.\r\nYou can download the" +
    " installer from the following link:";
            // 
            // XNAFrameworkMissingMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(491, 172);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblXNALink);
            this.Controls.Add(this.lblXNADescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XNAFrameworkMissingMessageForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XNA Framework Missing";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.LinkLabel lblXNALink;
        private System.Windows.Forms.Label lblXNADescription;
    }
}