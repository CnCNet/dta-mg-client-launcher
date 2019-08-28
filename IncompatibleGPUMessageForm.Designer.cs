namespace DTALauncherStub
{
    partial class IncompatibleGPUMessageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncompatibleGPUMessageForm));
            this.btnRunDX = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblXNALink = new System.Windows.Forms.LinkLabel();
            this.lblXNADescription = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRunXNAOnce = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRunDX
            // 
            this.btnRunDX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunDX.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnRunDX.Location = new System.Drawing.Point(85, 259);
            this.btnRunDX.Name = "btnRunDX";
            this.btnRunDX.Size = new System.Drawing.Size(209, 23);
            this.btnRunDX.TabIndex = 5;
            this.btnRunDX.Text = "Launch DirectX11 version";
            this.btnRunDX.UseVisualStyleBackColor = true;
            this.btnRunDX.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(10, 13);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(336, 26);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "The client has detected an incompatibility between your graphics card\r\nand the Di" +
    "rectX11 version of the CnCNet client.";
            // 
            // lblXNALink
            // 
            this.lblXNALink.AutoSize = true;
            this.lblXNALink.Location = new System.Drawing.Point(10, 126);
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
            this.lblXNADescription.Location = new System.Drawing.Point(10, 59);
            this.lblXNADescription.Name = "lblXNADescription";
            this.lblXNADescription.Size = new System.Drawing.Size(357, 52);
            this.lblXNADescription.TabIndex = 9;
            this.lblXNADescription.Text = resources.GetString("lblXNADescription.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(346, 39);
            this.label1.TabIndex = 11;
            this.label1.Text = "Alternatively, you can retry launching the DirectX11 version of the client.\r\n\r\nWe" +
    " apologize for the inconvenience.";
            // 
            // btnRunXNAOnce
            // 
            this.btnRunXNAOnce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunXNAOnce.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnRunXNAOnce.Location = new System.Drawing.Point(85, 230);
            this.btnRunXNAOnce.Name = "btnRunXNAOnce";
            this.btnRunXNAOnce.Size = new System.Drawing.Size(209, 23);
            this.btnRunXNAOnce.TabIndex = 13;
            this.btnRunXNAOnce.Text = "Launch XNA version";
            this.btnRunXNAOnce.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(85, 288);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(209, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // IncompatibleGPUMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(379, 323);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRunXNAOnce);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblXNALink);
            this.Controls.Add(this.lblXNADescription);
            this.Controls.Add(this.btnRunDX);
            this.Controls.Add(this.lblDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IncompatibleGPUMessageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Graphics Card Incompatibility Detected";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRunDX;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.LinkLabel lblXNALink;
        private System.Windows.Forms.Label lblXNADescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRunXNAOnce;
        private System.Windows.Forms.Button button1;
    }
}