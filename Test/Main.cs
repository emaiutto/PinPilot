using System.Diagnostics;
using System.Runtime.CompilerServices;
using MauiSoft.SRP.SaitekLibrary;

namespace MauiSoft.SRP
{
    public partial class Main : Form
    {

        static RadioPanel? Saitek;

        public Main() => InitializeComponent();


        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {

                Saitek = new RadioPanel();

                Saitek.EnconderB1R += Saitek_EnconderB1R;
                Saitek.EnconderB1L += Saitek_EnconderB1L;

                Saitek.EnconderS1R += Saitek_EnconderS1R;
                Saitek.EnconderS1L += Saitek_EnconderS1L;

                Saitek.EnconderB2R += Saitek_EnconderB2R;
                Saitek.EnconderB2L += Saitek_EnconderB2L;

                Saitek.EnconderS2R += Saitek_EnconderS2R;
                Saitek.EnconderS2L += Saitek_EnconderS2L;

                Saitek.Buttons1 += Saitek_Buttons1;
                Saitek.Buttons2 += Saitek_Buttons2;


                Saitek.UpdateEvent += Saitek_UpdateEvent;


            }
            catch (Exception ex)
            {
                Debug.WriteLine("RADIO PANEL NOT WORKING!");
                Debug.WriteLine(ex.Message);
            }
            


        }


        private void Saitek_UpdateEvent(int pos, float value)
        {

            switch (pos)
            {

                case 0:
                    ThreadSafe(() => DSP1.Text = value.ToString("00000"));
                    break;

                case 1:
                    ThreadSafe(() => DSP2.Text = value.ToString("00000"));
                    break;

                case 2:
                    ThreadSafe(() => DSP3.Text = value.ToString("00000"));
                    break;

                case 3:
                    ThreadSafe(() => DSP4.Text = value.ToString("00000"));
                    break;
            }
        }




        #region Events

        private void Saitek_Buttons2()
        {
            Debug.WriteLine(Show());
        }

        private void Saitek_Buttons1()
        {
            Debug.WriteLine(Show());
        }

        private void Saitek_EnconderS2L()
        {
            Debug.WriteLine(Show());
        }

        private void Saitek_EnconderS2R()
        {
            Debug.WriteLine(Show());
        }

        private void Saitek_EnconderB2L()
        {
            Debug.WriteLine(Show());
        }

        private void Saitek_EnconderB2R()
        {
            Debug.WriteLine(Show());
        }

        private void Saitek_EnconderS1L()
        {
            Debug.WriteLine(Show());
        }

        private void Saitek_EnconderS1R()
        {
            Debug.WriteLine(Show());
        }

        private void Saitek_EnconderB1L()
        {
            Debug.WriteLine(Show());
        }

        private void Saitek_EnconderB1R()
        {
            Debug.WriteLine(Show());
        }


        #endregion


        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string Show([CallerMemberName] string name = "")
        {
            return name;
        }


        private void TimerSaitek_Tick(object sender, EventArgs e)
        {

            try
            {
                Saitek?.UpdateDebug();
            }
            catch (Exception ex)
            {
                timerSaitek.Stop();

                MessageBox.Show(ex.ToString());

                Debug.WriteLine(ex.ToString());
            }
        }



        private void ThreadSafe(MethodInvoker method)
        {
            if (InvokeRequired)
                Invoke(method);
            else
                method();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            // CLEAR & DISPOSE

            // Hay diferentes opciones. Para testeo está bien que queden los puntitos o los guiones.
            // Se podría verificar si efectivamente luego se liberan los recursos y se pierde la conexión
            // y se apaga completamente (sin enviar 255 al display).

            _ = (Saitek?.DrawDots());

            Saitek?.Dispose();

            //_MCP?.Dispose();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            
        }

    }
}
