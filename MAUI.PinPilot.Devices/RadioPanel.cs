using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MAUI.PinPilot.Fsuipc;
using MAUI.PinPilot.Helpers;
using MAUI.PinPilot.MyExtensions;
using MAUI.PinPilot.TinyHIDLibrary;
using Newtonsoft.Json;

namespace MAUI.PinPilot.Devices
{

    /// <summary>
    /// Rotary and pushbutton event handler:
    /// It does not need parameters because the command is identified
    /// with the corresponding bit and it is unique.
    /// The name defines it uniquely!
    /// </summary>
    //public delegate void Handler();


    public delegate void UpdateHandler(int pos, float value);


    public class RadioPanel: HidDeviceId, IDisposable
    {

        private readonly Random rnd; // random value in display unit.

        private readonly ChangeTracker<float>[] _Display_Trackers =
        [
            new ChangeTracker<float>(0f),
            new ChangeTracker<float>(0f),
            new ChangeTracker<float>(0f),
            new ChangeTracker<float>(0f)
        ];
        
        private readonly ButtonEdgeTracker _tracker = new();

        
        
        private readonly HidReader? Reader;

        public event UpdateHandler? UpdateEvent;


        #region Events

        public event Handler? EnconderB1R; // Enconder Big1 Right
        public event Handler? EnconderB1L; // Enconder Big1 Left

        public event Handler? EnconderS1R; // Enconder Small1 Right
        public event Handler? EnconderS1L; // Enconder Small1 Left

        public event Handler? EnconderB2R; // Enconder Big2 Right
        public event Handler? EnconderB2L; // Enconder Big2 Left

        public event Handler? EnconderS2R; // Enconder Small2 Right
        public event Handler? EnconderS2L; // Enconder Small2 Left

        public event Handler? Buttons1; // Botón de arriba
        public event Handler? Buttons2; // Botón de abajo

        #endregion


        public int[] Switches = new int[2];

        const int DIGITOS = 5;
        const int CLEAR = 255;
        const int NEGATIVE_SIGN = 230;
        const int DECIMAL_POINT = 220;


        public RadioPanel(int vendorId, int productId)
        {
            VendorId = vendorId;
            ProductId = productId;

            Reader = new HidReader(VendorId, ProductId, OnReport);

            rnd = new Random(); // FOR TESTING ONLY

            Switches[0] = 2; // Cable soldado
            Switches[1] = 3; // Cable soldado

        }

        public void Start()
        {
            Reader?.Start();

            // Esperar a que el dispositivo esté listo
            int waitMs = 0;
            while (Reader?.Device == null && waitMs < 2000)
            {
                _ = DrawDots();

                Thread.Sleep(50);

                waitMs += 250;
            }

            if (Reader?.Device == null)
                throw new Exception("No se pudo conectar con el Saitek RadioPanel.");

            //if (!WakeUpDevice(Reader?.Device))
            //{
            //    Debug.WriteLine("El dispositivo Saitek RadioPanel no respondió a tiempo.");
            //    throw new Exception("El dispositivo Saitek RadioPanel no respondió a tiempo.");
            //}
        }

        public void Update()
        {
            SetDisplay(0);
            SetDisplay(1);
            SetDisplay(2);
            SetDisplay(3);

            _ = (Reader?.Device?.SetFeature());
        }


        private Task OnReport()
        {
            if (Reader?.Device == null)
                return Task.CompletedTask;

            byte encoders = Reader.Device.InputBuffer[3];

            if (encoders.IsBitSet(0))
                EnconderS1R?.Invoke();
            else if (encoders.IsBitSet(1))
                EnconderS1L?.Invoke();

            if (encoders.IsBitSet(2))
                EnconderB1R?.Invoke();
            else if (encoders.IsBitSet(3))
                EnconderB1L?.Invoke();

            if (encoders.IsBitSet(4))
                EnconderS2R?.Invoke();
            else if (encoders.IsBitSet(5))
                EnconderS2L?.Invoke();

            if (encoders.IsBitSet(6))
                EnconderB2R?.Invoke();
            else if (encoders.IsBitSet(7))
                EnconderB2L?.Invoke();
 
                         
            byte buttons = Reader.Device.InputBuffer[2];

            if (_tracker.CheckRisingEdge(nameof(Buttons1), buttons.IsBitSet(6))) Buttons1?.Invoke();
            if (_tracker.CheckRisingEdge(nameof(Buttons2), buttons.IsBitSet(7))) Buttons2?.Invoke();


            //byte switch1 = Reader.Device.InputBuffer[1];

            //if (switch1 >= 128) switch1 -= 128;

            //Switches[0] = (int)Math.Log(switch1, 2); // INT [0 - 6]


            //byte switch2 = Reader.Device.InputBuffer[2];

            //if (switch2 >= 128) switch2 -= 128;
            //if (switch2 >= 64) switch2 -= 64;

            //switch2 = (byte)(switch2 << 1);

            //if (switch2 == 0) switch2 = 1;

            //Switches[1] = (int)Math.Log(switch2, 2); // INT [0 - 6]
            

            return Task.CompletedTask;
        }


