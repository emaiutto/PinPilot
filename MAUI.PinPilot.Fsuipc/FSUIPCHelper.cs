using System.Diagnostics;
using FSUIPC;
using Newtonsoft.Json;

namespace MAUI.PinPilot.Fsuipc
{
    public sealed class CommandItem
    {
        public int Command { get; set; }
        public int? Parameter { get; set; }
    }

    public sealed class FSUIPCHelper
    {
        private static readonly Offset<int> _sendControl = new(DataGroupName: "Command", Address: 0x3110, WriteOnly: true);
        private static readonly Offset<int> _controlParameter = new(DataGroupName: "Command", Address: 0x3114, WriteOnly: true);








        private readonly Dictionary<string, CommandItem> _dictionary = [];
        private readonly Lock _lock = new(); // para sincronizar acceso a Execute/Load

        public IReadOnlyDictionary<string, CommandItem> Dictionary => _dictionary;

        #region SINGLETON (Thread-Safe)
        private static readonly Lazy<FSUIPCHelper> _instance =
            new(() => new FSUIPCHelper(), isThreadSafe: true);

        public static FSUIPCHelper Instance => _instance.Value;

        private FSUIPCHelper() => Load();
        #endregion

        public void Execute(string key, int? parameter = null)
        {
            try
            {
                lock (_lock)
                {
                    if (!_dictionary.TryGetValue(key, out var value))
                        return;

                    if (parameter.HasValue)
                        _controlParameter.Value = parameter.Value;
                    else if (value.Parameter.HasValue)
                        _controlParameter.Value = value.Parameter.Value;

                    _sendControl.Value = value.Command;
                    
                    FSUIPCConnection.Process("Command");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FSUIPCHelper.Execute] {ex}");
            }
        }

        /// <summary>
        /// Caso especial para ejes donde siempre hay parámetro y comando fijo.
        /// </summary>
        public void Execute(int cmd, int parameter)
        {
            try
            {
                lock (_lock)
                {

                    _controlParameter.Value = parameter;
                    _sendControl.Value = cmd;

                    FSUIPCConnection.Process("Command");

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FSUIPCHelper.Execute(int)] {ex}");
            }
        }




        private void Load()
        {
            lock (_lock)
            {
                _dictionary.Clear();

                var aircraftPath = Path.Combine(
                    Environment.CurrentDirectory,
                    "profiles",
                    Profile.Instance.AircraftProfile,
                    "commands");

                if (!Directory.Exists(aircraftPath))
                    return;

                foreach (var file in Directory.GetFiles(aircraftPath, "*.json"))
                {
                    try
                    {
                        var json = File.ReadAllText(file);
                        var aux = JsonConvert.DeserializeObject<Dictionary<string, CommandItem>>(json);

                        if (aux == null)
                            continue;

                        foreach (var kvp in aux)
                            _dictionary[kvp.Key] = kvp.Value;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"[FSUIPCHelper.Load] Error en {file}: {ex.Message}");
                    }
                }
            }
        }
    }
}
