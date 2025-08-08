using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MauiSoft.SRP.FsuipcWrapper;
using MauiSoft.SRP.Helpers;
using MauiSoft.SRP.MyExtensions;
using TinyHIDLibrary;

namespace MauiSoft.SRP.SaitekLibrary
{

    /// <summary>
    /// Rotary and pushbutton event handler:
    /// It does not need parameters because the command is identified
    /// with the corresponding bit and it is unique.
    /// The name defines it uniquely!
    /// </summary>
    public delegate void Handler();


    public delegate void UpdateHandler(int pos, float value);


    public sealed class RadioPanel : IDisposable
    {

        const int VendorId = 0x06a3;
        const int ProductId = 0x0d05;

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


        public RadioPanel()
        {

            rnd = new Random(); // FOR TESTING ONLY


            Switches[0] = 2; // Cable soldado
            Switches[1] = 3; // Cable soldado

            Reader = new HidReader(VendorId, ProductId, OnReport);
            
            Reader.Start();
 
            // Esperar a que el dispositivo esté listo
            int waitMs = 0;
            while (Reader.Device == null && waitMs < 2000)
            {
                _ = DrawDots();
                Thread.Sleep(50);
                waitMs += 250;
            }

            if (Reader.Device == null)
                throw new Exception("No se pudo conectar con el Saitek RadioPanel.");

            //if (!WakeUpDevice(Reader.Device))
            //{
            //    Debug.WriteLine("El dispositivo Saitek RadioPanel no respondió a tiempo.");
            //    throw new Exception("El dispositivo Saitek RadioPanel no respondió a tiempo.");
            //}

        }

        private bool WakeUpDevice(HidDevice device, int maxRetries = 5, int delayMs = 250)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                Debug.WriteLine($"Intento WakeUp #{i + 1}");


                _ = DrawDots();
                 

                Thread.Sleep(200); // Pequeña espera para que el dispositivo lo procese

                if (device.PingReal(500)) // Lee algo desde el panel
                {
                    Debug.WriteLine($"WakeUp exitoso en intento #{i + 1}");
                    return true;
                }

                Thread.Sleep(delayMs);
            }

            return false;
        }


        public void Update()
        {
            for (int i = 0; i <= 3; i++) SetDisplay(i);

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





        private void SetDisplay(int pos)
        {

            if (Reader?.Device == null) return;


            int sw = pos <= 1 ? 0 : 1; // SWITCH

            int index = Switches[sw]; // 0 - 6 (posicion del switch)


            
            string[]? data = null;

            ConfigItem? ConfigItem = Config.Instance.Get(index);

            if (pos == 0 || pos == 2) data = ConfigItem?.DSL; // DISPLAY LEFT

            if (pos == 1 || pos == 3) data = ConfigItem?.DSR; // DISPLAY RIGHT

            if (data == null) return; // RETORNA Y QUEDA LIMPIO!


            
            if (!_Display_Trackers[pos].HasChanged(Convert.ToSingle(OffsetList.Instance.GetValue(data[0])))) return;

            float? value = _Display_Trackers[pos].Current;
                 


            var item = OffsetList.Instance.Dictionary[data[0]];

            if (item.Frecuency != null && (bool)item.Frecuency)
                value = TransformFrecuency(value);
            else
                value = (float)Math.Round((double)value, 2);



            // FORMAT STRING
            string? str = data[1].IsNullEmptyOrSpace()
                ? (value?.ToString(CultureInfo.InvariantCulture))
                : (value?.ToString(data[1], CultureInfo.InvariantCulture));


            if (str == null) return; // RETORNA Y QUEDA LIMPIO!


            // Caso: Vertical Speed en GROUND
            // Negativo que no tienen que mostrarse (-16960)
            if (value == -16960) return;



            int ini = pos * DIGITOS + 1;

            int posdecimal = 0;

            int posicion = 4;

            for (int i = str.Length - 1; i >= 0; i--)
            {

                switch ((byte)str[i])
                {

                    case 45:
                        Reader.Device.FeaturesBuffer[ini + posicion] = NEGATIVE_SIGN;
                        break;

                    case 46:
                        posdecimal = posicion;
                        break;

                    default:
                        Reader.Device.FeaturesBuffer[ini + posicion] = (byte)str[i];
                        posicion--;
                        break;

                }

            }

            if (posdecimal > 0)
                Reader.Device.FeaturesBuffer[ini + posdecimal] += 160; // Agregar punto decimal


            byte? led = OffsetList.Instance.GetValue(data[2]);

            // Agrego un punto al final como indicador de "luz"
            if (led != null && led == 1)
            {
                if (Reader.Device.FeaturesBuffer[ini + 4] == CLEAR)
                {
                    Reader.Device.FeaturesBuffer[ini + 4] = DECIMAL_POINT;
                }
                else
                {
                    Reader.Device.FeaturesBuffer[ini + 4] += 160;
                }
            }

            _ = (Reader.Device?.SetFeature());

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
            Reader?.Stop();

            GC.SuppressFinalize(this);
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

    }
}
