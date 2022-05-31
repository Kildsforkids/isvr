using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Oculus.Interaction.Input;
using ISVR.Core.Bugs;
using ISVR.UI;
using UnityEngine.Events;

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
        [SerializeField] private List<Transform> bugMarksStopList;
        [SerializeField] private Counter bugMarksCounter;

        public UnityEvent OnAwake;

        public Player Player => player;
        public int BugMarkersCount => bugMarks.Length;

        private List<BugMark> _correctBugMarks;
        private bool _isLevelEnded;
        private int _bugMarksCount;

        private void Awake() {
            bugMarks = new BugMark[bugMarksMaxCount];
            _correctBugMarks = new List<BugMark>();
            Instance = this;
            Application.targetFrameRate = targetFrameRate;
            OnAwake?.Invoke();
        }

        private void Start() {
            // StartCoroutine(FPSUpdateCoroutine());
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
            _bugMarksCount++;
            bugMarksCounter.Add();
        }

        public void RemoveBugMark(int index) {
            bugMarks[index] = null;
            _bugMarksCount--;
            bugMarksCounter.Subtract();
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
            // resultText.text = $"{(result * 100f):0.##}%";
            // resultText.enabled = true;
            _isLevelEnded = true;
        }

        public void RestartScene() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public bool FindInStopList(Transform transform) {
            return bugMarksStopList.Contains(transform);
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
                        _correctBugMarks.Add(bugMark);
                    }
                }
                foreach (var bugMark in _correctBugMarks) {
                    bugMark.HighlightAsBug();
                }
                float bugsCount = bugs.Count;
                float correctBugMarksCount = _correctBugMarks.Count;
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

        private IEnumerator FPSUpdateCoroutine() {
            var waitForSeconds = new WaitForSeconds(1f);
            while (true) {
                fpsText.text = (Time.frameCount / Time.time).ToString("F0");
                yield return waitForSeconds;
            }
        }
    }
}