using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISVR.Core.Player.Input {
    public class PlayerInput : MonoBehaviour {
        
        [SerializeField] private List<ControllerAction> controllerActions;
        [SerializeField] private List<ControllerAction> inGameControllerActions;
        [SerializeField] private ControllerAction holdControllerAction;

        private Coroutine _coroutine;
        private bool _isHelded;

        private void Update() {
            if (!GameState.Instance.IsTaskEnded) {
                CheckControllerActions(inGameControllerActions);
                //CheckHoldAction(holdControllerAction);
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

        private void CheckHoldAction(ControllerAction controllerAction) {
            if (OVRInput.Get(controllerAction.Button, controllerAction.Controller)) {
                if (_isHelded) {
                    controllerAction.OnActivate?.Invoke();
                    _isHelded = false;
                } else {
                    if (_coroutine == null) {
                        _coroutine = StartCoroutine(HoldCoroutine());
                    }
                }
            } else {
                if (_coroutine != null) {
                    StopCoroutine(_coroutine);
                }
            }
        }

        private IEnumerator HoldCoroutine() {
            float time = 2f;
            while (time > 0f) {
                time -= Time.deltaTime;
                yield return null;
            }
            _isHelded = true;
        }
    }
}
