using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DTALauncherStub
{
    public partial class NETFramework45MissingMessageForm : Form
    {
        public NETFramework45MissingMessageForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using var _ = Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.microsoft.com/en-US/download/details.aspx?id=30653",
                UseShellExecute = true
            });
        }
    }
}
