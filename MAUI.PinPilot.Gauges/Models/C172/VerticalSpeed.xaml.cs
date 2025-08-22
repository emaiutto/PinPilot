using MAUI.PinPilot.Fsuipc;
using MAUI.PinPilot.Helpers;
using MAUI.PinPilot.MyExtensions;

namespace MAUI.PinPilot.Gauges.C172
{

    public partial class VerticalSpeed : UserControl
    {

        private readonly string[] _offsets;

        private readonly ChangeTracker<float> _valueTracker = new();

        public VerticalSpeed()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext); // nunca lo omitas si no dibujás nada custom

            if (!_valueTracker.HasChanged(OffsetList.Instance.GetValue(_offsets[0]))) return;

            float angle = _valueTracker.Current.MapRange(-2000, 2000, -171, 171);

            angle = float.Clamp(angle, -171, 171);

            needle.RenderTransform = Graph.GetTransformGroup(0, 0, angle, 0.7);

        }


    }
}
