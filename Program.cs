namespace DTALauncherStub;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

internal sealed class Program
{
    private const string Resources = "Resources";
    private const string Binaries = "Binaries";
    private const int DotNetMajorVersion = 7;

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

                if ("-UGL".Equals(arg, StringComparison.OrdinalIgnoreCase))
                {
                    RunUgl();
                    return;
                }
            }

            AutoRun();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Client Launcher Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
        }
    }

    private static void RunXna()
    {
        if (!IsXnaFramework4RefreshInstalled())
        {
            Application.Run(new XNAFrameworkMissingMessageForm());
            Environment.Exit(2);
        }

        StartProcess(
            Resources + Path.DirectorySeparatorChar + Binaries
            + Path.DirectorySeparatorChar + "XNA" + Path.DirectorySeparatorChar + "clientxna.dll",
            true);
    }

    private static void RunOgl()
    {
        StartProcess(Resources + Path.DirectorySeparatorChar + Binaries
            + Path.DirectorySeparatorChar + "OpenGL" + Path.DirectorySeparatorChar + "clientogl.dll");
    }

    private static void RunDx()
    {
        StartProcess(Resources + Path.DirectorySeparatorChar + Binaries
            + Path.DirectorySeparatorChar + "Windows" + Path.DirectorySeparatorChar + "clientdx.dll");
    }

    private static void RunUgl()
    {
        StartProcess(Resources + Path.DirectorySeparatorChar + Binaries
            + Path.DirectorySeparatorChar + "UniversalGL" + Path.DirectorySeparatorChar + "clientogl.dll");
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
        if (!Environment.Is64BitOperatingSystem)
            run32Bit = true;

        FileInfo? runtime64Bit = null;
        FileInfo? runtime32Bit = GetDotNetHost(Architecture.X86);

        if (run32Bit)
        {
            if (!(runtime32Bit?.Exists ?? false))
            {
                Application.Run(new DotNet32BitRuntimeMissingMessageForm());
                Environment.Exit(2);
            }
        }
        else if (Environment.Is64BitOperatingSystem)
        {
            runtime64Bit = GetDotNetHost(RuntimeInformation.OSArchitecture);

            if (!(runtime64Bit?.Exists ?? false))
            {
                Application.Run(new DotNet64BitRuntimeMissingMessageForm());
                Environment.Exit(2);
            }
        }

        string absolutePath = FormattableString.Invariant($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}{relativePath}");

        if (!File.Exists(absolutePath))
        {
            MessageBox.Show(
                FormattableString.CurrentCulture($"Main client library ({relativePath}) not found!"),
                "Client Launcher Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            Environment.Exit(3);
        }

#pragma warning disable SA1312 // Variable names should begin with lower-case letter
        using var _ = Process.Start(new ProcessStartInfo
        {
            FileName = run32Bit ? runtime32Bit!.FullName : runtime64Bit!.FullName,
            Arguments = "\"" + absolutePath + "\"",
            CreateNoWindow = true
        });
#pragma warning restore SA1312 // Variable names should begin with lower-case letter
    }

    private static bool IsXnaFramework4RefreshInstalled()
    {
        var localMachine32BitRegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
        using RegistryKey? xnaKey = localMachine32BitRegistryKey.OpenSubKey("SOFTWARE\\Microsoft\\XNA\\Framework\\v4.0");

        return "1".Equals(xnaKey?.GetValue("Refresh1Installed")?.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    private static FileInfo? GetDotNetHost(Architecture architecture)
    {
        if (!IsDotNetDesktopInstalled(architecture))
            return null;

        var localMachineRegistryKey = RegistryKey.OpenBaseKey(
            RegistryHive.LocalMachine, architecture is Architecture.X86 ? RegistryView.Registry32 : RegistryView.Registry64);
        using RegistryKey? dotnetKey = localMachineRegistryKey.OpenSubKey(
            FormattableString.Invariant($"SOFTWARE\\dotnet\\Setup\\InstalledVersions\\{architecture}\\sharedhost"));
        string? installLocation = dotnetKey?.GetValue("Path")?.ToString();

        return installLocation is null ? null : new FileInfo(FormattableString.Invariant($"{installLocation}\\dotnet.exe"));
    }

    private static bool IsDotNetDesktopInstalled(Architecture architecture)
    {
        var localMachine32BitRegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
        using RegistryKey? dotnetKey = localMachine32BitRegistryKey.OpenSubKey(
            FormattableString.Invariant($"SOFTWARE\\dotnet\\Setup\\InstalledVersions\\{architecture}\\sharedfx\\Microsoft.WindowsDesktop.App"));

        return dotnetKey?.GetValueNames().Any(q =>
            q.StartsWith(FormattableString.Invariant($"{DotNetMajorVersion}."), StringComparison.OrdinalIgnoreCase)
            && "1".Equals(dotnetKey.GetValue(q)?.ToString(), StringComparison.OrdinalIgnoreCase)) ?? false;
    }
}