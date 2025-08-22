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

        static readonly Offset<int> _sendControl = new(DataGroupName: "Command", Address: 0x3110, WriteOnly: true);

        static readonly Offset<int> _controlParameter = new(DataGroupName: "Command", Address: 0x3114, WriteOnly: true);


        private readonly Dictionary<string, CommandItem> _Dictionary;


        public bool ContainsKey(string key) => _Dictionary.ContainsKey(key);


        #region SINGLETON

        private static FSUIPCHelper? _Instance;

        public static FSUIPCHelper Instance
        {
            get
            {
                _Instance ??= new FSUIPCHelper();

                return _Instance;
            }
        }

        private FSUIPCHelper()
        {
            _Dictionary = [];

            Load();
        }

        #endregion
 

        public void Execute(string key, int? parameter = null)
        {
            //if (!FSUIPC_Running) return;

            if (!_Dictionary.TryGetValue(key, out CommandItem? value)) return;

            // Si se pasa un parámetro, se usa; si no, se evalúa si el CommandItem tiene uno por defecto
            if (parameter.HasValue)
            {
                _controlParameter.Value = parameter.Value;
            }
            else if (value.Parameter != null)
            {
                _controlParameter.Value = Convert.ToInt32(value.Parameter);
            }
            else
            {
                _controlParameter.Value = 0; // Default para casos sin parámetro
            }

            _sendControl.Value = value.Command;

            FSUIPCConnection.Process("Command");

            _sendControl.Value = 0;
            
        }
 



        private void Load()
        {
            var aircraftPath = Path.Combine(Environment.CurrentDirectory, "profiles", Profile.Instance.AircraftProfile, "commands");

            if (!Directory.Exists(aircraftPath))
                return;

            foreach (var file in Directory.GetFiles(aircraftPath, "*.json", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var aux = JsonConvert.DeserializeObject<Dictionary<string, CommandItem>>(json);

                    if (aux == null)
                        continue;

                    foreach (var kvp in aux)
                    {
                        _Dictionary[kvp.Key] = new CommandItem
                        {
                            Command = kvp.Value.Command,
                            Parameter = kvp.Value.Parameter
                        };
                    }
                }
                catch
                {
                    // Logueá si querés saber qué archivo falló
                }
            }
        }

    }

}
