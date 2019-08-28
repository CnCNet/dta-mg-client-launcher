using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DTALauncherStub
{
    public partial class XNAFrameworkMissingMessageForm : Form
    {
        public XNAFrameworkMissingMessageForm()
        {
            InitializeComponent();
        }

        private void lblLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.microsoft.com/en-us/download/details.aspx?id=27598");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
