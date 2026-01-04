using MC.Core.Patterns;
using MC.Core.Unity.Patterns;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MC.Core.Unity.Input.Context
{
    public struct InputContextChangedEvent
    {
        public InputContext Context;
    }

    public sealed class InputOrchestrator : Singleton<InputOrchestrator>
    {
        [SerializeField] PlayerInput _playerInput;
        IEventBus _eventBus;

        new void Awake()
        {
            base.Awake();
            _eventBus = GlobalEventBus.Instance;
            _eventBus.Subscribe<InputContextChangedEvent>(OnContextChanged);
        }

        void OnDestroy()
        {
            _eventBus.Unsubscribe<InputContextChangedEvent>(OnContextChanged);
        }

        void OnContextChanged(InputContextChangedEvent evt)
        {
            _playerInput.SwitchCurrentActionMap(evt.Context.Id);
        }
    }
}
