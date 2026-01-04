using MC.Core.Patterns;
using MC.Core.Unity.Input.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MC.Core.Unity.Input // Maybe MC.Game
{
    public class PlayerInputAdapter : MonoBehaviour
    {
        IEventBus _eventBus;

        void OnEnable()
        {
            _eventBus = GlobalEventBus.Instance;
        }

        public void Move(InputAction.CallbackContext ctx)
            => _eventBus.Publish(new MoveInputEvent() { Value = ctx.ReadValue<Vector2>() });

        public void Jump() => _eventBus.Publish(new JumpInputEvent() { });

        public void Action(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
                _eventBus.Publish(new ActionInputEvent() { });
        }

        public void Run(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
                _eventBus.Publish(new RunInputEvent() { IsRunning = true });
            if (ctx.canceled)
                _eventBus.Publish(new RunInputEvent() { IsRunning = false });
        }
    }
}
