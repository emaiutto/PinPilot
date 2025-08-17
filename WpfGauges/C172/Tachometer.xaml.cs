using System.Diagnostics;
using FSUIPC;
using MauiSoft.SRP.ArduinoComm;
using MauiSoft.SRP.FsuipcWrapper;
using MauiSoft.SRP.Helpers;

namespace MauiSoft.SRP.Gauges.C172
{

    public partial class Tachometer : UserControl
    {

        private const string ENGINE_RPM_LVAR = "Eng1_RPM";

        private static readonly double SCALE = 0.7;

        private const int ANGLE_DELTA = 1; // solo actualiza si el cambio supera 1 grado

        private readonly ValueTracker _angleTracker = new(ANGLE_DELTA);

        private ArduinoSerialConnector? _arduino;

        private readonly CancellationTokenSource cts = new();


        //private const int RPM_MIN = 0;
        //private const int RPM_MAX = 3500;

        //private const int ANGLE_MIN = -28;
        //private const int ANGLE_MAX = 203;


        public Tachometer() => InitializeComponent();

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
            base.OnRender(drawingContext);

            try
            {
                int rpm = (int)FSUIPCConnection.ReadLVar(ENGINE_RPM_LVAR);

                //if (rpm < RPM_MIN || rpm > RPM_MAX)
                //    return; // No es imprescindible esta validacion

                if (!_angleTracker.HasChanged(RpmTable.RpmToAngle[rpm]))
                    return; // no se actualiza si el cambio es menor al delta

                needle.RenderTransform = Graph.GetTransformGroup(0, 0, _angleTracker.Current, SCALE);

                _ = SafeSendAsync(rpm);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en OnRender: {ex.Message}");
            }
        }
        

        private async Task SafeSendAsync(int value)
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
