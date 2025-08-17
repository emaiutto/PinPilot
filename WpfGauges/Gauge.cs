using MauiSoft.SRP.MyExtensions;
using Newtonsoft.Json;

namespace MauiSoft.SRP.Gauges
{

    public sealed class GaugeItem
    {
        public string[]? Data { get; set; }

        public string? Label { get; set; }

        public string? UpdateRate { get; set; }
    }


    public sealed class Gauge
    {
        public Dictionary<string, GaugeItem>? Dictionary { get; set; }


        public string[]? GetOffsets(string key) =>
            Dictionary?[key] != null ? Dictionary[key].Data : null;

        public string? GetLabel(string key) => 
            Dictionary?[key] != null ? Dictionary[key].Label : null;

        public string? GetUpdateRate(string key) =>
            Dictionary?[key] != null ? Dictionary[key].UpdateRate : null;


        #region SINGLETON

        private static readonly Lazy<Gauge> _instance = new(() => new Gauge());

        public static Gauge Instance => _instance.Value;


        private Gauge()
        {
            Dictionary = [];

            Load();
        }

        private void Load()
        {
            string filename = Path.Combine(Environment.CurrentDirectory, "profiles", Profile.Profile.Instance.AircraftProfile, "gauges.json");

            string data = File.ReadAllText(filename);

            if (data.IsNullEmptyOrSpace()) throw new Exception("File Empty");

            Dictionary = JsonConvert.DeserializeObject<Dictionary<string, GaugeItem>>(data);

        }

        #endregion

        public static Control? CreateControlByName(string controlName)
        {

            // Obtén el ensamblado actual (donde se encuentra este código)
            Assembly assembly = Assembly.GetExecutingAssembly();

            try
            {
                // Encuentra el tipo de control por nombre en el ensamblado
                Type? controlType = assembly.GetTypes().FirstOrDefault(t => t.Name == controlName);

                // Crea una instancia del control
                if (controlType != null && typeof(Control).IsAssignableFrom(controlType))
                    return (Control?)Activator.CreateInstance(controlType);

            }
            catch
            {
                return null; // Devuelve null si no se encuentra el tipo de control
            }

            return null;
                        
        }


    }

}
