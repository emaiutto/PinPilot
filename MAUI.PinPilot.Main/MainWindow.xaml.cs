using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using FSUIPC;
using MAUI.PinPilot.Fsuipc;
using MAUI.PinPilot.Devices;
using MAUI.PinPilot.Audio;
using MAUI.PinPilot.Gauges;
using MAUI.PinPilot.Logger;
using MAUI.PinPilot.Main.Properties;


namespace MauiSoft.SRP
{
    static class UpdateRate
    {
        public const long Fast = 50L;
        public const long Slow = 1000L;
    }

    public static class SIMU
    {
        public static bool Has(string key) => FSUIPCHelper.Instance.ContainsKey(key);
    }

     
    public partial class MainWindow : Window
    {

        private readonly RadioPanel? RadioPanel = new(0x06a3, 0x0d05);
         
        private readonly ROF? MCP = new(0xffff, 0xb004);

        private readonly SideWinder? sideWinder = new(0x045e, 0x0038);


        #region Gauges update

        // Listas para cada frecuencia
        private readonly List<UIElement> fastGauges = [];
        private readonly List<UIElement> slowGauges = [];

        readonly DispatcherTimer timerProfile = new() { Interval = TimeSpan.FromMilliseconds(UpdateRate.Slow) };

        readonly DispatcherTimer fastGaugesTimer = new() { Interval = TimeSpan.FromMilliseconds(UpdateRate.Fast) };
        readonly DispatcherTimer slowGaugesTimer = new() { Interval = TimeSpan.FromMilliseconds(UpdateRate.Slow) };

        #endregion


        public MainWindow() => InitializeComponent();

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Restaurar bounds (si hay valores válidos)
            if (Settings.Default.Width > 0 &&
                Settings.Default.Height > 0)
            {
                this.Left = Settings.Default.Left;
                this.Top = Settings.Default.Top;
                this.Width = Settings.Default.Width;
                this.Height = Settings.Default.Height;
            }

            if (Enum.TryParse(Settings.Default.WindowState, out WindowState ws))
                this.WindowState = ws;

            // Restaurar zoom
            zoomSlider.Value = Settings.Default.Zoom;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            // Si la ventana está normal, usamos las dimensiones actuales.
            // Si está maximizada/minimizada, usamos RestoreBounds (el tamaño real antes de maximizar/minimizar).
            Rect bounds = this.WindowState == WindowState.Normal ?
                          new Rect(this.Left, this.Top, this.Width, this.Height) :
                          this.RestoreBounds;

            Settings.Default.Left = bounds.Left;
            Settings.Default.Top = bounds.Top;
            Settings.Default.Width = bounds.Width;
            Settings.Default.Height = bounds.Height;

            Settings.Default.WindowState = this.WindowState.ToString();

            // Guardar zoom
            Settings.Default.Zoom = zoomSlider.Value;

            // Persistir en disco
            Settings.Default.Save();

            RadioPanel?.ClearDisplay();
        }

 

