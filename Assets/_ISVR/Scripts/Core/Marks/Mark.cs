using UnityEngine;

namespace ISVR.Core.Marks {

    public class Mark : GhostMark {

        public void Select() {
            Color color = MarkTypeSO.Color;
            color.a = 0.5f;
            meshRenderer.material.color = color;
        }

        public void Deselect() {
            meshRenderer.material.color = MarkTypeSO.Color;
        }
    }
}