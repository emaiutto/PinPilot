using MAUI.PinPilot.Fsuipc;
using MAUI.PinPilot.Helpers;

namespace MAUI.PinPilot.Gauges.Generics
{
    public partial class TrimIndicator : UserControl
    {

        private readonly string[] _offsets;

        private readonly ChangeTracker<double> _valueTracker = new();

        public TrimIndicator()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];

            title.Content = Gauge.Instance.GetLabel(GetType().Name);

            //if (_offsets.Length == 0) return;

            SizeChanged += TrimIndicator_SizeChanged;
        }

        private void TrimIndicator_SizeChanged(object sender, SizeChangedEventArgs e)
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

            if (!_valueTracker.HasChanged(OffsetList.Instance.GetValue(_offsets[0]))) return;
            
            value.Content = _valueTracker.Current.ToString("0.00", CultureInfo.InvariantCulture);

        }
 
    }
}
