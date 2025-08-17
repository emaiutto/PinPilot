namespace FSUIPC;

internal class ConnectionInfo
{
	internal IntPtr hWnd = IntPtr.Zero;

	internal uint messageID;

	internal IntPtr atomFileName = IntPtr.Zero;

	internal IntPtr hMap = IntPtr.Zero;

	internal IntPtr pView = IntPtr.Zero;

	internal FSUIPCVersion fsuipcVersion;

	internal FsVersion fsVersion;

	internal bool wideClient;
}
