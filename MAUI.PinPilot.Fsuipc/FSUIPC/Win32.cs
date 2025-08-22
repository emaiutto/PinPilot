using System.Runtime.InteropServices;

namespace FSUIPC;

internal static class Win32
{
	[DllImport("user32.dll", SetLastError = true)]
	internal static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	internal static extern uint RegisterWindowMessage(string lpString);

	[DllImport("kernel32.dll", SetLastError = true)]
	internal static extern IntPtr CreateFileMapping(IntPtr hFile, IntPtr lpFileMappingAttributes, PageProtection flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);

	[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	internal static extern IntPtr GlobalAddAtom(string lpString);

	[DllImport("kernel32.dll", SetLastError = true)]
	internal static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, DesiredAccess dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

	[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
	public static extern ushort GlobalDeleteAtom(IntPtr nAtom);

	[DllImport("kernel32.dll", SetLastError = true)]
	internal static extern bool CloseHandle(IntPtr hObject);

	[DllImport("kernel32.dll")]
	internal static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern IntPtr SendMessageTimeout(IntPtr windowHandle, uint Msg, IntPtr wParam, IntPtr lParam, SendMessageTimeoutFlags flags, uint timeout, out IntPtr result);
}
