using UnityEngine;

namespace CustomEventBus.Signals
{
    public class SetCameraPositionSignal
    {
        public readonly Vector2 Focus;

        public SetCameraPositionSignal(Vector2 focus)
        {
            Focus = focus;
        }
    }
}