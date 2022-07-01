using Oculus.Interaction;
using UnityEngine;

namespace ISVR {
    public class ControllerHelper : MonoBehaviour {

        [SerializeField] private GrabInteractor grabInteractor;
        [SerializeField] private GameObject controller;

        private void Awake() {
            grabInteractor.WhenInteractableSelected.Action += HideController;
            grabInteractor.WhenInteractableUnselected.Action += ShowController;
        }

        private void ShowController(GrabInteractable grabInteractable) {
            controller.SetActive(true);
        }

        private void HideController(GrabInteractable grabInteractable) {
            controller.SetActive(false);
        }

        private void OnDestroy() {
            grabInteractor.WhenInteractableSelected.Action -= HideController;
            grabInteractor.WhenInteractableUnselected.Action -= ShowController;
        }
    }
}
