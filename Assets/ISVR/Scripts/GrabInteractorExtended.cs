using ISVR.Core;
using Oculus.Interaction;
using UnityEngine;

namespace ISVR {

    public class GrabInteractorExtended : GrabInteractor {

        [SerializeField] private Player player;
        [SerializeField] private OVRInput.Controller controller;

        private bool isGrabbed;

        protected override void InteractableSelected(GrabInteractable interactable) {
            if (isGrabbed) {
                base.InteractableUnselected(interactable);
                player.RemoveGrabbable(controller);
                isGrabbed = false;
            } else {
                base.InteractableSelected(interactable);
                if (interactable.TryGetComponent(out GrabbableExtended grabbableExtended)) {
                    player.AddGrabbable(grabbableExtended, controller);
                }
                isGrabbed = true;
            }
        }

        protected override void InteractableUnselected(GrabInteractable interactable) {
            // base.InteractableUnselected(interactable);
            // player.RemoveGrabbable(controller);
        }
    }
}