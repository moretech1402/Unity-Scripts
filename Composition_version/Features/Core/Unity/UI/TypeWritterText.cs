using System.Collections;
using TMPro;
using UnityEngine;

namespace MC.Core.Unity.UI
{
    public class TypewriterText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textDisplay;

        [Header("Settings")]
        [SerializeField] float charactersPerSecond = 30f;
        [SerializeField] bool useUnscaledTime = false;

        Coroutine _typingRoutine;

        public void Play(string fullText)
        {
            if (_typingRoutine != null)
                StopCoroutine(_typingRoutine);

            _typingRoutine = StartCoroutine(TypeRoutine(fullText));
        }

        public void Skip()
        {
            if (_typingRoutine != null)
            {
                StopCoroutine(_typingRoutine);
                _typingRoutine = null;
            }

            _textDisplay.maxVisibleCharacters = _textDisplay.text.Length;
        }

        IEnumerator TypeRoutine(string fullText)
        {
            _textDisplay.text = fullText;
            _textDisplay.maxVisibleCharacters = 0;

            int totalChars = fullText.Length;
            float delay = 1f / charactersPerSecond;

            for (int i = 0; i <= totalChars; i++)
            {
                _textDisplay.maxVisibleCharacters = i;
                yield return useUnscaledTime
                    ? new WaitForSecondsRealtime(delay)
                    : new WaitForSeconds(delay);
            }

            _typingRoutine = null;
        }
    }
}
