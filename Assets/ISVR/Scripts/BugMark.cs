using UnityEngine;

namespace ISVR {

    public class BugMark : MonoBehaviour {

        [SerializeField] private int index;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Color highlightColor;
        [SerializeField] private Color bugColor;

        public int Index {
            get {
                return index;
            }
            set {
                index = value;
            }
        }
        public bool IsSelected { get; private set; }

        private Color _defaultColor;

        private void Start() {
            _defaultColor = meshRenderer.material.color;
            Index = GameSetup.Instance.TryAddBugMark(this);
            if (Index < 0) {
                Destroy(gameObject);
            }
        }

        public void Remove() {
            Unselect();
            GameSetup.Instance.RemoveBugMark(Index);
            Destroy(gameObject);
        }

        public void Select() {
            if (IsSelected) return;
            IsSelected = true;
            Highlight();
        }

        public void Unselect() {
            if (!IsSelected) return;
            IsSelected = false;
            ReturnDefaultColor();
        }

        public void HighlightAsBug() {
            meshRenderer.material.color = bugColor;
        }

        private void Highlight() {
            meshRenderer.material.color = highlightColor;
        }

        private void ReturnDefaultColor() {
            meshRenderer.material.color = _defaultColor;
        }
    }
}