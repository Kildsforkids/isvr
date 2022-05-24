using UnityEngine;
using UnityEngine.UI;

namespace ISVR.UI {
    [RequireComponent(typeof(Slider))]
    public class Bar : MonoBehaviour {

        [SerializeField] private Color lowBorderColor;
        [SerializeField] private Color highBorderColor;
        [SerializeField] private Image fillImage;

        private Slider _slider;

        private void Awake() {
            _slider = GetComponent<Slider>();
        }

        public void SetValue(float value) {
            float clampedValue = Mathf.Clamp01(value);
            _slider.value = clampedValue;
            fillImage.color = Color.Lerp(lowBorderColor, highBorderColor, clampedValue);
        }
    }
}