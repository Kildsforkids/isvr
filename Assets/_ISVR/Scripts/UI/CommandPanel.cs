using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ISVR.UI {

    public class CommandPanel : MonoBehaviour {
        
        [SerializeField] private Transform projection;
        [SerializeField] private Fingertip fingertip;
        [SerializeField] private TouchButton powerButton;
        [SerializeField] private TouchButton powerBoostButton;
        [SerializeField] private TouchButton harmonicSoundButton;
        [SerializeField] private TouchButton volumePositiveButton;
        [SerializeField] private TouchButton volumeNegativeButton;
        [SerializeField] private List<TouchButton> otherButtons;
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

            powerBoostButton.OnTouch.AddListener(() => {
                _locator.ToggleBoost();
                SetDebugText(powerBoostButton.Name);
            });

            harmonicSoundButton.OnTouch.AddListener(() => {
                _locator.ToggleSound();
                SetDebugText(harmonicSoundButton.Name);
            });

            volumePositiveButton.OnTouch.AddListener(() => {
                _locator.VolumeUp();
                SetDebugText(volumePositiveButton.Name);
            });

            volumeNegativeButton.OnTouch.AddListener(() => {
                _locator.VolumeDown();
                SetDebugText(volumeNegativeButton.Name);
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

        public void ShowProjection() {
            if (projection.gameObject.activeSelf) return;
            projection.gameObject.SetActive(true);
            fingertip.Show();
        }

        public void HideProjection() {
            if (!projection.gameObject.activeSelf) return;
            projection.gameObject.SetActive(false);
        }
    }
}