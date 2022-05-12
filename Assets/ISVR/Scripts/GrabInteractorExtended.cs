using ISVR.Core;
using Oculus.Interaction;
using UnityEngine;

namespace ISVR {

    public class GrabInteractorExtended : GrabInteractor {

        [SerializeField] private Player player;
        [SerializeField] private OVRInput.Controller controller;

        protected override void InteractableSelected(GrabInteractable interactable) {
            base.InteractableSelected(interactable);
            if (interactable.TryGetComponent(out GrabbableExtended grabbableExtended)) {
                player.AddGrabbable(grabbableExtended, controller);
            }
        }

        protected override void InteractableUnselected(GrabInteractable interactable) {
            base.InteractableUnselected(interactable);
            player.RemoveGrabbable(controller);
        }
    }
}