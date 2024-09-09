namespace CnCNet.LauncherStub;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Win32;

internal sealed class Program
{
    private const string Resources = "Resources";
    private const int DotNetMajorVersion = 8;
    private static readonly string DotNetBinariesFolder = $"BinariesNET{DotNetMajorVersion}";

    private static bool NetFrameworkEnabled = true;

    private static readonly string CurrentDirectory = new FileInfo(Assembly.GetEntryAssembly().Location).Directory.FullName;

    private const int ERROR_CANCELLED_CODE = 1223;

#if NETFRAMEWORK
    private static bool? _isMono;

    /// <summary>
    /// Gets a value whether or not the application is running under Mono. Uses lazy loading and caching.
    /// </summary>
    private static bool IsMono => _isMono ??= Type.GetType("Mono.Runtime") != null;
#endif

    private static OSVersion? _currentOSVersion = null;
    public static OSVersion CurrentOSVersion = _currentOSVersion ??= GetOperatingSystemVersion();

    #region .NET Framework Registry Keys
    // https://learn.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed#detect-net-framework-45-and-later-versions
    // private const int NET_FRAMEWORK_4_5_RELEASE_KEY = 378389;
    // private const int NET_FRAMEWORK_4_5_1_RELEASE_KEY = 378675;
    // private const int NET_FRAMEWORK_4_5_2_RELEASE_KEY = 379893;
    // private const int NET_FRAMEWORK_4_6_RELEASE_KEY = 393295;
    // private const int NET_FRAMEWORK_4_6_1_RELEASE_KEY = 394254;
    // private const int NET_FRAMEWORK_4_6_2_RELEASE_KEY = 394802;
    // private const int NET_FRAMEWORK_4_7_RELEASE_KEY = 460798;
    // private const int NET_FRAMEWORK_4_7_1_RELEASE_KEY = 461308;
    // private const int NET_FRAMEWORK_4_7_2_RELEASE_KEY = 461808;
    private const int NET_FRAMEWORK_4_8_RELEASE_KEY = 528040;
    // private const int NET_FRAMEWORK_4_8_1_RELEASE_KEY = 533320;
    #endregion

    private static readonly Uri XnaDownloadLink = new("https://www.microsoft.com/download/details.aspx?id=27598");
    private static readonly Uri NetFrameworkDownloadLink = new("https://dotnet.microsoft.com/download/dotnet-framework");

    #region .NET Download Links
    private static readonly Uri DotNetDownloadLink = new("https://dotnet.microsoft.com/download");
    private static readonly Uri DotNetX64RuntimeDownloadLink = new($"https://aka.ms/dotnet/{DotNetMajorVersion}.0/dotnet-runtime-win-x64.exe");
    private static readonly Uri DotNetX64DesktopRuntimeDownloadLink = new($"https://aka.ms/dotnet/{DotNetMajorVersion}.0/windowsdesktop-runtime-win-x64.exe");
    private static readonly Uri DotNetX86RuntimeDownloadLink = new($"https://aka.ms/dotnet/{DotNetMajorVersion}.0/dotnet-runtime-win-x86.exe");
    private static readonly Uri DotNetX86DesktopRuntimeDownloadLink = new($"https://aka.ms/dotnet/{DotNetMajorVersion}.0/windowsdesktop-runtime-win-x86.exe");
    private static readonly Uri DotNetArm64RuntimeDownloadLink = new($"https://aka.ms/dotnet/{DotNetMajorVersion}.0/dotnet-runtime-win-arm64.exe");
    private static readonly Uri DotNetArm64DesktopRuntimeDownloadLink = new($"https://aka.ms/dotnet/{DotNetMajorVersion}.0/windowsdesktop-runtime-win-arm64.exe");
    #endregion

    private static void RequireXna()
    {
        if (!IsXNAFramework4RefreshInstalled())
        {
            ShowMissingComponent("'Microsoft XNA Framework 4.0 Refresh'", XnaDownloadLink);
            Environment.Exit(2);
        }
    }

    private static void RequireDotNetFramework()
    {
        bool installed = IsDotNet4Installed(NET_FRAMEWORK_4_8_RELEASE_KEY);
        if (!installed)
        {
            if (CurrentOSVersion == OSVersion.WIN1011)
                ShowMissingComponent("'.NET Framework 4.8.1'", NetFrameworkDownloadLink);
            else
                ShowMissingComponent("'.NET Framework 4.8'", NetFrameworkDownloadLink);
            Environment.Exit(2);
        }
    }

