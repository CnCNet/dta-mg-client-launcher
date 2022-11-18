using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace DTALauncherStub
{
    internal sealed class Program
    {
        private const string RESOURCES = "Resources";
        private const int ERROR_CANCELLED_CODE = 1223;

        private static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            OSVersion osVersion = GetOperatingSystemVersion();

            if (args != null)
            {
                foreach (string arg in args)
                {
                    if ("-XNA".Equals(arg, StringComparison.OrdinalIgnoreCase))
                    {
                        RunXNA();
                        return;
                    }

                    if ("-OGL".Equals(arg, StringComparison.OrdinalIgnoreCase))
                    {
                        RunOGL();
                        return;
                    }

                    if ("-DX".Equals(arg, StringComparison.OrdinalIgnoreCase))
                    {
                        RunDX();
                        return;
                    }
                }
            }

            switch (osVersion)
            {
                case OSVersion.WIN:
                    W7And10Autorun();
                    break;
                default:
                    RunOGL();
                    break;
            }
        }

        private static void RunXNA()
        {
            if (!IsXNAFramework4RefreshInstalled())
            {
                Application.Run(new XNAFrameworkMissingMessageForm());
                return;
            }

            StartProcess(RESOURCES + Path.DirectorySeparatorChar + "Binaries" + Path.DirectorySeparatorChar + "XNA" + Path.DirectorySeparatorChar + "clientxna.dll");
        }

        private static void RunOGL()
        {
            StartProcess(RESOURCES + Path.DirectorySeparatorChar + "Binaries" + Path.DirectorySeparatorChar + "OpenGL" + Path.DirectorySeparatorChar + "clientogl.dll");
        }

        private static void RunDX()
        {
            StartProcess(RESOURCES + Path.DirectorySeparatorChar + "Binaries" + Path.DirectorySeparatorChar + "Windows" + Path.DirectorySeparatorChar + "clientdx.dll");
        }

        private static void W7And10Autorun()
        {
            string basePath = Environment.CurrentDirectory +
                Path.DirectorySeparatorChar + "Client" + Path.DirectorySeparatorChar;
            string dxFailFilePath = basePath + ".dxfail";

            if (File.Exists(dxFailFilePath))
            {
                if (IsXNAFramework4RefreshInstalled())
                {
                    RunXNA();
                    return;
                }

                DialogResult dr = new IncompatibleGPUMessageForm().ShowDialog();
                if (dr == DialogResult.No)
                {
                    File.Delete(dxFailFilePath);
                    RunDX();
                }
                else if (dr == DialogResult.Yes)
                {
                    RunXNA();
                }

                return;
            }

            RunDX();
        }

        private static void StartProcess(string relativePath)
        {
            string completeFilePath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + relativePath;

            if (!File.Exists(completeFilePath))
            {
                MessageBox.Show("Main client executable (" + relativePath + ") not found!",
                    "Client Launcher Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Process.Start("dotnet", completeFilePath);
            }
            catch (Win32Exception ex)
            {
                if (ex.NativeErrorCode == ERROR_CANCELLED_CODE)
                {
                    MessageBox.Show("Unable to launch the main client. It could be blocked by Windows SmartScreen."
                        + Environment.NewLine + Environment.NewLine +
                        "Please try to launch the following file manually: " + relativePath
                        + Environment.NewLine + Environment.NewLine +
                        "If the client still doesn't run, please contact the mod's authors for support.",
                        "Client Launcher Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static OSVersion GetOperatingSystemVersion()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                return OSVersion.WIN;

            return OSVersion.UNKNOWN;
        }

        private static bool IsXNAFramework4RefreshInstalled()
        {
            try
            {
                RegistryKey xnaKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\XNA\\Framework\\v4.0");

                string installValue = xnaKey?.GetValue("Refresh1Installed").ToString();

                if (installValue == "1")
                    return true;
            }
            catch
            {
            }

            try
            {
                RegistryKey xnaKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\XNA\\Framework\\v4.0");

                string installValue = xnaKey.GetValue("Refresh1Installed").ToString();

                if (installValue == "1")
                    return true;
            }
            catch
            {
            }

            return false;
        }
    }

    enum OSVersion
    {
        UNKNOWN,
        WIN
    }
}