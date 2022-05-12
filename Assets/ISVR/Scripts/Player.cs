using UnityEngine;
using ISVR.Core;

namespace ISVR {

    public class Player : MonoBehaviour {

        private GrabbableExtended _grabbableLeft;
        private GrabbableExtended _grabbableRight;

        private void Update() {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)) {
                if (_grabbableRight) {
                    _grabbableRight.Activate();
                }
            }
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch)) {
                if (_grabbableLeft) {
                    _grabbableLeft.Activate();
                }
            }
        }

        public void AddGrabbable(GrabbableExtended grabbable, OVRInput.Controller controllerType) {
            switch (controllerType) {
                case OVRInput.Controller.RTouch:
                    if (_grabbableRight) return;
                    _grabbableRight = grabbable;
                    _grabbableRight.Grab();
                    break;
                case OVRInput.Controller.LTouch:
                    if (_grabbableLeft) return;
                    _grabbableLeft = grabbable;
                    _grabbableLeft.Grab();
                    break;
            }
        }

        public void RemoveGrabbable(OVRInput.Controller controllerType) {
            switch (controllerType) {
                case OVRInput.Controller.RTouch:
                    if (_grabbableRight == null) return;
                    _grabbableRight.Release();
                    _grabbableRight = null;
                    break;
                case OVRInput.Controller.LTouch:
                    if (_grabbableLeft == null) return;
                    _grabbableLeft.Release();
                    _grabbableLeft = null;
                    break;
            }
        }
    }
}