using UnityEngine;

namespace ISVR {

    public class Electrical : MonoBehaviour {

        [SerializeField] private float value;
        [SerializeField] private bool isBug;

        public float Value => value;
        public bool IsBug => isBug;

        private void Start() {
            if (IsBug) {
                GameSetup.Instance.AddBug(this);
            }
        }
    }
}