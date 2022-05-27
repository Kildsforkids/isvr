using UnityEngine.Events;

namespace ISVR.Core.Player.Input {

    [System.Serializable]
    public class ControllerAction {

        public OVRInput.Controller Controller;
        public OVRInput.Button Button;
        public UnityEvent OnActivate;
    }
}