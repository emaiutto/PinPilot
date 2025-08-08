using MauiSoft.SRP.FsuipcWrapper;
using MauiSoft.SRP.GeoTools;

namespace MauiSoft.SRP.Gauges.Generics
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
            Lat.FontSize = e.NewSize.Width * FontSizeHelpers.GPS;
            Lon.FontSize = e.NewSize.Width * FontSizeHelpers.GPS;
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
