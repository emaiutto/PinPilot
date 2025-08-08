using System.Diagnostics;
using MauiSoft.SRP.MyExtensions;

namespace MauiSoft.SRP.McpLibrary
{
    public enum GearState
    {
        Unknown,
        Up,
        Down,
        Locked
    }

    public class GearController(int bitUp, int bitDown)
    {
        private GearState _currentState = GearState.Unknown;

        // Eventos públicos que el resto del sistema puede suscribirse
        public event Action ?GearUp;
        public event Action ?GearDown;
        public event Action ?GearLocked;

        // Bits asignados al hardware
        private readonly int _bitUp = bitUp;
        private readonly int _bitDown = bitDown;

        /// <summary>
        /// Llamar esto en cada ciclo con el byte que representa los bits de entrada
        /// </summary>
        public void Update(byte inputByte)
        {
            bool up = inputByte.IsBitSet(_bitUp);
            bool down = inputByte.IsBitSet(_bitDown);

            GearState newState;

            if (up && !down)
                newState = GearState.Up;
            else if (!up && down)
                newState = GearState.Down;
            else if (!up && !down)
                newState = GearState.Locked;
            else
                newState = GearState.Unknown; // Ambos bits activos = error

            if (newState == _currentState)
                return; // no hay cambio → no hacemos nada

            _currentState = newState;

            //Debug.WriteLine($"{_currentState}");

            switch (newState)
            {
                case GearState.Up:
                    GearUp?.Invoke();
                    break;
                case GearState.Down:
                    GearDown?.Invoke();
                    break;
                case GearState.Locked:
                    GearLocked?.Invoke(); // opcional
                    break;
                case GearState.Unknown:
                    // Opcional: podrías loguear o ignorar
                    break;
            }
        }

        public GearState GetCurrentState() => _currentState;
    }

}