        public void UpdateDebug()
        {
            if (Reader?.Device == null) return;

            Span<char> buffer = stackalloc char[5];

            for (int pos = 0; pos < 4; pos++)
            {
                int x = rnd.Next(0, 100000);

                if (!x.TryFormat(buffer, out _, "D5")) // D5: Decimal con 5 dígitos, relleno con ceros
                   continue; // en caso raro de error, continuar

                for (int i = 0; i < DIGITOS; i++)
                {
                    Reader.Device.FeaturesBuffer[pos * DIGITOS + i + 1] = (byte)buffer[i];
                }

                UpdateEvent?.Invoke(pos, x);

            }

            _ = (Reader.Device?.SetFeature());
        }

        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetDisplay(int pos)
        {
            if (Reader?.Device == null) return;

            int sw = pos > 1 ? 1 : 0;
            int index = Switches[sw];

            var cfg = RadioPanelConfig.Instance.Get(index);
            if (cfg == null) return;

            string[]? data = pos switch
            {
                0 or 2 => cfg.DSL,
                1 or 3 => cfg.DSR,
                _ => null
            };
            if (data == null) return;

            // --- TRACKER ---
            float rawValue = OffsetList.Instance.GetValue(data[0]);
            if (!_Display_Trackers[pos].HasChanged(rawValue)) return;

            float? value = _Display_Trackers[pos].Current;
            if (value == -16960) return; // vertical speed special case

            // --- TRANSFORM ---
            var item = OffsetList.Instance.Dictionary[data[0]];
            value = item.Frecuency == true
                ? TransformFrecuency(value)
                : MathF.Round(value ?? 0, 2);

            // --- FORMAT ---
            string? str = string.IsNullOrWhiteSpace(data[1])
                ? value?.ToString(CultureInfo.InvariantCulture)
                : value?.ToString(data[1], CultureInfo.InvariantCulture);

            if (string.IsNullOrEmpty(str)) return;

            int ini = pos * DIGITOS + 1;
            int digitPos = 4;
            int posdecimal = 0;

            // Lookup rápido: valores predefinidos
            const byte DOT = (byte)'.';
            const byte MINUS = (byte)'-';

            for (int i = str.Length - 1; i >= 0 && digitPos >= 0; i--)
            {
                byte ch = (byte)str[i];

                // Branchless mapping:
                bool isDot = ch == DOT;
                bool isMinus = ch == MINUS;

                // Si es número o cualquier otro char válido
                Reader.Device.FeaturesBuffer[ini + digitPos] = (byte)(isMinus ? NEGATIVE_SIGN : ch);

                // Guardar posdecimal sin branch (solo OR lógico)
                posdecimal = isDot ? digitPos : posdecimal;

                // Avanzar digitPos sólo si no era '.' (branchless con suma)
                digitPos -= isDot || isMinus ? 0 : 1;
            }

            if (posdecimal > 0)
                Reader.Device.FeaturesBuffer[ini + posdecimal] += 160;

            // --- LED indicator ---
            if (OffsetList.Instance.GetValue(data[2]) == 1)
            {
                int ledPos = ini + 4;
                Reader.Device.FeaturesBuffer[ledPos] =
                    (byte)(Reader.Device.FeaturesBuffer[ledPos] == CLEAR
                        ? DECIMAL_POINT
                        : (byte)(Reader.Device.FeaturesBuffer[ledPos] + 160));
            }
        }



