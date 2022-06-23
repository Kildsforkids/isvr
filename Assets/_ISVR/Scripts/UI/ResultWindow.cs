using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ISVR.UI {
    public class ResultWindow : MonoBehaviour {

        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI wiretappersResultText;
        [SerializeField] private TextMeshProUGUI electronicsResultText;

        public UnityEvent OnShow;

        public void Show() {
            canvas.enabled = true;
            OnShow?.Invoke();
        }

        public void Hide() =>
            canvas.enabled = false;

        public void UpdateWiretappersResultValue(int value, int max) =>
            wiretappersResultText.text = $"{value} / {max}";

        public void UpdateElectronicsResultValue(int value, int max) =>
            electronicsResultText.text = $"{value} / {max}";
    }
}