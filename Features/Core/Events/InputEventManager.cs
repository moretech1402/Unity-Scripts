using System;
using UnityEngine;

namespace Core.Events
{
    public static class InputEventManager
    {
        public static event Action<Vector2> OnInputMove;
        public static void InputMove(Vector2 move) => OnInputMove?.Invoke(move);

        public static event Action<Vector2> OnInputMouse;
        public static void InputMouse(Vector2 move) => OnInputMouse?.Invoke(move);

        public static event Action OnInputAction;
        public static void InputAction() => OnInputAction?.Invoke();

        public static event Action<bool> OnInputRun = delegate { };
        public static void InputRun(bool run) => OnInputRun?.Invoke(run);

        public static event Action OnInputJump = delegate { };
        public static void InputJump() => OnInputJump();

        public static event Action OnInputMenu = delegate { };
        public static void InputMenu() => OnInputMenu();

        public static event Action OnInputEscape = delegate { };
        public static void InputEscape() => OnInputEscape();
    }

}
