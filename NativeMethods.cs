namespace CnCNet.LauncherStub;

using System.Runtime.InteropServices;

internal static class NativeMethods
{
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
#if NET45_OR_GREATER || NETSTANDARD || NET
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
#endif
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DeleteFile(string name);
}
