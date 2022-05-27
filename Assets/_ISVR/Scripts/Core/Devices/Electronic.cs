using UnityEngine;

namespace ISVR.Core.Devices {
    public class Electronic : MonoBehaviour {

        [SerializeField] private float value;

        public float Value {
            get {
                return value;
            }
            set {
                this.value = value;
            }
        }
    }
}
