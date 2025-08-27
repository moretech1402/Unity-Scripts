using System;
using UnityEngine;

namespace Core.Input
{
    public static class InputEventBus
    {
        public static event Action<Vector2> OnInputMove = delegate { };
        public static void InputMove(Vector2 move) => OnInputMove?.Invoke(move);

        public static event Action<Vector2> OnInputMouse = delegate { };
        public static void InputMouse(Vector2 move) => OnInputMouse?.Invoke(move);

        public static event Action OnInputAction = delegate { };
        public static void InputAction() => OnInputAction?.Invoke();

        public static event Action<bool> OnInputRun = delegate { };
        public static void InputRun(bool run) => OnInputRun?.Invoke(run);

        public static event Action OnInputJump = delegate { };
        public static void InputJump() => OnInputJump?.Invoke();

        public static event Action OnInputMenu = delegate { };
        public static void InputMenu() => OnInputMenu?.Invoke();

        public static event Action OnInputEscape = delegate { };
        public static void InputEscape() => OnInputEscape?.Invoke();
    }

}
