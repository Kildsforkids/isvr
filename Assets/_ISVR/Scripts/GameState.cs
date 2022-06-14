using System.Collections.Generic;
using ISVR.Core.Devices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ISVR {

    public class GameState : MonoBehaviour {

        public static GameState Instance;

        [SerializeField] private List<Wiretapper> wiretappers;

        public bool IsTaskEnded => _isTaskEnded;
        
        private bool _isTaskEnded;

        private void Awake() {
            Instance = this;
        }

        public void EndTask() {
            if (_isTaskEnded) return;
            _isTaskEnded = true;
        }

        public void ResetLevel() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void AddWiretapper(Wiretapper wiretapper) {
            wiretappers.Add(wiretapper);
        }
    }
}
