namespace DTALauncherStub;

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
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
        using var _ = Process.Start(new ProcessStartInfo
        {
            FileName = e.Link!.LinkData as string,
            UseShellExecute = true
        });
#pragma warning restore SA1312 // Variable names should begin with lower-case letter
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        Close();
    }
}