namespace CnCNet.LauncherStub;

using System;
using System.Diagnostics;
using System.Windows.Forms;

internal sealed partial class ComponentMissingMessageForm : Form
{
    public ComponentMissingMessageForm()
    {
        InitializeComponent();
    }

    private void LblDotNetLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        using var _ = Process.Start(new ProcessStartInfo
        {
            FileName = ((Uri)e.Link!.LinkData).ToString(),
            UseShellExecute = true
        });
    }

    private void BtnExit_Click(object sender, EventArgs e)
        => Close();
}