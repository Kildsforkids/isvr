using UnityEngine;
using ISVR.Core;

namespace ISVR {

    public class Player : MonoBehaviour {

        private GrabbableExtended _grabbable;

        private void Update() {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) {
                if (_grabbable) {
                    _grabbable.Activate();
                }
            }
        }

        public void AddGrabbableExtended(GrabbableExtended grabbable) {
            if (_grabbable) return;
            _grabbable = grabbable;
            Debug.Log("Added grabbable");
        }

        public void RemoveGrabbableExtended() {
            if (_grabbable == null) return;
            _grabbable = null;
            Debug.Log("Removed grabbable");
        }
    }
}