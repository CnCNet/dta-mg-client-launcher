namespace DTALauncherStub;

using System;
using System.Diagnostics;
using System.Windows.Forms;

internal sealed partial class IncompatibleGPUMessageForm : Form
{
    public IncompatibleGPUMessageForm()
    {
        InitializeComponent();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void LblXNALink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        using var _ = Process.Start(new ProcessStartInfo
        {
            FileName = "https://www.microsoft.com/en-us/download/details.aspx?id=27598",
            UseShellExecute = true
        });
    }
}