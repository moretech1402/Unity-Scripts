using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MC.Core.Unity.Animations
{
    public enum EasingType { Linear, OutQuad, OutBack, InCubic }
    public enum RotateType { Fast, FastBeyond360 }

    public interface ITweenService
    {
        void AnchorMove(RectTransform target, Vector2 goal, TweenOptions options);
        void Move(Transform target, Vector3 goal, TweenOptions options);
        void LocalMove(Transform target, Vector3 goal, TweenOptions options);
        void Rotate(RotationAnimationData data, TweenOptions options);
        void LocalRotate(RotationAnimationData data, TweenOptions options);
        void AnchorPunch(RectTransform target, Vector2 punch, PunchOptions options);
        void BlinkGraphic(Graphic target, BlinkOptions options);
        void FadeAndDrop(Graphic target, RectTransform rect, FadeAndDropOptions options);
    }

    public class DOTweenService : ITweenService
    {
        public void AnchorMove(RectTransform target, Vector2 goal, TweenOptions options)
        {
            target.DOAnchorPos(goal, options.Duration)
                  .SetDelay(options.Delay)
                  .SetEase(ConvertEase(options.Ease))
                  .OnComplete(() => options.OnComplete?.Invoke());
        }

        public void Move(Transform target, Vector3 goal, TweenOptions options)
        {
            target.DOMove(goal, options.Duration)
                  .SetDelay(options.Delay)
                  .SetEase(ConvertEase(options.Ease))
                  .OnComplete(() => options.OnComplete?.Invoke());
        }

        public void LocalMove(Transform target, Vector3 goal, TweenOptions options)
        {
            target.DOLocalMove(goal, options.Duration)
                  .SetDelay(options.Delay)
                  .SetEase(ConvertEase(options.Ease))
                  .OnComplete(() => options.OnComplete?.Invoke());
        }

        public void Rotate(RotationAnimationData data, TweenOptions options)
        {
            data.Target.DORotate(data.EndAngle, options.Duration, ConvertRotateMode(data.RotateMode))
            .SetEase(ConvertEase(options.Ease))
            .OnComplete(() => options.OnComplete?.Invoke());
        }

        public void LocalRotate(RotationAnimationData data, TweenOptions options)
        {
            data.Target.DOLocalRotate(
                data.EndAngle,
                options.Duration,
                ConvertRotateMode(data.RotateMode)
            )
            .SetDelay(options.Delay)
            .SetEase(ConvertEase(options.Ease))
            .OnComplete(() => options.OnComplete?.Invoke());
        }

        public void AnchorPunch(RectTransform target, Vector2 punch, PunchOptions options)
        {
            target.DOPunchAnchorPos(
                    punch,
                    options.Duration,
                    options.Vibrato,
                    options.Elasticity
                )
                .SetDelay(options.Delay)
                .OnComplete(() => options.OnComplete?.Invoke());
        }

        public void BlinkGraphic(Graphic target, BlinkOptions options)
        {
            Color original = target.color;

            target.DOColor(options.BlinkColor, options.Duration / (options.Blinks * 2))
                  .SetLoops(options.Blinks * 2, LoopType.Yoyo)
                  .SetDelay(options.Delay)
                  .OnComplete(() =>
                  {
                      target.color = original;
                      options.OnComplete?.Invoke();
                  });
        }

        public void FadeAndDrop(Graphic target, RectTransform rect, FadeAndDropOptions options)
        {
            Color original = target.color;
            Vector2 originalPos = rect.anchoredPosition;
            Vector2 targetPos = originalPos - new Vector2(0, options.DropDistance);

            Sequence seq = DOTween.Sequence();
            seq.Append(rect.DOAnchorPos(targetPos, options.Duration).SetEase(Ease.InQuad));
            seq.Join(target.DOColor(options.FadeTo, options.Duration));
            seq.SetDelay(options.Delay);
            seq.OnComplete(() =>
            {
                // target.color = original;
                // rect.anchoredPosition = originalPos;
                options.OnComplete?.Invoke();
            });
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

}
