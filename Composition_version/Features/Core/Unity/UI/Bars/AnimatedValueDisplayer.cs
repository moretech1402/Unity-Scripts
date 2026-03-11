using TMPro;
using UnityEngine;

namespace MC.Core.Unity.UI
{
    public class AnimatedValueDisplayer : MonoBehaviour
    {
        [SerializeField] private AnimatedFillableBar _fillableBar;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _currentDisplay;
        [SerializeField] private TextMeshProUGUI _maxDisplay;

        [Header("Formatting")]
        [SerializeField] private string _format = "0";

        public void SetValue(float current, float max, bool instant = false)
        {
            UpdateText(current, max);

            if (instant)
                _fillableBar.SetFillInstant(current, max);
            else
                _fillableBar.SetFillAmount(current, max);
        }

        private void UpdateText(float current, float max)
        {
            if (_currentDisplay != null)
                _currentDisplay.text = current.ToString(_format);

            if (_maxDisplay != null)
                _maxDisplay.text = max.ToString(_format);
        }
    }
}