using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using MAUI.PinPilot.Devices;

namespace Discovery
{
    public partial class MainWindow : Window
    {

        private readonly double _minProcessed = -16383;
        private readonly double _maxProcessed = 16383;


        //private readonly ROF? dev = new(0xffff, 0xb004);
        //private readonly double _minRaw = -50;
        //private readonly double _maxRaw = 280;


        private readonly SideWinder? dev = new(0x045e, 0x0038);
        private readonly double _minRaw = 0;
        private readonly double _maxRaw = 255;


        private readonly int buttonCount = 4;
        private readonly int axisCount = 3;

        private Rectangle[]? buttonIndicators;
        private Slider[]? axisSliders;

        private byte[] lastBuffer = new byte[7];


        public MainWindow()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                DrawCenterCross(XPlotCanvas);
                DrawCenterCross(YPlotCanvas);
                DrawCenterCross(ZPlotCanvas);
            };
        }

        protected override void OnInitialized(EventArgs e)
        {

            base.OnInitialized(e);

            try
            {
                //CreateButtonIndicators();
                //CreateAxisSliders();

                if (dev != null)
                {
                    //dev.XAxisUpdate += Dev_XAxisUpdate;
                    //dev.YAxisUpdate += Dev_YAxisUpdate;
                    //dev.ZAxisUpdate += Dev_ZAxisUpdate;

                    dev.Start();
                }

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

        }

        private void Dev_XAxisUpdate(int raw, int filter) 
            => Dispatcher.BeginInvoke(new Action(() => DrawPoint(XPlotCanvas, raw, filter)));
        
        private void Dev_YAxisUpdate(int raw, int filter) 
            => Dispatcher.BeginInvoke(new Action(() => DrawPoint(YPlotCanvas, raw, filter)));

        private void Dev_ZAxisUpdate(int raw, int filter) 
            => Dispatcher.BeginInvoke(new Action(() => DrawPoint(ZPlotCanvas, raw, filter)));


        private void CreateButtonIndicators()
        {
            buttonIndicators = new Rectangle[buttonCount];
            for (int i = 0; i < buttonCount; i++)
            {
                var rect = new Rectangle
                {
                    Width = 40,
                    Height = 40,
                    Fill = Brushes.Gray,
                    Margin = new Thickness(5)
                };
                buttonIndicators[i] = rect;
                ButtonsPanel.Children.Add(rect);
            }
        }

        private void CreateAxisSliders()
        {
            axisSliders = new Slider[axisCount];
            for (int i = 0; i < axisCount; i++)
            {
                var slider = new Slider
                {
                    Minimum = 0,
                    Maximum = 255,
                    Height = 30,
                    Margin = new Thickness(0, 5, 0, 5)
                };
                axisSliders[i] = slider;
                AxesPanel.Children.Add(slider);
            }
        }


        //private void SideWinder_BufferUpdate(byte[] buffer)
        //{
        //    int byteIndex = DetectFirstChange(lastBuffer, buffer);
        //    if (byteIndex == -1) return;

        //    byte oldVal = lastBuffer[byteIndex];
        //    byte newVal = buffer[byteIndex];

        //    // Actualizar lastBuffer SOLO si hubo cambios
        //    Array.Copy(buffer, lastBuffer, buffer.Length);

        //    int bitIndex = DetectChangedBit(oldVal, newVal);

        //    if (bitIndex <0) return;

        //    bool pressed = (newVal & (1 << bitIndex)) != 0;


        //    switch  (byteIndex)
        //    {
        //        case 5:

        //            switch (bitIndex)
        //            {
        //                case 5: EncenderBoton(0, pressed, byteIndex, bitIndex); break;

        //                case 2: EncenderBoton(1, pressed, byteIndex, bitIndex); break;

        //                case 3: EncenderBoton(2, pressed, byteIndex, bitIndex); break;

        //                case 0: EncenderBoton(3, pressed, byteIndex, bitIndex); break;
        //            }
        //            break;

        //        case 1:
        //            Dispatcher.Invoke(() =>
        //            {
        //                axisSliders[0].Value = newVal;
        //                Result.Text = $"Analog {newVal}";
        //            });
        //            break;

        //        case 2:
        //            Dispatcher.Invoke(() =>
        //            {
        //                axisSliders[1].Value = newVal;
        //                Result.Text = $"Analog {newVal}";
        //            });
        //            break;
        //        case 4:
        //            Dispatcher.Invoke(() =>
        //            {
        //                axisSliders[2].Value = newVal;
        //                Result.Text = $"Analog {newVal}";
        //            });
        //            break;
        //    }
         
        //}
    
        public static int DetectFirstChange(byte[] oldBuffer, byte[] newBuffer)
        {
            if (oldBuffer == null || newBuffer == null) return -1;
            if (oldBuffer.Length != newBuffer.Length) return -1;

            for (int i = 0; i < oldBuffer.Length; i++)
            {
                if (oldBuffer[i] != newBuffer[i])
                    return i; // Devuelve el índice del primer cambio
            }

            return -1; // No hubo cambios
        }
        public static int DetectChangedBit(byte oldValue, byte newValue)
        {
            // XOR -> deja en 1 los bits que cambiaron
            byte diff = (byte)(oldValue ^ newValue);

            if (diff == 0)
                return -1; // no hubo cambios

            // Si hay más de un bit cambiado, no es válido para tu caso
            if ((diff & (diff - 1)) != 0)
                return -2; // más de un bit cambió

            // Encontrar el índice del bit (0 = LSB, 7 = MSB)
            int bitIndex = 0;
            while ((diff >>= 1) != 0)
                bitIndex++;

            return bitIndex;
        }

        private void EncenderBoton(int boton, bool pressed, int by, int bi)
        {
            if (buttonIndicators == null) return;

            Dispatcher.Invoke(() =>
            {
                
                buttonIndicators[boton].Fill = pressed ? Brushes.Green : Brushes.Gray;

                string str = $"if (_tracker.CheckRisingEdge(nameof(Button{boton + 1}_Pressed), buffer[{by}].IsBitSet({bi}))) Button{boton + 1}_Pressed?.Invoke();";

                Result.Text = str;


            });
        }




        private void DrawPoint(Canvas canvas, int raw, int processed)
        {
            double width = canvas.ActualWidth;
            double height = canvas.ActualHeight;

            if (width <= 0 || height <= 0)
                return;

            // --- Escalar RAW -> eje X (cero en el centro del canvas) ---
            double xNorm = (raw - _minRaw) / (double)(_maxRaw - _minRaw); // 0..1
            double x = (xNorm * width) - (width / 2); // -width/2 .. +width/2
            x += width / 2; // desplazar para centrar

            // --- Escalar Processed -> eje Y (cero en el medio del canvas) ---
            double yNorm = (processed - _minProcessed) / (double)(_maxProcessed - _minProcessed); // 0..1
            double y = height - (yNorm * height);

            // Dibujar un punto
            Ellipse point = new()
            {
                Width = 1,
                Height = 1,
                Fill = Brushes.LimeGreen
            };

            Canvas.SetLeft(point, x);
            Canvas.SetTop(point, y);

            canvas.Children.Add(point);
        }
                

        private static void DrawCenterCross(Canvas canvas)
        {
            double width = canvas.ActualWidth;
            double height = canvas.ActualHeight;

            // Línea vertical (eje X=0 → centro en ancho/2 si RAW está centrado)
            Line vLine = new()
            {
                X1 = width / 2,
                Y1 = 0,
                X2 = width / 2,
                Y2 = height,
                Stroke = Brushes.Red,
                StrokeThickness = 1
            };

            // Línea horizontal (eje Y=0 → centro en alto/2)
            Line hLine = new()
            {
                X1 = 0,
                Y1 = height / 2,
                X2 = width,
                Y2 = height / 2,
                Stroke = Brushes.Red,
                StrokeThickness = 1
            };

            canvas.Children.Add(vLine);
            canvas.Children.Add(hLine);
        }

    }

}
