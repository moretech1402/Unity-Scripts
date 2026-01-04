using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace MC.Core.Unity.Input
{
    [Serializable]
    public class InputActionEvent : UnityEvent<InputAction.CallbackContext> { }

    [Serializable]
    public struct InputActionEvents
    {
        public bool started;
        public bool performed;
        public bool canceled;
    }

    [Serializable]
    public struct InputBinding
    {
        public string actionName;
        public InputActionReference actionReference;
        public InputActionEvents listenEvents;
        public InputActionEvent callback;
    }

    public class InputAdapterGeneric : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private InputBinding[] bindings;

        private void OnEnable()
        {
            foreach (var binding in bindings)
            {
                var action = GetAction(binding);
                if (action == null) continue;

                var listenEvents = binding.listenEvents;
                if (listenEvents.started) action.started += binding.callback.Invoke;
                if (listenEvents.performed) action.performed += binding.callback.Invoke;
                if (listenEvents.canceled) action.canceled += binding.callback.Invoke;
            }
        }

        private void OnDisable()
        {
            foreach (var binding in bindings)
            {
                var action = GetAction(binding);
                if (action == null) continue;

                var listenEvents = binding.listenEvents;
                if (listenEvents.started) action.started -= binding.callback.Invoke;
                if (listenEvents.performed) action.performed -= binding.callback.Invoke;
                if (listenEvents.canceled) action.canceled -= binding.callback.Invoke;
            }
        }

        private InputAction GetAction(InputBinding binding)
        {
            if (binding.actionReference != null) return binding.actionReference.action;
            if (!string.IsNullOrEmpty(binding.actionName)) return playerInput.actions[binding.actionName];
            return null;
        }
    }
}
