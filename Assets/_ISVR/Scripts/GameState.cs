using System.Collections.Generic;
using ISVR.Core.Devices;
using ISVR.Core.Managers;
using ISVR.Core.Marks;
using ISVR.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ISVR {

    public class GameState : MonoBehaviour {

        public static GameState Instance;

        [SerializeField] private TaskManager taskManager;
        [SerializeField] private MarksManager marksManager;
        [SerializeField] private ResultWindow resultWindow;
        [SerializeField] private List<Wiretapper> wiretappers;
        [SerializeField] private List<Electronic> electronics;

        public bool IsTaskEnded => _isTaskEnded;
        
        private bool _isTaskEnded;

        private void Awake() {
            Instance = this;
        }

        public void EndTask() {
            if (_isTaskEnded) return;

            var wiretapperMarks = marksManager.GetMarksOfType(MarkType.Wiretapper);
            var electronicMarks = marksManager.GetMarksOfType(MarkType.Electronic);
            //var questionMarks = marksManager.GetMarksOfType(MarkType.Question);

            int foundWiretappersCount = taskManager.CalculatePredictResult(wiretappers, wiretapperMarks);
            int foundElectronicsCount = taskManager.CalculatePredictResult(electronics, electronicMarks);

            resultWindow.UpdateWiretappersResultValue(foundWiretappersCount, wiretappers.Count);
            resultWindow.UpdateElectronicsResultValue(foundElectronicsCount, electronics.Count);

            resultWindow.Show();

            _isTaskEnded = true;
        }

        public void ResetLevel() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void AddWiretapper(Wiretapper wiretapper) {
            wiretappers.Add(wiretapper);
        }

        public void AddElectronic(Electronic electronic) {
            electronics.Add(electronic);
        }
    }
}
