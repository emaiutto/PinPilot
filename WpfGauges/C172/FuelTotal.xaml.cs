using MauiSoft.SRP.FsuipcWrapper;
using MauiSoft.SRP.MyExtensions;

namespace MauiSoft.SRP.Gauges.C172
{
    public partial class FuelTotal : UserControl
    {
        private readonly string[] _offsets;

        readonly SolidColorBrush solidColorBrush = new((Color)ColorConverter.ConvertFromString("#FF37FF00"));

        public FuelTotal()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];
        }

        protected override void OnRender(DrawingContext drawingContext)
        {

            base.OnRender(drawingContext); // nunca lo omitas si no dibujás nada custom


            double y = -ActualHeight * 0.7;


            //double left_tank = 15;

            double left_tank = OffsetList.Instance.GetValue(_offsets[0]);

            double angle_left = left_tank.MapRange(0, 26, 145, 36); // MAPPING OK!

            needle_left.RenderTransform = Graph.GetTransformGroup(ActualWidth * 0.05, y, angle_left, 0.4);


            //double right_tank = 15;

            double right_tank = OffsetList.Instance.GetValue(_offsets[1]);

            double angle_right = right_tank.MapRange(0, 26, -145, -36); // MAPPING OK!

            needle_right.RenderTransform = Graph.GetTransformGroup(ActualWidth * 1.5 - ActualWidth * 0.05, y, angle_right, 0.4);


            value.Content = Math.Round(left_tank + right_tank);
            
            value.Foreground = Brushes.Orange;


            
            // ESPECIFICO PARA EL C172

            //byte left_sel_tank = OffsetList.Instance.GetValue(offsets[2]);
            //byte right_sel_tank = OffsetList.Instance.GetValue(offsets[3]);

            //SelTankLeft.Fill = left_sel_tank == 1 ? solidColorBrush : (Brush)Brushes.Gray;
            //SelTankRight.Fill = right_sel_tank == 1 ? solidColorBrush : (Brush)Brushes.Gray;

            //SelTankLeft.RenderTransform = Graph.GetTransformGroup(-6, 14, 0, 2);
            //SelTankRight.RenderTransform = Graph.GetTransformGroup(7, 14, 0, 2);

            //value.Content = total.ToString("0", CultureInfo.InvariantCulture);
            // 3,785411784 litros (el GALON en sistema USA)

        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            value.FontSize = Width * FontSizeHelpers.Medium;
        }
    }
}
