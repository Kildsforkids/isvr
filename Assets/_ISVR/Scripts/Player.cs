using System.Collections.Generic;
using UnityEngine;
using ISVR.Core;
using ISVR.Core.Bugs;
using ISVR.Core.Player.Input;

namespace ISVR {

    public class Player : MonoBehaviour {

        [SerializeField] private Transform head;
        [SerializeField] private BugMarker leftBugMarker;
        [SerializeField] private BugMarker rightBugMarker;
        [SerializeField] private HandMenu handMenu;
        [SerializeField] private OVRPlayerController playerController;
        [SerializeField] private List<ControllerAction> controllerActions;
        [SerializeField] private List<ControllerAction> inGameControllerActions;

        public Transform Head => head;

        private GrabbableExtended _grabbableLeft;
        private GrabbableExtended _grabbableRight;
        // private bool _isLevelEnded;

        private void Update() {
            if (!GameState.Instance.IsTaskEnded) {

                CheckInGameControllerActions();

                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)) {
                    if (_grabbableRight) {
                        _grabbableRight.Activate();
                    } else if (rightBugMarker.IsActive) {
                        rightBugMarker.Use();
                    }
                }
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch)) {
                    if (!handMenu.IsActive) {
                        if (_grabbableLeft) {
                            _grabbableLeft.Activate();
                        } else if (leftBugMarker.IsActive) {
                            leftBugMarker.Use();
                        }
                    }
                }
                // if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) {
                //     if (_grabbableRight == null && rightBugMarker) {
                //         rightBugMarker.Toggle();
                //     }
                // }
                // if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch)) {
                //     if (!handMenu.IsActive) {
                //         if (_grabbableLeft == null && leftBugMarker) {
                //             leftBugMarker.Toggle();
                //         }
                //     }
                // }
            }

            CheckControllerActions();

            // HandleHandMenu();
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

        public void EndLevel() {
            // _isLevelEnded = true;
        }

        public void EnableLocomotion() {
            playerController.EnableLinearMovement = true;
        }

        public void DisableLocomotion() {
            playerController.EnableLinearMovement = false;
        }

        private void CheckControllerActions() {
            foreach (var action in controllerActions) {
                if (OVRInput.GetDown(action.Button, action.Controller)) {
                    action.OnActivate?.Invoke();
                }
            }
        }

        private void CheckInGameControllerActions() {
            foreach (var action in inGameControllerActions) {
                if (OVRInput.GetDown(action.Button, action.Controller)) {
                    action.OnActivate?.Invoke();
                }
            }
        }

        private void HandleHandMenu() {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch)) {
                if (handMenu.IsActive) {
                    handMenu.ActivateSelectedOption();
                }
            }
            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch)) {
                if (handMenu.IsActive) {
                    handMenu.SelectNextOption();
                }
            }
            if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch)) {
                if (handMenu.IsActive) {
                    handMenu.SelectPreviousOption();
                }
            }
            if (OVRInput.GetDown(OVRInput.Button.Start, OVRInput.Controller.LTouch)) {
                handMenu.Toggle();
                if (leftBugMarker.IsActive) {
                    leftBugMarker.Deactivate();
                }
            }
        }
    }
}