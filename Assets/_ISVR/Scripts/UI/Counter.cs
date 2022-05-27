using UnityEngine;
using TMPro;

namespace ISVR.UI {
    public class Counter : MonoBehaviour {

        [SerializeField] private string prefix;
        [SerializeField] private TextMeshProUGUI text;

        private int _value;
        
        public void Add(int value = 1) {
            _value += value;
            UpdateText();
        }

        public void Subtract(int value = 1) {
            _value -= value;
            UpdateText();
        }

        private void UpdateText() {
            text.text = prefix + _value.ToString();
        }
    }
}
