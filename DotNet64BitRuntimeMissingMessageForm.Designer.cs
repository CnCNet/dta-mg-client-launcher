namespace DTALauncherStub;

partial class DotNet64BitRuntimeMissingMessageForm
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
            this.lblDotNetDescription = new System.Windows.Forms.Label();
            this.lblDotNetLink = new System.Windows.Forms.LinkLabel();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDotNetDescription
            // 
            this.lblDotNetDescription.AutoSize = true;
            this.lblDotNetDescription.Location = new System.Drawing.Point(12, 14);
            this.lblDotNetDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDotNetDescription.Name = "lblDotNetDescription";
            this.lblDotNetDescription.Size = new System.Drawing.Size(299, 45);
            this.lblDotNetDescription.TabIndex = 0;
            this.lblDotNetDescription.Text = "Your system is missing the 64bit .NET Desktop Runtime.\r\n\r\nYou can download the in" +
    "staller from the following link:\r\n";
            // 
            // lblDotNetLink
            // 
            this.lblDotNetLink.AutoSize = true;
            this.lblDotNetLink.LinkArea = new System.Windows.Forms.LinkArea(0, 62);
            this.lblDotNetLink.Location = new System.Drawing.Point(12, 71);
            this.lblDotNetLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDotNetLink.Name = "lblDotNetLink";
            this.lblDotNetLink.Size = new System.Drawing.Size(363, 15);
            this.lblDotNetLink.TabIndex = 1;
            this.lblDotNetLink.TabStop = true;
            this.lblDotNetLink.Text = "https://dotnet.microsoft.com/en-us/download/dotnet/7.0/runtime";
            this.lblDotNetLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LblDotNetLink_LinkClicked);
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
            // DotNet64BitRuntimeMissingMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(491, 172);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblDotNetLink);
            this.Controls.Add(this.lblDotNetDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DotNet64BitRuntimeMissingMessageForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "64bit .NET Desktop Runtime Missing";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblDotNetDescription;
    private System.Windows.Forms.LinkLabel lblDotNetLink;
    private System.Windows.Forms.Button btnExit;
}