        /// <summary>
        /// Binary-Coded Decimal (BCD)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float? TransformFrecuency(float? value)
        {

            int entero = Convert.ToInt32(value);

            return Convert.ToSingle("1" + entero.ToString("X4")) / 100f;
        }


        #region Liberar Recursos

        public void Dispose()
        {
             _ = ClearDisplay();
            
            Reader?.Stop();

        }

        //private void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            CloseDevice();
        //        }

        //        disposed = true;
        //    }
        //}


        #endregion


        // Método para limpiar el display (rellena con 255)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ClearDisplay() => SetFullBuffer(FullBuffer255);

        // Método para dibujar puntitos (rellena con 220)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool DrawDots() => SetFullBuffer(FullBuffer220);

        // Copia los 23 bytes del buffer predefinido al FeaturesBuffer
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool SetFullBuffer(ReadOnlySpan<byte> src)
        {
            if (Reader?.Device == null) return false;

            Unsafe.CopyBlockUnaligned(
                ref Reader.Device.FeaturesBuffer[0],
                ref MemoryMarshal.GetReference(src),
                23);

            return Reader.Device.SetFeature();
        }

        // Buffers predefinidos completos
        private static readonly byte[] _fullBuffer255 =
        [
        0,
        255,255,255,255,255,255,255,255,255,255,
        255,255,255,255,255,255,255,255,255,255,
        0, 0
        ];

        private static readonly byte[] _fullBuffer220 =
        [
        0,
        220,220,220,220,220,220,220,220,220,220,
        220,220,220,220,220,220,220,220,220,220,
        0, 0
        ];

        // Propiedades que exponen los buffers como ReadOnlySpan<byte>
        private static ReadOnlySpan<byte> FullBuffer255 => _fullBuffer255;
        private static ReadOnlySpan<byte> FullBuffer220 => _fullBuffer220;



        //private bool WakeUpDevice(HidDevice device, int maxRetries = 5, int delayMs = 250)
        //{
        //    for (int i = 0; i < maxRetries; i++)
        //    {
        //        Debug.WriteLine($"Intento WakeUp #{i + 1}");


        //        _ = DrawDots();


        //        Thread.Sleep(200); // Pequeña espera para que el dispositivo lo procese

        //        if (device.PingReal(500)) // Lee algo desde el panel
        //        {
        //            Debug.WriteLine($"WakeUp exitoso en intento #{i + 1}");
        //            return true;
        //        }

        //        Thread.Sleep(delayMs);
        //    }

        //    return false;
        //}

    }


    public sealed class ConfigItem
    {
        public string[]? BRR { get; set; } // Big Rotary Right
        public string[]? BRL { get; set; } // Big Rotary Left

        public string[]? SRR { get; set; } // Small Rotary Right
        public string[]? SRL { get; set; } // Small Rotary Left

        public string[]? BTN { get; set; } // Button

        public string[]? DSL { get; set; } // Display Left
        public string[]? DSR { get; set; } // Display Right
    }


    public sealed class RadioPanelConfig
    {

        const string CONFIG_FILE = "SaitekLibrary.json";

        private Dictionary<int, ConfigItem>? Dictionary { get; set; }


        #region SINGLETON

        private static readonly Lazy<RadioPanelConfig> _instance = new(() => new RadioPanelConfig());

        public static RadioPanelConfig Instance => _instance.Value;

        private RadioPanelConfig()
        {
            Dictionary = [];

            Load();
        }

        #endregion


        private void Load()
        {
            try
            {
                string filename = Path.Combine(Environment.CurrentDirectory, "profiles", Profile.Instance.AircraftProfile, CONFIG_FILE);

                string data = File.ReadAllText(filename);

                if (data.IsNullEmptyOrSpace()) throw new Exception("Saitek Config Empty");

                Dictionary = JsonConvert.DeserializeObject<Dictionary<int, ConfigItem>>(data);

            }
            catch (Exception ex)
            {
                Dictionary = null;

                throw new Exception("Saitek Config Not Load!", ex);
            }
        }

        public ConfigItem? Get(int index) => Dictionary != null && Dictionary.TryGetValue(index, out var item) ? item : null;


    }



}
