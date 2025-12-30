using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MC.Core.Unity.UI
{
    public class AnimatedFillableBar : MonoBehaviour
    {
        [SerializeField] FillableBar _bar;
        [SerializeField] float _animationDuration = 0.5f;
        [SerializeField] UnityEvent _onFilled;

        public float CurrentValue => _bar.CurrentValue;

        Coroutine _animationRoutine;

        public void SetFillAmount(float current, float max)
        {
            float target = (max <= 0f) ? 0f : current / max;
            AnimateTo(target);
        }

        public void SetFillInstant(float current, float max)
        {
            StopAnimation();
            _bar.SetFillAmount(current, max);
            _onFilled?.Invoke();
        }

        void AnimateTo(float targetFill)
        {
            StopAnimation();
            _animationRoutine = StartCoroutine(AnimateRoutine(targetFill));
        }

        IEnumerator AnimateRoutine(float targetFill)
        {
            float startFill = CurrentValue;
            float time = 0f;

            while (time < _animationDuration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / _animationDuration);
                _bar.SetFillAmount(Mathf.Lerp(startFill, targetFill, t));
                yield return null;
            }

            _bar.SetFillAmount(targetFill);
            _animationRoutine = null;
            _onFilled?.Invoke();
        }

        void StopAnimation()
        {
            if (_animationRoutine != null)
            {
                StopCoroutine(_animationRoutine);
                _animationRoutine = null;
            }
        }
    }
}
