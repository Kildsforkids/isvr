using System.Collections.Generic;
using UnityEngine;

namespace ISVR.UI {
    public class LedPanel : MonoBehaviour, IActivateable {

        private List<IActivateable> leds = new List<IActivateable>();

        private void Start() {
            Transform children;
            IActivateable childrenActivateable;
            for (int i = 0; i < transform.childCount; i++) {
                children = transform.GetChild(i);
                if (children.TryGetComponent(out IActivateable activateable)) {
                    leds.Add(activateable);
                } else {
                    childrenActivateable = children.GetComponentInChildren<IActivateable>();
                    if (childrenActivateable != null) {
                        leds.Add(childrenActivateable);
                    }
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