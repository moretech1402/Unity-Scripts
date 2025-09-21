using Core.Unity.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MC.Core.Unity.Input
{
    public class InputController : MonoBehaviour
    {
        Debounce _debounce;

        void Awake()
        {
            _debounce = new Debounce(this, 0.1f);
        }

        public static void Move(InputAction.CallbackContext ctx)
            => InputEventBus.InputMove(ctx.ReadValue<Vector2>());

        public static void Jump() => InputEventBus.InputJump();

        public static void Action(InputAction.CallbackContext ctx) {
            if (ctx.performed)
                InputEventBus.InputAction();
        } 

        public static void Run(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
                InputEventBus.InputRun(true);
            if (ctx.canceled)
                InputEventBus.InputRun(false);
        }

        public static void Escape() => InputEventBus.InputEscape();

        public void Menu(bool enabled)
        {
            _debounce.Execute(() =>
            {
                InputEventBus.InputMenu(enabled);
                InputEventBus.ChangeContext(enabled ? Context.Menu : Context.Exploration);
            });
        }
    }
}
