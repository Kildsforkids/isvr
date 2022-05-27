using UnityEngine;

namespace ISVR.Utils {

    [RequireComponent(typeof(LineRenderer))]
    public class ColliderOutline : MonoBehaviour {

        private Collider _collider;
        private LineRenderer _lineRenderer;

        private void Awake() {
            _collider = GetComponent<Collider>();
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start() {
            Draw();
        }

        private void Draw() {
            var vertices = GetColliderVertexPositions(_collider);
            _lineRenderer.positionCount = vertices.Length;
            _lineRenderer.SetPositions(vertices);
        }

        private Vector3[] GetColliderVertexPositions(Collider collider) {
            var vertices = new Vector3[8];
            var matrix = transform.localToWorldMatrix;
            var storedRotation = transform.rotation;
            transform.rotation = Quaternion.identity;

            var extents = collider.bounds.extents;
            vertices[0] = matrix.MultiplyPoint3x4(extents);
            vertices[1] = matrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, extents.z));
            vertices[2] = matrix.MultiplyPoint3x4(new Vector3(extents.x, extents.y, -extents.z));
            vertices[3] = matrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, -extents.z));
            vertices[4] = matrix.MultiplyPoint3x4(new Vector3(extents.x, -extents.y, extents.z));
            vertices[5] = matrix.MultiplyPoint3x4(new Vector3(-extents.x, -extents.y, extents.z));
            vertices[6] = matrix.MultiplyPoint3x4(new Vector3(extents.x, -extents.y, -extents.z));
            vertices[7] = matrix.MultiplyPoint3x4(-extents);

            transform.rotation = storedRotation;
            return vertices;
        }
    }
}