using UnityEngine;

namespace ISVR.Core.Marks {

    public class Mark : GhostMark {

        private Color _defaultColor;

        private void Awake() {
            _defaultColor = MarkTypeSO.Color;
        }

        public void Select() {
            Debug.Log("Select");
            Color color = _defaultColor;
            color.a = 0.5f;
            meshRenderer.material.color = color;
        }

        public void Deselect() {
            Debug.Log("Deselect");
            meshRenderer.material.color = _defaultColor;
        }
    }
}