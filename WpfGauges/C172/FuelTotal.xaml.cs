using FSUIPC;
using MauiSoft.SRP.MyExtensions;

namespace MauiSoft.SRP.Gauges.C172
{
    public partial class FuelTotal : UserControl
    {
        private const string FuelLeftWingTank = "FuelLeftWingTank";
        private const string FuelRightWingTank = "FuelRightWingTank";
        private const string FSelC172State = "FSelC172State";

        private static readonly SolidColorBrush LedOn = new((Color)ColorConverter.ConvertFromString("#FF37FF00"));
        private static readonly SolidColorBrush LedOff = Brushes.Gray;

        private double _needleY;

        public FuelTotal() => InitializeComponent();

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            // Left tank
            double leftTank = FSUIPCConnection.ReadLVar(FuelLeftWingTank);
            double angleLeft = leftTank.MapRange(0, 26, 145, 36);
            needle_left.RenderTransform = Graph.GetTransformGroup(ActualWidth * 0.05, _needleY, angleLeft, 0.4);

            // Right tank
            double rightTank = FSUIPCConnection.ReadLVar(FuelRightWingTank);
            double angleRight = rightTank.MapRange(0, 26, -145, -36);
            needle_right.RenderTransform = Graph.GetTransformGroup(ActualWidth * 1.5 - ActualWidth * 0.05, _needleY, angleRight, 0.4);

            // Total fuel
            double total = Math.Round(leftTank + rightTank);
            value.Content = total.ToString("0", CultureInfo.InvariantCulture);
            value.Foreground = Brushes.Orange;

            // Fuel selector (0 = Left, 1 = Both, 2 = Right)
            (bool leftSelected, bool rightSelected) = ((int)FSUIPCConnection.ReadLVar(FSelC172State)) switch
            {
                0 => (true, false),
                1 => (true, true),
                2 => (false, true),
                _ => (false, false)
            };

            // Encender luces
            SelTankLeft.Fill = leftSelected ? LedOn : LedOff;
            SelTankRight.Fill = rightSelected ? LedOn : LedOff;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            value.FontSize = Width * FontSizeHelpers.Medium;

            // precálculo del offset vertical de las agujas
            _needleY = -ActualHeight * 0.7;

            // posiciones fijas de los selectores de tanque
            SelTankLeft.RenderTransform = Graph.GetTransformGroup(-6, 14, 0, 2);
            SelTankRight.RenderTransform = Graph.GetTransformGroup(7, 14, 0, 2);
        }
    }
}
