﻿namespace CnCNet.LauncherStub;

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
        => Close();

    private void LblXNALink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        using var _ = Process.Start(new ProcessStartInfo
        {
            FileName = e.Link!.LinkData as string,
            UseShellExecute = true
        });
    }
}