    [STAThread]
    private static void Main(string[] args)
    {
        try
        {
            RemoveZoneIdentifer(CurrentDirectory);
        }
        catch (Exception ex)
        {
            AdvancedMessageBoxHelper.ShowOkMessageBox("An error occured when the launcher tries to unlock files downloaded from Internet. Re-run the launcher with administrator privileges might help.\n" + ex.ToString(), "Client Launcher Warning", okText: "Continue");
        }

        try
        {
            foreach (string arg in args)
            {
                switch (arg.ToUpperInvariant())
                {
                    case "-DX":
                        RunDX();
                        return;
                    case "-XNA":
                        RunXNA();
                        return;
                    case "-OGL":
                        RunOGL();
                        return;
                    case "-UGL":
                        NetFrameworkEnabled = false;
                        RunUGL();
                        return;
                    case "-NET8":
                        NetFrameworkEnabled = false;
                        break;
                    case "-DIALOGTEST":
                        RunDialogTest();
                        return;
                }
            }

#if DEBUG
            RunDialogTest();
#else
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

    private static void RemoveZoneIdentifer(string directory)
    {
        // https://stackoverflow.com/a/6375373

        List<string> failedFiles = [];

        // Enumerate all files recursively
        string[] files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
        string[] directories = Directory.GetDirectories(directory, "*", SearchOption.AllDirectories);

        // For each file or directory, remove the Zone.Identifier alternate data stream
        foreach (string file in files.Concat(directories))
        {
            string zoneIdentifier = file + ":Zone.Identifier";
            bool success = NativeMethods.DeleteFile(zoneIdentifier);
            if (!success)
            {
                uint err = NativeMethods.GetLastError();
                if (err == NativeConstants.ERROR_FILE_NOT_FOUND)
                    continue;

                failedFiles.Add(file);
            }
        }

        if (failedFiles.Count > 0)
            throw new Exception("Failed to remove Zone.Identifier from the following files:\n" + string.Join("\n", failedFiles));
    }

    private static void RunXNA()
    {
        RequireXna();
        if (NetFrameworkEnabled)
            StartProcessNetFramework(Resources + Path.DirectorySeparatorChar + "clientxna.exe");
        else
            StartProcessDotNet(Resources + Path.DirectorySeparatorChar + DotNetBinariesFolder + Path.DirectorySeparatorChar + "XNA" + Path.DirectorySeparatorChar + "clientxna.dll", run32Bit: true, runDesktop: true);
    }

    private static void RunOGL()
    {
        if (NetFrameworkEnabled)
            StartProcessNetFramework(Resources + Path.DirectorySeparatorChar + "clientogl.exe");
        else
            StartProcessDotNet(Resources + Path.DirectorySeparatorChar + DotNetBinariesFolder + Path.DirectorySeparatorChar + "OpenGL" + Path.DirectorySeparatorChar + "clientogl.dll", run32Bit: false, runDesktop: true);
    }

    private static void RunDX()
    {
        if (NetFrameworkEnabled)
            StartProcessNetFramework(Resources + Path.DirectorySeparatorChar + "clientdx.exe");
        else
            StartProcessDotNet(Resources + Path.DirectorySeparatorChar + DotNetBinariesFolder + Path.DirectorySeparatorChar + "Windows" + Path.DirectorySeparatorChar + "clientdx.dll", run32Bit: false, runDesktop: true);
    }

    private static void RunUGL()
    {
        Debug.Assert(!NetFrameworkEnabled);
        NetFrameworkEnabled = false;
        StartProcessDotNet(Resources + Path.DirectorySeparatorChar + DotNetBinariesFolder + Path.DirectorySeparatorChar + "UniversalGL" + Path.DirectorySeparatorChar + "clientogl.dll", run32Bit: false, runDesktop: false);
    }

    public enum OSVersion
    {
        UNKNOWN,
        WIN9X,
        WINXP,
        WINVISTA,
        WIN7,
        WIN8,
        WIN1011,
        UNIX
    }

    private static OSVersion GetOperatingSystemVersion()
    {
        Version osVersion = Environment.OSVersion.Version;

        if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
            return OSVersion.WIN9X;

        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            if (osVersion.Major < 5)
                return OSVersion.UNKNOWN;

            if (osVersion.Major == 5)
                return OSVersion.WINXP;

            if (osVersion.Major == 6 && osVersion.Minor == 0)
                return OSVersion.WINVISTA;

            if (osVersion.Major == 6 && osVersion.Minor == 1)
                return OSVersion.WIN7;

            if (osVersion.Major == 6 && osVersion.Minor >= 2)
                return OSVersion.WIN8;

            if (osVersion.Major >= 10)
                return OSVersion.WIN1011;

            return OSVersion.WIN7;
        }

        if (IsMono)
            return OSVersion.UNIX;

        // http://mono.wikia.com/wiki/Detecting_the_execution_platform
        int p = (int)Environment.OSVersion.Platform;
        if (p is 4 or 6 or 128)
        {
            return OSVersion.UNIX;
        }

        return OSVersion.UNKNOWN;
    }

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
        switch (CurrentOSVersion)
        {
            case OSVersion.WIN9X:
            case OSVersion.WINXP:
            case OSVersion.WINVISTA:
                ShowUnsupportedOSMessage();
                Environment.Exit(5);
                break;
            case OSVersion.WIN7:
            case OSVersion.WIN8:
            case OSVersion.WIN1011:
                W7And10Autorun();
                break;
            case OSVersion.UNIX:
            case OSVersion.UNKNOWN:
            default:
                RunOGL();
                break;
        }
    }