        protected override void OnInitialized(EventArgs e)
        {

            base.OnInitialized(e);

            labelstatus1.Content = "Waiting for profile...";

            try
            {
                FSUIPCConnection.Open();

                timerProfile.Tick += TimerProfile_Tick;
                timerProfile.Start();
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.Message);

                labelstatus1.Content = "Modo testing...";

                LoadGauges();
            }
        }

 

        private void TimerProfile_Tick(object? sender, EventArgs e)
        {
            try
            {
                // Esto asegura que el SINGLETON y la instancia del OFFSET profile
                // para que se cargue el grupo PROFILE antes de llamar a PROCESS.
                labelstatus1.Content = $"{Profile.Instance.AircraftDescription}";

                FSUIPCConnection.Process("profile");

                if (string.IsNullOrWhiteSpace(Profile.Instance.AircraftProfile)) return;

                timerProfile.Stop();

                labelstatus1.Content = $"{Profile.Instance.AircraftDescription}";

                MCP?.Start();

                RadioPanel?.Start();

                LoadGauges();

                SuscribirEventos();

                fastGaugesTimer.Tick += FastGaugesTimer_Tick;
                fastGaugesTimer.Start();

                slowGaugesTimer.Tick += SlowGaugesTimer_Tick;
                slowGaugesTimer.Start();

            }
            catch (Exception ex)
            {
                timerProfile.Stop();
                Debug.WriteLine(ex.Message);
            }
        }

        private void FastGaugesTimer_Tick(object? sender, EventArgs e)
        {
            try
            {
                FSUIPCConnection.Process("");

                RadioPanel?.Update();

                foreach (var control in fastGauges) control.InvalidateVisual();

            }
            catch (Exception ex)
            {
                fastGaugesTimer.Stop();
                slowGaugesTimer.Stop();

                MessageBox.Show(ex.ToString());
                Debug.WriteLine(ex.ToString());
            }
        }

        private void SlowGaugesTimer_Tick(object? sender, EventArgs e)
        {
            try
            {
                foreach (var control in slowGauges) control.InvalidateVisual();

                var flapspos = FSUIPCConnection.ReadLVar("LandFlapsPos");
                Debug.WriteLine($"{flapspos}");

            }
            catch (Exception ex)
            {
                fastGaugesTimer.Stop();
                slowGaugesTimer.Stop();

                MessageBox.Show(ex.ToString());
                Debug.WriteLine(ex.ToString());
            }
        }
 


        #region SAITEK EVENTS

        private DateTime _lastExecution = DateTime.MinValue;
        private readonly TimeSpan _minInterval = TimeSpan.FromMilliseconds(100);

        private bool CanExecute()
        {
            if (DateTime.UtcNow - _lastExecution < _minInterval)
                return false;

            _lastExecution = DateTime.UtcNow;
            return true;
        }

        private void SaitekRadioPanel_EnconderB1R()
        {
            if (RadioPanel == null) return;
            if (!CanExecute()) return;

            var cmd = RadioPanelConfig.Instance.Get(RadioPanel.Switches[0])?.BRR?.FirstOrDefault();
            if (cmd == null) return;

            FSUIPCHelper.Instance.Execute(cmd);
            AudioPlayer.Play("switch_small.wav");

            Debug.WriteLine(cmd);
        }

        private void SaitekRadioPanel_EnconderB1L()
        {
            if (RadioPanel == null) return;
            if (!CanExecute()) return;

            var cmd = RadioPanelConfig.Instance.Get(RadioPanel.Switches[0])?.BRL?.FirstOrDefault();
            if (cmd == null) return;

            FSUIPCHelper.Instance.Execute(cmd);
            AudioPlayer.Play("switch_small.wav");
        }

        private void SaitekRadioPanel_EnconderS1R()
        {
            if (RadioPanel == null) return;
            if (!CanExecute()) return;

            var cmd = RadioPanelConfig.Instance.Get(RadioPanel.Switches[0])?.SRR?.FirstOrDefault();
            if (cmd == null) return;

            FSUIPCHelper.Instance.Execute(cmd);
            AudioPlayer.Play("switch_small.wav");
        }

        private void SaitekRadioPanel_EnconderS1L()
        {
            if (RadioPanel == null) return;
            if (!CanExecute()) return;

            var cmd = RadioPanelConfig.Instance.Get(RadioPanel.Switches[0])?.SRL?.FirstOrDefault();
            if (cmd == null) return;

            FSUIPCHelper.Instance.Execute(cmd);
            AudioPlayer.Play("switch_small.wav");
        }

        private void SaitekRadioPanel_EnconderB2R()
        {
            if (RadioPanel == null) return;
            if (!CanExecute()) return;

            var cmd = RadioPanelConfig.Instance.Get(RadioPanel.Switches[1])?.BRR?.FirstOrDefault();
            if (cmd == null) return;

            FSUIPCHelper.Instance.Execute(cmd);
            AudioPlayer.Play("switch_small.wav");
        }

        private void SaitekRadioPanel_EnconderB2L()
        {
            if (RadioPanel == null) return;
            if (!CanExecute()) return;

            var cmd = RadioPanelConfig.Instance.Get(RadioPanel.Switches[1])?.BRL?.FirstOrDefault();
            if (cmd == null) return;

            FSUIPCHelper.Instance.Execute(cmd);
            AudioPlayer.Play("switch_small.wav");
        }

        private void SaitekRadioPanel_EnconderS2R()
        {
            if (RadioPanel == null) return;
            if (!CanExecute()) return;

            var cmd = RadioPanelConfig.Instance.Get(RadioPanel.Switches[1])?.SRR?.FirstOrDefault();
            if (cmd == null) return;

            FSUIPCHelper.Instance.Execute(cmd);
            AudioPlayer.Play("switch_small.wav");
        }

        private void SaitekRadioPanel_EnconderS2L()
        {
            if (RadioPanel == null) return;
            if (!CanExecute()) return;

            var cmd = RadioPanelConfig.Instance.Get(RadioPanel.Switches[1])?.SRL?.FirstOrDefault();
            if (cmd == null) return;

            FSUIPCHelper.Instance.Execute(cmd);
            AudioPlayer.Play("switch_small.wav");
        }

        private void SaitekRadioPanel_Buttons1()
        {
            if (RadioPanel == null) return;
            if (!CanExecute()) return;

            var cmd = RadioPanelConfig.Instance.Get(RadioPanel.Switches[0])?.BTN?.FirstOrDefault();
            if (cmd == null) return;

            FSUIPCHelper.Instance.Execute(cmd);
            AudioPlayer.Play("switch_rotary.wav");
        }

        private void SaitekRadioPanel_Buttons2()
        {
            if (RadioPanel == null) return;
            if (!CanExecute()) return;

            var cmd = RadioPanelConfig.Instance.Get(RadioPanel.Switches[1])?.BTN?.FirstOrDefault();
            if (cmd == null) return;

            FSUIPCHelper.Instance.Execute(cmd);
            AudioPlayer.Play("switch_rotary.wav");
        }

        #endregion


        #region MCP EVENTS

        #region ROTARYS

        private void MCP_CRSL_INC() => ExecuteRotary();

        private void MCP_CRSL_DEC() => ExecuteRotary();

        private void MCP_SPEED_INC() => ExecuteRotary();

        private void MCP_SPEED_DEC() => ExecuteRotary();

        private void MCP_HEADING_INC() => ExecuteRotary();

        private void MCP_HEADING_DEC() => ExecuteRotary();

        private void MCP_ALTITUDE_INC() => ExecuteRotary();

        private void MCP_ALTITUDE_DEC() => ExecuteRotary();

        private void MCP_VERSPEED_INC() => ExecuteRotary();

        private void MCP_VERSPEED_DEC() => ExecuteRotary();

        private void MCP_CRSR_INC() => ExecuteRotary();

        private void MCP_CRSR_DEC() => ExecuteRotary();

        private void MCP_ELEVATOR_TRIM_DW() => Run(times: 3);

        private void MCP_ELEVATOR_TRIM_UP() => Run(times: 3);


        #endregion

        #region BUTTONS

        private void MCP_AT_BUTTON() => Run();
        private void MCP_FD_BUTTON() => Run();
        private void MCP_SPD_BUTTON() => Run();
        private void MCP_VNAV_BUTTON() => Run();
        private void MCP_LVL_CHG_BUTTON() => Run();
        private void MCP_HDGSEL_BUTTON() => Run();
        private void MCP_LNAV_BUTTON() => Run();
        private void MCP_APP_BUTTON() => Run();
        private void MCP_ALTHLD_BUTTON() => Run();
        private void MCP_VS_BUTTON() => Run();
        private void MCP_CMD_BUTTON() => Run();
        private void MCP_CWS_BUTTON() => Run();
        private void MCP_CRSL_BUTTON() => Run();
        private void MCP_CRSR_BUTTON() => Run();

        #endregion

        #endregion
 


        private void SuscribirEventos()
        {
      
            if (MCP == null) return;

            if (SIMU.Has("MCP_AT_BUTTON")) MCP.AT_BUTTON += MCP_AT_BUTTON;
            if (SIMU.Has("MCP_FD_BUTTON")) MCP.FD_BUTTON += MCP_FD_BUTTON;
            if (SIMU.Has("MCP_SPD_BUTTON")) MCP.SPD_BUTTON += MCP_SPD_BUTTON;
            if (SIMU.Has("MCP_VNAV_BUTTON")) MCP.VNAV_BUTTON += MCP_VNAV_BUTTON;
            if (SIMU.Has("MCP_LVL_CHG_BUTTON")) MCP.LVL_CHG_BUTTON += MCP_LVL_CHG_BUTTON;
            if (SIMU.Has("MCP_HDGSEL_BUTTON")) MCP.HDGSEL_BUTTON += MCP_HDGSEL_BUTTON;
            if (SIMU.Has("MCP_LNAV_BUTTON")) MCP.LNAV_BUTTON += MCP_LNAV_BUTTON;
            if (SIMU.Has("MCP_APP_BUTTON")) MCP.APP_BUTTON += MCP_APP_BUTTON;
            if (SIMU.Has("MCP_ALTHLD_BUTTON")) MCP.ALTHLD_BUTTON += MCP_ALTHLD_BUTTON;
            if (SIMU.Has("MCP_VS_BUTTON")) MCP.VS_BUTTON += MCP_VS_BUTTON;
            if (SIMU.Has("MCP_CMD_BUTTON")) MCP.CMD_BUTTON += MCP_CMD_BUTTON;
            if (SIMU.Has("MCP_CWS_BUTTON")) MCP.CWS_BUTTON += MCP_CWS_BUTTON;

            if (SIMU.Has("MCP_CRSL_BUTTON")) MCP.CRSL_BUTTON += MCP_CRSL_BUTTON;
            if (SIMU.Has("MCP_CRSR_BUTTON")) MCP.CRSR_BUTTON += MCP_CRSR_BUTTON;

            if (SIMU.Has("MCP_CRSL_INC")) MCP.CRSL_INC += MCP_CRSL_INC;
            if (SIMU.Has("MCP_CRSL_DEC")) MCP.CRSL_DEC += MCP_CRSL_DEC;

            if (SIMU.Has("MCP_SPEED_INC")) MCP.SPEED_INC += MCP_SPEED_INC;
            if (SIMU.Has("MCP_SPEED_DEC")) MCP.SPEED_DEC += MCP_SPEED_DEC;

            if (SIMU.Has("MCP_HEADING_INC")) MCP.HEADING_INC += MCP_HEADING_INC;
            if (SIMU.Has("MCP_HEADING_DEC")) MCP.HEADING_DEC += MCP_HEADING_DEC;

            if (SIMU.Has("MCP_ALTITUDE_INC")) MCP.ALTITUDE_INC += MCP_ALTITUDE_INC;
            if (SIMU.Has("MCP_ALTITUDE_DEC")) MCP.ALTITUDE_DEC += MCP_ALTITUDE_DEC;

            if (SIMU.Has("MCP_VERSPEED_INC")) MCP.VERSPEED_INC += MCP_VERSPEED_INC;
            if (SIMU.Has("MCP_VERSPEED_DEC")) MCP.VERSPEED_DEC += MCP_VERSPEED_DEC;

            if (SIMU.Has("MCP_CRSR_INC")) MCP.CRSR_INC += MCP_CRSR_INC;
            if (SIMU.Has("MCP_CRSR_DEC")) MCP.CRSR_DEC += MCP_CRSR_DEC;

            if (SIMU.Has("MCP_ELEVATOR_TRIM_UP")) MCP.ELEVATOR_TRIM_UP += MCP_ELEVATOR_TRIM_UP;
            if (SIMU.Has("MCP_ELEVATOR_TRIM_DW")) MCP.ELEVATOR_TRIM_DW += MCP_ELEVATOR_TRIM_DW;


            if (SIMU.Has("MCP_FlapsUp")) MCP.FlapsUp += MCP_FlapsUp;
            if (SIMU.Has("MCP_FlapsDown")) MCP.FlapsDown += MCP_FlapsDown;

            if (SIMU.Has("GearController_GearUp"))
                MCP.gearController.GearUp += GearController_GearUp;

            if (SIMU.Has("GearController_GearDown"))
                MCP.gearController.GearDown += GearController_GearDown;

            if (SIMU.Has("GearController_GearLocked"))
                MCP.gearController.GearLocked += GearController_GearLocked;
 
       
            if (RadioPanel == null) return;

            RadioPanel.EnconderB1R += SaitekRadioPanel_EnconderB1R;
            RadioPanel.EnconderB1L += SaitekRadioPanel_EnconderB1L;

            RadioPanel.EnconderS1R += SaitekRadioPanel_EnconderS1R;
            RadioPanel.EnconderS1L += SaitekRadioPanel_EnconderS1L;

            RadioPanel.EnconderB2R += SaitekRadioPanel_EnconderB2R;
            RadioPanel.EnconderB2L += SaitekRadioPanel_EnconderB2L;

            RadioPanel.EnconderS2R += SaitekRadioPanel_EnconderS2R;
            RadioPanel.EnconderS2L += SaitekRadioPanel_EnconderS2L;

            RadioPanel.Buttons1 += SaitekRadioPanel_Buttons1;
            RadioPanel.Buttons2 += SaitekRadioPanel_Buttons2;
 



        }

        private void GearController_GearLocked() => Run();

        private void GearController_GearUp()
        {
            bool IsGround = Convert.ToBoolean(OffsetList.Instance.GetValue("ON_GROUND"));

            if (IsGround) return;

            Run(times: 3);
        }

        private void GearController_GearDown() => Run(times: 3);

        private void MCP_FlapsUp() => Run();

        private void MCP_FlapsDown() => Run();


        private void LoadGauges()
        {
            var gauges = Gauge.Instance.Dictionary;
            if (gauges == null) return;

            double size = Settings.Default.Zoom + 100;

            foreach (var item in gauges)
            {
                var control = Gauge.CreateControlByName(item.Key);
                if (control == null) continue;

                control.Width = size;
                control.Height = size;

                wrappanel1.Children.Add(control);

                // Si es null, asumimos Slow
                var rate = item.Value.UpdateRate?.Equals("fast", StringComparison.OrdinalIgnoreCase) == true
                    ? UpdateRate.Fast
                    : UpdateRate.Slow;

                if (rate == UpdateRate.Fast)
                    fastGauges.Add(control);
                else
                    slowGauges.Add(control);
 
            }
        }



        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Run([CallerMemberName] string command = "", int times = 1)
        {
            try
            {
                for (int i = 0; i < times; i++)
                    FSUIPCHelper.Instance.Execute(command);

                //Debug.WriteLine($"Event: {command}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ExecuteRotary([CallerMemberName] string command = "")
        {
            try
            {
                //Debug.WriteLine(command);

                FSUIPCHelper.Instance.Execute(command);

                string Release = command.Replace("_INC", "_REL").Replace("_DEC", "_REL");

                FSUIPCHelper.Instance.Execute(Release);

                //AudioPlayer.Play("switch_small.wav");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }


        #region ZOOM CONTROL

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            foreach (Control c in wrappanel1.Children)
            {
                c.Width = zoomSlider.Value + 100;
                c.Height = zoomSlider.Value + 100;
            }
        }

        #endregion


    }

}
