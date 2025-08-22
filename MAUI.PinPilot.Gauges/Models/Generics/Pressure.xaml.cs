using MAUI.PinPilot.Fsuipc;
using MAUI.PinPilot.Gauges;

namespace MAUI.PinPilot.Gauges.Generics
{

    public partial class Pressure : UserControl
    {

        private readonly string[] _offsets;

        public Pressure()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];

            title.Content = Gauge.Instance.GetLabel(GetType().Name);

            SizeChanged += Pressure_SizeChanged;
        }

        private void Pressure_SizeChanged(object sender, SizeChangedEventArgs e)
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




            double data = OffsetList.Instance.GetValue(_offsets[0]);

            value.Content = data.ToString("0.00", CultureInfo.InvariantCulture);
            

        }

 
    }
}
