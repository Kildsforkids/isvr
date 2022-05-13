using UnityEngine;
using ISVR.Core;

namespace ISVR {

    public class Player : MonoBehaviour {

        [SerializeField] private Transform head;
        [SerializeField] private BugMarker leftBugMarker;
        [SerializeField] private BugMarker rightBugMarker;
        [SerializeField] private HandMenu handMenu;

        public Transform Head => head;

        private GrabbableExtended _grabbableLeft;
        private GrabbableExtended _grabbableRight;

        private void Update() {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)) {
                if (_grabbableRight) {
                    _grabbableRight.Activate();
                } else if (rightBugMarker.IsActive) {
                    rightBugMarker.Use();
                }
            }
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch)) {
                if (handMenu.IsActive) {
                    handMenu.ActivateSelectedOption();
                } else if (_grabbableLeft) {
                    _grabbableLeft.Activate();
                } else if (leftBugMarker.IsActive) {
                    leftBugMarker.Use();
                }
            }
            if (OVRInput.GetDown(OVRInput.Button.One)) {
                if (_grabbableRight == null && rightBugMarker) {
                    rightBugMarker.Toggle();
                }
            }
            if (OVRInput.GetDown(OVRInput.Button.Three)) {
                if (handMenu.IsActive) {
                    handMenu.SelectPreviousOption();
                } else {
                    if (_grabbableLeft == null && leftBugMarker) {
                        leftBugMarker.Toggle();
                    }
                }
                
            }
            if (OVRInput.GetDown(OVRInput.Button.Four)) {
                if (handMenu.IsActive) {
                    handMenu.SelectNextOption();
                }
            }
            if (OVRInput.GetDown(OVRInput.Button.Start)) {
                handMenu.Toggle();
                if (leftBugMarker.IsActive) {
                    leftBugMarker.Deactivate();
                }
            }
        }

        public void AddGrabbable(GrabbableExtended grabbable, OVRInput.Controller controllerType) {
            switch (controllerType) {
                case OVRInput.Controller.RTouch:
                    if (_grabbableRight) return;
                    _grabbableRight = grabbable;
                    _grabbableRight.Grab();
                    rightBugMarker.Deactivate();
                    break;
                case OVRInput.Controller.LTouch:
                    if (_grabbableLeft) return;
                    _grabbableLeft = grabbable;
                    _grabbableLeft.Grab();
                    leftBugMarker.Deactivate();
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