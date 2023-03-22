namespace CnCNet.LauncherStub;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
    };

    private static bool automaticX86Fallback;

    [STAThread]
    private static void Main(string[] args)
    {
        try
        {
#if DEBUG
            RunDialogTest();
#else
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

            if (args.Any(q => q.Equals("-DialogTest", StringComparison.OrdinalIgnoreCase)))
            {
                RunDialogTest();
                return;
            }

            AutoRun();
#endif
        }
        catch (Exception ex)
        {
            AdvancedMessageBoxHelper.ShowOkMessageBox(ex.ToString(), "Client Launcher Error", okText: "Exit");
            Environment.Exit(1);
        }
    }

    private static void RunDialogTest()
    {
        var msgbox = new AdvancedMessageBox();
        var model = (AdvancedMessageBoxViewModel)msgbox.DataContext;
        model.Title = "Client Launcher Dialog Test";
        model.Message = "Click the buttons below.";
        model.Commands = new ObservableCollection<CommandViewModel>()
        {
            new CommandViewModel()
            {
                Text = "Show incompatible GPU dialog",
                Command = new RelayCommand(_ => ShowIncompatibleGPUMessage(new[] { "Open link (All buttons here won't work)", "Launch XNA version", "Launch DirectX11 version", "Exit" })),
            },

            new CommandViewModel()
            {
                Text = "Show missing component dialog",
                Command = new RelayCommand(_ => ShowMissingComponent("Component name here", new Uri("https://github.com/CnCNet/dta-mg-client-launcher"))),
            },

            new CommandViewModel()
            {
                Text = "Throw an exception",
                Command = new RelayCommand(_ => throw new Exception("Exception message here")),
            },

            new CommandViewModel()
            {
                Text = "Exit",
                Command = new RelayCommand(_ => msgbox.Close()),
            },
        };
        msgbox.ShowDialog();
    }

    private static void RunXNA()
    {
        if (!IsXNAFramework4RefreshInstalled())
        {
            ShowMissingComponent("'Microsoft XNA Framework 4.0 Refresh'", XnaDownloadLink);
            Environment.Exit(2);
        }

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

    private static int? ShowIncompatibleGPUMessage(string[] selections) => AdvancedMessageBoxHelper.ShowMessageBoxWithSelection(
            string.Format(
                "The client has detected an incompatibility between your graphics card\nand both the DirectX11 and OpenGL versions of the CnCNet client.\n\n" +
                "The XNA version of the client could still work on your system, but it needs\nMicrosoft XNA Framework 4.0 Refresh to be installed.\n\n" +
                "You can download the installer from the following link:\n\n" +
                "{0}\n\n" +
                "Alternatively, you can retry launching the DirectX11 version of the client.\n\n" +
                "We apologize for the inconvenience.", XnaDownloadLink.ToString()),
            "Graphics Card Incompatibility Detected",
            selections);

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

                int? result = ShowIncompatibleGPUMessage(new[] { "Open link", "Launch XNA version", "Launch DirectX11 version", "Exit" });
                switch (result)
                {
                    case 0:
                        OpenUri(XnaDownloadLink);
                        break;
                    case 1:
                        RunXNA();
                        break;
                    case 2:
                        dxFailFile.Delete();
                        oglFailFile.Delete();
                        AutoRun();
                        break;
                    case 3:
                    default:
                        Environment.Exit(4);
                        return;
                }
            }

            RunOGL();
        }

        RunDX();
    }

    private static void StartProcess(string relativePath, bool run32Bit = false, bool runDesktop = true)
    {
        if (!Environment.Is64BitOperatingSystem)
            run32Bit = true;

        FileInfo dotnetHost = CheckAndRetrieveDotNetHost(run32Bit ? Architecture.X86 : RuntimeInformation.OSArchitecture, runDesktop);
        string absolutePath = $"{Environment.CurrentDirectory}\\{relativePath}";

        if (!File.Exists(absolutePath))
        {
            AdvancedMessageBoxHelper.ShowOkMessageBox($"Main client library ({relativePath}) not found!", "Client Launcher Error", okText: "Exit");

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

    private static FileInfo CheckAndRetrieveDotNetHost(Architecture machineArchitecture, bool runDesktop)
    {
        // Architectures to be searched for
        List<Architecture> architectures = new() { machineArchitecture };
        if (automaticX86Fallback && machineArchitecture != Architecture.X86)
            architectures.Add(Architecture.X86);

        // Search for installed dotnet architectures
        Architecture? availableArchitecture = null;
        foreach (Architecture architecture in architectures)
        {
            if (IsDotNetCoreInstalled(architecture)
                && (!runDesktop || IsDotNetDesktopInstalled(architecture)))
            {
                availableArchitecture = architecture;
                break;
            }
        }

        // Prompt the download link and terminate the program if no architectures are available
        if (availableArchitecture is null)
        {
            string missingComponent = runDesktop
                ? $"'.NET Desktop Runtime' version {DotNetMajorVersion} for platform {machineArchitecture}"
                : $"'.NET Runtime' version {DotNetMajorVersion} for platform {machineArchitecture}";
            ShowMissingComponent(missingComponent, DotNetDownloadLinks[(machineArchitecture, runDesktop)]);
            Environment.Exit(2);
            return null;
        }
        else
        {
            FileInfo? dotnetHost = GetDotNetHost(availableArchitecture.GetValueOrDefault());
            return dotnetHost!;
        }
    }

    private static void OpenUri(Uri uri)
    {
        using var _ = Process.Start(new ProcessStartInfo
        {
            FileName = uri.ToString(),
            UseShellExecute = true
        });
    }

    private static void ShowMissingComponent(string missingComponent, Uri downloadLink)
    {
        bool dialogResult = AdvancedMessageBoxHelper.ShowYesNoMessageBox(
            string.Format(
            "The component {0} is missing.\n\n" +
            "You can download the installer from the following link:\n\n{1}",
            missingComponent, downloadLink.ToString()), "Component Missing",
            yesText: "Open link", noText: "Exit");
        if (dialogResult)
            OpenUri(downloadLink);
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