using MC.Core.Unity.Input;
using UnityEngine;
using UnityEngine.Events;

namespace MC.Core.Unity.Movement
{
    public class PlayerMovementAI : MonoBehaviour
    {
        [SerializeField] Camera cameraRef;
        [SerializeField] MoveDirectionStrategySO _moveStrategy;

        [Header("Events")]
        [SerializeField] UnityEvent<Vector3> _onMove;
        [SerializeField] UnityEvent<bool> _onRun;
        [SerializeField] UnityEvent _onJump;
        [SerializeField] UnityEvent _onAttack;

        Vector2 _currentMovementInput = new();

        void OnEnable()
        {
            InputEventBus.OnInputMove += OnMoveInput;
            InputEventBus.OnInputJump += Jump;
            InputEventBus.OnInputRun += Run;
            InputEventBus.OnInputAction += Attack;
        }

        void OnDisable()
        {
            InputEventBus.OnInputMove -= OnMoveInput;
            InputEventBus.OnInputJump -= Jump;
            InputEventBus.OnInputRun -= Run;
            InputEventBus.OnInputAction -= Attack;
        }

        void Update()
        {
            if (_currentMovementInput == Vector2.zero)
            {
                _onMove?.Invoke(Vector2.zero);
                return;
            }

            _onMove?.Invoke(_moveStrategy.GetDirection(_currentMovementInput, cameraRef));
        }

        private void OnMoveInput(Vector2 movement) => _currentMovementInput = movement;

        private void Jump() => _onJump?.Invoke();

        private void Run(bool isRunning) => _onRun?.Invoke(isRunning);

        private void Attack() => _onAttack?.Invoke();
    }
}
