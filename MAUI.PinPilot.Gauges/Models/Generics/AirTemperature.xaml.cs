using System.Diagnostics;
using MAUI.PinPilot.Fsuipc;
using MAUI.PinPilot.Helpers;
using Newtonsoft.Json.Linq;

namespace MAUI.PinPilot.Gauges.Generics
{
    public partial class AirTemperature : UserControl
    {
        private readonly string[] _offsets;

        private readonly ChangeTracker<short> _oatTracker = new();
        private readonly ChangeTracker<short> _tatTracker = new();

        public AirTemperature()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];

            Debug.Assert(_offsets.Length >= 2, "Se esperaban al menos 2 offsets para AirTemperature");

            title.Content = Gauge.Instance.GetLabel(GetType().Name);

            SizeChanged += AirTemperature_SizeChanged;

        }

        private void AirTemperature_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Tomar el ancho recién calculado
            double safeWidth = FontSizeHelpers.SafeWidth(e.NewSize.Width);

            // Actualizar fuentes
            title.FontSize = safeWidth * FontSizeHelpers.Small;

            OAT.FontSize = safeWidth * FontSizeHelpers.Medium;

            TAT.FontSize = safeWidth * FontSizeHelpers.Medium;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {

            base.OnRender(drawingContext); // nunca lo omitas si no dibujás nada custom

            if (_oatTracker.HasChanged((short)OffsetList.Instance.GetValue(_offsets[0])))
                OAT.Content = $"OAT {(_oatTracker.Current)}°";

            if (_tatTracker.HasChanged((short)OffsetList.Instance.GetValue(_offsets[1])))
                TAT.Content = $"TAT {(_tatTracker.Current)}°";

        }


    }
}
