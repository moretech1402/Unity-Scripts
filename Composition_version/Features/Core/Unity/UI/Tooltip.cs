using TMPro;
using UnityEngine;

namespace MC.Core.Unity.UI
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] GameObject _tooltipObject;
        [SerializeField] TextMeshProUGUI _tooltipText;

        void Awake()
        {
            _tooltipObject.SetActive(false);

            if (_tooltipObject == null) _tooltipObject = gameObject;

            if (_tooltipText == null)
            {
                Debug.LogWarning("Tooltip text component is not assigned.", this);
                enabled = false;
            }
        }

        public void Show(string text)
        {
            _tooltipObject.SetActive(true);
            _tooltipText.SetText(text);
        }

        public void Hide()
        {
            _tooltipObject.SetActive(false);
        }
    }
}
