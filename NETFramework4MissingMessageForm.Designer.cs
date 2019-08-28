namespace DTALauncherStub
{
    partial class NETFramework4MissingMessageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NETFramework4MissingMessageForm));
            this.btnExit = new System.Windows.Forms.Button();
            this.lblLink = new System.Windows.Forms.LinkLabel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblSP3 = new System.Windows.Forms.Label();
            this.lblXNALink = new System.Windows.Forms.LinkLabel();
            this.lblXNADescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(159, 246);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(98, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblLink
            // 
            this.lblLink.AutoSize = true;
            this.lblLink.Location = new System.Drawing.Point(10, 82);
            this.lblLink.Name = "lblLink";
            this.lblLink.Size = new System.Drawing.Size(327, 13);
            this.lblLink.TabIndex = 4;
            this.lblLink.TabStop = true;
            this.lblLink.Text = "https://www.microsoft.com/en-us/download/details.aspx?id=17718";
            this.lblLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLink_LinkClicked);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(10, 13);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(401, 52);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = resources.GetString("lblDescription.Text");
            // 
            // lblSP3
            // 
            this.lblSP3.AutoSize = true;
            this.lblSP3.Location = new System.Drawing.Point(10, 114);
            this.lblSP3.Name = "lblSP3";
            this.lblSP3.Size = new System.Drawing.Size(311, 26);
            this.lblSP3.TabIndex = 6;
            this.lblSP3.Text = "If you\'re running Windows XP, you need to have Service Pack 3\r\ninstalled to be ab" +
    "le to install .NET Framework 4.0.";
            // 
            // lblXNALink
            // 
            this.lblXNALink.AutoSize = true;
            this.lblXNALink.Location = new System.Drawing.Point(10, 210);
            this.lblXNALink.Name = "lblXNALink";
            this.lblXNALink.Size = new System.Drawing.Size(327, 13);
            this.lblXNALink.TabIndex = 10;
            this.lblXNALink.TabStop = true;
            this.lblXNALink.Text = "https://www.microsoft.com/en-us/download/details.aspx?id=20914";
            this.lblXNALink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblXNALink_LinkClicked);
            // 
            // lblXNADescription
            // 
            this.lblXNADescription.AutoSize = true;
            this.lblXNADescription.Location = new System.Drawing.Point(10, 155);
            this.lblXNADescription.Name = "lblXNADescription";
            this.lblXNADescription.Size = new System.Drawing.Size(387, 39);
            this.lblXNADescription.TabIndex = 9;
            this.lblXNADescription.Text = "On Windows XP and Vista the client also requires Microsoft XNA Framework 4.0.\r\n\r\n" +
    "You can download the XNA Framework 4.0 installer from the following link:";
            // 
            // NETFramework4MissingMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(421, 281);
            this.ControlBox = false;
            this.Controls.Add(this.lblXNALink);
            this.Controls.Add(this.lblXNADescription);
            this.Controls.Add(this.lblSP3);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblLink);
            this.Controls.Add(this.lblDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NETFramework4MissingMessageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".NET Framework 4 Missing";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.LinkLabel lblLink;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblSP3;
        private System.Windows.Forms.LinkLabel lblXNALink;
        private System.Windows.Forms.Label lblXNADescription;
    }
}