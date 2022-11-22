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

        StartProcess(GetClientProcessPath("XNA", "clientxna.dll"), true);
    }

    private static void RunOgl()
        => StartProcess(GetClientProcessPath("OpenGL", "clientogl.dll"));

    private static void RunDx()
        => StartProcess(GetClientProcessPath("Windows", "clientdx.dll"));

    private static void RunUgl()
        => StartProcess(GetClientProcessPath("UniversalGL", "clientogl.dll"), false, false);

    private static string GetClientProcessPath(string directory, string file)
        => FormattableString.Invariant($"{Resources}\\{Binaries}\\{directory}\\{file}");

    private static void AutoRun()
    {
        string basePath = FormattableString.Invariant($"{Environment.CurrentDirectory}\\Client\\");
        var dxFailFile = new FileInfo(FormattableString.Invariant($"{basePath}.dxfail"));
        var oglFailFile = new FileInfo(FormattableString.Invariant($"{basePath}.oglfail"));

        if (dxFailFile.Exists)
        {
            if (oglFailFile.Exists)
            {
                if (IsXnaFramework4RefreshInstalled())
                {
                    RunXna();
                    return;
                }

                DialogResult dialogResult = new IncompatibleGPUMessageForm().ShowDialog();

                switch (dialogResult)
                {
                    case DialogResult.No:
                        dxFailFile.Delete();
                        oglFailFile.Delete();
                        AutoRun();
                        break;
                    case DialogResult.Yes:
                        RunXna();
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            RunOgl();
        }

        RunDx();
    }

    private static void StartProcess(string relativePath, bool run32Bit = false, bool runDesktop = true)
    {
        if (!Environment.Is64BitOperatingSystem)
            run32Bit = true;

        FileInfo? dotnetHost;

        if (run32Bit)
        {
            dotnetHost = GetDotNetHost(Architecture.X86, runDesktop);

            if (!(dotnetHost?.Exists ?? false))
            {
                Application.Run(new DotNet32BitRuntimeMissingMessageForm());
                Environment.Exit(2);
            }
        }
        else
        {
            dotnetHost = GetDotNetHost(RuntimeInformation.OSArchitecture, runDesktop);

            if (!(dotnetHost?.Exists ?? false))
            {
                Application.Run(new DotNet64BitRuntimeMissingMessageForm());
                Environment.Exit(2);
            }
        }

        string absolutePath = FormattableString.Invariant($"{Environment.CurrentDirectory}\\{relativePath}");

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
            FileName = dotnetHost.FullName,
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

    private static FileInfo? GetDotNetHost(Architecture architecture, bool runDesktop)
    {
        if (runDesktop && !IsDotNetDesktopInstalled(architecture))
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