using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using MauiSoft.SRP.Helpers;
using MauiSoft.SRP.MyExtensions;
using TinyHIDLibrary;

namespace MauiSoft.SRP.McpLibrary
{

    public delegate void Handler();

    public sealed class ROF : IDisposable
    {

        const int VendorId = 0xffff; // ROF
        const int ProductId = 0xb004;

        public GearController gearController = new(bitUp: 5, bitDown: 6);

        private readonly ButtonEdgeTracker _tracker = new();

        private readonly ChangeTracker<int> _ThottleTracker = new();
        public event Action<int>? ThrottleChanged;
        
        private readonly ChangeTracker<int> _RudderTracker = new();
        public event Action<int>? RudderChanged;


        private readonly HidReader? Reader00;
        private readonly HidReader? Reader01;
        private readonly HidReader? Reader02;
        private readonly HidReader? Reader03;


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


        const int AXIS_RANGE_MIN = -16384;
        const int AXIS_RANGE_MAX = 16384;


        private const int Bit0 = 0;
        private const int Bit1 = 1;
        private const int Bit2 = 2;
        private const int Bit3 = 3;
        private const int Bit4 = 4;
        private const int Bit5 = 5;
        private const int Bit6 = 6;
        private const int Bit7 = 7;


        public ROF()
        {
            
            Reader00 = new HidReader(VendorId, ProductId, OnReport00, "mi_00");
            Reader00.Start();

            Reader01 = new HidReader(VendorId, ProductId, OnReport01, "mi_01");
            Reader01.Start();

            Reader02 = new HidReader(VendorId, ProductId, OnReport02, "mi_02");
            Reader02.Start();

            Reader03 = new HidReader(VendorId, ProductId, OnReport03, "mi_03");
            Reader03.Start();

        }


        private Task OnReport00()
        {

            if (Reader00?.Device == null)
                return Task.CompletedTask;

            var buffer = Reader00.Device.InputBuffer;

            byte B7 = buffer[7];

            if (B7.IsBitSet(Bit2)) CRSL_INC?.Invoke();
            if (B7.IsBitSet(Bit3)) CRSL_DEC?.Invoke();

            if (B7.IsBitSet(Bit0)) SPEED_INC?.Invoke();
            if (B7.IsBitSet(Bit1)) SPEED_DEC?.Invoke();


            byte B6 = buffer[6];

            if (B6.IsBitSet(Bit6)) HEADING_INC?.Invoke();
            if (B6.IsBitSet(Bit7)) HEADING_DEC?.Invoke();

            if (B6.IsBitSet(Bit4)) ALTITUDE_INC?.Invoke();
            if (B6.IsBitSet(Bit5)) ALTITUDE_DEC?.Invoke();

            if (B6.IsBitSet(Bit3)) VERSPEED_INC?.Invoke();
            if (B6.IsBitSet(Bit2)) VERSPEED_DEC?.Invoke();

            if (B6.IsBitSet(Bit0)) CRSR_INC?.Invoke();
            if (B6.IsBitSet(Bit1)) CRSR_DEC?.Invoke();


            if (_tracker.CheckRisingEdge(nameof(CWS_BUTTON), buffer[9].IsBitSet(Bit4))) CWS_BUTTON?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(APP_BUTTON), buffer[9].IsBitSet(Bit7))) APP_BUTTON?.Invoke();


            // Rudder

            int raw = Get10BitSigned(((buffer[4] >> 6) | (buffer[5] << 2)) & 0x3FF);

            int axis = raw.MapRange(-220, 220, AXIS_RANGE_MIN, AXIS_RANGE_MAX);

            // Aplicar zona muerta (ajustable)
            const int DEADZONE = 1000;

            if (Math.Abs(axis) < DEADZONE)
                axis = 0;
            else
                axis = axis < 0
                    ? axis + DEADZONE
                    : axis - DEADZONE;

            if (_RudderTracker.HasChanged(Math.Clamp(axis, AXIS_RANGE_MIN, AXIS_RANGE_MAX)))
                RudderChanged?.Invoke(_RudderTracker.Current);

            //Debug.WriteLine($"{raw}");

            return Task.CompletedTask;
        }

        private Task OnReport01()
        {
            if (Reader01?.Device == null)
                return Task.CompletedTask;


            var buffer = Reader01.Device.InputBuffer;

            byte B6 = buffer[6];

            if (_tracker.CheckRisingEdge(nameof(LNAV_BUTTON), B6.IsBitSet(Bit4))) LNAV_BUTTON?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(ALTHLD_BUTTON), B6.IsBitSet(Bit2))) ALTHLD_BUTTON?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(VS_BUTTON), B6.IsBitSet(Bit3))) VS_BUTTON?.Invoke();


            if (B6.IsBitSet(Bit0))
            {
                ELEVATOR_TRIM_DW?.Invoke();
            }
            else if (B6.IsBitSet(Bit1))
            {
                ELEVATOR_TRIM_UP?.Invoke();
            }


            // ignorar si el avion no tiene tren retractil
            gearController.Update(B6);


            byte B7 = buffer[7];

            if (_tracker.CheckRisingEdge(nameof(FlapsDown), B7.IsBitSet(0))) FlapsDown?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(FlapsUp), B6.IsBitSet(7))) FlapsUp?.Invoke();


            if (_tracker.CheckRisingEdge(nameof(CMD_BUTTON), buffer[9].IsBitSet(Bit4))) CMD_BUTTON?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(CRSR_BUTTON), buffer[9].IsBitSet(Bit5))) CRSR_BUTTON?.Invoke();




            // Throttle

            int raw = Get10BitSigned(((buffer[3] >> 4) | (buffer[4] << 4)) & 0x3FF) + 512;

            int axis = raw.MapRange(8, 970, AXIS_RANGE_MAX, AXIS_RANGE_MIN); // EJE INVERTIDO

            if (_ThottleTracker.HasChanged(Math.Clamp(axis, AXIS_RANGE_MIN, AXIS_RANGE_MAX)))
                ThrottleChanged?.Invoke(_ThottleTracker.Current);

            //Debug.WriteLine($"{raw}");

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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int Get10BitSigned(int raw) => (raw & 0x200) != 0 ? raw | unchecked((int)0xFFFFFC00) : raw;


        //private static void Analyzer(int player, byte[] buffer)
        //{

        //    for (int i = 0; i <= 9; i++)
        //    {
        //        byte b = buffer[i];

        //        if (b == 0) continue;

        //        int v = (buffer[i] & 0x3F);

        //        //// reordenar: esto lo vuelve lineal de 0 a 63
        //        int fixedRaw = v <= 31 ? 31 - v : 95 - v;

        //        string strbin = Convert.ToString(b, 2).PadLeft(8, '0');

        //        // int j = strbin.IndexOf('1');

        //        Debug.WriteLine($"PLAYER {player} : BYTE {i} -> {strbin} | {fixedRaw}");

        //    }

        //}


    }

}
