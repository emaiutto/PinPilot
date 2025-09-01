using MAUI.PinPilot.MyExtensions;

namespace MAUI.PinPilot.Devices
{
    public class AnalogController(
        int axisRawMin,
        int axisRawMax,
        short axisRangeMin,
        short axisRangeMax,
        float alpha,
        ushort delta,
        int deadZone = 0,
        bool inverted = false)
    {
        public short Value { get; private set; }

        private readonly int _axisRawMin = axisRawMin;
        private readonly int _axisRawMax = axisRawMax;
        private readonly short _axisRangeMin = axisRangeMin;
        private readonly short _axisRangeMax = axisRangeMax;
        private readonly float _alpha = alpha;
        private readonly float _oneMinusAlpha = 1f - alpha;
        private readonly ushort _delta = delta;
        private readonly int _deadZone = deadZone;
        private readonly bool _inverted = inverted;

        private float _smoothedValue;

        public void Process(int raw)
        {
            // Escalar a rango destino
            int axis = raw.MapRange(
                _axisRawMin, _axisRawMax,
                _inverted ? _axisRangeMax : _axisRangeMin,
                _inverted ? _axisRangeMin : _axisRangeMax
            );

            // Aplicar zona muerta
            if (_deadZone > 0)
            {
                if (Math.Abs(axis) <= _deadZone)
                {
                    axis = 0;
                }
                else
                {
                    axis -= Math.Sign(axis) * _deadZone;
                }
            }

            // Filtrado exponencial (EMA)
            _smoothedValue = _alpha * axis + _oneMinusAlpha * _smoothedValue;

            // Actualizar valor solo si supera Delta
            short newValue = short.Clamp((short)_smoothedValue, _axisRangeMin, _axisRangeMax);
            
            if (Math.Abs(newValue - Value) > _delta)
            {
                Value = newValue;
            }
        }

    }
}
