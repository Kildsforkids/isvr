using UnityEngine;

namespace ISVR.Core.Marks {

    public class Marker : MonoBehaviour {

        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Mark ghost;

        private bool _isActive;

        private void Start() {
            Hide();
        }

        private void Update() {
            if (!_isActive) return;
        }

        public void SetGhostMark(MarkTypeSO markTypeSO) {
            ghost.ChangeType(markTypeSO);
        }

        public void Toggle() {
            if (_isActive) {
                Hide();
            } else {
                Show();
            }
        }

        private void Show() {
            lineRenderer.enabled = true;
            _isActive = true;
        }

        private void Hide() {
            lineRenderer.enabled = false;
            _isActive = false;
        }
    }
}