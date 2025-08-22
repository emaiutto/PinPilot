using MAUI.PinPilot.Audio;
using MAUI.PinPilot.Fsuipc;
using MAUI.PinPilot.Helpers;

namespace MAUI.PinPilot.Gauges.Generics
{

    public partial class OnGround : UserControl
    {
        private readonly string[] _offsets;

        private readonly ChangeTracker<bool> _OnGround_StateTracker = new();

        private readonly ChangeTracker<bool> _ParkingBreak_StateTracker = new();

        private readonly ChangeTracker<bool> _PositiveRate_StateTracker = new();


        public OnGround()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];

            SizeChanged += OnGround_SizeChanged;
        }

        private void OnGround_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
            // Tomar el ancho recién calculado
            double safeWidth = FontSizeHelpers.SafeWidth(e.NewSize.Width);

            // Actualizar fuentes
            label.FontSize = safeWidth * FontSizeHelpers.Small;
           
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext); // nunca lo omitas si no dibujás nada custom


            if (_OnGround_StateTracker.HasChanged(Convert.ToBoolean(OffsetList.Instance.GetValue(_offsets[0]))))
            {
                image.Source = _OnGround_StateTracker.Current ? new BitmapImage(new Uri(GaugeResources.Path + "onground.png")) : 
                    new BitmapImage(new Uri(GaugeResources.Path + "onfly.png"));



            }

            if (_OnGround_StateTracker.Current)
            {
                if (_ParkingBreak_StateTracker.HasChanged(Convert.ToBoolean(OffsetList.Instance.GetValue(_offsets[1]))))
                {
                    tile.Background = _ParkingBreak_StateTracker.Current ? Brushes.Red : Brushes.Aqua;


                    AudioPlayer.Play("parking_break_set.wav", "parking_break_released.wav", _ParkingBreak_StateTracker.Current);


                }
            }

            bool x = Convert.ToUInt16(OffsetList.Instance.GetValue(_offsets[2])) > 1000;

            AudioPlayer.Play("positive rate of climb gear up.mp3", _PositiveRate_StateTracker.HasChangedToTrueValue(x));


            // ELEVATION
            //label.Visibility = value0 ? Visibility.Visible : Visibility.Hidden;

            //double elevation = Math.Round(OffsetList.Instance.GetValue(offsets[2]), 0);
            
            //label.Content = $"{elevation} feets";

            
        }

 
    }
}
