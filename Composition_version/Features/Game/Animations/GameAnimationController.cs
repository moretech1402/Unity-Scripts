using MC.Core;
using MC.Core.Unity.Animations;
using UnityEngine;
using UnityEngine.Events;

namespace MC.Game.Animations
{
    public class GameAnimationController : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private UnityEvent _onComplete;

        public void ArcAttack()
        {
            var tweenService = ServiceLocator.Get<ITweenService>();
            var startPos = _gameObject.transform.localPosition;
            var startAngle = _gameObject.transform.localEulerAngles;

            // --- 1️⃣ Preparation: Raise arm and rotate slightly backwards ---
            Vector3 prepPos = startPos + new Vector3(0, 0.2f, 0);
            Vector3 prepAngle = startAngle + new Vector3(0, 0, 30);
            tweenService.LocalMoveTo(_gameObject.transform, prepPos, 0.07f, EasingType.OutQuad);
            tweenService.LocalRotate(new RotationAnimationData
            {
                Target = _gameObject.transform,
                EndAngle = prepAngle,
                Duration = 0.07f,
                RotateMode = RotateType.FastBeyond360,
                Easing = EasingType.OutQuad
            }, () =>
            {
                // --- 2️⃣ Descending swing: go down below while it rotates forward ---
                Vector3 swingPos = startPos + new Vector3(0, -0.5f, 0);
                Vector3 swingAngle = startAngle + new Vector3(0, 0, -135);
                tweenService.LocalMoveTo(_gameObject.transform, swingPos, 0.15f, EasingType.InCubic);
                tweenService.LocalRotate(new RotationAnimationData
                {
                    Target = _gameObject.transform,
                    EndAngle = swingAngle,
                    Duration = 0.15f,
                    RotateMode = RotateType.FastBeyond360,
                    Easing = EasingType.InCubic
                }, () =>
                {
                    // --- 3️⃣ Return to initial position and rotation ---
                    tweenService.LocalMoveTo(_gameObject.transform, startPos, 0.07f, EasingType.OutQuad);
                    tweenService.LocalRotate(new RotationAnimationData
                    {
                        Target = _gameObject.transform,
                        EndAngle = startAngle,
                        Duration = 0.07f,
                        RotateMode = RotateType.FastBeyond360,
                        Easing = EasingType.OutQuad
                    }, () =>
                    {
                        _onComplete?.Invoke();
                    });
                });
            });
        }

    }
}
