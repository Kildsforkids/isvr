using UnityEngine;

namespace ISVR.Core.Marks {

    public class Mark : MonoBehaviour {

        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private MarkTypeSO markTypeSO;

        private void Start() {
            UpdateView();
        }

        public void ChangeType(MarkTypeSO markTypeSO) {
            Debug.Log($"Changed on {markTypeSO.Type}");
            this.markTypeSO = markTypeSO;
            UpdateView();
        }

        private void UpdateView() {
            meshRenderer.material.mainTexture = markTypeSO.Sprite.texture;
            meshRenderer.material.color = markTypeSO.Color;
        }
    }
}