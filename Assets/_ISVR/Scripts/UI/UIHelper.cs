using UnityEngine;
using UnityEngine.Events;

namespace ISVR.UI {

    public class UIHelper : MonoBehaviour {

        [SerializeField] private MainMenu mainMenu;

        public UnityEvent OnShowMainMenu;
        public UnityEvent OnHideMainMenu;

        private bool _mainMenuIsActive;

        public void ToggleMainMenu() {
            if (!_mainMenuIsActive) {
                ShowMainMenu();
            } else {
                HideMainMenu();
            }
        }

        private void ShowMainMenu() {
            mainMenu.gameObject.SetActive(true);
            mainMenu.Show();
            _mainMenuIsActive = true;
            OnShowMainMenu?.Invoke();
        }

        private void HideMainMenu() {
            mainMenu.gameObject.SetActive(false);
            _mainMenuIsActive = false;
            OnHideMainMenu?.Invoke();
        }
    }
}
