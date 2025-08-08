using MauiSoft.SRP.FsuipcWrapper;
using MauiSoft.SRP.Helpers;

namespace MauiSoft.SRP.Gauges.Generics
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
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext); // nunca lo omitas si no dibujás nada custom

            if (!_valueTracker.HasChanged(OffsetList.Instance.GetValue(_offsets[0]))) return;
            
            value.Content = _valueTracker.Current.ToString("0.00", CultureInfo.InvariantCulture);


        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            title.FontSize = Width * FontSizeHelpers.Small;
            value.FontSize = Width * FontSizeHelpers.Medium;
        }
    }
}
