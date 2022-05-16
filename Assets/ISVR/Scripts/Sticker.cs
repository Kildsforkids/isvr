using UnityEngine;

namespace ISVR {

    [RequireComponent(typeof(FixedJoint))]
    public class Sticker : MonoBehaviour {

        [SerializeField] private Rigidbody targetRigidbody;

        private FixedJoint _fixedJoint;

        private void Awake() {
            _fixedJoint = GetComponent<FixedJoint>();
        }

        public void SetTarget(Rigidbody target) {
            targetRigidbody = target;
            _fixedJoint.connectedBody = target;
        }
    }
}