namespace MAUI.PinPilot.Helpers
{
    public class ChangeTracker<T> where T : struct
    {
        private T _lastValue;
        private bool _initialized;

        public T Current { get; private set; }

        public ChangeTracker(T? initialValue = null)
        {
            if (initialValue.HasValue)
            {
                Current = initialValue.Value;
                _lastValue = initialValue.Value;
                _initialized = true;
            }
        }

        public bool HasChanged(T newValue)
        {
            Current = newValue;

            if (!_initialized || !EqualityComparer<T>.Default.Equals(newValue, _lastValue))
            {
                _lastValue = newValue;
                _initialized = true;
                return true;
            }

            return false;
        }


        public bool HasChangedToTrueValue(T newValue)
        {
            if (typeof(T) != typeof(bool))
                throw new InvalidOperationException("HasChanged returning current value only makes sense with bool.");


            Current = newValue;

            if (!_initialized || !EqualityComparer<T>.Default.Equals(newValue, _lastValue))
            {
                _lastValue = newValue;

                _initialized = true;

                return Convert.ToBoolean(Current); // <-- esto es lo clave
            }

            return false;
        }


    }

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

        //public int Current => _lastValue;
    }

}
