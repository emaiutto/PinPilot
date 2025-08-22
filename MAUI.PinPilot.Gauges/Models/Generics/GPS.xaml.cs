using MAUI.PinPilot.Fsuipc;
using MAUI.PinPilot.Gauges;
using MAUI.PinPilot.GeoTools;

namespace MAUI.PinPilot.Gauges.Generics
{
    public partial class GPS : UserControl
    {
        private readonly string[] _offsets;

        public GPS()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];

            SizeChanged += GPS_SizeChanged;
        }

        private void GPS_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
            // Tomar el ancho recién calculado
            double safeWidth = FontSizeHelpers.SafeWidth(e.NewSize.Width);

            // Actualizar fuentes
            Lat.FontSize = safeWidth * FontSizeHelpers.GPS;
            Lon.FontSize = safeWidth * FontSizeHelpers.GPS;

        }

        protected override void OnRender(DrawingContext drawingContext)
        {

            base.OnRender(drawingContext); // nunca lo omitas si no dibujás nada custom

            var GPS = new GpsPosition(OffsetList.Instance.GetValue(_offsets[0]),
                                      OffsetList.Instance.GetValue(_offsets[1]));

            Lat.Content = $" {GPS.LatitudeDMS}";

            Lon.Content = $" {GPS.LongitudeDMS}";


        }

    }
}
