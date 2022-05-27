using System.Collections.Generic;
using UnityEngine;

namespace ISVR.Core.Player.Input {
    public class PlayerInput : MonoBehaviour {
        
        [SerializeField] private List<ControllerAction> controllerActions;
        [SerializeField] private List<ControllerAction> inGameControllerActions;

        private void Update() {
            if (!GameState.Instance.IsTaskEnded) {
                CheckControllerActions(inGameControllerActions);
            }
            CheckControllerActions(controllerActions);
        }

        private void CheckControllerActions(List<ControllerAction> controllerActions) {
            foreach (var action in controllerActions) {
                if (OVRInput.GetDown(action.Button, action.Controller)) {
                    action.OnActivate?.Invoke();
                }
            }
        }
    }
}
