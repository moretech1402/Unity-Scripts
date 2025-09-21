using System;
using DG.Tweening;
using UnityEngine;

namespace MC.Core.Unity.Animations
{
    public enum EasingType { Linear, OutQuad, OutBack, InCubic }
    public enum RotateType { Fast, FastBeyond360 }

    public interface ITweenService
    {
        void LocalMoveTo(Transform target, Vector3 goal, float duration,
        EasingType ease = EasingType.OutQuad, Action onComplete = null);
        void LocalRotate(RotationAnimationData data, Action onComplete = null);
        void MoveTo(Transform target, Vector3 goal, float duration,
        EasingType ease = EasingType.OutQuad, Action onComplete = null);
        void Rotate(RotationAnimationData data, Action onComplete = null);
    }

    public class DOTweenService : ITweenService
    {
        public void LocalMoveTo(Transform target, Vector3 goal, float duration,
        EasingType ease = EasingType.OutQuad, Action onComplete = null)
        {
            target.DOLocalMove(goal, duration).SetEase(ConvertEase(ease)).OnComplete(
                () => onComplete?.Invoke());
        }

        public void LocalRotate(RotationAnimationData data, Action onComplete = null)
        {
            data.Target.DOLocalRotate(data.EndAngle, data.Duration, ConvertRotateMode(data.RotateMode))
            .SetEase(Ease.OutQuad).OnComplete(() => onComplete?.Invoke());
        }

        public void MoveTo(Transform target, Vector3 goal, float duration,
        EasingType ease = EasingType.OutQuad, Action onComplete = null)
        {
            target.DOMove(goal, duration).SetEase(ConvertEase(ease)).OnComplete(() => onComplete?.Invoke());
        }

        public void Rotate(RotationAnimationData data, Action onComplete = null)
        {
            data.Target.DORotate(data.EndAngle, data.Duration, ConvertRotateMode(data.RotateMode))
            .SetEase(Ease.OutQuad).OnComplete(() => onComplete?.Invoke());
        }

        private static Ease ConvertEase(EasingType ease)
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
        
        private static RotateMode ConvertRotateMode(RotateType rotateMode)
        {
            return rotateMode switch
            {
                RotateType.FastBeyond360 => RotateMode.FastBeyond360,
                RotateType.Fast => RotateMode.Fast,
                _ => RotateMode.FastBeyond360,
            };
        }
    }

    [Serializable]
    public struct RotationAnimationData
    {
        public Transform Target;
        public Vector3 EndAngle;
        public float Duration;
        public RotateType RotateMode;
        public EasingType Easing;
    }
}
