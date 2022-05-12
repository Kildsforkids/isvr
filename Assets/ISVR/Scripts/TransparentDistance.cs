using UnityEngine;

namespace ISVR {

    public class TransparentDistance : MonoBehaviour {

        [SerializeField] private Transform target;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;
        [SerializeField] private float minAlpha;
        [SerializeField] private float maxAlpha;

        private Color _defaultColor;

        private void Start() {
            _defaultColor = meshRenderer.material.color;
        }

        private void LateUpdate() {
            var distance = Vector3.Distance(target.position, transform.position);
            var result = (distance - maxDistance) / (maxDistance - minDistance);
            SetTransparency(result);
        }

        private void SetTransparency(float alpha) {
            meshRenderer.material.color = new Color(
                _defaultColor.r,
                _defaultColor.g,
                _defaultColor.b,
                Mathf.Clamp(alpha, minAlpha, maxAlpha)
            );
        }
    }
}