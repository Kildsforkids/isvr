using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace ISVR {

    public class HandMenu : MonoBehaviour {

        [SerializeField] private Canvas canvas;
        [SerializeField] private List<HandMenuOption> options;

        public UnityEvent OnShow;
        public UnityEvent OnHide;

        public bool IsActive { get; private set; }

        private int selectedOptionIndex;

        private void Start() {
            selectedOptionIndex = 0;
            SelectOption(selectedOptionIndex);
        }

        public void Show() {
            if (IsActive) return;
            canvas.enabled = true;
            IsActive = true;
            OnShow?.Invoke();
        }

        public void Hide() {
            if (!IsActive) return;
            canvas.enabled = false;
            IsActive = false;
            OnHide?.Invoke();
        }

        public void Toggle() {
            if (IsActive) {
                Hide();
            } else {
                Show();
            }
        }

        public void SelectNextOption() {
            if (!IsActive) return;
            UnselectOption(selectedOptionIndex);
            selectedOptionIndex++;
            selectedOptionIndex = selectedOptionIndex < options.Count ? selectedOptionIndex : 0;
            SelectOption(selectedOptionIndex);
        }

        public void SelectPreviousOption() {
            if (!IsActive) return;
            UnselectOption(selectedOptionIndex);
            selectedOptionIndex--;
            selectedOptionIndex = selectedOptionIndex >= 0 ? selectedOptionIndex : options.Count-1;
            SelectOption(selectedOptionIndex);
        }

        public void ActivateSelectedOption() {
            if (!IsActive) return;
            options[selectedOptionIndex].Activate();
        }

        private void SelectOption(int index) {
            options[index].Select();
        }

        private void UnselectOption(int index) {
            options[index].Unselect();
        }
    }
}