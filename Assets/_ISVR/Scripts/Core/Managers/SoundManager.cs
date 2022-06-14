using UnityEngine;

namespace ISVR.Core.Managers {
    public class SoundManager : MonoBehaviour {

        [SerializeField] private AudioListener audioListener;

        private bool isSilentMode;

        public void ToggleSoundMode() {
            if (isSilentMode) {
                SilentModeOff();
            } else {
                SilentModeOn();
            }
        }

        public void SilentModeOn() {
            audioListener.enabled = false;
            isSilentMode = true;
        }

        public void SilentModeOff() {
            audioListener.enabled = true;
            isSilentMode = false;
        }
    }
}