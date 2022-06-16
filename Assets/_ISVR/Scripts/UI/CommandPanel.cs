using UnityEngine;

namespace ISVR.UI {

    public class CommandPanel : MonoBehaviour {

        [SerializeField] private NonlinearLocator locator;
        
        [SerializeField] private Transform projection;
        [SerializeField] private Fingertip fingertip;

        public void Toggle() {
            locator.Toggle();
        }

        public void ToggleBoost() {
            locator.ToggleBoost();
        }
        
        public void ToggleSound() {
            locator.ToggleSound();
        }

        public void VolumeUp() {
            locator.VolumeUp();
        }

        public void VolumeDown() {
            locator.VolumeDown();
        }

        public void ShowProjection() {
            if (projection.gameObject.activeSelf) return;
            projection.gameObject.SetActive(true);
            fingertip.Show();
        }

        public void HideProjection() {
            if (!projection.gameObject.activeSelf) return;
            projection.gameObject.SetActive(false);
        }
    }
}