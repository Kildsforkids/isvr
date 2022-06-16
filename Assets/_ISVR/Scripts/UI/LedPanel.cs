using System.Collections.Generic;
using UnityEngine;

namespace ISVR.UI {
    public class LedPanel : MonoBehaviour, IActivateable {

        private List<IActivateable> leds = new List<IActivateable>();

        private void Start() {
            for (int i = 0; i < transform.childCount; i++) {
                if (transform.GetChild(i).TryGetComponent(out IActivateable activateable)) {
                    leds.Add(activateable);
                }
            }
        }

        public void Activate() {
            foreach (var led in leds) {
                led.Activate();
            }
        }

        public void Deactivate() {
            foreach (var led in leds) {
                led.Deactivate();
            }
        }
    }
}