    private static void W7And10Autorun()
    {
        string basePath = CurrentDirectory + Path.DirectorySeparatorChar + "Client" + Path.DirectorySeparatorChar;
        string dxFailFilePath = basePath + ".dxfail";
        string oglFailFilePath = basePath + ".oglfail";

        if (File.Exists(dxFailFilePath))
        {
            if (File.Exists(oglFailFilePath))
            {
                if (IsXNAFramework4RefreshInstalled())
                {
                    RunXNA();
                    return;
                }

                int? result = ShowIncompatibleGPUMessage(["Open link", "Launch XNA version", "Launch DirectX11 version", "Exit"]);
                switch (result)
                {
                    case 0:
                        OpenUri(XnaDownloadLink);
                        return;
                    case 1:
                        RunXNA();
                        return;
                    case 2:
                        File.Delete(dxFailFilePath);
                        File.Delete(oglFailFilePath);
                        AutoRun();
                        return;
                    default:
                        Environment.Exit(4);
                        return;
                }
            }

            RunOGL();
            return;
        }

        RunDX();
    }

    private static void StartProcessDotNet(string relativePath, bool run32Bit = false, bool runDesktop = true)
    {
        if (!Environment.Is64BitOperatingSystem)
            run32Bit = true;

        string dotnetHost = CheckAndRetrieveDotNetHost(run32Bit ? "x86" : GetMachineArchitecture(), runDesktop);
        string absolutePath = CurrentDirectory + Path.DirectorySeparatorChar + relativePath;

        if (!File.Exists(absolutePath))
        {
            AdvancedMessageBoxHelper.ShowOkMessageBox($"Main client library ({relativePath}) not found!", "Client Launcher Error", okText: "Exit");

            Environment.Exit(3);
        }

        var processStartInfo = new ProcessStartInfo
        {
            FileName = dotnetHost,
            Arguments = "\"" + absolutePath + "\"",
            CreateNoWindow = true,
            UseShellExecute = false,
        };

        // Required on Win7 due to W^X causing issues there.
        if (CurrentOSVersion == OSVersion.WIN7)
            processStartInfo.EnvironmentVariables["DOTNET_EnableWriteXorExecute"] = "0";

        using var _ = Process.Start(processStartInfo);
    }

