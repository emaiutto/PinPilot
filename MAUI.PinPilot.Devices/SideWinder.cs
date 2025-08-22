using System.Runtime.CompilerServices;
using MAUI.PinPilot.Helpers;
using MAUI.PinPilot.TinyHIDLibrary;

namespace MAUI.PinPilot.Devices
{

    public sealed class SideWinder : HidDeviceId, IDisposable
    {

        private readonly HidReader? Reader00;


        //private AnalogController _rudderProcessor = new(dead_zone: 2048, axis_raw_min: -130, axis_raw_max: 295, alpha: 0.9f, delta: 256);

        //private AnalogController _throttleProcessor = new(dead_zone: 0, axis_raw_min: 905, axis_raw_max: 7, alpha: 0.9f, delta: 256);



        private readonly ButtonEdgeTracker _tracker = new();



        #region Events
         
        public delegate void BufferUpdateHandler(byte[] buffer);
        public event BufferUpdateHandler? BufferUpdate;

        public event Handler? Button1_Pressed;
        public event Handler? Button2_Pressed;
        public event Handler? Button3_Pressed;
        public event Handler? Button4_Pressed; 


        #endregion

        public SideWinder(int vendorId, int productId)
        {
           
            Reader00 = new HidReader(vendorId, productId, OnReport00);
            
            Reader00.Start();
 
        }


        private Task OnReport00()
        {

            if (Reader00?.Device == null) return Task.CompletedTask;


            var buffer = Reader00.Device.InputBuffer;


            BufferUpdate?.Invoke(buffer);


            //if (_tracker.CheckRisingEdge(nameof(Button1_Pressed), buffer[9].IsBitSet(0))) Button1_Pressed?.Invoke();

            //if (_tracker.CheckRisingEdge(nameof(Button2_Pressed), buffer[9].IsBitSet(0))) Button2_Pressed?.Invoke();

            //if (_tracker.CheckRisingEdge(nameof(Button3_Pressed), buffer[9].IsBitSet(0))) Button3_Pressed?.Invoke();

            //if (_tracker.CheckRisingEdge(nameof(Button4_Pressed), buffer[9].IsBitSet(0))) Button4_Pressed?.Invoke();


            //_rudderProcessor.ProcessRawRudder(Get10BitSigned(((buffer[4] >> 6) | (buffer[5] << 2)) & 0x3FF));

            return Task.CompletedTask;
        }

 



        #region Liberar Recursos

        public void Dispose()
        {

            Reader00?.Stop();
 
            GC.SuppressFinalize(this);
        }

        #endregion


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int Get10BitSigned(int raw) => (raw & 0x200) != 0 ? raw | unchecked((int)0xFFFFFC00) : raw;


    }

}
