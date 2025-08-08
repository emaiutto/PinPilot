using System.Diagnostics;
using MauiSoft.SRP.ArduinoComm;
using MauiSoft.SRP.FsuipcWrapper;
using MauiSoft.SRP.Helpers;
using MauiSoft.SRP.MyExtensions;

namespace MauiSoft.SRP.Gauges.C172
{

    public partial class Tachometer : UserControl
    {
        private readonly string[] _offsets;

        CancellationTokenSource cts = new();

        ArduinoSerialConnector? _arduino;

        private readonly ChangeTracker<float> _valueTracker = new();

        public Tachometer()
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

                if (!_valueTracker.HasChanged(OffsetList.Instance.GetValue(_offsets[0]))) return;

                _ = SafeSendAsync(_valueTracker.Current);

                float angle = _valueTracker.Current.MapRange(0, 3500, -28, 203);

                needle.RenderTransform = Graph.GetTransformGroup(0, 0, angle, 0.7);
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en OnRender: {ex.Message}");
            }
        }

        private async Task SafeSendAsync(float value)
        {

            if (_arduino == null || cts?.IsCancellationRequested == true)
                return;

            try
            {
                if (cts == null) return;

                await _arduino.SendAsync($"{value:0}", cts.Token);
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
