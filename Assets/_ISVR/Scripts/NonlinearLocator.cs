using UnityEngine;
using ISVR.Core;
using ISVR.UI;
using ISVR.Core.Devices;
using UnityEngine.Events;

namespace ISVR {

    [RequireComponent(typeof(Raycaster))]
    public class NonlinearLocator : MonoBehaviour {

        [SerializeField] private Indicator indicator;
        [SerializeField] private Bar bar;
        [SerializeField] private Vector2 errorRate;
        [SerializeField] private AudioSource audioSource;

        [SerializeField] private LedLightGroupTracker secondHarmonic;
        [SerializeField] private LedLightGroupTracker thirdHarmonic;

        public UnityEvent OnTurnOn;
        public UnityEvent OnTurnOff;
        public UnityEvent OnToggleBoost;
        public UnityEvent OnToggleSound;
        public UnityEvent OnToggleChannel;
        public UnityEvent OnToggle20KMode;

        public bool IsActive => _isActive;

        private Raycaster _raycaster;
        private bool _isActive;
        private bool _isBoosted;

        private void Awake() {
            _raycaster = GetComponent<Raycaster>();
        }

        private void Start() {
            _raycaster.OnRayReturn.AddListener(CalculateImpact);
        }

        public void TurnOn() {
            StartRaycaster();
            indicator?.Activate();
            _isActive = true;
            OnTurnOn?.Invoke();
        }

        public void TurnOff() {
            StopRaycaster();
            bar?.SetValue(0f);
            indicator?.Deactivate();
            _isActive = false;
            audioSource.Pause();
            OnTurnOff.Invoke();
        }

        public void Toggle() {
            if (IsActive) {
                TurnOff();
            } else {
                TurnOn();
            }
        }

        public void ToggleBoost() {
            if (!IsActive) return;
            if (_isBoosted) {
                BoostDown();
            } else {
                BoostUp();
            }
            OnToggleBoost?.Invoke();
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
            if (!IsActive) return;
            if (audioSource.enabled) {
                TurnSoundOff();
            } else {
                TurnSoundOn();
            }
            OnToggleSound?.Invoke();
        }

        public void VolumeUp(float value = 0.1f) {
            if (!IsActive) return;
            audioSource.volume = Mathf.Clamp01(audioSource.volume + value);
        }

        public void VolumeDown(float value = 0.1f) {
            if (!IsActive) return;
            audioSource.volume = Mathf.Clamp01(audioSource.volume - value);
        }

        private void StartRaycaster() {
            _raycaster.CastRayInfinite();
        }

        private void StopRaycaster() {
            _raycaster.StopCastRay();
        }

        private void CalculateImpact(RaycastHit[] hits) {
            float average = 0f;
            int electricalCount = 0;
            float maxValue = 0;
            foreach (var hit in hits) {
                if (hit.transform.TryGetComponent(out Electronic electrical)) {
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

        private void BoostUp() {
            if (!IsActive) return;
            if (_isBoosted) return;
            _raycaster.Distance = _raycaster.Distance * 2f;
            _isBoosted = true;
        }

        private void BoostDown() {
            if (!IsActive) return;
            if (!_isBoosted) return;
            _raycaster.Distance = _raycaster.Distance / 2f;
            _isBoosted = false;
        }
    }
}