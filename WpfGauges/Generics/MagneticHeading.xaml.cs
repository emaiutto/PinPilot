using MauiSoft.SRP.FsuipcWrapper;
using MauiSoft.SRP.GeoTools;
using MauiSoft.SRP.MyExtensions;

namespace MauiSoft.SRP.Gauges.Generics
{

    public partial class MagneticHeading : UserControl
    {

        private readonly string[] _offsets;

        public MagneticHeading()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];

            title.Content = Gauge.Instance.GetLabel(GetType().Name);
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

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            title.FontSize = Width * FontSizeHelpers.Small;
            value.FontSize = Width * FontSizeHelpers.Medium;
        }
    }
}
