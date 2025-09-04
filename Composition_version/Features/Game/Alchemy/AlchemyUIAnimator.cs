using MC.Core;
using MC.Core.Unity.Animations;
using UnityEngine;

namespace Game.Alchemy
{
    public class AlchemyUIAnimator : MonoBehaviour
    {
        [SerializeField] Transform _bagPanelTransform;
        [SerializeField] float _animationDuration = 0.5f;
        [SerializeField] float _animationDistance = 200f;

        ITweenService TweenService => ServiceLocator.Get<ITweenService>();

        void Start()
        {
            var finalPos = _bagPanelTransform.position;
            _bagPanelTransform.position += Vector3.down * _animationDistance;
            TweenService.MoveTo(_bagPanelTransform, finalPos, _animationDuration, EasingType.OutBack);
        }
    }
}
