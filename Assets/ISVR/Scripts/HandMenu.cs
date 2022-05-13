using UnityEngine;
using System.Collections.Generic;

namespace ISVR {

    public class HandMenu : MonoBehaviour {

        [SerializeField] private Canvas canvas;
        [SerializeField] private List<HandMenuOption> options;

        public bool IsActive { get; private set; }

        private int selectedOptionIndex;

        private void Start() {
            SelectOption(selectedOptionIndex);
        }

        public void Show() {
            if (IsActive) return;
            canvas.enabled = true;
            IsActive = true;
        }

        public void Hide() {
            if (!IsActive) return;
            canvas.enabled = false;
            IsActive = false;
        }

        public void Toggle() {
            if (IsActive) {
                Hide();
            } else {
                Show();
            }
        }

        public void SelectNextOption() {
            UnselectOption(selectedOptionIndex);
            selectedOptionIndex++;
            selectedOptionIndex = selectedOptionIndex < options.Count ? selectedOptionIndex : 0;
            SelectOption(selectedOptionIndex);
        }

        public void SelectPreviousOption() {
            UnselectOption(selectedOptionIndex);
            selectedOptionIndex--;
            selectedOptionIndex = selectedOptionIndex >= 0 ? selectedOptionIndex : options.Count-1;
            SelectOption(selectedOptionIndex);
        }

        public void ActivateSelectedOption() {
            options[selectedOptionIndex].Activate();
        }

        public void SelectOption(int index) {
            options[index].Select();
        }

        public void UnselectOption(int index) {
            options[index].Unselect();
        }
    }
}