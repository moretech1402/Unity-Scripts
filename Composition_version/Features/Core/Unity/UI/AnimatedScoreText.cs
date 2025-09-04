using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events;

namespace MC.Core.Unity.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AnimatedScoreText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _textDisplay;

        [Header("Animation")]
        [SerializeField] private float _duration = 1.5f;

        [Header("Integration")]
        [SerializeField] private UnityEvent _onScoreChanged;

        private Coroutine _animationCoroutine;

        private void Awake()
        {
            if (_textDisplay == null)
            {
                Debug.LogWarning($"No TextMeshProUGUI assigned in {nameof(AnimatedScoreText)}. Disabling script.");
                enabled = false;
                return;
            }
        }

        /// <summary> Encourage the text to count from scratch to the final value. </summary>
        public void AnimateTo(int finalValue)
        {
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }
            _animationCoroutine = StartCoroutine(AnimateValue(finalValue));
        }

        private IEnumerator AnimateValue(int finalValue)
        {
            float timer = 0f;
            while (timer < _duration)
            {
                timer += Time.deltaTime;
                float progress = timer / _duration;

                int currentValue = (int)Mathf.Lerp(0, finalValue, progress);

                _onScoreChanged?.Invoke();
                
                _textDisplay.SetText(currentValue.ToString());

                yield return null;
            }

            _textDisplay.SetText(finalValue.ToString());
            _animationCoroutine = null;
        }
    }
}