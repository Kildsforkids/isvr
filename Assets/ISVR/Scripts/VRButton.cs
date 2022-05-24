using UnityEngine;
using UnityEngine.Events;

namespace ISVR {

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class VRButton : MonoBehaviour {

        [SerializeField] private string name;

        public UnityEvent OnTouch;
        public UnityEvent OnStay;

        public string Name => name;
        
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