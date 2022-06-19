using UnityEngine;
using ISVR.Core;
using ISVR.UI;
using ISVR.Core.Devices;
using UnityEngine.Events;

namespace ISVR {

    [RequireComponent(typeof(Raycaster))]
    public class NonlinearLocator : MonoBehaviour {

        [SerializeField] private AudioSource audioSource;

        [SerializeField] private LedLightGroupTracker secondHarmonic;
        [SerializeField] private LedLightGroupTracker thirdHarmonic;
        [SerializeField] private LedLightGroupTracker secondHarmonicGhost;
        [SerializeField] private LedLightGroupTracker thirdHarmonicGhost;

        public UnityEvent OnTurnOn;
        public UnityEvent OnTurnOff;
        public UnityEvent OnToggleBoost;
        public UnityEvent OnToggleSound;
        public UnityEvent OnToggleChannel;
        public UnityEvent OnToggle20KMode;
        public UnityEvent OnAttenuationUp;
        public UnityEvent OnAttenuationDown;

        public bool IsActive => _isActive;

        private Raycaster _raycaster;
        private bool _isActive;
        private bool _isBoosted;
        private bool _is20KModeOn;
        private bool _isRadioChannelOn;
        private Harmonic _currentHarmonic;
        private float[] _attenuation = { 10f, 20f, 30f, 40f };
        private int _currentAttenuationIndex;

        private void Awake() {
            _raycaster = GetComponent<Raycaster>();
            _currentHarmonic = Harmonic.Second;
        }

        private void Start() {
            _raycaster.OnRayReturn.AddListener(CalculateImpact);
        }

        public void TurnOn() {
            StartRaycaster();
            _isActive = true;
            OnTurnOn?.Invoke();
        }

        public void TurnOff() {
            StopRaycaster();
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

        public void Toggle20KMode() {
            if (!IsActive) return;
            _is20KModeOn = !_is20KModeOn;
            OnToggle20KMode?.Invoke();
        }

        public void ToggleRadioChannel() {
            if (!IsActive) return;
            _isRadioChannelOn = !_isRadioChannelOn;
            OnToggleChannel?.Invoke();
        }

        public void ToggleSound() {
            if (!IsActive) return;

            _currentHarmonic = _currentHarmonic == Harmonic.Second ? Harmonic.Third : Harmonic.Second;

            OnToggleSound?.Invoke();
        }

        public void AttenuationUp() {
            if (!IsActive) return;
            int lastIndex = _currentAttenuationIndex++;
            if (_currentAttenuationIndex > _attenuation.Length - 1) {
                _currentAttenuationIndex = lastIndex;
            } else {
                OnAttenuationUp?.Invoke();
            }
        }

        public void AttenuationDown() {
            if (!IsActive) return;
            int lastIndex = _currentAttenuationIndex--;
            if (_currentAttenuationIndex < 0) {
                _currentAttenuationIndex = lastIndex;
            } else {
                OnAttenuationDown?.Invoke();
            }
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

            secondHarmonicGhost.SetTrackValue(secondHarmonicMaxValue);
            thirdHarmonicGhost.SetTrackValue(thirdHarmonicMaxValue);

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