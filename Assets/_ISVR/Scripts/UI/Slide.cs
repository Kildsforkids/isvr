using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ISVR.UI {
    public class Slide : MonoBehaviour {

        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI text;

        public void SetBackgoundImage(Sprite sprite) {
            image.sprite = sprite;
        }

        public void SetText(string text) {
            this.text.text = text;
        }
    }
}
