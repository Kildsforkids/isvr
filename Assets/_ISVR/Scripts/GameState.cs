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
        //[SerializeField] private ResultWindow resultWindow;
        [SerializeField] private GameObject additionalPanel;
        [SerializeField] private ResultTable resultTable;
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

            //resultWindow.UpdateWiretappersResultValue(foundWiretappersCount, wiretappers.Count);
            //resultWindow.UpdateElectronicsResultValue(foundElectronicsCount, electronics.Count);
            resultTable.UpdateValue(0, 0, wiretapperMarks.Count);
            resultTable.UpdateValue(1, 0, electronicMarks.Count);

            resultTable.UpdateValue(0, 1, foundWiretappersCount);
            resultTable.UpdateValue(1, 1, foundElectronicsCount);

            resultTable.UpdateValue(0, 2, wiretappers.Count);
            resultTable.UpdateValue(1, 2, electronics.Count);

            additionalPanel.SetActive(false);
            resultTable.gameObject.SetActive(true);

            //resultWindow.Show();

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
