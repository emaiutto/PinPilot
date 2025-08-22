namespace MAUI.PinPilot.TinyHIDLibrary
{
    public interface IHidDevice
    {

        nint ReadHandle { get; }

        nint WriteHandle { get; }
        
        bool IsOpen { get; }
        
        bool IsConnected { get; }
        
        string Description { get; }
        
        HidDeviceCapabilities Capabilities { get; }
        
        HidDeviceAttributes Attributes { get;  }
        
        string DevicePath { get; }


        void OpenDevice();

        void OpenDevice(DeviceMode readMode, DeviceMode writeMode, ShareMode shareMode);

        void CloseDevice();


        Task<ReadStatus> ReadAsync();
                
        
    }
}
