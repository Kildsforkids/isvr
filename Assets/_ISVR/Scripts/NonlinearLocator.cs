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
        private Harmonic _currentHarmonic;

        private void Awake() {
            _raycaster = GetComponent<Raycaster>();
            _currentHarmonic = Harmonic.Second;
        }

        private void Start() {
            _raycaster.OnRayReturn.AddListener(CalculateImpact);
        }

        public void TurnOn() {
            StartRaycaster();
            //indicator?.Activate();
            _isActive = true;
            OnTurnOn?.Invoke();
        }

        public void TurnOff() {
            StopRaycaster();
            //bar?.SetValue(0f);
            //indicator?.Deactivate();
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
            float secondHarmonicMaxValue = 0f;
            float thirdHarmonicMaxValue = 0f;

            foreach (var hit in hits) {
                if (hit.transform.TryGetComponent(out Emitter emitter)) {

                    Vector3 vectorA = emitter.transform.position - transform.position;
                    Vector3 vectorB = _raycaster.Origin.forward;

                    float angle = Vector3.Angle(vectorA, vectorB);

                    float secondHarmonicValue = GetValueByAngle(emitter.SecondHarmonicValue, angle);
                    float thirdHarmonicValue = GetValueByAngle(emitter.ThirdHarmonicValue, angle);

                    secondHarmonicMaxValue = Mathf.Max(secondHarmonicMaxValue, secondHarmonicValue);
                    thirdHarmonicMaxValue = Mathf.Max(thirdHarmonicMaxValue, thirdHarmonicValue);
                }
            }

            secondHarmonic.SetTrackValue(secondHarmonicMaxValue);
            thirdHarmonic.SetTrackValue(thirdHarmonicMaxValue);

            switch (_currentHarmonic) {
                case Harmonic.Second:
                    PlaySoundOnValue(secondHarmonicMaxValue);
                break;
                case Harmonic.Third:
                    PlaySoundOnValue(thirdHarmonicMaxValue);
                break;
            }
        }

        private float GetValueByAngle(float value, float angle) =>
            value * Mathf.Cos(angle * Mathf.Deg2Rad);

        private void PlaySoundOnValue(float value) {
            if (value > 20f) {
                audioSource.Play();
            } else {
                audioSource.Pause();
            }
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

    public enum Harmonic {
        None,
        Second,
        Third
    }
}