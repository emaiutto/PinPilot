using FSUIPC;
using Newtonsoft.Json;

namespace MauiSoft.SRP.FsuipcWrapper
{

    public sealed class CommandItem
    {
        public int Command { get; set; }

        public int? Parameter { get; set; }

    }

    public sealed class FSUIPCHelper
    {

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


        private static bool _FSUIPC_Running;
        public static bool FSUIPC_Running { get => _FSUIPC_Running; }


        static readonly Offset<int> _sendControl = new(0x3110, true);

        static readonly Offset<int> _controlParameter = new(0x3114, true);

        
        // Definí el offset especial para comandos LUA (string)
        //static readonly Offset<string> _luaCommand = new("LuaCmd", 0x0D70, 40, true);
          

        //public int Count => _Dictionary.Count;

        public static void Open()
        {
            try
            {
                _FSUIPC_Running = true;

                FSUIPCConnection.Open();
            }
            catch
            {
                //_FSUIPC_Running = false;

                //Console.WriteLine("FSUIPC not running!");
            }
        }

        public static void Update()
        {
            try
            {
                FSUIPCConnection.Process();
            }
            catch
            {
                _FSUIPC_Running = false;

                Console.WriteLine("(Update) FSUIPC not running!");
            }
        }

        public void Execute(string key, int? parameter = null)
        {
            if (!FSUIPC_Running) return;

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

            Update();

            //Thread.Sleep(50);

            _sendControl.Value = 0;
            
        }


       
          



        private void Load()
        {
            var aircraftPath = Path.Combine(Environment.CurrentDirectory, "profiles", Profile.Profile.Instance.AircraftProfile, "commands");

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
