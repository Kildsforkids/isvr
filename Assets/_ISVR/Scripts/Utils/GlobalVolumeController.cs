using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Events;

namespace ISVR.Utils {
    public class GlobalVolumeController : MonoBehaviour {

        [SerializeField] private Volume _volume;

        public UnityEvent OnAwake;

        private bool _isNoir;

        private void Awake() {
            OnAwake?.Invoke();
        }

        public void ToggleNoirDefault() {
            if (_isNoir) {
                Default();
            } else {
                Noir();
            }
        }

        public void Default() {
            if (_volume.profile.TryGet(out ColorAdjustments colorAdjustments)) {
                colorAdjustments.saturation.value = 0;
                _isNoir = false;
            }
        }

        public void Noir() {
            if (_volume.profile.TryGet(out ColorAdjustments colorAdjustments)) {
                colorAdjustments.saturation.value = -100;
                _isNoir = true;
            }
        }
    }
}
