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
        using var _ = Process.Start(new ProcessStartInfo
        {
            FileName = e.Link!.LinkData as string,
            UseShellExecute = true
        });
    }

    private void BtnExit_Click(object sender, EventArgs e)
        => Close();
}