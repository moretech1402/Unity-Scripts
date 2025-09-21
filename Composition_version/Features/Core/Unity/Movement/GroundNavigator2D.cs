using UnityEngine;
using UnityEngine.Events;

namespace MC.Core.Unity.Movement {

    public class GroundNavigator2D : MonoBehaviour, INavigator {
        [SerializeField] float _speed = 5f;
        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] UnityEvent<Vector2> _onNavigate;

        void Awake()
        {
            if (_rigidbody == null)
            {
                Debug.LogWarning($"Rigidbody2D not assigned in {nameof(GroundNavigator2D)}. Attempting to get it from the GameObject.");
                enabled = false;
                return;
            }

            _rigidbody.gravityScale = 0;
            _rigidbody.freezeRotation = true;
        }

        public void Navigate(Vector3 direction) {
            var normalizedDirection = (Vector2)direction.normalized;
            _rigidbody.linearVelocity = normalizedDirection * _speed;
            _onNavigate?.Invoke(normalizedDirection);
        }

        public void NavigateTo(Vector3 targetPosition) =>
            Navigate(targetPosition - _rigidbody.transform.position);

        public void Stop() => _rigidbody.linearVelocity = Vector2.zero;

        public bool IsMoving => _rigidbody.linearVelocity.sqrMagnitude > 0.01f;
    }
}
