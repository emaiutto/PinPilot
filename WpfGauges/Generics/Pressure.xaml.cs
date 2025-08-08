using MauiSoft.SRP.FsuipcWrapper;

namespace MauiSoft.SRP.Gauges.Generics
{

    public partial class Pressure : UserControl
    {

        private readonly string[] _offsets;

        public Pressure()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];

            title.Content = Gauge.Instance.GetLabel(GetType().Name);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext); // nunca lo omitas si no dibujás nada custom




            double data = OffsetList.Instance.GetValue(_offsets[0]);

            value.Content = data.ToString("0.00", CultureInfo.InvariantCulture);
            

        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            title.FontSize = Width * FontSizeHelpers.Small;
            value.FontSize = Width * FontSizeHelpers.Medium;
        }
    }
}
