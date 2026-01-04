using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MC.Core.Unity.Input
{
    public enum Context { Exploration, Menu, KeyBlocked }

    public class InputContextChanger : MonoBehaviour
    {
        [SerializeField] PlayerInput _playerInput;

        string CurrentContext => _playerInput.currentActionMap.name;

        private readonly Dictionary<Context, string> _contextMap = new()
        {
            { Context.Exploration, "Exploration" },
            { Context.Menu, "Menu" },
            { Context.KeyBlocked, "KeyBlock" }
        };

        void Start()
        {
            Debug.Log(CurrentContext);
        }

        void OnEnable()
        {
            InputEventBus.OnChangeContext += ChangeContext;
        }

        void OnDisable()
        {
            InputEventBus.OnChangeContext -= ChangeContext;
        }

        void ChangeContext(Context ctx)
        {
            _playerInput.SwitchCurrentActionMap(_contextMap[ctx]);
            Debug.Log($"Switched to context: {CurrentContext}");
        }
    }

}
