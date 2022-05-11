using ISVR.Core;
using Oculus.Interaction;
using UnityEngine;

namespace ISVR {

    public class GrabInteractorExtended : GrabInteractor {

        [SerializeField] private Player player;
        [SerializeField] private OVRInput.Controller controller;

        protected override void InteractableSelected(GrabInteractable interactable) {
            base.InteractableSelected(interactable);
            var grabbableExtended = interactable.Grabbable as GrabbableExtended;
            player.AddGrabbable(grabbableExtended, controller);
        }

        protected override void InteractableUnselected(GrabInteractable interactable) {
            base.InteractableUnselected(interactable);
            player.RemoveGrabbable(controller);
        }
    }
}