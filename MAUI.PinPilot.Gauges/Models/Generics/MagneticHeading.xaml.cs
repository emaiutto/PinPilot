using MAUI.PinPilot.Fsuipc;
using MAUI.PinPilot.Gauges;
using MAUI.PinPilot.MyExtensions;

namespace MAUI.PinPilot.Gauges.Generics
{

    public partial class MagneticHeading : UserControl
    {

        private readonly string[] _offsets;

        public MagneticHeading()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];

            title.Content = Gauge.Instance.GetLabel(GetType().Name);

            SizeChanged += MagneticHeading_SizeChanged;
        }

        private void MagneticHeading_SizeChanged(object sender, SizeChangedEventArgs e)
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

            double declination = OffsetList.Instance.GetValue(_offsets[0]);

            double trueHeading = OffsetList.Instance.GetValue(_offsets[1]);

            double result = (trueHeading - declination).Normalize360();

            // REDODNEAR ???

            value.Content = $"{result:0} °";

        }
 
    }
}
