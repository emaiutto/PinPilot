using MAUI.PinPilot.Fsuipc;

namespace MAUI.PinPilot.Gauges.Generics
{

    public partial class LocalTime : UserControl
    {
        private readonly string[] _offsets;
        public LocalTime()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];

            SizeChanged += LocalTime_SizeChanged;
        }

        private void LocalTime_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            // Tomar el ancho recién calculado
            double safeWidth = FontSizeHelpers.SafeWidth(e.NewSize.Width);

            // Actualizar fuentes
            title.FontSize = safeWidth * FontSizeHelpers.Small;

            value1.FontSize = safeWidth * FontSizeHelpers.Big;
            
            value2.FontSize = safeWidth * FontSizeHelpers.Big;

        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            
            base.OnRender(drawingContext); // nunca lo omitas si no dibujás nada custom

            double HH1 = OffsetList.Instance.GetValue(_offsets[0]);
            double MM1 = OffsetList.Instance.GetValue(_offsets[1]);

            double HH2 = OffsetList.Instance.GetValue(_offsets[2]);
            double MM2 = OffsetList.Instance.GetValue(_offsets[3]);

            value1.Content = $"{(int)HH1:00}:{(int)MM1:00} LT";
            value2.Content = $"{(int)HH2:00}:{(int)MM2:00} UT";


            double offset = -OffsetList.Instance.GetValue(_offsets[4]) / 60;

            title.Content = $"{Gauge.Instance.GetLabel(GetType().Name)} (GMT {offset:0})";

        }
 
    }
}
