using UnityEngine;

namespace ISVR.Core.Bugs {

    public class BugMarker : MonoBehaviour {

        [SerializeField] private Transform origin;
        [SerializeField] private GhostMark ghostBugMark;
        [SerializeField] private Transform bugMark;
        [SerializeField] private float distance;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private LineRenderer lineRenderer;

        public bool IsActive { get; private set; }

        private RaycastHit _hit;
        private MarkerState _markerState;
        private BugMark _currentMark;

        private void LateUpdate() {
            if (!IsActive) return;
            if (Physics.Raycast(
                origin.position,
                origin.forward,
                out _hit,
                distance,
                layerMask
            )) {
                //if (GameSetup.Instance.FindInStopList(_hit.transform)) return;
                if (!lineRenderer.enabled) {
                    lineRenderer.enabled = true;
                }
                lineRenderer.SetPosition(0, origin.position);
                lineRenderer.SetPosition(1, _hit.point);
                if (_hit.transform.TryGetComponent(out BugMark currentMark)) {
                    _currentMark = currentMark;
                    _currentMark.Select();
                    ghostBugMark.Hide();
                    _markerState = MarkerState.Remove;
                } else {
                    ClearCurrentMark();
                    _markerState = MarkerState.Add;
                    ghostBugMark.SetPosition(_hit.point, _hit.normal);
                    ghostBugMark.Show();
                }
            } else {
                ClearCurrentMark();
                ghostBugMark.Hide();
                lineRenderer.enabled = false;
            }
        }

        public void Activate() {
            if (IsActive) return;
            IsActive = true;
            lineRenderer.enabled = true;
        }

        public void Deactivate() {
            if (!IsActive) return;
            IsActive = false;
            ghostBugMark.Hide();
            lineRenderer.enabled = false;
            ClearCurrentMark();
        }

        public void Toggle() {
            if (IsActive) {
                Deactivate();
            } else {
                Activate();
            }
        }

        public void Use() {
            if (!IsActive) return;
            switch (_markerState) {
                case MarkerState.Add:
                    SetBugMark();
                    break;
                case MarkerState.Remove:
                    RemoveBugMark();
                    break;
            }
        }

        private void SetBugMark() {
            if (_hit.transform == null) return;
            if (GameSetup.Instance.GetFreeBugMarkSlotIndex() < 0) return;
            var bugMarkTransform = Instantiate(bugMark, _hit.point, Quaternion.identity);
            bugMarkTransform.GetComponent<BugMark>().SetParentOrJoint(_hit.transform);
        }

        private void RemoveBugMark() {
            if (_currentMark == null) return;
            _currentMark.Remove();
        }

        private void ClearCurrentMark() {
            if (_currentMark == null) return;
            _currentMark.Unselect();
            _currentMark = null;
        }
    }

    public enum MarkerState {
        Add,
        Remove
    }
}