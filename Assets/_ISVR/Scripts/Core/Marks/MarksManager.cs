using System.Collections.Generic;
using UnityEngine;

namespace ISVR.Core.Marks {

    public class MarksManager : MonoBehaviour {

        [Header("Marks")]
        [SerializeField] private List<MarkTypeSO> markTypesSO;
        [SerializeField] private ObjectPooler objectPooler;
        [SerializeField] private int marksMaxCount;
        [SerializeField] private List<Mark> marks;

        [Header("Markers")]
        [SerializeField] private Marker leftMarker;
        [SerializeField] private Marker rightMarker;

        [Header("Markers Settings")]
        [SerializeField] private float rayOffset;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float maxDistance;
        [SerializeField] private float deactivateTime;

        public float RayOffset => rayOffset;
        public LayerMask LayerMask => layerMask;
        public float MaxDistance => maxDistance;
        public float DeactivateTime => deactivateTime;

        private int _lastId => markTypesSO.Count - 1;
        private int _id;

        private void Start() {
            Select(0);
            objectPooler.Init(marksMaxCount);
            marks = new List<Mark>(objectPooler.GetComponentsInChildren<Mark>(true));
            leftMarker.SetMarksManager(this);
            rightMarker.SetMarksManager(this);
        }

        public void ActivateLeftMarker() {
            leftMarker.Show();
        }

        public void ActivateRightMarker() {
            rightMarker.Show();
        }

        public void ToggleLeftMarker() {
            leftMarker.Toggle();
        }

        public void ToggleRightMarker() {
            rightMarker.Toggle();
        }

        public void UseLeftMarker() {
            leftMarker.Use(this);
        }

        public void UseRightMarker() {
            rightMarker.Use(this);
        }

        public void SelectNext() {
            Select(++_id);
        }

        public void SelectPrevious() {
            Select(--_id);
        }

        public Mark GetMarkFromPool() {
            return objectPooler.GetFromPool<Mark>();
        }

        public void ReturnMarkToPool(Mark mark) {
            objectPooler.AddToPool(mark.gameObject);
        }

        private void Select(int id) {
            if (id < 0) {
                id = _lastId;
            } else if (_id > _lastId) {
                id = 0;
            }
            MarkTypeSO markTypeSO = markTypesSO[id];
            leftMarker.SetGhostMark(markTypeSO);
            rightMarker.SetGhostMark(markTypeSO);
            _id = id;
        }
    }
}