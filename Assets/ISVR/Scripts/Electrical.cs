using UnityEngine;

namespace ISVR {

    public class Electrical : MonoBehaviour {

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