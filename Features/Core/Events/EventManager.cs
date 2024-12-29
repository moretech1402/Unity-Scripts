using System;
using System.Collections;
using System.Collections.Generic;
using Core;
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
    }

}
