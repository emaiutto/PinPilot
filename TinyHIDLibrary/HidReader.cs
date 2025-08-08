namespace TinyHIDLibrary
{
    public class HidReader
    {
        public HidDevice Device => _device;

        private readonly HidDevice _device;

        private readonly Func<Task> _onReadSuccess;

        private readonly int _delayMs;

        private bool _running = false;

        public HidReader(int VendorId, int ProductId, Func<Task> onReadSuccess, int delayMs = 5)
        {
            _device = HidDevices.GetDevice(VendorId, ProductId);

            _onReadSuccess = onReadSuccess ?? throw new ArgumentNullException(nameof(onReadSuccess));

            _delayMs = delayMs;
        }

        public HidReader(int VendorId, int ProductId, Func<Task> onReadSuccess, string VirtualDevice, int delayMs = 5)
        {
            _device = HidDevices.Enumerate(VendorId, ProductId).Where(dev => dev.DevicePath.Contains(VirtualDevice)).First();

            _onReadSuccess = onReadSuccess ?? throw new ArgumentNullException(nameof(onReadSuccess));

            _delayMs = delayMs;
        }

        public void Start()
        {
            if (_running) return;

            _device.OpenDevice();

            Thread.Sleep(500);

            _running = true;

            _ = Task.Run(ReadLoop);
        }

        public void Stop()
        {
            _device.CloseDevice();

            _running = false;
        }

        private async Task ReadLoop()
        {
            while (_running && Device.IsOpen)
            {
                var status = await Device.ReadAsync();
                
                if (status == ReadStatus.Success) await _onReadSuccess();

                if (_delayMs > 0) await Task.Delay(_delayMs);

            }
        }


    }
}
