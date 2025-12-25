using MC.Core.Unity.Animation.Driver;
using MC.Core.Unity.Animation.Parameters;
using UnityEngine;

namespace MC.Core.Unity.Animation.Mapping
{
    [CreateAssetMenu(menuName = "Core/Animation/Mappers/Movement")]
    public sealed class MovementAnimationMapperSO : ScriptableObject
    {
        [SerializeField] private AnimationParameterSO _directionX;
        [SerializeField] private AnimationParameterSO _directionY;
        [SerializeField] private AnimationParameterSO _speed;
        [SerializeField] private AnimationParameterSO _isMoving;

        public void Apply(
            IAnimationDriver driver,
            Vector2 direction,
            float speed)
        {
            driver.SetFloat(_directionX.Hash, direction.x);
            driver.SetFloat(_directionY.Hash, direction.y);
            driver.SetFloat(_speed.Hash, speed);
            driver.SetBool(_isMoving.Hash, speed > 0f);
        }
    }
}
