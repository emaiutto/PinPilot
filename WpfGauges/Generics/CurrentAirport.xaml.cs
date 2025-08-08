using MauiSoft.SRP.FsuipcWrapper;
using MauiSoft.SRP.MyExtensions;

namespace MauiSoft.SRP.Gauges.Generics
{

    public partial class CurrentAirport : UserControl
    {

        private readonly string[] _offsets;

        public CurrentAirport()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];

            SizeChanged += CurrentAirport_SizeChanged;
        }

        private void CurrentAirport_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            label.FontSize = e.NewSize.Width * FontSizeHelpers.Big;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            UpdateText();

        }

        private void UpdateText()
        {
            if (_offsets.Length < 1) return;

            string? str = OffsetList.Instance.GetValue(_offsets[0]);

            if (string.IsNullOrWhiteSpace(str))
                str = "####";

            label.Content = str;
        }

    }
}
