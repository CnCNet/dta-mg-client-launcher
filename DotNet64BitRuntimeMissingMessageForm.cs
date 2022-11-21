namespace DTALauncherStub;

using System;
using System.Diagnostics;
using System.Windows.Forms;

internal sealed partial class DotNet64BitRuntimeMissingMessageForm : Form
{
    public DotNet64BitRuntimeMissingMessageForm()
    {
        InitializeComponent();
    }

    private void LblDotNetLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        using var _ = Process.Start(new ProcessStartInfo
        {
            FileName = "https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-7.0.0-windows-x64-installer",
            UseShellExecute = true
        });
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        Close();
    }
}