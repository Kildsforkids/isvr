using UnityEngine;

namespace ISVR {

    public class Player : MonoBehaviour {

        private void Update() {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) {
                Debug.Log("Hello");
            }
        }
    }
}