using UnityEngine;

namespace ISVR {

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Fingertip : MonoBehaviour {

        [SerializeField] private MeshRenderer meshRenderer;

        public void Show() {
            if (!meshRenderer) return;
            meshRenderer.enabled = true;
        }

        public void Hide() {
            if (!meshRenderer) return;
            meshRenderer.enabled = false;
        }
    }
}
