using System.Diagnostics;
using System.Runtime.CompilerServices;
using MauiSoft.SRP.FsuipcWrapper;
using MauiSoft.SRP.MyExtensions;

namespace MauiSoft.SRP.McpLibrary
{
    public class ValueTracker(int delta)
    {
        private int _lastValue = int.MinValue;
        private readonly int _delta = delta;

        public bool HasChanged(int newValue)
        {
            if (_lastValue == int.MinValue)
            {
                _lastValue = newValue;
                return true;
            }

            if (Math.Abs(newValue - _lastValue) < _delta)
                return false;

            _lastValue = newValue;
            return true;

        }

        public int Current => _lastValue;
    }

    public class RudderProcessor(int dead_zone, int axis_raw_min, int axis_raw_max, float alpha, int delta)
    {
        
        private readonly int Dead_Zone = dead_zone; // Zona Neutra (4096)
                
        private readonly int Axis_Raw_Min = axis_raw_min; // Valor calibración mínimo

        private readonly int Axis_Raw_Max = axis_raw_max; // Valor calibración Máximo

        private readonly float Alpha = alpha; // Factor Suavizado (0.1f)

        private readonly float OneMinusAlpha = 1f - alpha;

        private ValueTracker _rudderTracker = new(delta);
        
        private float _smoothedValue = 0f;

        const int AXIS_RANGE_MIN = -16384, AXIS_RANGE_MAX = 16384;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ProcessRawRudder(int raw)
        {

#if DEBUG
            //Debug.WriteLine($"RAW {raw}");
#endif

            // Mapear el raw a la escala interna
            //int axis = raw.MapRange(Axis_Raw_Min, Axis_Raw_Max, AXIS_RANGE_MIN, AXIS_RANGE_MAX);

            // Mapear a rango
            int axis = (int)((long)(raw - Axis_Raw_Min) * (AXIS_RANGE_MAX - AXIS_RANGE_MIN) / (Axis_Raw_Max - Axis_Raw_Min) + AXIS_RANGE_MIN);


            // Aplicar zona muerta
            if (axis >= -Dead_Zone && axis <= Dead_Zone)
                axis = 0;
            else
                axis = axis < 0 ? axis + Dead_Zone : axis - Dead_Zone;


            // Filtrado EMA
            _smoothedValue = Alpha * axis + OneMinusAlpha * _smoothedValue;
            int filteredValue = _smoothedValue >= 0 ? (int)_smoothedValue : (int)_smoothedValue; // truncado rápido

            // Rango seguro y cambio significativo
            if (filteredValue < AXIS_RANGE_MIN) filteredValue = AXIS_RANGE_MIN;
            else if (filteredValue > AXIS_RANGE_MAX) filteredValue = AXIS_RANGE_MAX;

            if (_rudderTracker.HasChanged(filteredValue))
            {
#if DEBUG
                //Debug.WriteLine($"RUDDER {filteredValue}");
#endif

                FSUIPCHelper.Instance.Execute("AXIS_RUDDER_SET", filteredValue);
            }
        }

    }

}
