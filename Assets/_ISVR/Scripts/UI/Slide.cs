using UnityEngine;

namespace ISVR.UI {
    public class Slide : MonoBehaviour {

        private bool _isVisible;
        
        public void Show() {
            if (_isVisible) return;
        }

        public void Hide() {
            if (!_isVisible) return;
        }
    }
}
