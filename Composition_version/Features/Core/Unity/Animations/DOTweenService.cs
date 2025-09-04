using DG.Tweening;
using UnityEngine;

namespace MC.Core.Unity.Animations
{
    public enum EasingType { Linear, OutQuad, OutBack, InCubic }

    public interface ITweenService
    {
        void MoveTo(Transform target, Vector3 goal, float duration, EasingType ease = EasingType.OutQuad);
    }

    public class DOTweenService : ITweenService
    {
        public void MoveTo(Transform target, Vector3 goal, float duration,
        EasingType ease = EasingType.OutQuad)
        {
            target.DOMove(goal, duration).SetEase(ConvertEase(ease));
        }

        private Ease ConvertEase(EasingType ease)
        {
            return ease switch
            {
                EasingType.Linear => Ease.Linear,
                EasingType.OutQuad => Ease.OutQuad,
                EasingType.OutBack => Ease.OutBack,
                EasingType.InCubic => Ease.InCubic,
                _ => Ease.Linear,
            };
        }
    }
}
