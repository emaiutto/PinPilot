using System.Text;
using Microsoft.Extensions.Configuration;
using RJCP.IO.Ports;

namespace MAUI.PinPilot.Arduino
{
    public interface IArduinoSerialConnector : IDisposable
    {
        Task ConnectAsync(CancellationToken cancellationToken = default);
        Task DisconnectAsync();
        Task SendAsync(string message, CancellationToken cancellationToken = default);
        Task<string> ReadAsync(CancellationToken cancellationToken = default);
        bool IsConnected { get; }
    }

    public sealed class ArduinoSerialConnector : IArduinoSerialConnector
    {
        private readonly string _portName;
        private readonly int _baudRate;
        private readonly int _timeout;

        private SerialPortStream? _serialPort;

        public bool IsConnected => _serialPort?.IsOpen == true;

        public ArduinoSerialConnector()
        {
            // Defaults
            string defaultPort = "COM9";
            int defaultBaud = 57600;
            int defaultTimeout = 5000;

            try
            {
                var config = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                   .Build();


                var section = config.GetSection("Arduino");

                _portName = section["PortName"] ?? defaultPort;

                _baudRate = int.TryParse(section["BaudRate"], out var br)
                    ? br : defaultBaud;

                _timeout = int.TryParse(section["Timeout"], out var to)
                    ? to : defaultTimeout;

           }
            catch
            {
                // Si hay error cargando config, usar defaults
                _portName = defaultPort;
                _baudRate = defaultBaud;
                _timeout = defaultTimeout;
            }
        }

        public Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            if (IsConnected) return Task.CompletedTask;

            _serialPort = new SerialPortStream(_portName, _baudRate)
            {
                NewLine = "\n",
                ReadTimeout = _timeout,
                WriteTimeout = _timeout
            };

            _serialPort.Open();
            return Task.CompletedTask;
        }

        public Task DisconnectAsync()
        {
            if (_serialPort is { IsOpen: true })
                _serialPort.Close();

            return Task.CompletedTask;
        }

        public async Task SendAsync(string message, CancellationToken cancellationToken = default)
        {
            if (!IsConnected)
                throw new InvalidOperationException("Puerto no conectado");

            var data = Encoding.ASCII.GetBytes(message + "\n");
            await _serialPort!.WriteAsync(data, 0, data.Length, cancellationToken).ConfigureAwait(false);
        }

        public async Task<string> ReadAsync(CancellationToken cancellationToken = default)
        {
            if (!IsConnected)
                throw new InvalidOperationException("Puerto no conectado");

            var buffer = new byte[256];
            int bytesRead = await _serialPort!.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);

            return Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();
        }

        public void Dispose()
        {
            if (_serialPort != null)
            {
                if (_serialPort.IsOpen)
                    _serialPort.Close();

                _serialPort.Dispose();
                _serialPort = null;
            }
        }

    }
    
}

