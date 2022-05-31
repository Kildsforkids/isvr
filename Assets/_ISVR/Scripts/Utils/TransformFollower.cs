using UnityEngine;

namespace ISVR.Utils {
    public class TransformFollower : MonoBehaviour {
        
        [SerializeField] private Transform target;
        [SerializeField] private Transform verticalTarget;
        [SerializeField] private Vector3 offset;

        private void Start() {
            SetInFront();
        }

        private void LateUpdate() {
            if (verticalTarget) {
                transform.position = new Vector3(
                    transform.position.x,
                    verticalTarget.position.y,
                    transform.position.z
                );
            }
        }

        public void SetInFront() {
            var offset = target.forward * this.offset.z +
                target.up * this.offset.y;
            transform.position = target.position + offset;
            transform.forward = target.forward;
        }
    }
}
