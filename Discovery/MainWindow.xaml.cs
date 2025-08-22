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

        private readonly SideWinder? sideWinder = new(0x045e, 0x0038);

        private readonly int buttonCount = 4;
        private readonly int axisCount = 3;

        private Rectangle[] buttonIndicators;
        private Slider[] axisSliders;

        private byte[] lastBuffer = new byte[7];


        public MainWindow()
        {
            InitializeComponent();

            CreateButtonIndicators();
            CreateAxisSliders();

            sideWinder.BufferUpdate += SideWinder_BufferUpdate;

        }

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

        private void SideWinder_BufferUpdate(byte[] buffer)
        {
            int byteIndex = DetectFirstChange(lastBuffer, buffer);
            if (byteIndex == -1) return;

            byte oldVal = lastBuffer[byteIndex];
            byte newVal = buffer[byteIndex];

            // Actualizar lastBuffer SOLO si hubo cambios
            Array.Copy(buffer, lastBuffer, buffer.Length);

            int bitIndex = DetectChangedBit(oldVal, newVal);

            if (bitIndex == -1 || bitIndex == -2) return;

            bool pressed = (newVal & (1 << bitIndex)) != 0;


            if (byteIndex == 5)
            {
                switch (bitIndex)
                {
                    case 5: EncenderBoton(0, pressed, byteIndex, bitIndex); break;

                    case 2: EncenderBoton(1, pressed, byteIndex, bitIndex); break;

                    case 3: EncenderBoton(2, pressed, byteIndex, bitIndex); break;

                    case 0: EncenderBoton(3, pressed, byteIndex, bitIndex); break;
                }
            }
            

        }

        private void EncenderBoton (int boton, bool pressed, int by, int bi)
        {

            Debug.WriteLine($"BYTE {by} -> BIT {bi}");

            Dispatcher.BeginInvoke(() =>
            {
                buttonIndicators[boton].Fill = pressed ? Brushes.Green : Brushes.Gray;
            });
        }
    

        private void UpdateAxes(byte[] buffer)
        {
            for (int i = 0; i < axisCount; i++)
            {
                int index = (buttonCount + 7) / 8 + i;
                byte value = buffer[index];

                // Thread-safe: actualizar UI solo desde hilo principal
                Dispatcher.BeginInvoke(() =>
                {
                    axisSliders[0].Value = value;
                });
            }
        }


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

    }

}
