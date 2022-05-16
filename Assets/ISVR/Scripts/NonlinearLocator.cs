using UnityEngine;
using ISVR.Core;

namespace ISVR {

    [RequireComponent(typeof(Raycaster), typeof(CommandPanel))]
    public class NonlinearLocator : MonoBehaviour {

        [SerializeField] private string defaultDebugText;
        [SerializeField] private Indicator indicator;
        [SerializeField] private Bar bar;
        [SerializeField] private Vector2 errorRate;
        [SerializeField] private AudioSource audioSource;

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
            bar.SetValue(0f);
            indicator?.Deactivate();
            _isActive = false;
            audioSource.Pause();
        }

        public void Toggle() {
            if (IsActive) {
                TurnOff();
            } else {
                TurnOn();
            }
        }

        public void TurnSoundOn() {
            if (audioSource.enabled) return;
            audioSource.enabled = true;
        }

        public void TurnSoundOff() {
            if (!audioSource.enabled) return;
            audioSource.enabled = false;
        }

        public void ToggleSound() {
            if (audioSource.enabled) {
                TurnSoundOff();
            } else {
                TurnSoundOn();
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
                if (hit.transform.TryGetComponent(out Electrical electrical)) {
                    average += electrical.Value;
                    maxValue = electrical.Value > maxValue ? electrical.Value : maxValue;
                    electricalCount++;
                }
            }
            if (electricalCount > 0) {
                average /= electricalCount / maxValue;
            }
            average = Mathf.Clamp01(average + Random.Range(errorRate.x, errorRate.y));
            if (average > 0.8f) {
                audioSource.Play();
            } else {
                audioSource.Pause();
            }
            bar.SetValue(average);
        }
    }
}