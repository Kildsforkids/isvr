using UnityEngine;

namespace ISVR.Core.Marks {

    public class Marker : MonoBehaviour {

        [SerializeField] private Transform origin;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private GhostMark ghost;
        [SerializeField] private float maxDistance;
        [SerializeField] private LayerMask layerMask;

        private bool _isActive;
        private bool _isHidden;
        private enum MarkerState {
            Add,
            Remove
        }
        private MarkerState _markerState;
        private RaycastHit _hit;
        private Mark _selectedMark;

        private void Start() {
            Hide();
        }

        private void Update() {
            if (!_isActive) return;
            if (CastRay(origin.position, origin.forward, out _hit, maxDistance, layerMask)) {

                SetLine(origin.position, _hit.point);
                ghost.SetPosition(_hit.point);

                if (_hit.transform.TryGetComponent(out _selectedMark)) {
                    ChangeState(MarkerState.Remove);
                    ghost.Hide();
                } else {
                    ChangeState(MarkerState.Add);
                    ShowGhostLine();
                    ghost.Show();
                }
            } else {
                HideGhostLine();
            }
        }

        public void Use(MarksManager marksManager) {
            if (!_isActive) return;
            switch (_markerState) {
                case MarkerState.Add:
                    CreateMark(marksManager);
                    break;
                case MarkerState.Remove:
                    RemoveMark(marksManager);
                    break;
            }
        }

        public void SetGhostMark(MarkTypeSO markTypeSO) {
            if (!_isActive) return;
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
            ShowGhostLine();
            _isActive = true;
        }

        private void Hide() {
            HideGhostLine();
            _isActive = false;
        }

        private void ShowGhostLine() {
            if (!_isHidden) return;
            lineRenderer.enabled = true;
            ghost.Show();
            _isHidden = false;
        }

        private void HideGhostLine() {
            if (_isHidden) return;
            lineRenderer.enabled = false;
            ghost.Hide();
            _isHidden = true;
        }

        private bool CastRay(Vector3 origin, Vector3 direction, out RaycastHit hit, float maxDisctance, LayerMask layerMask) {
            if (Physics.Raycast(origin, direction, out hit, maxDisctance, layerMask)) {
                return true;
            }
            return false;
        }

        private void SetLine(Vector3 startPosition, Vector3 endPosition) {
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, endPosition);
        }

        private void CreateMark(MarksManager marksManager) {
            if (!_hit.transform) return;
            var mark = marksManager.GetMarkFromPool();
            if (mark != null) {
                mark.ChangeType(ghost.MarkTypeSO);
                mark.SetPosition(_hit.point);
            }
        }

        private void RemoveMark(MarksManager marksManager) {
            if (!_selectedMark) return;
            marksManager.ReturnMarkToPool(_selectedMark);
        }

        private void ChangeState(MarkerState state) {
            if (state != _markerState) {
                _markerState = state;
            }
        }
    }
}