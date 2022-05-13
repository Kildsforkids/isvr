using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace ISVR {

    public class GameSetup : MonoBehaviour {

        public static GameSetup Instance { get; private set; }

        [SerializeField] private Player player;
        [SerializeField] private int targetFrameRate;
        [SerializeField] private TextMeshProUGUI fpsText;
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private int bugMarksMaxCount;
        [SerializeField] private float bugMarkMaxSearchDistance;
        [SerializeField] private List<Bug> bugs;
        [SerializeField] private BugMark[] bugMarks;

        public Player Player => player;
        public int BugMarkersCount => bugMarks.Length;

        private List<BugMark> correctBugMarks;
        private bool _isLevelEnded;

        private void Awake() {
            bugMarks = new BugMark[bugMarksMaxCount];
            correctBugMarks = new List<BugMark>();
            Instance = this;
            Application.targetFrameRate = targetFrameRate;
        }

        private void Start() {
            StartCoroutine(FPSUpdateCoroutine());
        }

        public void AddBug(Bug bug) {
            bugs.Add(bug);
        }

        public int TryAddBugMark(BugMark bugMark) {
            int freeSlotIndex = GetFreeBugMarkSlotIndex();
            if (freeSlotIndex >= 0) {
                AddBugMark(bugMark, freeSlotIndex);
            }
            return freeSlotIndex;
        }

        public void AddBugMark(BugMark bugMark, int index) {
            bugMarks[index] = bugMark;
        }

        public void RemoveBugMark(int index) {
            bugMarks[index] = null;
        }

        public int GetFreeBugMarkSlotIndex() {
            for (int i = 0; i < bugMarks.Length; i++) {
                if (bugMarks[i] == null) {
                    return i;
                }
            }
            return -1;
        }

        public void EndLevel() {
            if (_isLevelEnded) return;
            float result = CalculatePredictResult();
            resultText.text = $"{(result * 100f):0.##}%";
            Player.EndLevel();
            _isLevelEnded = true;
        }

        public void RestartScene() {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        private float CalculatePredictResult() {
            int bugMarksCount = 0;
            for (int i = 0; i < bugMarksMaxCount; i++) {
                if (bugMarks[i] != null) {
                    bugMarksCount++;
                }
            }
            foreach (var bug in bugs) {
                var bugMark = GetNearestBugMark(bug.transform.position, bugMarkMaxSearchDistance);
                if (bugMark != null) {
                    correctBugMarks.Add(bugMark);
                }
            }
            foreach (var bugMark in correctBugMarks) {
                bugMark.HighlightAsBug();
            }
            float result = (float)correctBugMarks.Count / bugMarksCount;
            return result;
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

        private IEnumerator FPSUpdateCoroutine() {
            var waitForSeconds = new WaitForSeconds(1f);
            while (true) {
                fpsText.text = (Time.frameCount / Time.time).ToString("F0");
                yield return waitForSeconds;
            }
        }
    }
}