using MC.Core.Events;
using MC.Core.Unity.Input;
using MC.Core.Unity.Input.Events;
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

        public Camera CameraRef { get => cameraRef; set => cameraRef = value; }

        Vector2 _currentMovementInput = new();

        private IEventBus _eventBus;

        void OnEnable()
        {
            _eventBus = GlobalEventBus.Instance;
            _eventBus.Subscribe<MoveInputEvent>(OnMoveInput);
            _eventBus.Subscribe<JumpInputEvent>(Jump);
            _eventBus.Subscribe<RunInputEvent>(Run);
            _eventBus.Subscribe<InputActionEvent>(Attack);
        }

        void OnDisable()
        {
            _eventBus.Unsubscribe<MoveInputEvent>(OnMoveInput);
            _eventBus.Unsubscribe<JumpInputEvent>(Jump);
            _eventBus.Unsubscribe<RunInputEvent>(Run);
            _eventBus.Unsubscribe<InputActionEvent>(Attack);
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

        private void OnMoveInput(MoveInputEvent evt) => _currentMovementInput = evt.Value;

        private void Jump(JumpInputEvent evt) => _onJump?.Invoke();

        private void Run(RunInputEvent evt) => _onRun?.Invoke(evt.IsRunning);

        private void Attack(InputActionEvent evt) => _onAttack?.Invoke();
    }
}
