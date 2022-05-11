using System.Collections.Generic;
using UnityEngine;

namespace ISVR {

    public class GameSetup : MonoBehaviour {

        public static GameSetup Instance { get; private set; }

        [SerializeField] private List<Electrical> bugs;

        private void Awake() {
            Instance = this;
            Application.targetFrameRate = 72;
        }

        public void AddBug(Electrical electrical) {
            bugs.Add(electrical);
        }
    }
}