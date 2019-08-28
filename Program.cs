using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace DTALauncherStub
{
    class Program
    {
        private const string RESOURCES = "Resources";
        private const int ERROR_CANCELLED_CODE = 1223;

        private static void Main(string[] args)
        {
            var osVersion = GetOperatingSystemVersion();

            char dsc = Path.DirectorySeparatorChar;

            if (args != null)
            {
                foreach (string arg in args)
                {
                    if (arg.ToUpper() == "-XNA")
                    {
                        RunXNA();
                        return;
                    }
                    else if (arg.ToUpper() == "-OGL")
                    {
                        RunOGL();
                        return;
                    }
                    else if (arg.ToUpper() == "-DX")
                    {
                        RunDX();
                        return;
                    }
                }
            }

            switch (osVersion)
            {
                case OSVersion.WINXP:
                case OSVersion.WINVISTA:
                    if (!IsNetFramework4Installed())
                    {
                        Application.Run(new NETFramework4MissingMessageForm());
                        break;
                    }

                    RunXNA();
                    break;
                case OSVersion.WIN7:
                case OSVersion.WIN810:
                    W7And10Autorun();
                    break;
                case OSVersion.UNIX:
                case OSVersion.UNKNOWN:
                    RunOGL();
                    break;
            }
        }

        private static void RunXNA()
        {
            if (!IsXNAFramework4Installed())
            {
                Application.Run(new XNAFrameworkMissingMessageForm());
                return;
            }

            StartProcess(RESOURCES + Path.DirectorySeparatorChar + "clientxna.exe");
        }

        private static void RunOGL()
        {
            StartProcess(RESOURCES + Path.DirectorySeparatorChar + "clientogl.exe");
        }

        private static void RunDX()
        {
            StartProcess(RESOURCES + Path.DirectorySeparatorChar + "clientdx.exe");
        }

        private static void W7And10Autorun()
        {
            string basePath = Environment.CurrentDirectory +
                Path.DirectorySeparatorChar + "Client" + Path.DirectorySeparatorChar;
            string dxFailFilePath = basePath + ".dxfail";

            if (File.Exists(dxFailFilePath))
            {
                if (IsXNAFramework4Installed())
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
                Process.Start(completeFilePath);
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
                    return;
                }
            }
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

                if (osVersion.Minor > 1)
                    return OSVersion.WIN810;
                else if (osVersion.Minor == 0)
                    return OSVersion.WINVISTA;

                return OSVersion.WIN7;
            }

            int p = (int)Environment.OSVersion.Platform;

            if ((p == 4) || (p == 6) || (p == 128))
            {
                return OSVersion.UNIX;
            }

            return OSVersion.UNKNOWN;
        }

        private static bool IsNetFramework4Installed()
        {
            try
            {
                RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full");

                string installValue = ndpKey.GetValue("Install").ToString();

                if (installValue == "1")
                    return true;
            }
            catch
            {
                
            }

            return false;
        }

        private static bool IsXNAFramework4Installed()
        {
            try
            {
                RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\XNA\\Framework\\v4.0");

                string installValue = ndpKey.GetValue("Installed").ToString();

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
        WIN9X,
        WINXP,
        WINVISTA,
        WIN7,
        WIN810,
        UNIX
    }
}
