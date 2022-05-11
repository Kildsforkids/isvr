using UnityEngine;
using ISVR.Core;

namespace ISVR {

    [RequireComponent(typeof(Raycaster), typeof(CommandPanel))]
    public class NonlinearLocator : MonoBehaviour {

        [SerializeField] private string defaultDebugText;
        [SerializeField] private Indicator indicator;
        [SerializeField] private Bar bar;
        [SerializeField] private Vector2 errorRate;

        public bool IsActive => _isActive;

        private Raycaster _raycaster;
        private CommandPanel _commandPanel;
        private bool _isActive;

        private void Awake() {
            _raycaster = GetComponent<Raycaster>();
            _commandPanel = GetComponent<CommandPanel>();
        }

        private void Start() {
            _raycaster.OnRayReturn.AddListener(CalculateImpact);
        }

        public void TurnOn() {
            StartRaycaster();
            indicator?.Activate();
            _isActive = true;
        }

        public void TurnOff() {
            StopRaycaster();
            UpdateDebugText(defaultDebugText);
            indicator?.Deactivate();
            _isActive = false;
        }

        public void Toggle() {
            if (IsActive) {
                TurnOff();
            } else {
                TurnOn();
            }
        }

        private void StartRaycaster() {
            _raycaster.CastRayInfinite();
        }

        private void StopRaycaster() {
            _raycaster.StopCastRay();
        }

        private void UpdateDebugText(string text) {
            if (text == null) {
                _commandPanel.UpdateDebugText(defaultDebugText);
            } else {
                _commandPanel.UpdateDebugText(text);
            }
        }

        private void CalculateImpact(RaycastHit[] hits) {
            float average = 0f;
            int electricalCount = 0;
            float maxValue = 0;
            foreach (var hit in hits) {
                if (hit.transform.TryGetComponent<Electrical>(out Electrical electrical)) {
                    average += electrical.Value;
                    maxValue = electrical.Value > maxValue ? electrical.Value : maxValue;
                    Debug.Log(hit.distance);
                    electricalCount++;
                }
            }
            if (electricalCount > 0) {
                average /= electricalCount / maxValue;
            }
            average = Mathf.Clamp01(average + Random.Range(errorRate.x, errorRate.y));
            bar.SetValue(average);
            // UpdateDebugText(average.ToString("F1"));
        }
    }
}