using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ISVR {

    public class CommandPanel : MonoBehaviour {
        
        [SerializeField] private VRButton powerButton;
        [SerializeField] private List<VRButton> otherButtons;
        [SerializeField] private TextMeshProUGUI debugText;

        private NonlinearLocator _locator;

        private void Awake() {
            _locator = GetComponent<NonlinearLocator>();
        }

        private void Start() {
            powerButton.OnTouch.AddListener(() => {
                _locator.Toggle();
                SetDebugText(powerButton.Name);
            });

            foreach (var button in otherButtons) {
                button.OnTouch.AddListener(() => {
                    SetDebugText(button.Name);
                });
            }
        }

        private void SetDebugText(string text) {
            debugText.text = text;
        }
    }
}