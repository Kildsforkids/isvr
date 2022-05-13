using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace ISVR {

    public class HandMenuOption : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI text;

        public UnityEvent OnActivate;

        public void Activate() {
            OnActivate?.Invoke();
        }

        public void Select() {
            text.color = Color.yellow;
        }

        public void Unselect() {
            text.color = Color.white;
        }
    }
}