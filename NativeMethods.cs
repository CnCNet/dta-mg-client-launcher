using System;
using System.Text; 
using System.Runtime.InteropServices;

namespace DTALauncherStub
{
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
