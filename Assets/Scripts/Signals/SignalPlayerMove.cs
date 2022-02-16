using UnityEngine;

namespace Signals
{
    public class SignalPlayerMove
    {
        public Vector2 Direction;

        public SignalPlayerMove(Vector2 direction)
        {
            Direction = direction;
        }
    }
}
