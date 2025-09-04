using UnityEngine;
using UnityEngine.UI;

namespace MC.Core.Unity.UI
{
    public class FillableBar : MonoBehaviour
    {
        [SerializeField] Image _valueImage;

        void Awake()
        {
            if (_valueImage == null)
            {
                Debug.LogError("FillableBar: Value Image reference is missing.");
                enabled = false;
                return;
            }

            _valueImage.type = Image.Type.Filled;
        }

        public void SetFillAmount(float amount) => _valueImage.fillAmount = Mathf.Clamp01(amount);

        public void SetFillAmount(float current, float max) =>
            SetFillAmount((max == 0f) ? 0f : current / max);

        public void ReverseSetFillAmount(float current, float max) =>
            SetFillAmount((max == 0f) ? 0f : 1f - (current / max));
    }
}
