using ISVR.Core;
using Oculus.Interaction;
using UnityEngine;

namespace ISVR {

    public class GrabInteractorExtended : GrabInteractor {

        [SerializeField] private Player player;
        [SerializeField] private OVRInput.Controller controller;

        private bool isGrabbed;

        protected override void Awake() {
            base.Awake();
            WhenInteractableSelected.Action += HideController;
        }

        private void HideController(GrabInteractable grabInteractable) {
        }
    }
}