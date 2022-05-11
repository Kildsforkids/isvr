using UnityEngine;

namespace ISVR {

    public class GameSetup : MonoBehaviour {
        private void Awake() {
            Application.targetFrameRate = 72;
        }
    }
}