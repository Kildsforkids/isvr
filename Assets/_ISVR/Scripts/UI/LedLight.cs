using UnityEngine;
using UnityEngine.UI;

namespace ISVR.UI {

    [RequireComponent(typeof(Image))]
    public class LedLight : MonoBehaviour, IActivateable {

        private Image _image;
        private bool _isActive;

        private void Awake() {
            _image = GetComponent<Image>();
        }

        public void Toggle() {
            if (_isActive) {
                Deactivate();
            } else {
                Activate();
            }
        }

        public void Activate() {
            _image.enabled = true;
            _isActive = true;
        }

        public void Deactivate() {
            _image.enabled = false;
            _isActive = false;
        }
    }
}