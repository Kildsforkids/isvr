using System.Collections;
using UnityEngine;

namespace ISVR.UI {

    public class Popup : MonoBehaviour {

        [SerializeField] private bool hideByTime;
        [SerializeField] private float lifeTime;

        private Coroutine _hideByTimeCoroutine;
        private float _time;

        private void OnEnable() {
            if (hideByTime) {
                _time = lifeTime;
                HideByTime();
            }
        }

        public void Refresh() {
            _time = lifeTime;
        }

        private void HideByTime() {
            if (_hideByTimeCoroutine != null) return;
            _hideByTimeCoroutine = StartCoroutine(HideByTimeCoroutine());
        }

        private IEnumerator HideByTimeCoroutine() {
            while (_time > 0f) {
                _time -= Time.deltaTime;
                yield return null;
            }
            Hide();
        }

        private void Hide() {
            _hideByTimeCoroutine = null;
            gameObject.SetActive(false);
        }
    }
}