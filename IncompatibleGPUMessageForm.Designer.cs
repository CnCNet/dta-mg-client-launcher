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
            this.btnRunDX.Location = new System.Drawing.Point(99, 299);
            this.btnRunDX.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRunDX.Name = "btnRunDX";
            this.btnRunDX.Size = new System.Drawing.Size(244, 27);
            this.btnRunDX.TabIndex = 5;
            this.btnRunDX.Text = "Launch DirectX11 version";
            this.btnRunDX.UseVisualStyleBackColor = true;
            this.btnRunDX.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 15);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(376, 30);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "The client has detected an incompatibility between your graphics card\r\nand the Di" +
    "rectX11 and OpenGL versions of the CnCNet client.";
            // 
            // lblXNALink
            // 
            this.lblXNALink.AutoSize = true;
            this.lblXNALink.Location = new System.Drawing.Point(12, 145);
            this.lblXNALink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblXNALink.Name = "lblXNALink";
            this.lblXNALink.Size = new System.Drawing.Size(0, 15);
            this.lblXNALink.TabIndex = 2;
            this.lblXNALink.TabStop = true;
            this.lblXNALink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LblXNALink_LinkClicked);
            // 
            // lblXNADescription
            // 
            this.lblXNADescription.AutoSize = true;
            this.lblXNADescription.Location = new System.Drawing.Point(12, 68);
            this.lblXNADescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblXNADescription.Name = "lblXNADescription";
            this.lblXNADescription.Size = new System.Drawing.Size(395, 60);
            this.lblXNADescription.TabIndex = 1;
            this.lblXNADescription.Text = "The XNA version of the client could still work on your system, but it needs\r\nMicr" +
    "osoft XNA Framework 4.0 Refresh to be installed.\r\n\r\nYou can download the install" +
    "er from the following link:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 182);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(387, 45);
            this.label1.TabIndex = 3;
            this.label1.Text = "Alternatively, you can retry launching the DirectX11 version of the client.\r\n\r\nWe" +
    " apologize for the inconvenience.";
            // 
            // btnRunXNAOnce
            // 
            this.btnRunXNAOnce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunXNAOnce.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnRunXNAOnce.Location = new System.Drawing.Point(99, 265);
            this.btnRunXNAOnce.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRunXNAOnce.Name = "btnRunXNAOnce";
            this.btnRunXNAOnce.Size = new System.Drawing.Size(244, 27);
            this.btnRunXNAOnce.TabIndex = 4;
            this.btnRunXNAOnce.Text = "Launch XNA version";
            this.btnRunXNAOnce.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(99, 332);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(244, 27);
            this.button1.TabIndex = 6;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // IncompatibleGPUMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(442, 373);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRunXNAOnce);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblXNALink);
            this.Controls.Add(this.lblXNADescription);
            this.Controls.Add(this.btnRunDX);
            this.Controls.Add(this.lblDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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
        internal System.Windows.Forms.LinkLabel lblXNALink;
        private System.Windows.Forms.Label lblXNADescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRunXNAOnce;
        private System.Windows.Forms.Button button1;
    }
}