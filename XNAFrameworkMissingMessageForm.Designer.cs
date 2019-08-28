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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XNAFrameworkMissingMessageForm));
            this.btnExit = new System.Windows.Forms.Button();
            this.lblXNALink = new System.Windows.Forms.LinkLabel();
            this.lblXNADescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(159, 113);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(98, 23);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblXNALink
            // 
            this.lblXNALink.AutoSize = true;
            this.lblXNALink.Location = new System.Drawing.Point(10, 81);
            this.lblXNALink.Name = "lblXNALink";
            this.lblXNALink.Size = new System.Drawing.Size(327, 13);
            this.lblXNALink.TabIndex = 8;
            this.lblXNALink.TabStop = true;
            this.lblXNALink.Text = "https://www.microsoft.com/en-us/download/details.aspx?id=20914";
            this.lblXNALink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLink_LinkClicked);
            // 
            // lblXNADescription
            // 
            this.lblXNADescription.AutoSize = true;
            this.lblXNADescription.Location = new System.Drawing.Point(10, 12);
            this.lblXNADescription.Name = "lblXNADescription";
            this.lblXNADescription.Size = new System.Drawing.Size(394, 52);
            this.lblXNADescription.TabIndex = 7;
            this.lblXNADescription.Text = resources.GetString("lblXNADescription.Text");
            // 
            // XNAFrameworkMissingMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(421, 149);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblXNALink);
            this.Controls.Add(this.lblXNADescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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