    private static string CheckAndRetrieveDotNetHost(string machineArchitecture, bool runDesktop)
    {
        // Architectures to be searched for
        List<string> architectures = [machineArchitecture];

        // Search for installed dotnet architectures
        string? availableArchitecture = null;
        foreach (string architecture in architectures)
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
                ? $"'.NET Desktop Runtime' version {DotNetMajorVersion} for architecture {machineArchitecture}"
                : $"'.NET Runtime' version {DotNetMajorVersion} for architecture {machineArchitecture}";
            ShowMissingComponent(missingComponent, GetDotNetDownloadLinks(machineArchitecture, runDesktop));
            Environment.Exit(2);
            return null;
        }
        else
        {
            return new FileInfo(GetDotNetHost(availableArchitecture)).FullName;
        }
    }

    private static string GetMachineArchitecture()
    {
#if NET471_OR_GREATER || NET || NETSTANDARD1_1_OR_GREATER
        return System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant();
#else
        return System.Environment.Is64BitOperatingSystem ? "x64" : "x86";
#endif
    }

    private static Uri GetDotNetDownloadLinks(string machineArchitecture, bool runDesktop)
    {
        if (runDesktop)
        {
            switch (machineArchitecture.ToLowerInvariant())
            {
                case "x64": return DotNetX64DesktopRuntimeDownloadLink;
                case "x86": return DotNetX86DesktopRuntimeDownloadLink;
                case "arm64": return DotNetArm64DesktopRuntimeDownloadLink;
                default: return DotNetDownloadLink;
            }
        }
        else
        {
            switch (machineArchitecture.ToLowerInvariant())
            {
                case "x64": return DotNetX64RuntimeDownloadLink;
                case "x86": return DotNetX86RuntimeDownloadLink;
                case "arm64": return DotNetArm64RuntimeDownloadLink;
                default: return DotNetDownloadLink;
            }
        }
    }

    private static void StartProcessNetFramework(string relativePath)
    {
        RequireDotNetFramework();

        string completeFilePath = CurrentDirectory + Path.DirectorySeparatorChar + relativePath;

        if (!File.Exists(completeFilePath))
        {
            throw new Exception("Main client executable (" + relativePath + ") not found!");
        }

        try
        {
            Process.Start(completeFilePath);
        }
        catch (Win32Exception ex)
        {
            if (ex.NativeErrorCode == ERROR_CANCELLED_CODE)
            {
                throw new Exception("Unable to launch the main client. It could be blocked by Windows SmartScreen."
                    + Environment.NewLine + Environment.NewLine +
                    "Please try to launch the following file manually: " + relativePath
                    + Environment.NewLine + Environment.NewLine +
                    "If the client still doesn't run, please contact the mod's authors for support.");
            }

            throw ex;
        }
    }

    private static void OpenUri(Uri uri)
    {
        using var _ = Process.Start(new ProcessStartInfo
        {
            FileName = uri.ToString(),
            UseShellExecute = true,
        });
    }

    private static void ShowMissingComponent(string missingComponent, Uri downloadLink)
    {
        bool dialogResult = AdvancedMessageBoxHelper.ShowYesNoMessageBox(
            string.Format(
            "The component {0} is missing.\n\n" +
            "You can download the installer from the following link:\n\n{1}",
            missingComponent,
            downloadLink.ToString()),
            "Component Missing",
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

    private static string? GetDotNetHost(string architecture)
    {
        // architecture: e.g., "x86", "x64", etc
        if (!IsDotNetCoreInstalled(architecture))
            return null;

        using var localMachine32BitRegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
        using RegistryKey? dotnetArchitectureKey = localMachine32BitRegistryKey.OpenSubKey(
            $"SOFTWARE\\dotnet\\Setup\\InstalledVersions\\{architecture}");
        string? installLocation = dotnetArchitectureKey?.GetValue("InstallLocation")?.ToString();

        return installLocation is null ? null : installLocation + Path.DirectorySeparatorChar + "dotnet.exe";
    }

    private static bool IsDotNetCoreInstalled(string architecture)
        => IsDotNetInstalled(architecture, "Microsoft.NETCore.App");

    private static bool IsDotNetDesktopInstalled(string architecture)
        => IsDotNetInstalled(architecture, "Microsoft.WindowsDesktop.App");

    private static bool IsDotNetInstalled(string architecture, string sharedFrameworkName)
    {
        using var localMachine32BitRegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
        using RegistryKey? dotnetSharedFrameworkKey = localMachine32BitRegistryKey.OpenSubKey(
            $"SOFTWARE\\dotnet\\Setup\\InstalledVersions\\{architecture}\\sharedfx\\{sharedFrameworkName}");

        return dotnetSharedFrameworkKey?.GetValueNames().Any(q =>
            q.StartsWith($"{DotNetMajorVersion}.", StringComparison.OrdinalIgnoreCase)
            && !q.Contains('-')
            && "1".Equals(dotnetSharedFrameworkKey.GetValue(q)?.ToString(), StringComparison.OrdinalIgnoreCase)) ?? false;
    }

    private static bool IsDotNet4Installed(int version = NET_FRAMEWORK_4_8_RELEASE_KEY)
    {
        using RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full", false);
        object? installValue = key?.GetValue("Release");
        int installValueInt = installValue != null ? (int)installValue : 0;

        return installValueInt >= version;
    }

    private static void ShowUnsupportedOSMessage()
    {
        AdvancedMessageBoxHelper.ShowOkMessageBox(
            "The client requires at least .NET Framework 4.8 to run, but it is not supported on your operating system." +
            "Please consider upgrading to a newer version of Windows.",
            "Unsupported Operating System",
            okText: "Exit");
    }
}