namespace DTALauncherStub;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

internal sealed class Program
{
    private const string RESOURCES = "Resources";
    private const string BINARIES = "Binaries";
    private const int ERROR_CANCELLED_CODE = 1223;

    private static void Main(string[] args)
    {
        try
        {
            ApplicationConfiguration.Initialize();

            foreach (string arg in args)
            {
                if ("-XNA".Equals(arg, StringComparison.OrdinalIgnoreCase))
                {
                    RunXna();
                    return;
                }

                if ("-OGL".Equals(arg, StringComparison.OrdinalIgnoreCase))
                {
                    RunOgl();
                    return;
                }

                if ("-DX".Equals(arg, StringComparison.OrdinalIgnoreCase))
                {
                    RunDx();
                    return;
                }
            }

            AutoRun();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Console.ReadKey();
            Environment.Exit(1);
        }
    }

    private static void RunXna()
    {
        if (!IsXnaFramework4RefreshInstalled())
        {
            Application.Run(new XNAFrameworkMissingMessageForm());
            return;
        }

        StartProcess(
            RESOURCES + Path.DirectorySeparatorChar + BINARIES
            + Path.DirectorySeparatorChar + "XNA" + Path.DirectorySeparatorChar + "clientxna.dll",
            true);
    }

    private static void RunOgl()
    {
        StartProcess(RESOURCES + Path.DirectorySeparatorChar + BINARIES
            + Path.DirectorySeparatorChar + "OpenGL" + Path.DirectorySeparatorChar + "clientogl.dll");
    }

    private static void RunDx()
    {
        StartProcess(RESOURCES + Path.DirectorySeparatorChar + BINARIES
            + Path.DirectorySeparatorChar + "Windows" + Path.DirectorySeparatorChar + "clientdx.dll");
    }

    private static void AutoRun()
    {
        string basePath = Environment.CurrentDirectory +
            Path.DirectorySeparatorChar + "Client" + Path.DirectorySeparatorChar;
        string dxFailFilePath = basePath + ".dxfail";
        string oglFailFilePath = basePath + ".oglfail";

        if (File.Exists(dxFailFilePath))
        {
            if (File.Exists(oglFailFilePath))
            {
                if (IsXnaFramework4RefreshInstalled())
                {
                    RunXna();
                    return;
                }

                DialogResult dr = new IncompatibleGPUMessageForm().ShowDialog();

                if (dr == DialogResult.No)
                {
                    File.Delete(dxFailFilePath);
                    File.Delete(oglFailFilePath);
                    AutoRun();
                }
                else if (dr == DialogResult.Yes)
                {
                    RunXna();
                }

                return;
            }

            RunOgl();
        }

        RunDx();
    }

    private static void StartProcess(string relativePath, bool run32Bit = false)
    {
        string completeFilePath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + relativePath;
        FileInfo? runtime64Bit = new FileInfo("C:\\Program Files\\dotnet\\dotnet.exe");
        var runtime32Bit = new FileInfo("C:\\Program Files (x86)\\dotnet\\dotnet.exe");

        if (!Environment.Is64BitOperatingSystem)
        {
            runtime32Bit = runtime64Bit;
            runtime64Bit = null;
            run32Bit = true;
        }

        if (Environment.Is64BitOperatingSystem && !runtime64Bit!.Exists)
        {
            Application.Run(new DotNet64BitRuntimeMissingMessageForm());
            return;
        }

        if (run32Bit && !runtime32Bit.Exists)
        {
            Application.Run(new DotNet32BitRuntimeMissingMessageForm());
            return;
        }

        if (!File.Exists(completeFilePath))
        {
            MessageBox.Show(
                "Main client executable (" + relativePath + ") not found!",
                "Client Launcher Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        try
        {
            using var _ = Process.Start(new ProcessStartInfo
            {
                FileName = run32Bit ? runtime32Bit.FullName : runtime64Bit!.FullName,
                Arguments = "\"" + completeFilePath + "\"",
                CreateNoWindow = true
            });
        }
        catch (Win32Exception ex) when (ex.NativeErrorCode == ERROR_CANCELLED_CODE)
        {
            MessageBox.Show(
                "Unable to launch the main client. It could be blocked by Windows SmartScreen."
                + Environment.NewLine + Environment.NewLine +
                "Please contact the mod's authors for support.",
                "Client Launcher Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private static bool IsXnaFramework4RefreshInstalled()
    {
        try
        {
            var localMachine32BitRegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            RegistryKey? xnaKey = localMachine32BitRegistryKey.OpenSubKey("SOFTWARE\\Microsoft\\XNA\\Framework\\v4.0");
            string? installValue = xnaKey?.GetValue("Refresh1Installed")?.ToString();

            if (installValue == "1")
                return true;
        }
        catch
        {
        }

        return false;
    }
}