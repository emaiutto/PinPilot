using System.Runtime.CompilerServices;
using FSUIPC;
using MAUI.PinPilot.Helpers;
using MAUI.PinPilot.MyExtensions;
using MAUI.PinPilot.TinyHIDLibrary;

namespace MAUI.PinPilot.Devices
{

    public delegate void Handler();

    public sealed class ROF : IDisposable
    {

        // Rudder
        private readonly Offset<short> _offset_rudder = new(0x0BBA); // (signed 16-bit, -16383..16383)

        private readonly AnalogController _axis_rudder 
            = new(axisRawMin: -50, axisRawMax: 280, axisRangeMin: -16383, axisRangeMax: 16383, alpha: 0.9f, delta: 256, deadZone: 4096, inverted: true);

        
        // Throttle
        private readonly Offset<short> _offset_throttle = new(0x089A); // –4096 +16384 (segun documentacion)

        private readonly AnalogController _axis_throttle 
            = new(axisRawMin: 7, axisRawMax: 905, axisRangeMin: -4096, axisRangeMax: 16383, alpha: 0.9f, delta: 256);

        

        public GearController gearController = new(bitUp: 5, bitDown: 6);

        private readonly ButtonEdgeTracker _tracker = new();


        private readonly HidReader? Reader00;
        private readonly HidReader? Reader01;
        private readonly HidReader? Reader02;
        private readonly HidReader? Reader03;


        public ROF(int vendorId, int productId)
        {

            Reader00 = new HidReader(vendorId, productId, OnReport00, "mi_00", delayMs: 0);
            Reader01 = new HidReader(vendorId, productId, OnReport01, "mi_01", delayMs: 0);
            Reader02 = new HidReader(vendorId, productId, OnReport02, "mi_02", delayMs: 0);
            Reader03 = new HidReader(vendorId, productId, OnReport03, "mi_03", delayMs: 0);

        }

        public void Start()
        {
            Reader00?.Start();
            Reader01?.Start();
            Reader02?.Start();
            Reader03?.Start();
        }


        #region Events

        // ENCODERS
        public event Handler? CRSL_INC;
        public event Handler? CRSL_DEC;

        public event Handler? SPEED_INC;
        public event Handler? SPEED_DEC;

        public event Handler? HEADING_INC;
        public event Handler? HEADING_DEC;

        public event Handler? ALTITUDE_INC;
        public event Handler? ALTITUDE_DEC;

        public event Handler? VERSPEED_INC;
        public event Handler? VERSPEED_DEC;

        public event Handler? CRSR_INC;
        public event Handler? CRSR_DEC;

        public event Handler? ELEVATOR_TRIM_UP;
        public event Handler? ELEVATOR_TRIM_DW;


        // BUTTONS

        public event Handler? CRSL_BUTTON;

        public event Handler? AT_BUTTON;
        public event Handler? FD_BUTTON;

        public event Handler? SPD_BUTTON;

        public event Handler? VNAV_BUTTON;
        public event Handler? LVL_CHG_BUTTON;

        public event Handler? HDGSEL_BUTTON;

        public event Handler? LNAV_BUTTON;
        public event Handler? APP_BUTTON;

        public event Handler? ALTHLD_BUTTON;
        public event Handler? VS_BUTTON;

        public event Handler? CMD_BUTTON;
        public event Handler? CWS_BUTTON;

        public event Handler? CRSR_BUTTON;


        public event Handler? FlapsUp;
        public event Handler? FlapsDown;

        #endregion


        private const int Bit0 = 0;
        private const int Bit1 = 1;
        private const int Bit2 = 2;
        private const int Bit3 = 3;
        private const int Bit4 = 4;
        private const int Bit5 = 5;
        private const int Bit6 = 6;
        private const int Bit7 = 7;


        private Task OnReport00()
        {

            if (Reader00?.Device == null)
                return Task.CompletedTask;

            var buffer = Reader00.Device.InputBuffer;

            _axis_rudder.Process(ReadAxisX(buffer, 1));

            byte B6 = buffer[6];
            byte B7 = buffer[7];

            if (B7.IsBitSet(Bit2))
                CRSL_INC?.Invoke();
            else if (B7.IsBitSet(Bit3))
                CRSL_DEC?.Invoke();

            if (B7.IsBitSet(Bit0))
                SPEED_INC?.Invoke();
            else if (B7.IsBitSet(Bit1))
                SPEED_DEC?.Invoke();
            

            if (B6.IsBitSet(Bit6))
                HEADING_INC?.Invoke();
            else if (B6.IsBitSet(Bit7))
                HEADING_DEC?.Invoke();

            if (B6.IsBitSet(Bit4))
                ALTITUDE_INC?.Invoke();
            else if (B6.IsBitSet(Bit5))
                ALTITUDE_DEC?.Invoke();

            if (B6.IsBitSet(Bit3))
                VERSPEED_INC?.Invoke();
            else if (B6.IsBitSet(Bit2))
                VERSPEED_DEC?.Invoke();

            if (B6.IsBitSet(Bit0))
                CRSR_INC?.Invoke();
            else if (B6.IsBitSet(Bit1))
                CRSR_DEC?.Invoke();


            if (_tracker.CheckRisingEdge(nameof(CWS_BUTTON), buffer[9].IsBitSet(Bit4))) CWS_BUTTON?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(APP_BUTTON), buffer[9].IsBitSet(Bit7))) APP_BUTTON?.Invoke();

