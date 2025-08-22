using MAUI.PinPilot.Fsuipc;
using MAUI.PinPilot.Gauges;

namespace MAUI.PinPilot.Gauges.Generics
{

    public partial class Kohlsman : UserControl
    {
        private readonly string[] _offsets;

        public Kohlsman()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];

            title.Content = Gauge.Instance.GetLabel(GetType().Name);

            SizeChanged += Kohlsman_SizeChanged;
        }

        private void Kohlsman_SizeChanged(object sender, SizeChangedEventArgs e)
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
