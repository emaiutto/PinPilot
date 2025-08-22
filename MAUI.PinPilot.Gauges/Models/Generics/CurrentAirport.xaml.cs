using MAUI.PinPilot.Fsuipc;
using MAUI.PinPilot.Gauges;

namespace MAUI.PinPilot.Gauges.Generics
{

    public partial class CurrentAirport : UserControl
    {

        private readonly string[] _offsets;

        string lastvalue ="";

        public CurrentAirport()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];

            SizeChanged += CurrentAirport_SizeChanged;
        }

        private void CurrentAirport_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Tomar el ancho recién calculado
            double safeWidth = FontSizeHelpers.SafeWidth(e.NewSize.Width);

            // Actualizar fuentes
            label.FontSize = safeWidth * FontSizeHelpers.Big;

        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            // primero, solo llamar si label.Content es distinto de lastvalue
            var currentContent = label.Content as string;

            if (currentContent == lastvalue && !string.IsNullOrWhiteSpace(currentContent))
                return;

            // GETVALUE solo se ejecuta si necesitamos actualizar
            var str = OffsetList.Instance.GetValue(_offsets[0]) as string;
            if (string.IsNullOrEmpty(str))
                return;

            label.Content = str;
            lastvalue = str;

        }


    }
}
