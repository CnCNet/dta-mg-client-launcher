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
            Process.Start("https://www.microsoft.com/en-us/download/details.aspx?id=30653");
        }
    }
}
