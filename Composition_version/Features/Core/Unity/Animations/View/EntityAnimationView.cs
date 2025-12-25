using MC.Core.Unity.Animation.Driver;
using MC.Core.Unity.Animation.Mapping;
using UnityEngine;

namespace MC.Core.Unity.Animation.View
{
    public sealed class CharacterAnimationView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private MovementAnimationMapperSO _movementMapper;

        private IAnimationDriver _driver;
        private Vector2 _currentFacingDirection = Vector2.down;

        private void Awake()
        {
            _driver = new AnimatorDriver(_animator);
        }

        public void AnimateMovement(Vector2 direction, float speed)
        {
            _currentFacingDirection = direction == Vector2.zero
                ? _currentFacingDirection
                : direction;

            _movementMapper.Apply(
                _driver,
                _currentFacingDirection,
                speed);
        }
    }
}
