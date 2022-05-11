using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Events;

namespace ISVR.Core {

    public class GrabbableExtended : Grabbable {

        public UnityEvent OnGrab;
        public UnityEvent OnRelease;
        public UnityEvent OnActivate;

        public override void AddGrabPoint(int id, Pose pose) {
            base.AddGrabPoint(id, pose);
            OnGrab?.Invoke();
        }

        public override void RemoveGrabPoint(int id, Pose pose) {
            base.RemoveGrabPoint(id, pose);
            OnRelease?.Invoke();
        }

        public void Activate() {
            OnActivate?.Invoke();
        }
    }
}