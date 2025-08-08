namespace TinyHIDLibrary
{

    public enum DeviceMode
    {
        NonOverlapped = 0,
        Overlapped = 1
    }

    [Flags]
    public enum ShareMode
    {
        Exclusive = 0,
        ShareRead = NativeMethods.FILE_SHARE_READ,
        ShareWrite = NativeMethods.FILE_SHARE_WRITE
    }


    public enum ReadStatus
    {
        Success = 0,
        WaitTimedOut = 1,
        WaitFail = 2,
        NoDataRead = 3,
        ReadError = 4,
        NotConnected = 5
    }

    public class HidDevice : IHidDevice, IDisposable
    {

        public byte[] InputBuffer { get; private set; }

        public byte[] FeaturesBuffer { get; set; }

        public int ReadTimeOut { get; set; } = 5;
        public int WriteTimeOut { get; set; } = 5;
        

        public string DevicePath { get; }
        public string Description { get; }
        
        public HidDeviceCapabilities Capabilities { get; }
        public HidDeviceAttributes Attributes { get; }



        private DeviceMode _deviceReadMode = DeviceMode.NonOverlapped;

        //private DeviceMode _deviceWriteMode = DeviceMode.NonOverlapped;
        //private ShareMode _deviceShareMode = ShareMode.ShareRead | ShareMode.ShareWrite;


        public IntPtr ReadHandle { get; private set; }
        public IntPtr WriteHandle { get; private set; }


        public bool IsOpen { get; private set; }
        public bool IsConnected => HidDevices.IsConnected(DevicePath);


        internal HidDevice(string devicePath, string description)
        {
            DevicePath = devicePath;
            Description = description;

            try
            {
                var hidHandle = OpenDeviceIO(devicePath, DeviceMode.NonOverlapped, NativeMethods.ACCESS_NONE, ShareMode.ShareRead |ShareMode.ShareWrite);

                // Validación mínima (según tipo que devuelva OpenDeviceIO)
                if (hidHandle == IntPtr.Zero)
                    throw new Exception($"No se pudo abrir el dispositivo HID: {devicePath}");

                Attributes = GetDeviceAttributes(hidHandle);
                Capabilities = GetDeviceCapabilities(hidHandle);

                InputBuffer = new byte[Capabilities.InputReportByteLength];
                
                FeaturesBuffer = new byte[Capabilities.FeatureReportByteLength];

                CloseDeviceIO(hidHandle); // tu método que llama a CloseHandle(handle)

            }
            catch (Exception ex)
            {
                throw new Exception($"Error inicializando {nameof(HidDevice)} en '{devicePath}'.", ex);
            }
        }


        public async Task<ReadStatus> ReadAsync() => await Task.Run(Read);



        private ReadStatus Read()
        {
            var status = ReadStatus.NoDataRead;
            uint bytesRead;

            if (_deviceReadMode == DeviceMode.Overlapped)
            {
                var security = new NativeMethods.SECURITY_ATTRIBUTES();
                var overlapped = new NativeOverlapped();

                var overlapTimeout = ReadTimeOut <= 0 ? NativeMethods.WAIT_INFINITE : ReadTimeOut;

                security.lpSecurityDescriptor = IntPtr.Zero;
                security.bInheritHandle = true;
                security.nLength = Marshal.SizeOf(security);

                overlapped.OffsetLow = 0;
                overlapped.OffsetHigh = 0;
                overlapped.EventHandle = NativeMethods.CreateEvent(ref security, 0, 1, string.Empty);

                try
                {
                    unsafe
                    {
                        fixed (byte* bufferPtr = InputBuffer)
                        {
                            var success = NativeMethods.ReadFile(ReadHandle, (IntPtr)bufferPtr, (uint)InputBuffer.Length, out bytesRead, ref overlapped);

                            if (success)
                            {
                                status = ReadStatus.Success;
                            }
                            else
                            {
                                var result = NativeMethods.WaitForSingleObject(overlapped.EventHandle, overlapTimeout);

                                switch (result)
                                {
                                    case NativeMethods.WAIT_OBJECT_0:
                                        status = ReadStatus.Success;
                                        NativeMethods.GetOverlappedResult(ReadHandle, ref overlapped, out bytesRead, false);
                                        break;

                                    case NativeMethods.WAIT_TIMEOUT:
                                        status = ReadStatus.WaitTimedOut;
                                        break;

                                    case NativeMethods.WAIT_FAILED:
                                        status = ReadStatus.WaitFail;
                                        break;

                                    default:
                                        status = ReadStatus.NoDataRead;
                                        break;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    status = ReadStatus.ReadError;
                }
                finally
                {
                    CloseDeviceIO(overlapped.EventHandle);
                }
            }
            else
            {
                try
                {
                    var overlapped = new NativeOverlapped();

                    unsafe
                    {
                        fixed (byte* bufferPtr = InputBuffer)
                        {
                            NativeMethods.ReadFile(ReadHandle, (IntPtr)bufferPtr, (uint)InputBuffer.Length, out bytesRead, ref overlapped);
                        }
                    }

                    status = ReadStatus.Success;
                }
                catch
                {
                    status = ReadStatus.ReadError;
                }
            }

            return status;
        }


        public bool SetFeature()
        {
            
            try
            {
                return NativeMethods.HidD_SetFeature(WriteHandle, FeaturesBuffer, FeaturesBuffer.Length);
            }
            catch (Exception exception)
            {
                throw new Exception($"Error accessing HID device HidD_SetFeature '{DevicePath}'.", exception);
            }

        }


        private static HidDeviceAttributes GetDeviceAttributes(IntPtr hidHandle)
        {
            var deviceAttributes = default(NativeMethods.HIDD_ATTRIBUTES);
            
            deviceAttributes.Size = Marshal.SizeOf(deviceAttributes);
           
            bool success = NativeMethods.HidD_GetAttributes(hidHandle, ref deviceAttributes);

            if (!success) throw new Exception("GetDeviceAttributes failed");

            return new HidDeviceAttributes(deviceAttributes);
        }


        private static HidDeviceCapabilities GetDeviceCapabilities(IntPtr hidHandle)
        {
            var capabilities = default(NativeMethods.HIDP_CAPS);

            var preparsedDataPointer = default(IntPtr);

            if (NativeMethods.HidD_GetPreparsedData(hidHandle, ref preparsedDataPointer))
            {
                int c = NativeMethods.HidP_GetCaps(preparsedDataPointer, ref capabilities);

                // check c.. ? >0 

                NativeMethods.HidD_FreePreparsedData(preparsedDataPointer);
            }

            return new HidDeviceCapabilities(capabilities);
        }


        #region OPEN / CLOSE


        public void OpenDevice() => OpenDevice(DeviceMode.NonOverlapped, DeviceMode.NonOverlapped, ShareMode.ShareRead | ShareMode.ShareWrite);

        public void OpenDevice(DeviceMode readMode, DeviceMode writeMode, ShareMode shareMode)
        {

            if (IsOpen) return;

            _deviceReadMode = readMode;
            //_deviceWriteMode = writeMode;
            //_deviceShareMode = shareMode;

            try
            {
                ReadHandle = OpenDeviceIO(DevicePath, readMode, NativeMethods.GENERIC_READ, shareMode);

                WriteHandle = OpenDeviceIO(DevicePath, writeMode, NativeMethods.GENERIC_WRITE, shareMode);

            }
            catch (Exception exception)
            {
                IsOpen = false;

                throw new Exception("Error opening HID device.", exception);
            }

            IsOpen = ReadHandle != IntPtr.Zero && WriteHandle != IntPtr.Zero;

        }
         

        private static IntPtr OpenDeviceIO(string devicePath, DeviceMode deviceMode, uint deviceAccess, ShareMode shareMode)
        {
            var security = new NativeMethods.SECURITY_ATTRIBUTES();
            var flags = 0;

            if (deviceMode == DeviceMode.Overlapped) flags = NativeMethods.FILE_FLAG_OVERLAPPED;

            security.lpSecurityDescriptor = IntPtr.Zero;
            security.bInheritHandle = true;
            security.nLength = Marshal.SizeOf(security);

            return NativeMethods.CreateFile(devicePath, deviceAccess, (int)shareMode, ref security, NativeMethods.OPEN_EXISTING, flags, hTemplateFile: IntPtr.Zero);
        }


        public void CloseDevice()
        {
            if (!IsOpen) return;
            CloseDeviceIO(ReadHandle);
            CloseDeviceIO(WriteHandle);
            IsOpen = false;
        }


        private static void CloseDeviceIO(IntPtr handle)
        {

            NativeMethods.CancelIoEx(handle, IntPtr.Zero);
            NativeMethods.CloseHandle(handle);
        }


        #endregion



        public bool PingReal(int timeoutMs = 1000)
        {
            if (!IsOpen)
                return false;

            ReadTimeOut = timeoutMs;

            var result = Read();

            return result == ReadStatus.Success;
        }

        public void Dispose()
        {
            if (IsOpen) CloseDevice();

            GC.SuppressFinalize(this); // Evita que el GC llame al finalizador
        }


        /// <summary>
        /// For debug only
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"VendorId = {Attributes.VendorHexId}, ProductId = {Attributes.ProductHexId}, DevicePath = {DevicePath}";


    }

}
