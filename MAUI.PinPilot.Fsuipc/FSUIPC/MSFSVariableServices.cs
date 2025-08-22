using System;
using System.Collections.Generic;

namespace FSUIPC;

public static class MSFSVariableServices
{
	private static int DEFAULT_EVENT_START_NO;

	private static int SIMCONNECT_OPEN_CONFIGINDEX_LOCAL;

	private static LOGLEVEL DEFAULT_LOG_LEVEL;

	private static int DEFAULT_LVAR_UPDATE_FREQ;

	private static IntPtr randomPtr;

	private static LVarCollection lvars;

	private static LVarCollection changedLvars;

	private static HVarCollection hvars;

	private static Dictionary<string, WeakReference> lvarCache;

	private static Dictionary<string, WeakReference> hvarCache;

	private static WAPI.LogCallback delLogCallback;

	private static WAPI.NotifyLVarsChangedCallback delLVarsValueChangedCallback;

	private static Action delVarsChanged;

	internal static bool DLLExists
	{
		get
		{
			try
			{
				WAPI.fsuipcw_setLogLevel(2);
				return true;
			}
			catch (DllNotFoundException)
			{
				return false;
			}
		}
	}

	public static int LVARUpdateFrequency
	{
		get
		{
			return WAPI.fsuipcw_getLvarUpdateFrequency();
		}
		set
		{
			WAPI.fsuipcw_setLvarUpdateFrequency(value);
		}
	}

	public static LOGLEVEL LogLevel
	{
		set
		{
			WAPI.fsuipcw_setLogLevel((int)value);
		}
	}

	public static int SimConfigConnection
	{
		set
		{
			WAPI.fsuipcw_setSimConfigConnection(value);
		}
	}

	public static bool IsRunning => WAPI.fsuipcw_isRunning() != 0;

	public static HVarCollection HVars => hvars;

	public static LVarCollection LVars => lvars;

	public static LVarCollection LVarsChanged => changedLvars;

	public static event EventHandler<LogEventArgs> OnLogEntryReceived;

	public static event EventHandler OnValuesChanged;

	public static event EventHandler OnVariableListChanged;

	static MSFSVariableServices()
	{
		DEFAULT_EVENT_START_NO = 131056;
		SIMCONNECT_OPEN_CONFIGINDEX_LOCAL = -1;
		DEFAULT_LOG_LEVEL = LOGLEVEL.LOG_LEVEL_INFO;
		DEFAULT_LVAR_UPDATE_FREQ = 0;
		lvars = [];
		changedLvars = [];
		hvars = [];
		lvarCache = [];
		hvarCache = [];
		delLogCallback = logCallback;
		delLVarsValueChangedCallback = lvarsValueChanged;
		delVarsChanged = varsChangedCallback;
	}

	public static void Init()
	{
		LogLevel = DEFAULT_LOG_LEVEL;
		SimConfigConnection = SIMCONNECT_OPEN_CONFIGINDEX_LOCAL;
		LVARUpdateFrequency = DEFAULT_LVAR_UPDATE_FREQ;
		WAPI.fsuipcw_init(delLogCallback);
		WAPI.fsuipcw_registerUpdateCallback(delVarsChanged);
		WAPI.fsuipcw_registerLvarUpdateCallbackById(delLVarsValueChangedCallback);
	}

	public static void Start() => WAPI.fsuipcw_start();

	public static void Stop()
	{
		WAPI.fsuipcw_end();
		lvars.Clear();
		hvars.Clear();
	}

	private static void RefreshData()
	{
		changedLvars.Clear();
		WAPI.fsuipcw_getLvarList(receiveLVARListCallback);
		WAPI.fsuipcw_getLvarValues(receiveValuesCallback);
		WAPI.fsuipcw_getHvarList(receiveHVARListCallback);
	}

	public static void Reload() => WAPI.fsuipcw_reload();

	public static void LogHVars() => WAPI.fsuipcw_logHvars();

	public static void LogLVars() => WAPI.fsuipcw_logLvars();

	public static bool CreateLVar(string Name, double Value) => WAPI.fsuipcw_createLvar(Name, Value);

	public static void ExecuteCalculatorCode(string Code) => WAPI.fsuipcw_executeCalclatorCode(Code);

	private static void logCallback(string logEntry)
	{
		MSFSVariableServices.OnLogEntryReceived?.Invoke(null, new LogEventArgs(logEntry));
	}

	private static void receiveHVARListCallback(int id, string name)
	{
		if (hvars.Exists(name)) return;
		
		hvars.Add(getHVarFromCache(id, name));
	}

	private static void receiveLVARListCallback(int id, string name)
	{
		if (lvars.Exists(name)) return;

		lvars.Add(getLVarFromCache(id, name));
	}

	private static void receiveValuesCallback(string name, double value)
	{
		if (!lvars.Exists(name)) return;

		FsLVar fsLVar = lvars[name];

		fsLVar.UpdateValue(value);

		if (fsLVar.ValueChanged) changedLvars.Add(fsLVar);
	}

	private static void varsChangedCallback()
	{
		lvars.Clear();
		hvars.Clear();
		WAPI.fsuipcw_flagLvarForUpdateCallbackById(-1);
		RefreshData();
		MSFSVariableServices.OnVariableListChanged?.Invoke(null, new EventArgs());
	}

	private static void lvarsValueChanged(IntPtr id, IntPtr Value)
	{
		RefreshData();
		MSFSVariableServices.OnValuesChanged?.Invoke(null, new EventArgs());
	}

	private static FsLVar getLVarFromCache(int ID, string Name)
	{
		FsLVar? fsLVar;
		if (lvarCache.TryGetValue(Name, out WeakReference? weakReference))
		{
			if (!weakReference.IsAlive)
			{
				fsLVar = (FsLVar)(weakReference.Target = new FsLVar(ID, Name));
			}
			else
			{
				fsLVar = weakReference.Target as FsLVar;
				fsLVar.ID = ID;
			}
		}
		else
		{
			fsLVar = new FsLVar(ID, Name);
			WeakReference value = new(fsLVar, trackResurrection: false);
			lvarCache.Add(Name, value);
		}
		return fsLVar;
	}

	private static FsHVar getHVarFromCache(int ID, string Name)
	{
		FsHVar fsHVar;
		if (hvarCache.TryGetValue(Name, out WeakReference? weakReference))
		{
			if (!weakReference.IsAlive)
			{
				fsHVar = (FsHVar)(weakReference.Target = new FsHVar(ID, Name));
			}
			else
			{
				fsHVar = weakReference.Target as FsHVar;
				fsHVar.ID = ID;
			}
		}
		else
		{
			fsHVar = new FsHVar(ID, Name);
			WeakReference value = new(fsHVar, trackResurrection: false);
			hvarCache.Add(Name, value);
		}
		return fsHVar;
	}
}
