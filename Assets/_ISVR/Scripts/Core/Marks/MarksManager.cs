using System.Collections.Generic;
using UnityEngine;

namespace ISVR.Core.Marks {

    public class MarksManager : MonoBehaviour {

        [SerializeField] private Marker leftMarker;
        [SerializeField] private Marker rightMarker;
        [SerializeField] private List<MarkTypeSO> markTypesSO;

        private int _lastId => markTypesSO.Count - 1;
        private int _id;

        private void Start() {
            Select(0);
        }

        public void ToggleLeftMarker() {
            leftMarker.Toggle();
        }

        public void ToggleRightMarker() {
            rightMarker.Toggle();
        }

        public void SelectNext() {
            Select(_id++);
        }

        public void SelectPrevious() {
            Select(_id--);
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