using UnityEngine;
using UnityEngine.Events;

namespace ISVR.UI {

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class TouchButton : MonoBehaviour {

        public UnityEvent OnTouch;
        public UnityEvent OnStay;

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out Fingertip fingertip)) {
                OnTouch?.Invoke();
            }
        }

        private void OnTriggerStay(Collider other) {
            if (other.TryGetComponent(out Fingertip fingertip)) {
                OnStay?.Invoke();
            }
        }
    }
}
