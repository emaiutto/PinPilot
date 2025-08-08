using System.Windows.Shapes;
using MauiSoft.SRP.Audio;
using MauiSoft.SRP.FsuipcWrapper;
using MauiSoft.SRP.Helpers;

namespace MauiSoft.SRP.Gauges.Generics
{

    public partial class GearPosition : UserControl
    {
        private readonly string[] _offsets;

        private readonly ChangeTracker<int> _GearPosition_Nose_StateTracker = new();

        private readonly ChangeTracker<int> _GearPosition_Right_StateTracker = new();

        private readonly ChangeTracker<int> _GearPosition_Left_StateTracker = new();

        private readonly ChangeTracker<bool> _GearDown_ThreeGreen_StateTracker = new();

        public GearPosition()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];

            SizeChanged += GearPosition_SizeChanged;
        }

        private void GearPosition_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }



        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext); // nunca lo omitas si no dibujás nada custom


            if (_GearPosition_Nose_StateTracker.HasChanged(Convert.ToUInt16(OffsetList.Instance.GetValue(_offsets[0]))))
            {
                UpdateGearLed(Led_Nose, _GearPosition_Nose_StateTracker.Current);
            }

            if (_GearPosition_Right_StateTracker.HasChanged(Convert.ToUInt16(OffsetList.Instance.GetValue(_offsets[1]))))
            {
                UpdateGearLed(Led_Right, _GearPosition_Right_StateTracker.Current);
            }

            if (_GearPosition_Left_StateTracker.HasChanged(Convert.ToUInt16(OffsetList.Instance.GetValue(_offsets[2]))))
            {
                UpdateGearLed(Led_Left, _GearPosition_Left_StateTracker.Current);
            }

            bool all3 = _GearPosition_Nose_StateTracker.Current == 16383 && _GearPosition_Right_StateTracker.Current == 16383 &&                _GearPosition_Left_StateTracker.Current == 16383;


            AudioPlayer.Play("GearDown-ThreeGreen.mp3", _GearDown_ThreeGreen_StateTracker.HasChanged(all3) && _GearDown_ThreeGreen_StateTracker.Current);

        }

        private static void UpdateGearLed(Rectangle led, int gearPosition)
        {

            if (gearPosition == 16383)
            {
                led.Fill = Brushes.Green;
                
            }
            else if (gearPosition == 0)
            {
                led.Fill = Brushes.Gray;
            }
            else
            {
                led.Fill = Brushes.Orange;
            }
        }


    }
}
