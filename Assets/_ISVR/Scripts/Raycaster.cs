using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ISVR.Core {

    public class Raycaster : MonoBehaviour {

        [SerializeField] private Transform origin;
        [SerializeField] private float distance;
        [SerializeField] private float radius;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float castRate = 1f;

        public UnityEvent<RaycastHit[]> OnRayReturn;

        public Transform Origin => origin;
        public float Distance {
            get { return distance; }
            set { distance = value; }
        }

        private Coroutine _castRayCoroutine;

        private void Awake() {
            if (origin == null) {
                origin = transform;
            }
        }
        
        public void CastRayOnce() {
            RaycastHit[] hits = Physics.SphereCastAll(
                origin.position,
                radius,
                origin.forward,
                distance,
                layerMask
            );

            OnRayReturn?.Invoke(hits);
        }

        public void CastRayInfinite() {
            _castRayCoroutine = StartCoroutine(CastRayCoroutine());
        }

        public void StopCastRay() {
            if (_castRayCoroutine == null) return;
            StopCoroutine(_castRayCoroutine);
        }

        private IEnumerator CastRayCoroutine() {
            var waitForSeconds = new WaitForSeconds(castRate);
            while (true) {
                CastRayOnce();
                yield return waitForSeconds;
            }
        }
    }
}
