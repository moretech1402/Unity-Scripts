using System.Collections.Generic;
using Core.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputController : Singleton<InputController>
    {
        private PlayerInput playerInput;

        public enum InputActions
        {
            Action, Jump, Menu, Escape, Run, Move
        }

        readonly Dictionary<InputActions, string> actionsMap = new(){
            {InputActions.Action, "Action"}, {InputActions.Jump, "Jump"}, {InputActions.Menu, "Menu"}, {InputActions.Escape, "Escape"}, {InputActions.Run, "Run"}, {InputActions.Move, "Move"}
        };

        private struct InputHandlers
        {
            public System.Action<InputAction.CallbackContext> OnPerformed { get; set; }
            public System.Action<InputAction.CallbackContext> OnCanceled { get; set; }
        }

        private Dictionary<InputActions, InputHandlers> inputActions;

        private void InitializeInputActions()
        {
            inputActions = new Dictionary<InputActions, InputHandlers>
            {
                { InputActions.Action, new InputHandlers { OnPerformed = ctx => InputEventManager.InputAction() } },
                { InputActions.Jump, new InputHandlers { OnPerformed = ctx => InputEventManager.InputJump() } },
                { InputActions.Menu, new InputHandlers { OnPerformed = ctx => InputEventManager.InputMenu() } },
                { InputActions.Escape, new InputHandlers { OnPerformed = ctx => InputEventManager.InputEscape() } },
                { InputActions.Run, new InputHandlers {
                        OnPerformed = ctx => InputEventManager.InputRun(true),
                        OnCanceled = ctx => InputEventManager.InputRun(false)
                    }
                }
            };
        }

        private void RegisterEvents(bool register = true)
        {
            foreach (var action in inputActions)
            {
                string actionName = actionsMap[action.Key];
                var handlers = action.Value;

                if (register)
                {
                    if (handlers.OnPerformed != null)
                        playerInput.actions[actionName].performed += handlers.OnPerformed;
                    if (handlers.OnCanceled != null)
                        playerInput.actions[actionName].canceled += handlers.OnCanceled;
                }
                else
                {
                    if (handlers.OnPerformed != null)
                        playerInput.actions[actionName].performed -= handlers.OnPerformed;
                    if (handlers.OnCanceled != null)
                        playerInput.actions[actionName].canceled -= handlers.OnCanceled;
                }
            }
        }

        #region Life Cycle

        private void Update()
        {
            var movement = playerInput.actions[actionsMap[InputActions.Move]].ReadValue<Vector2>();
            InputEventManager.InputMove(movement);
        }

        private void OnEnable()
        {
            playerInput = GetComponent<PlayerInput>();
            InitializeInputActions();
            RegisterEvents();
        }

        private void OnDisable() => RegisterEvents(false);

        #endregion
    }
}
