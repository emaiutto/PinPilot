using System.Diagnostics;
using MAUI.PinPilot.Arduino;
using MAUI.PinPilot.Fsuipc;
using MAUI.PinPilot.Gauges;
using MAUI.PinPilot.Helpers;
using MAUI.PinPilot.MyExtensions;

namespace MAUI.PinPilot.Gauges.MAULE
{

    public partial class Prop_RPM : UserControl
    {
        private readonly string[] _offsets;

        CancellationTokenSource cts = new();

        ArduinoSerialConnector? _arduino;

        private readonly ChangeTracker<float> _valueTracker = new();

        public Prop_RPM()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _arduino = new ArduinoSerialConnector();

                Console.CancelKeyPress += (_, args) =>
                {
                    args.Cancel = true;
                    cts.Cancel();
                };

                await _arduino.ConnectAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                // Cancelación esperada, no hace falta alertar
                Debug.WriteLine("Conexión cancelada por el usuario.");
            }
            catch (UnauthorizedAccessException ex)
            {
                // Acceso denegado al puerto COM, típico error con Arduino
                Debug.WriteLine($"Error de acceso al puerto: {ex.Message}");
                MessageBox.Show("No se pudo acceder al puerto serial. Verificá permisos o si otro programa lo está usando.",
                                "Error de conexión", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                // Error de hardware o desconexión
                Debug.WriteLine($"Error de E/S al conectar con Arduino: {ex.Message}");
                MessageBox.Show("Fallo de comunicación con el dispositivo. Verificá el cable o el puerto.",
                                "Error de conexión", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Cualquier otro error no esperado
                Debug.WriteLine($"Error inesperado al conectar con Arduino: {ex.Message}");
                MessageBox.Show("Ocurrió un error inesperado al conectar con el Arduino.",
                                "Error crítico", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {

            base.OnRender(drawingContext); // nunca lo omitas si no dibujás nada custom

            try
            {

                int factor = (int)OffsetList.Instance.GetValue(_offsets[0]);

                ushort value = (ushort)(factor * 0.025f);


                _ = SafeSendAsync(value);

                double angle = ((double)value).MapRange(0, 3500, -28, 203);

                needle.RenderTransform = Graph.GetTransformGroup(0, 0, angle, 0.7);
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en OnRender: {ex.Message}");
            }
        }

        private async Task SafeSendAsync(ushort value)
        {

            if (_arduino == null || cts?.IsCancellationRequested == true)
                return;

            try
            {
                if (cts == null) return;

                await _arduino.SendAsync($"{value}", cts.Token);
            }
            catch (OperationCanceledException)
            {
                // Cancelado, ignorar
            }
            catch (IOException ex)
            {
                Debug.WriteLine($"Error de E/S al enviar al Arduino: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error inesperado al enviar al Arduino: {ex.Message}");
            }
        }

  
    }
}
