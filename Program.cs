namespace CnCNet.LauncherStub;

using System;
using System.Collections.Generic;
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

    private static readonly Uri XnaDownloadLink = new("https://www.microsoft.com/download/details.aspx?id=27598");
    private static readonly Uri DotNetX64RuntimeDownloadLink = new(FormattableString.Invariant($"https://aka.ms/dotnet/{DotNetMajorVersion}.0/dotnet-runtime-win-x64.exe"));
    private static readonly Uri DotNetX64DesktopRuntimeDownloadLink = new(FormattableString.Invariant($"https://aka.ms/dotnet/{DotNetMajorVersion}.0/windowsdesktop-runtime-win-x64.exe"));
    private static readonly Uri DotNetX86RuntimeDownloadLink = new(FormattableString.Invariant($"https://aka.ms/dotnet/{DotNetMajorVersion}.0/dotnet-runtime-win-x86.exe"));
    private static readonly Uri DotNetX86DesktopRuntimeDownloadLink = new(FormattableString.Invariant($"https://aka.ms/dotnet/{DotNetMajorVersion}.0/windowsdesktop-runtime-win-x86.exe"));
    private static readonly Uri DotNetArm64RuntimeDownloadLink = new(FormattableString.Invariant($"https://aka.ms/dotnet/{DotNetMajorVersion}.0/dotnet-runtime-win-arm64.exe"));
    private static readonly Uri DotNetArm64DesktopRuntimeDownloadLink = new(FormattableString.Invariant($"https://aka.ms/dotnet/{DotNetMajorVersion}.0/windowsdesktop-runtime-win-arm64.exe"));
    private static readonly IReadOnlyDictionary<(Architecture Architecture, bool Desktop), Uri> DotNetDownloadLinks = new Dictionary<(Architecture Architecture, bool Desktop), Uri>
    {
        { (Architecture.X64, false), DotNetX64RuntimeDownloadLink },
        { (Architecture.X64, true), DotNetX64DesktopRuntimeDownloadLink },
        { (Architecture.X86, false), DotNetX86RuntimeDownloadLink },
        { (Architecture.X86, true), DotNetX86DesktopRuntimeDownloadLink },
        { (Architecture.Arm64, false), DotNetArm64RuntimeDownloadLink },
        { (Architecture.Arm64, true), DotNetArm64DesktopRuntimeDownloadLink }
    }.AsReadOnly();

    private static bool automaticX86Fallback;

    [STAThread]
    private static void Main(string[] args)
    {
        try
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.SetHighDpiMode(HighDpiMode.SystemAware);

            automaticX86Fallback = !args.Any(q => q.Equals("-64Bit", StringComparison.OrdinalIgnoreCase));

            if (args.Any(q => q.Equals("-XNA", StringComparison.OrdinalIgnoreCase)))
            {
                RunXNA();
                return;
            }

            if (args.Any(q => q.Equals("-OGL", StringComparison.OrdinalIgnoreCase)))
            {
                RunOGL();
                return;
            }

            if (args.Any(q => q.Equals("-DX", StringComparison.OrdinalIgnoreCase)))
            {
                RunDX();
                return;
            }

            if (args.Any(q => q.Equals("-UGL", StringComparison.OrdinalIgnoreCase)))
            {
                RunUGL();
                return;
            }

            AutoRun();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Client Launcher Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
        }
    }

    private static void RunXNA()
    {
        if (!IsXNAFramework4RefreshInstalled())
            ShowMissingComponentForm("'Microsoft XNA Framework 4.0 Refresh'", XnaDownloadLink);

        StartProcess(GetClientProcessPath("XNA", "clientxna.dll"), true);
    }

    private static void RunOGL()
        => StartProcess(GetClientProcessPath("OpenGL", "clientogl.dll"));

    private static void RunDX()
        => StartProcess(GetClientProcessPath("Windows", "clientdx.dll"));

    private static void RunUGL()
        => StartProcess(GetClientProcessPath("UniversalGL", "clientogl.dll"), false, false);

    private static string GetClientProcessPath(string directory, string file)
        => $"{Resources}\\{Binaries}\\{directory}\\{file}";

    private static void AutoRun()
    {
        string basePath = $"{Environment.CurrentDirectory}\\Client\\";
        var dxFailFile = new FileInfo($"{basePath}.dxfail");
        var oglFailFile = new FileInfo($"{basePath}.oglfail");

        if (dxFailFile.Exists)
        {
            if (oglFailFile.Exists)
            {
                if (IsXNAFramework4RefreshInstalled())
                {
                    RunXNA();
                    return;
                }

                using var incompatibleGpuForm = new IncompatibleGPUMessageForm();

                SetLinkLabelUrl(incompatibleGpuForm.lblXNALink, XnaDownloadLink);

                switch (incompatibleGpuForm.ShowDialog())
                {
                    case DialogResult.No:
                        dxFailFile.Delete();
                        oglFailFile.Delete();
                        AutoRun();
                        break;
                    case DialogResult.Yes:
                        RunXNA();
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            RunOGL();
        }

        RunDX();
    }

    private static void SetLinkLabelUrl(LinkLabel linkLabel, Uri uri)
    {
        linkLabel.Text = uri.ToString();
        linkLabel.Links[0].LinkData = uri;
    }

    private static void StartProcess(string relativePath, bool run32Bit = false, bool runDesktop = true)
    {
        if (!Environment.Is64BitOperatingSystem)
            run32Bit = true;

        FileInfo dotnetHost = CheckAndRetrieveDotNetHost(run32Bit ? Architecture.X86 : RuntimeInformation.OSArchitecture, runDesktop);
        string absolutePath = $"{Environment.CurrentDirectory}\\{relativePath}";

        if (!File.Exists(absolutePath))
        {
            MessageBox.Show(
                $"Main client library ({relativePath}) not found!",
                "Client Launcher Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            Environment.Exit(3);
        }

        using var _ = Process.Start(new ProcessStartInfo
        {
            FileName = dotnetHost.FullName,
            Arguments = "\"" + absolutePath + "\"",
            CreateNoWindow = true,
            UseShellExecute = false,
        });
    }

    private static FileInfo CheckAndRetrieveDotNetHost(Architecture architecture, bool runDesktop)
    {
        if (architecture is not Architecture.X86 && automaticX86Fallback
            && ((runDesktop && !IsDotNetDesktopInstalled(architecture)) || !IsDotNetCoreInstalled(architecture)))
        {
            architecture = Architecture.X86;
        }

        if (runDesktop && !IsDotNetDesktopInstalled(architecture))
        {
            string missingComponent = $"'.NET Desktop Runtime' version {DotNetMajorVersion} for platform {architecture}";

            ShowMissingComponentForm(missingComponent, DotNetDownloadLinks[(architecture, true)]);
        }

        FileInfo? dotnetHost = GetDotNetHost(architecture);

        if (!(dotnetHost?.Exists ?? false))
        {
            string missingComponent = $"'.NET Runtime' version {DotNetMajorVersion} for platform {architecture}";

            ShowMissingComponentForm(missingComponent, DotNetDownloadLinks[(architecture, false)]);
        }

        return dotnetHost!;
    }

    private static void ShowMissingComponentForm(string missingComponent, Uri downloadLink)
    {
        using var messageForm = new ComponentMissingMessageForm();

        messageForm.lblDescription.Text = $"The component {missingComponent} is missing.";

        SetLinkLabelUrl(messageForm.lblLink, downloadLink);
        Application.Run(messageForm);
        Environment.Exit(2);
    }

    private static bool IsXNAFramework4RefreshInstalled()
    {
        using var localMachine32BitRegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
        using RegistryKey? xnaKey = localMachine32BitRegistryKey.OpenSubKey("SOFTWARE\\Microsoft\\XNA\\Framework\\v4.0");

        return "1".Equals(xnaKey?.GetValue("Refresh1Installed")?.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    private static FileInfo? GetDotNetHost(Architecture architecture)
    {
        if (!IsDotNetCoreInstalled(architecture))
            return null;

        using var localMachine32BitRegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
        using RegistryKey? dotnetArchitectureKey = localMachine32BitRegistryKey.OpenSubKey(
            $"SOFTWARE\\dotnet\\Setup\\InstalledVersions\\{architecture}");
        string? installLocation = dotnetArchitectureKey?.GetValue("InstallLocation")?.ToString();

        return installLocation is null ? null : new FileInfo($"{installLocation}\\dotnet.exe");
    }

    private static bool IsDotNetCoreInstalled(Architecture architecture)
        => IsDotNetInstalled(architecture, "Microsoft.NETCore.App");

    private static bool IsDotNetDesktopInstalled(Architecture architecture)
        => IsDotNetInstalled(architecture, "Microsoft.WindowsDesktop.App");

    private static bool IsDotNetInstalled(Architecture architecture, string sharedFrameworkName)
    {
        using var localMachine32BitRegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
        using RegistryKey? dotnetSharedFrameworkKey = localMachine32BitRegistryKey.OpenSubKey(
            $"SOFTWARE\\dotnet\\Setup\\InstalledVersions\\{architecture}\\sharedfx\\{sharedFrameworkName}");

        return dotnetSharedFrameworkKey?.GetValueNames().Any(q =>
            q.StartsWith($"{DotNetMajorVersion}.", StringComparison.OrdinalIgnoreCase)
            && !q.Contains('-')
            && "1".Equals(dotnetSharedFrameworkKey.GetValue(q)?.ToString(), StringComparison.OrdinalIgnoreCase)) ?? false;
    }
}