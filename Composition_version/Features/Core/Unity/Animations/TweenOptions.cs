using System;
using UnityEngine;

namespace MC.Core.Unity.Animations
{
    public interface ITweenOptions
    {
        float Duration { get; }
        float Delay { get; }
        Action OnComplete { get; }
    }

    public struct TweenOptions : ITweenOptions
    {
        public float Duration { get; set; }
        public float Delay { get; set; }
        public Action OnComplete { get; set; }
        public EasingType Ease;

        public static TweenOptions Default(float duration) => new()
        {
            Duration = duration,
            Ease = EasingType.OutQuad,
            Delay = 0f,
            OnComplete = null
        };
    }

    [Serializable]
    public struct RotationAnimationData
    {
        public Transform Target;
        public Vector3 EndAngle;
        public RotateType RotateMode;
    }

    public struct PunchOptions : ITweenOptions
    {
        public float Duration { get; set; }
        public float Delay { get; set; }
        public Action OnComplete { get; set; }
        public int Vibrato;
        public float Elasticity;

        public static PunchOptions Default(float duration) => new()
        {
            Duration = duration,
            Vibrato = 8,
            Elasticity = 0.9f,
            Delay = 0f,
            OnComplete = null
        };
    }

    public struct BlinkOptions : ITweenOptions
    {
        public float Duration { get; set; }
        public float Delay { get; set; }
        public Action OnComplete { get; set; }

        public int Blinks;
        public Color BlinkColor;

        public static BlinkOptions Default(float duration) => new()
        {
            Duration = duration,
            Blinks = 2,
            BlinkColor = new Color(1f, 1f, 1f, 0f), // transparente
            Delay = 0f,
            OnComplete = null
        };
    }

    public struct FadeAndDropOptions : ITweenOptions
    {
        public float Duration { get; set; }
        public float Delay { get; set; }
        public Action OnComplete { get; set; }

        public float DropDistance;
        public Color FadeTo;
    }

    public static class BattleTweenPresets
    {
        public static PunchOptions AttackPunch(Action onComplete = null, float duration = 0.35f)
        {
            var options = PunchOptions.Default(duration);
            options.Vibrato = 8;
            options.Elasticity = 0.9f;
            options.OnComplete = onComplete;
            return options;
        }

        public static BlinkOptions HitBlink(Action onComplete = null, float duration = 0.2f)
        {
            var options = BlinkOptions.Default(duration);
            options.Blinks = 2;
            options.BlinkColor = new Color(1f, 1f, 1f, 0f);
            options.OnComplete = onComplete;
            return options;
        }

        public static FadeAndDropOptions FaintedDrop(Action onComplete = null, float duration = 0.5f, float dropDistance = 50f)
        {
            return new FadeAndDropOptions
            {
                Duration = duration,
                DropDistance = dropDistance,
                FadeTo = new Color(1f, 1f, 1f, 0f),
                OnComplete = onComplete
            };
        }
    }
}