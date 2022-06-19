using UnityEngine;
using UnityEngine.Events;

namespace ISVR.UI {

    public class CommandPanel : MonoBehaviour {

        [SerializeField] private Transform projection;

        public UnityEvent OnShowProjection;

        public void ShowProjection() {
            if (projection.gameObject.activeSelf) return;
            projection.gameObject.SetActive(true);
            OnShowProjection?.Invoke();
        }

        public void HideProjection() {
            if (!projection.gameObject.activeSelf) return;
            projection.gameObject.SetActive(false);
        }
    }
}