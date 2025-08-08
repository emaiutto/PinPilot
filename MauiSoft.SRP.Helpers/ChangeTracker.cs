namespace MauiSoft.SRP.Helpers
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

}
