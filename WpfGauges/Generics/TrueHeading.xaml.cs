using MauiSoft.SRP.FsuipcWrapper;

namespace MauiSoft.SRP.Gauges.Generics
{

    public partial class TrueHeading : UserControl
    {

        private readonly string[] _offsets;

        public TrueHeading()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];

            title.Content = Gauge.Instance.GetLabel(GetType().Name);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext); // nunca lo omitas si no dibujás nada custom


            double data = Math.Round(OffsetList.Instance.GetValue(_offsets[0]), 0);
            
            value.Content = $"{(int)data:0}°";


        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            title.FontSize = Width * FontSizeHelpers.Small;
            value.FontSize = Width * FontSizeHelpers.Medium;
        }
    }
}
