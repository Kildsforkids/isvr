using UnityEngine;
using Oculus.Interaction;

namespace ISVR {

    public class RayInteractorHelper : MonoBehaviour {

        [SerializeField] private RayInteractor rayInteractor;
        [SerializeField] private RayInteractorCursorVisual rayInteractorCursorVisual;
        [SerializeField] private ControllerRayVisual controllerRayVisual;
        [SerializeField] private MeshRenderer bugMarkMesh;

        public void EnableRayInteractor() {
            rayInteractor.enabled = true;
            rayInteractorCursorVisual.enabled = true;
            controllerRayVisual.enabled = true;
            bugMarkMesh.enabled = true;
        }

        public void DisableRayInteractor() {
            rayInteractor.enabled = false;
            rayInteractorCursorVisual.enabled = false;
            controllerRayVisual.enabled = false;
            bugMarkMesh.enabled = false;
        }

        public void ToggleRayInteractor() {
            if (rayInteractor.enabled) {
                DisableRayInteractor();
            } else {
                EnableRayInteractor();
            }
        }
    }
}