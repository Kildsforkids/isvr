using Oculus.Interaction;
using UnityEngine.Events;

namespace ISVR.Core {

    public class GrabbableExtended : Grabbable {

        public UnityEvent OnGrab;
        public UnityEvent OnRelease;
        public UnityEvent OnActivate;

        public void Grab() {
            OnGrab?.Invoke();
        }

        public void Release() {
            OnRelease?.Invoke();
        }

        public void Activate() {
            OnActivate?.Invoke();
        }
    }
}