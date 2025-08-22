using MAUI.PinPilot.Fsuipc;
using MAUI.PinPilot.MyExtensions;

namespace MAUI.PinPilot.Gauges.Generics
{

    public partial class Wind : UserControl
    {
        private readonly string[] _offsets;

        public Wind()
        {
            InitializeComponent();

            _offsets = Gauge.Instance.GetOffsets(GetType().Name) ?? [];

            SizeChanged += Wind_SizeChanged;
        }

        private void Wind_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Tomar el ancho recién calculado
            double safeWidth = FontSizeHelpers.SafeWidth(e.NewSize.Width);

            // Actualizar fuentes
            label.FontSize = safeWidth * FontSizeHelpers.Small;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext); // nunca lo omitas si no dibujás nada custom


            double? AmbientWindDirection = OffsetList.Instance.GetValue(_offsets[0]);

            if (AmbientWindDirection == null) return;

            double declination = OffsetList.Instance.GetValue(_offsets[1]);

            double trueHeading = OffsetList.Instance.GetValue(_offsets[2]);

            double MagneticHeading = trueHeading - declination;

            double angulo = (double)(AmbientWindDirection - MagneticHeading + 180d);

            angulo = angulo.Normalize360();
                                     
            
            Uri u = new(GaugeResources.Path + "wind.png");
            
            image.Source = new CroppedBitmap(new BitmapImage(u), new Int32Rect(Index(angulo) * 32, 0, 32, 32));

            // VELOCIDAD DEL VIENTO EN NUDOS (relativo al avion! NO DE SUPERFICIE)
            //double speed = OffsetList.Instance.GetValue(offsets[3]);

            //label.Content = $"{speed:0} kts";
            

        }

        private static int Index(double angulo)
        {

            // Lo invierto y normalizo porque quiero representar de donde provienen los vientos no hacia donde van!
            // y mejor aún a donde impacta el viento en el avión, independientemente del heading


            return (int)(angulo / 10) % 36;
        }
        
    }
}
