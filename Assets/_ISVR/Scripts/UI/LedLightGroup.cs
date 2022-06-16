using System.Collections.Generic;
using UnityEngine;

namespace ISVR.UI {

    public abstract class LedLightGroup : MonoBehaviour, IActivateable {

        [SerializeField] private List<LedLight> lights;

        public int LastIndex => lights.Count - 1;

        protected List<LedLight> Lights => lights;

        public virtual void Activate() {
            lights[0].Activate();
        }

        public virtual void Deactivate() {
            foreach (LedLight ledLight in lights) {
                ledLight.Deactivate();
            }
        }
    }
}