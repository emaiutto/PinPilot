using MAUI.PinPilot.Fsuipc;
using MAUI.PinPilot.Helpers;

namespace MAUI.PinPilot.Devices
{

    public class AnalogController(int dead_zone, int axis_raw_min, int axis_raw_max, float alpha, int delta)
    {
        
        private readonly int Dead_Zone = dead_zone;

        private readonly int Axis_Raw_Min = axis_raw_min;

        private readonly int Axis_Raw_Max = axis_raw_max;

        private readonly float Alpha = alpha;

        private readonly float OneMinusAlpha = 1f - alpha;

        private readonly ValueTracker _Tracker = new(delta);

        private float _smoothedValue = 0f;

        const int AXIS_RANGE_MIN = -16384, AXIS_RANGE_MAX = 16384;


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ProcessRawRudder(int raw)
        {

            #if DEBUG
            //Debug.WriteLine($"RAW {raw}");
            #endif

            // Mapear a rango
            int axis = (int)((long)(raw - Axis_Raw_Min) * (AXIS_RANGE_MAX - AXIS_RANGE_MIN) / (Axis_Raw_Max - Axis_Raw_Min) + AXIS_RANGE_MIN);


            // Aplicar zona muerta
            if (axis >= -Dead_Zone && axis <= Dead_Zone)
                axis = 0;
            else
                axis = axis < 0 ? axis + Dead_Zone : axis - Dead_Zone;


            // Filtrado EMA
            _smoothedValue = Alpha * axis + OneMinusAlpha * _smoothedValue;

            int filteredValue = (int)_smoothedValue;


            // Rango seguro y cambio significativo
            if (filteredValue < AXIS_RANGE_MIN) filteredValue = AXIS_RANGE_MIN;
            else if (filteredValue > AXIS_RANGE_MAX) filteredValue = AXIS_RANGE_MAX;

            if (_Tracker.HasChanged(filteredValue))
            {
                #if DEBUG
                //Debug.WriteLine($"{filteredValue}");
                #endif

                FSUIPCHelper.Instance.Execute("AXIS_RUDDER_SET", filteredValue);
            }
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ProcessRawThrottle(int raw)
        {

            #if DEBUG
            //Debug.WriteLine($"RAW {raw}");
            #endif

            // Mapear el raw a la escala interna
            int axis = (int)((long)(raw - Axis_Raw_Min) *
                        (AXIS_RANGE_MAX - AXIS_RANGE_MIN) /
                        (Axis_Raw_Max - Axis_Raw_Min) + AXIS_RANGE_MIN);

            // Filtrado EMA (exponencial)
            _smoothedValue = Alpha * axis + OneMinusAlpha * _smoothedValue;

            int filteredValue = (int)_smoothedValue;

            // Clampear al rango válido
            if (filteredValue < AXIS_RANGE_MIN) filteredValue = AXIS_RANGE_MIN;
            else if (filteredValue > AXIS_RANGE_MAX) filteredValue = AXIS_RANGE_MAX;

            // Enviar solo si hubo cambio significativo
            if (_Tracker.HasChanged(filteredValue))
            {
                #if DEBUG
                //Debug.WriteLine($"{filteredValue}");
                #endif

                FSUIPCHelper.Instance.Execute("AXIS_THROTTLE_SET", filteredValue);
            }
        }

    }

}