            return Task.CompletedTask;
        }

        private Task OnReport01()
        {
            if (Reader01?.Device == null)
                return Task.CompletedTask;


            var buffer = Reader01.Device.InputBuffer;


            //_axis_throttle.Process(ReadAxisX(buffer, 1));
     

            byte B6 = buffer[6];
            byte B7 = buffer[7];


            if (_tracker.CheckRisingEdge(nameof(LNAV_BUTTON), B6.IsBitSet(Bit4))) LNAV_BUTTON?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(ALTHLD_BUTTON), B6.IsBitSet(Bit2))) ALTHLD_BUTTON?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(VS_BUTTON), B6.IsBitSet(Bit3))) VS_BUTTON?.Invoke();


            if (B6.IsBitSet(Bit0))
                ELEVATOR_TRIM_DW?.Invoke();
            else if (B6.IsBitSet(Bit1))
                ELEVATOR_TRIM_UP?.Invoke();


            // ignorar si el avion no tiene tren retractil
            gearController.Update(B6);


            if (_tracker.CheckRisingEdge(nameof(FlapsDown), B7.IsBitSet(0))) FlapsDown?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(FlapsUp), B6.IsBitSet(7))) FlapsUp?.Invoke();


            if (_tracker.CheckRisingEdge(nameof(CMD_BUTTON), buffer[9].IsBitSet(Bit4))) CMD_BUTTON?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(CRSR_BUTTON), buffer[9].IsBitSet(Bit5))) CRSR_BUTTON?.Invoke();


            return Task.CompletedTask;
        }

        private Task OnReport02()
        {
            if (Reader02?.Device == null)
                return Task.CompletedTask;


            var buffer = Reader02.Device.InputBuffer;

            if (_tracker.CheckRisingEdge(nameof(FD_BUTTON), buffer[7].IsBitSet(Bit3))) FD_BUTTON?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(LVL_CHG_BUTTON), buffer[6].IsBitSet(Bit4))) LVL_CHG_BUTTON?.Invoke();
            
            return Task.CompletedTask;
        }

        private Task OnReport03()
        {

            if (Reader03?.Device == null)
                return Task.CompletedTask;

            var buffer = Reader03.Device.InputBuffer;

            byte B7 = buffer[7];

            if (_tracker.CheckRisingEdge(nameof(CRSL_BUTTON), B7.IsBitSet(Bit3))) CRSL_BUTTON?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(AT_BUTTON), B7.IsBitSet(Bit2))) AT_BUTTON?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(SPD_BUTTON), B7.IsBitSet(Bit1))) SPD_BUTTON?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(VNAV_BUTTON), B7.IsBitSet(Bit0))) VNAV_BUTTON?.Invoke();


            if (_tracker.CheckRisingEdge(nameof(HDGSEL_BUTTON), buffer[6].IsBitSet(Bit7))) HDGSEL_BUTTON?.Invoke();
            
            return Task.CompletedTask;
        }


 

        #region Liberar Recursos

        public void Dispose()
        {

            Reader00?.Stop();
            Reader01?.Stop();
            Reader02?.Stop();
            Reader03?.Stop();

            GC.SuppressFinalize(this);
        }

        #endregion




        // --- Extraer eje X (10 bits, little-endian, signo two's-complement) ---
        // offset = índice del primer byte donde empieza el campo X

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int ReadAxisX(byte[] buffer, int offset = 1)
        {
            // tomar LSB y los 2 bits bajos del siguiente byte (bits 8..9)
            int low = buffer[offset];                // byte con bits 0..7
            int high2 = buffer[offset + 1] & 0x03;   // solo bits 0..1 del siguiente byte

            int raw10 = (high2 << 8) | low;          // 0..1023 (10 bits)

            // convertir a signed 10-bit (two's complement): -512..+511
            int signed = (raw10 & 0x200) != 0 ? raw10 - 1024 : raw10;
            
            return signed; // resultado centrado en 0 (aprox -512..+511)
        }


        public void UpdateControls()
        {
            _offset_rudder.Value = _axis_rudder.Value;

            //_offset_throttle.Value = _axis_throttle.Value;
        }

    }

}
