using UnityEngine;

namespace ISVR {

    public class LookFollower : MonoBehaviour {

        [SerializeField] private Transform target;

        private void LateUpdate() {
            var targetPoint = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(targetPoint, Vector3.up);
        }
    }
}