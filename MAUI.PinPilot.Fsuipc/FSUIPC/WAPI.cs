using System;
using System.Runtime.InteropServices;

namespace FSUIPC;

internal static class WAPI
{
	internal delegate void LogCallback([MarshalAs(UnmanagedType.LPStr)] string LogString);

	internal delegate void ReceiveListCallback(int ID, [MarshalAs(UnmanagedType.LPStr)] string Name);

	internal delegate void ReceiveValuesCallback([MarshalAs(UnmanagedType.LPStr)] string Name, double Value);

	internal delegate void NotifyLVarsChangedCallback(IntPtr id, IntPtr Value);

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_init(LogCallback logCallback);

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_start();

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern byte fsuipcw_isRunning();

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_end();

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern int fsuipcw_getLvarUpdateFrequency();

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_setLvarUpdateFrequency(int freq);

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_setSimConfigConnection(int connection);

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_reload();

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_logLvars();

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_logHvars();

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_setLogLevel(int logLevel);

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_getLvarList(ReceiveListCallback ReceiveFunction);

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_getHvarList(ReceiveListCallback ReceiveFunction);

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_getLvarValues(ReceiveValuesCallback ReceiveFunction);

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern bool fsuipcw_createLvar([MarshalAs(UnmanagedType.LPStr)] string lvarName, double value);

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_setLvarAsDouble(ushort id, double value);

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_setLvarAsShort(ushort id, short value);

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_setLvarAsUShort(ushort id, ushort value);

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_setHvar(int id);

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_executeCalclatorCode([MarshalAs(UnmanagedType.LPStr)] string code);

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_registerUpdateCallback(Action NotfiyFunction);

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_registerLvarUpdateCallbackById(NotifyLVarsChangedCallback NotifyFunction);

	[DllImport("FSUIPC_WAPID.dll")]
	internal static extern void fsuipcw_flagLvarForUpdateCallbackById(int lvarId);
}
