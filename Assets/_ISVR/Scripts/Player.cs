using UnityEngine;
using ISVR.Core;
using System.Collections.Generic;
using Oculus.Interaction;

namespace ISVR {

    public class Player : MonoBehaviour {

        [SerializeField] private Transform head;
        [SerializeField] private OVRPlayerController playerController;
        [SerializeField] private List<RayInteractor> rayInteractors;

        public Transform Head => head;

        private GrabbableExtended _grabbableLeft;
        private GrabbableExtended _grabbableRight;

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

        public void EnableLocomotion() {
            playerController.EnableLinearMovement = true;
        }

        public void DisableLocomotion() {
            playerController.EnableLinearMovement = false;
        }

        public void EnableRayInteractors() {
            foreach (var rayInteractor in rayInteractors) {
                rayInteractor.gameObject.SetActive(true);
            }
        }

        public void DisableRayInteractors() {
            foreach (var rayInteractor in rayInteractors) {
                rayInteractor.gameObject.SetActive(false);
            }
        }
    }
}