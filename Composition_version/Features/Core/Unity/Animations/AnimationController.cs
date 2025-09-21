using UnityEngine;
using UnityEngine.Events;

namespace MC.Core.Unity.Animations
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] GameObject _gameObject;
        [SerializeField] UnityEvent _onComplete;

        public void FlipX(Vector2 direction)
        {
            if (direction.x != 0)
            {
                Vector3 scale = _gameObject.transform.localScale;
                scale.x = Mathf.Sign(direction.x) * Mathf.Abs(scale.x);
                _gameObject.transform.localScale = scale;
            }
        }

        public void Rotate(RotationAnimationData data)
        { 
            ServiceLocator.Get<ITweenService>().Rotate(data, () => _onComplete?.Invoke());
        }
    }
}
