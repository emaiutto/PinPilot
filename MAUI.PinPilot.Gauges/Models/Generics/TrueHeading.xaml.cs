using MAUI.PinPilot.Fsuipc;

namespace MAUI.PinPilot.Gauges.Generics
{

    public partial class TrueHeading : UserControl
    {

        private readonly string[] _offsets;

        public TrueHeading()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];

            title.Content = Gauge.Instance.GetLabel(GetType().Name);

            SizeChanged += TrueHeading_SizeChanged;
        }

        private void TrueHeading_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Tomar el ancho recién calculado
            double safeWidth = FontSizeHelpers.SafeWidth(e.NewSize.Width);

            // Actualizar fuentes
            title.FontSize = safeWidth * FontSizeHelpers.Small;
            value.FontSize = safeWidth * FontSizeHelpers.Medium;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext); // nunca lo omitas si no dibujás nada custom


            double data = Math.Round(OffsetList.Instance.GetValue(_offsets[0]), 0);
            
            value.Content = $"{(int)data:0}°";

        }
 
    }
}
