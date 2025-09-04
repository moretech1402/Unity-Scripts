using UnityEngine;
using TMPro;
using System.Collections;
using System;

namespace MC.Core.Unity.UI
{
    public class FadingTextUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI _textDisplay;

        [Header("Animation")]

        [Tooltip("Total duration of animation (blurred and movement)")]
        [SerializeField] private float _fadeDuration = 2.0f;

        [Tooltip("Speed at which the text moves upwards (optional)")]
        [SerializeField] private float _moveSpeed = 0.5f;

        [Tooltip("Delay before the animation starts (optional)")]
        [SerializeField] private float _startDelay = 0.0f;

        public Action OnFadeComplete;

        private Coroutine _fadeCoroutine;

        void Awake()
        {
            if (_textDisplay == null)
            {
                Debug.LogError("FadingTextUI: No se encontró un componente TextMeshProUGUI en este GameObject o en el hijo.");
                enabled = false;
            }
        }

        public void Show(string message, Color startColor)
        {
            if (_textDisplay == null) return;

            gameObject.SetActive(true);
            _textDisplay.text = message;
            _textDisplay.color = startColor;

            if (_fadeCoroutine != null)
            {
                StopCoroutine(_fadeCoroutine);
            }
            _fadeCoroutine = StartCoroutine(FadeAndMoveText());
        }
        
        public void Hide()
        {
            if (_fadeCoroutine != null)
            {
                StopCoroutine(_fadeCoroutine);
            }
            gameObject.SetActive(false);
            SetAlpha(0f);
        }

        private IEnumerator FadeAndMoveText()
        {
            yield return new WaitForSeconds(_startDelay);

            float timer = 0f;
            Color initialColor = _textDisplay.color;
            Vector3 startPosition = transform.position;

            while (timer < _fadeDuration)
            {
                timer += Time.deltaTime;
                float progress = timer / _fadeDuration;

                Color currentColor = initialColor;
                currentColor.a = Mathf.Lerp(initialColor.a, 0f, progress);
                _textDisplay.color = currentColor;

                transform.position = startPosition + (_moveSpeed * timer * Vector3.up);

                yield return null;
            }

            SetAlpha(0f);
            gameObject.SetActive(false);

            transform.position = startPosition;
            OnFadeComplete?.Invoke();
        }

        private void SetAlpha(float alpha)
        {
            if (_textDisplay != null)
            {
                Color color = _textDisplay.color;
                color.a = alpha;
                _textDisplay.color = color;
            }
        }
    }
}