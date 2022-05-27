using System.Collections.Generic;
using ISVR.Core.Bugs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ISVR {

    public class GameState : MonoBehaviour {

        public static GameState Instance;

        [SerializeField] private Player player;
        [SerializeField] private NonlinearLocator nonlinearLocator;
        [SerializeField] private int bugMarksMaxCount;
        [SerializeField] private float bugMarkMaxSearchDistance;
        [SerializeField] private List<Bug> bugs;
        [SerializeField] private BugMark[] bugMarks;

        public bool IsTaskEnded => _isTaskEnded;
        
        private List<BugMark> correctBugMarks;
        private bool _isTaskEnded;
        private bool _isMuted;

        private void Awake() {
            bugMarks = new BugMark[bugMarksMaxCount];
            correctBugMarks = new List<BugMark>();
            Instance = this;
        }

        public void EndTask() {
            if (_isTaskEnded) return;
            float result = CalculatePredictResult();
            _isTaskEnded = true;
        }

        public void ResetLevel() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void ToggleSound() {
            if (_isMuted) {
                TurnSoundOn();
            } else {
                TurnSoundOff();
            }
        }

        private void TurnSoundOn() {
            nonlinearLocator.TurnSoundOn();
        }

        private void TurnSoundOff() {
            nonlinearLocator.TurnSoundOff();
        }

        private float CalculatePredictResult() {
            int bugMarksCount = 0;
            for (int i = 0; i < bugMarksMaxCount; i++) {
                if (bugMarks[i] != null) {
                    bugMarksCount++;
                }
            }
            // foreach (var bug in bugs) {
            //     var outline = bug.gameObject.AddComponent<Outline>();
            //     outline.OutlineColor = Color.yellow;
            //     outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
            // }
            foreach (var bug in bugs) {
                bug.gameObject.layer = 7;
                var meshRenderer = bug.GetComponent<MeshRenderer>();
                if (!meshRenderer.enabled) {
                    meshRenderer.enabled = true;
                }
            }
            if (bugMarksCount > 0) {
                foreach (var bug in bugs) {
                    var bugMark = GetNearestBugMark(bug.transform.position, bugMarkMaxSearchDistance);
                    if (bugMark != null) {
                        correctBugMarks.Add(bugMark);
                    }
                }
                foreach (var bugMark in correctBugMarks) {
                    bugMark.HighlightAsBug();
                }
                float bugsCount = bugs.Count;
                float correctBugMarksCount = correctBugMarks.Count;
                float result = (float)correctBugMarksCount / bugMarksCount -
                    (float)(bugsCount - correctBugMarksCount) / bugsCount;
                if (result < 0f) {
                    result = 0f;
                }
                return result;
            } else {
                return 0;
            }
        }

        private BugMark GetNearestBugMark(Vector3 position, float maxDistance) {
            BugMark nearestBugMark = null;
            float minDistance = Mathf.Infinity;
            for (int i = 0; i < bugMarks.Length; i++) {
                if (bugMarks[i] != null) {
                    var distance = (bugMarks[i].transform.position - position).sqrMagnitude;
                    if (distance < maxDistance && distance < minDistance) {
                        minDistance = distance;
                        nearestBugMark = bugMarks[i];
                    }
                }
            }
            return nearestBugMark;
        }
    }
}
