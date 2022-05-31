using ISVR.Utils;
using UnityEngine;

namespace ISVR.UI {
    public class MainMenu : MonoBehaviour {

        [SerializeField] private TransformFollower transformFollower;

        public void Show() {
            transformFollower.SetInFront();
        }
    }
}
