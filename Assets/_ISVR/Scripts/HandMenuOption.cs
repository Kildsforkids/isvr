using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace ISVR {

    public class HandMenuOption : MonoBehaviour {

        // [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Transform background;

        public UnityEvent OnActivate;

        public void Activate() {
            OnActivate?.Invoke();
        }

        public void Select() {
            background.gameObject.SetActive(true);
            // text.color = Color.yellow;
        }

        public void Unselect() {
            background.gameObject.SetActive(false);
            // text.color = Color.white;
        }
    }
}