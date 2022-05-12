using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ISVR {

    public class GameSetup : MonoBehaviour {

        public static GameSetup Instance { get; private set; }

        [SerializeField] private Player player;
        [SerializeField] private int targetFrameRate;
        [SerializeField] private TextMeshProUGUI fpsText;
        [SerializeField] private List<Electrical> bugs;
        [SerializeField] private List<BugMark> bugMarks;

        public Player Player => player;
        public int BugMarkersCount => bugMarks.Count;

        private void Awake() {
            Instance = this;
            Application.targetFrameRate = targetFrameRate;
        }

        private void Start() {
            StartCoroutine(FPSUpdateCoroutine());
        }

        public void AddBug(Electrical electrical) {
            bugs.Add(electrical);
        }

        public void AddBugMark(BugMark bugMark) {
            bugMarks.Add(bugMark);
        }

        public void RemoveBugMark(int index) {
            bugMarks.RemoveAt(index);
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