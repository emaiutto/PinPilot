using FSUIPC;
using MAUI.PinPilot.Helpers;
using MAUI.PinPilot.MyExtensions;
using MAUI.PinPilot.TinyHIDLibrary;

namespace MAUI.PinPilot.Devices
{

    public sealed class SideWinder : IDisposable
    {

        // AILERON
        private readonly Offset<short> _offset_aileron = new(0x0BB6); // (signed 16-bit, -16383..16383)

        private readonly AnalogController _axis_aileron 
            = new(axisRawMin: 0, axisRawMax: 255, axisRangeMin: -16383, axisRangeMax: 16383, alpha: 0.9f, delta: 256, deadZone: 2048);

        // ELEVATOR
        private readonly Offset<short> _offset_elevator = new(0x0BB2); // (signed 16-bit, -16383..16383)

        private readonly AnalogController _axis_elevator 
            = new(axisRawMin: 0, axisRawMax: 255, axisRangeMin: -16383, axisRangeMax: 16383, alpha: 0.9f, delta: 256, deadZone: 2048);

        
        
        // THROTTLE (temporal en este control para test)

        private readonly Offset<short> _offset_throttle = new(0x089A); // –4096 +16384 (segun documentacion)

        private readonly AnalogController _axis_mixture 
            = new(axisRawMin: 0, axisRawMax: 255, axisRangeMin: -4096, axisRangeMax: 16383, alpha: 0.9f, delta: 32, inverted: true);



        private readonly ButtonEdgeTracker _tracker = new();

        private readonly HidReader? Reader00;


        #region Button Events

        public event Handler? Button1_Pressed;
        public event Handler? Button2_Pressed;
        public event Handler? Button3_Pressed;
        public event Handler? Button4_Pressed;

        #endregion


        public SideWinder(int vendorId, int productId) => Reader00 = new HidReader(vendorId, productId, OnReport00, delayMs: 0);

        public void Start() => Reader00?.Start();


        private Task OnReport00()
        {

            if (Reader00?.Device == null) return Task.CompletedTask;

            var buffer = Reader00.Device.InputBuffer;


            _axis_aileron.Process(buffer[1]);
            
            _axis_elevator.Process(buffer[2]);
            
            _axis_mixture.Process(buffer[4]);



            byte buttons = buffer[5];

            if (_tracker.CheckRisingEdge(nameof(Button1_Pressed), buttons.IsBitSet(5))) Button1_Pressed?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(Button2_Pressed), buttons.IsBitSet(0))) Button2_Pressed?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(Button3_Pressed), buttons.IsBitSet(3))) Button3_Pressed?.Invoke();

            if (_tracker.CheckRisingEdge(nameof(Button4_Pressed), buttons.IsBitSet(2))) Button4_Pressed?.Invoke();


            return Task.CompletedTask;
        }


 
        public void UpdateControls()
        {
            _offset_aileron.Value = _axis_aileron.Value;
            
            _offset_elevator.Value = _axis_elevator.Value;

            _offset_throttle.Value = _axis_mixture.Value;    // TEMPORAL
        }



        #region Liberar Recursos

        public void Dispose()
        {

            Reader00?.Stop();
 
            GC.SuppressFinalize(this);
        }

        #endregion


    }

}
