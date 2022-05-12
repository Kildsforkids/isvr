using UnityEngine;

namespace ISVR {

    public class GhostMark : MonoBehaviour {

        [SerializeField] private MeshRenderer meshRenderer;

        public bool IsHidden { get; private set; }

        public void SetPosition(Vector3 position) {
            transform.position = position;
        }

        public void SetPosition(Vector3 position, Vector3 up) {
            transform.position = position;
            transform.up = up;
        }

        public void Show() {
            if (!IsHidden) return;
            meshRenderer.enabled = true;
            IsHidden = false;
        }

        public void Hide() {
            if (IsHidden) return;
            meshRenderer.enabled = false;
            IsHidden = true;
        }
    }
}