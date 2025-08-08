namespace MauiSoft.SRP.Helpers
{

    public class ButtonEdgeTracker
    {

        public enum Edge
        {
            None,
            Rising,
            Falling
        }

        private readonly Dictionary<string, bool> _lastStates = [];

        public bool CheckRisingEdge(string name, bool current)
        {
            bool rising = false;

            if (_lastStates.TryGetValue(name, out var last))
            {
                rising = current && !last;
            }

            _lastStates[name] = current;
            return rising;
        }


        //public bool CheckRisingEdge(string name, bool current)
        //{
        //    bool last = _lastStates.TryGetValue(name, out var prev) && prev;
        //    _lastStates[name] = current;
        //    return current && !last;
        //}

        //public bool CheckFallingEdge(string name, bool current)
        //{
        //    bool last = _lastStates.TryGetValue(name, out var prev) && prev;
        //    _lastStates[name] = current;
        //    return !current && last;
        //}

        //public Edge DetectEdge(string name, bool current)
        //{
        //    bool last = _lastStates.TryGetValue(name, out var prev) && prev;
        //    _lastStates[name] = current;
        //    if (current && !last) return Edge.Rising;
        //    if (!current && last) return Edge.Falling;
        //    return Edge.None;
        //}

    }

}
