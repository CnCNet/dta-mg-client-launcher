namespace DTALauncherStub;

using System;
using System.Diagnostics;
using System.Windows.Forms;

internal sealed partial class DotNet32BitRuntimeMissingMessageForm : Form
{
    public DotNet32BitRuntimeMissingMessageForm()
    {
        InitializeComponent();
    }

    private void LblDotNetLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
        using var _ = Process.Start(new ProcessStartInfo
        {
            FileName = "https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-7.0.0-windows-x86-installer",
            UseShellExecute = true
        });
#pragma warning restore SA1312 // Variable names should begin with lower-case letter
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        Close();
    }
}