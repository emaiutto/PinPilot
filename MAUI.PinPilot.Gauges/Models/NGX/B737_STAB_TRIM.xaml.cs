using MAUI.PinPilot.Fsuipc;
using MAUI.PinPilot.MyExtensions;

namespace MAUI.PinPilot.Gauges.PMDG
{

    public partial class B737_STAB_TRIM : UserControl
    {
        private readonly string[] _offsets;

        public B737_STAB_TRIM()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext); // nunca lo omitas si no dibujás nada custom


            double value = OffsetList.Instance.GetValue(_offsets[0]);
             
            double trim = value.MapRange(0, 6, 4, 10); // Mapping para el trim B738
                       
            double y = trim.MapRange(4, 10, -7, 15);

            needle.RenderTransform = Graph.GetTransformGroup(22, y, 0, 2);

        }

   
    }
}
