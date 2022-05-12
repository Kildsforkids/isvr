using UnityEngine;

namespace ISVR {

    public class LookFollower : MonoBehaviour {

        [SerializeField] private Transform target;
        [SerializeField] private bool isLocal;

        private void Start() {
            if (target == null) {
                SetLookTargetAsPlayer();
            }
        }

        private void LateUpdate() {
            if (target == null) return;
            var targetPoint = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(targetPoint, isLocal ? transform.up : Vector3.up);
        }

        private void SetLookTargetAsPlayer() {
            target = GameSetup.Instance.Player.Head;
        }
    }
}