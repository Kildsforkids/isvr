using UnityEngine;
using ISVR.Core;

namespace ISVR {

    [RequireComponent(typeof(Raycaster), typeof(CommandPanel))]
    public class NonlinearLocator : MonoBehaviour {

        [SerializeField] private string defaultDebugText;

        private Raycaster _raycaster;
        private CommandPanel _commandPanel;

        private void Awake() {
            _raycaster = GetComponent<Raycaster>();
            _commandPanel = GetComponent<CommandPanel>();
        }

        private void Start() {
            _raycaster.OnRayReturn.AddListener(UpdateDebugText);
        }

        public void TurnOn() {
            StartRaycaster();
        }

        public void TurnOff() {
            StopRaycaster();
            UpdateDebugText(defaultDebugText);
        }

        private void StartRaycaster() {
            _raycaster.CastRayInfinite();
        }

        private void StopRaycaster() {
            _raycaster.StopCastRay();
        }

        private void UpdateDebugText(string text) {
            if (text == null) {
                _commandPanel.UpdateDebugText(defaultDebugText);
            } else {
                _commandPanel.UpdateDebugText(text);
            }
        }
    }
}