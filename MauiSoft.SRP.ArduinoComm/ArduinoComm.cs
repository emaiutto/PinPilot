using System.Text;
using RJCP.IO.Ports;

namespace MauiSoft.SRP.ArduinoComm
{
    public interface IArduinoSerialConnector : IDisposable
    {
        Task ConnectAsync(CancellationToken cancellationToken = default);
        Task DisconnectAsync();
        Task SendAsync(string message, CancellationToken cancellationToken = default);
        Task<string> ReadAsync(CancellationToken cancellationToken = default);
        bool IsConnected { get; }
    }

    public class ArduinoSerialConnector : IArduinoSerialConnector
    {
        private readonly string? _portName;
        private readonly int _baudRate;

        private SerialPortStream? _serialPort;

        public bool IsConnected => _serialPort?.IsOpen == true;

        public ArduinoSerialConnector(string portName = "COM9", int baudRate = 57600)
        {
            _portName = string.IsNullOrWhiteSpace(portName) ? "COM9" : portName;
            _baudRate = baudRate;
        }


        public async Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            if (_serialPort != null && _serialPort.IsOpen) return;

            _serialPort = new SerialPortStream(_portName, _baudRate)
            {
                NewLine = "\n",
                ReadTimeout = Timeout.Infinite,
                WriteTimeout = Timeout.Infinite
            };

            _serialPort.Open();
            await Task.CompletedTask;
        }

        public async Task DisconnectAsync()
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
                await Task.CompletedTask;
            }
        }

        public async Task SendAsync(string message, CancellationToken cancellationToken = default)
        {

            if (_serialPort == null) return;

            if (!IsConnected) throw new InvalidOperationException("Puerto no conectado");

            byte[] data = Encoding.ASCII.GetBytes(message + "\n"); // UTF8 ??

            await _serialPort.WriteAsync(data, 0, data.Length, cancellationToken);

        }



        public Task<string> ReadAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //public async Task<string> ReadAsync(CancellationToken cancellationToken = default)
        //{
        //    if (!IsConnected) throw new InvalidOperationException("Puerto no conectado");

        //    byte[] buffer = new byte[256];
        //    int bytesRead = await _serialPort.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
        //    return Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();
        //}

        public void Dispose() => _serialPort?.Dispose();

    }
}

