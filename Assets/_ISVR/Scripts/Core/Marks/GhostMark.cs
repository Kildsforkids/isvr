using UnityEngine;

namespace ISVR.Core.Marks {

    public class GhostMark : MonoBehaviour {

        [SerializeField] protected MeshRenderer meshRenderer;
        [SerializeField] private MarkTypeSO markTypeSO;

        public MarkTypeSO MarkTypeSO => markTypeSO;

        private bool _isHidden;

        private void Start() {
            UpdateView();
        }

        public void SetPosition(Vector3 position) {
            transform.position = position;
        }

        public void Show() {
            if (!_isHidden) return;
            meshRenderer.enabled = true;
            _isHidden = false;
        }

        public void Hide() {
            if (_isHidden) return;
            meshRenderer.enabled = false;
            _isHidden = true;
        }

        public void ChangeType(MarkTypeSO markTypeSO) {
            this.markTypeSO = markTypeSO;
            UpdateView();
        }

        private void UpdateView() {
            meshRenderer.material.mainTexture = markTypeSO.Sprite.texture;
            meshRenderer.material.color = markTypeSO.Color;
        }
    }
}