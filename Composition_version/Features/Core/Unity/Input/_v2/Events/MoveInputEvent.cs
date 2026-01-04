using UnityEngine;

namespace MC.Core.Unity.Input.Events
{
    public struct MoveInputEvent
    {
        public Vector2 Value;
    }

    public struct JumpInputEvent { }

    public struct RunInputEvent
    {
        public bool IsRunning;
    }

    public struct ActionInputEvent { }
}
