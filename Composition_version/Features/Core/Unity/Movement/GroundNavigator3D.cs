using UnityEngine;
using UnityEngine.Events;

namespace Game.Movement
{
    public class GroundNavigator3D : MovementController3D
    {
        // TODO: Extract stats to another component
        [Header("Stats")]
        [SerializeField] float _defaultRunMult = 2;
        [SerializeField] float _defaultJumpForce = 5f, slideSpeed = 7, slideDownSpeed = 10;

        [Header("Events")]

        [SerializeField] UnityEvent<Vector3, bool> _onMoved;
        [SerializeField] UnityEvent _onStopped;
        [SerializeField] UnityEvent _onJumped;
        [SerializeField] UnityEvent<bool> _onGrounded;

        public bool IsActive { get; set; } = true;

        bool _isRunning, _lastIsGrounded = true;
        Vector3 _currentMovementInput, _hitNormal;

        const float _gravityValue = -9.81f;

        bool IsGrounded => _controller.isGrounded;
        bool CanDoGroundActions => IsActive && IsGrounded;

        private void OnControllerColliderHit(ControllerColliderHit hit) => _hitNormal = hit.normal;

        private void Update()
        {
            if (!IsActive) return;

            CalculateAndApplyHorizontalMovement();

            // --- Vertical Velocity Modifiers ---
            HandleGravity();
            SlopeDown();

            DoMove();
        }

        public void Jump()
        {
            if (CanDoGroundActions)
            {
                _velocity.y = _defaultJumpForce;
                _onJumped?.Invoke();
            }
        }

        public void Run(bool running)
        {
            _isRunning = running;
        }

        public override void Move(Vector3 movement)
        {
            if (CanDoGroundActions)
                _currentMovementInput = movement;
        }

        private void CalculateAndApplyHorizontalMovement()
        {
            if (!(IsActive || IsGrounded)) return;

            // Calculate Speed
            var speed = _velocity == Vector3.zero ? 0 : _moveSpeed;
            var runMult = _isRunning ? _defaultRunMult : 1;

            // To Avoid speed increase when diagonal move
            var normalizedMovement = Vector3.ClampMagnitude(_currentMovementInput, 1);

            var finalMove = speed * runMult * normalizedMovement;
            _velocity = new(finalMove.x, _velocity.y, finalMove.z);

            if (finalMove.magnitude > 0)
            {
                transform.LookAt(transform.position + normalizedMovement);
                _onMoved?.Invoke(finalMove, _isRunning);
            }
            else _onStopped?.Invoke();
        }

        private void HandleGravity()
        {
            if (!IsActive) return;

            var yAceleration = _gravityValue * Time.deltaTime;
            _velocity.y += yAceleration;

            var isGrounded = IsGrounded;
            if (isGrounded && _velocity.y < 0) _velocity.y = -1;

            if (_lastIsGrounded != isGrounded)
            {
                _lastIsGrounded = isGrounded;
                _onGrounded?.Invoke(isGrounded);
            }
        }

        void SlopeDown()
        {
            if (!IsActive) return;

            var isOnSlope = Vector3.Angle(Vector3.up, _hitNormal) >= _controller.slopeLimit;
            if (isOnSlope)
            {
                float AddSlideSpeed(float current, float axis)
                {
                    var slopeFactor = 1f - _hitNormal.y;
                    return current + slopeFactor * axis * slideSpeed;
                }

                _velocity = new(
                    AddSlideSpeed(_velocity.x, _hitNormal.x),
                    _velocity.y - slideDownSpeed,
                    AddSlideSpeed(_velocity.z, _hitNormal.z));
            }
        }
    }
}
