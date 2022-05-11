using UnityEngine;

namespace ISVR {

    public class Indicator : MonoBehaviour {

        [SerializeField] private Color inactiveColor;
        [SerializeField] private Color activeColor;

        private MeshRenderer _meshRenderer;

        private void Awake() {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start() {
            _meshRenderer.material.color = inactiveColor;
        }

        public void Activate() {
            _meshRenderer.material.color = activeColor;
        }

        public void Deactivate() {
            _meshRenderer.material.color = inactiveColor;
        }
    }
}