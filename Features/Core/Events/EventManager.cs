using System;
using UnityEngine;

namespace Core.Events
{
    public static class EventManager
    {
        #region Input
        internal static event Action<Vector2> OnInputMove;
        internal static void InputMove(Vector2 move) => OnInputMove?.Invoke(move);

        internal static event Action<bool> OnInputRun;
        internal static void InputRun(bool running) => OnInputRun?.Invoke(running);

        internal static event Action OnInputAction;
        internal static void InputAction() => OnInputAction?.Invoke();

        internal static event Action OnInputMenu;
        internal static void InputMenu() => OnInputMenu?.Invoke();

        internal static event Action OnInputEscape;
        internal static void InputEscape() => OnInputEscape?.Invoke();
        #endregion

        #region Movement
        public static event Action<int, Vector2, float> OnGOMoved;
        public static void GOMoved(int goID, Vector2 direction, float speed) => OnGOMoved?.Invoke(goID, direction, speed);

        internal static event Action<int> OnGOMoveStopped;
        internal static void GOMoveStopped(int goID) => OnGOMoveStopped?.Invoke(goID);

        #endregion
    }

}
