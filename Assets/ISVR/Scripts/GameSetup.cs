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
        [SerializeField] private int bugMarksMaxCount;
        [SerializeField] private List<Bug> bugs;
        [SerializeField] private BugMark[] bugMarks;

        public Player Player => player;
        public int BugMarkersCount => bugMarks.Length;

        private void Awake() {
            bugMarks = new BugMark[bugMarksMaxCount];
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

        public BugMark GetNearestBugMark(Vector3 position) {
            BugMark nearestBugMark = null;
            float minDistance = Mathf.Infinity;
            for (int i = 0; i < bugMarks.Length; i++) {
                if (bugMarks[i] != null) {
                    var distance = (bugMarks[i].transform.position - position).sqrMagnitude;
                    if (distance < minDistance) {
                        minDistance = distance;
                        nearestBugMark = bugMarks[i];
                    }
                }
            }
            return nearestBugMark;
        }

        public void EndLevel() {
            Debug.Log("You just ended level! Congrats!");
        }

        public void RestartScene() {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
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