using System.Diagnostics;
using Newtonsoft.Json;
using FSUIPC;

namespace MAUI.PinPilot.Fsuipc
{
    public sealed class OffsetItem
    {
        public int Address { get; set; }          // Dirección en memoria (hexadecimal)
        public string? DataType { get; set; }     // Tipo de dato (Byte, Int16, UInt16, etc.)
        public int? Length { get; set; }          // Solo para strings
        public float? Factor { get; set; }        // Factor de conversión
        public bool? Frecuency { get; set; }      // Transformación especial (ej: BCD -> float)
        public object? Offset { get; set; }       // Objeto Offset<T>
    }

    public sealed class OffsetList
    {
        private readonly Dictionary<string, OffsetItem> _dictionary = [];
        private readonly Lock _lock = new();

        public IReadOnlyDictionary<string, OffsetItem> Dictionary => _dictionary;

        #region SINGLETON (thread-safe)
        private static readonly Lazy<OffsetList> _instance =
            new(() => new OffsetList(), isThreadSafe: true);

        public static OffsetList Instance => _instance.Value;

        private OffsetList() => Load();
        #endregion

        private void Load()
        {
            lock (_lock)
            {
                _dictionary.Clear();

                var files = new List<string>();

                // TODO: activar default si lo necesitás
                // string defaultPath = Path.Combine(Environment.CurrentDirectory, "profiles", "default", "offsets");
                // files.AddRange(Directory.GetFiles(defaultPath, "*.json"));

                string aircraftPath = Path.Combine(
                    Environment.CurrentDirectory,
                    "profiles",
                    Profile.Instance.AircraftProfile,
                    "offsets");

                if (!Directory.Exists(aircraftPath))
                    return;

                files.AddRange(Directory.GetFiles(aircraftPath, "*.json"));

                foreach (var filename in files)
                {
                    try
                    {
                        var data = File.ReadAllText(filename);
                        var aux = JsonConvert.DeserializeObject<Dictionary<string, OffsetItem>>(data);

                        if (aux == null)
                            continue;

                        foreach (var kvp in aux)
                        {
                            var item = kvp.Value;

                            if (string.IsNullOrWhiteSpace(item.DataType))
                            {
                                Debug.WriteLine($"[OffsetList.Load] Offset {kvp.Key} sin DataType");
                                continue;
                            }

                            var type = Type.GetType($"System.{item.DataType}");
                            if (type == null)
                            {
                                Debug.WriteLine($"[OffsetList.Load] Tipo inválido en {kvp.Key}: {item.DataType}");
                                continue;
                            }

                            try
                            {
                                Type tipoGenerado = typeof(Offset<>).MakeGenericType(type);

                                item.Offset = item.DataType == "String"
                                    ? Activator.CreateInstance(tipoGenerado, new object[] { item.Address, item.Length ?? 0 })
                                    : Activator.CreateInstance(tipoGenerado, new object[] { item.Address });

                                _dictionary[kvp.Key] = item;
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"[OffsetList.Load] Error creando Offset {kvp.Key}: {ex.Message}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"[OffsetList.Load] Error leyendo {filename}: {ex.Message}");
                    }
                }
            }
        }

        public dynamic? GetValue(string key)
        {
            lock (_lock)
            {
                if (!_dictionary.TryGetValue(key, out var item))
                    return null;

                if (item.Offset == null)
                    return null;

                float factor = item.Factor ?? 1.0f;

                return item.DataType switch
                {
                    "Byte" => (item.Offset as Offset<byte>)?.Value,
                    "Int16" => (item.Offset as Offset<short>)?.Value * factor,
                    "UInt16" => (item.Offset as Offset<ushort>)?.Value * factor,
                    "Int32" => (item.Offset as Offset<int>)?.Value * factor,
                    "UInt32" => (item.Offset as Offset<uint>)?.Value * factor,
                    "Single" => (item.Offset as Offset<float>)?.Value * factor,
                    "Double" => (item.Offset as Offset<double>)?.Value * factor,
                    "String" => (item.Offset as Offset<string>)?.Value,
                    _ => null
                };
            }
        }
    }
}
