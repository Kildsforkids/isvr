using UnityEngine;

namespace ISVR.UI {
    public class LedLightGroupTracker : LedLightGroup {

        [SerializeField] private int chunkSize;
        [SerializeField] private float chunkMaxValue;

        private float _oneLedValue;

        private void Awake() {
            _oneLedValue = chunkMaxValue / chunkSize;
        }

        public void SetTrackValue(float value) {

            int lightsAmount = Mathf.FloorToInt(value / _oneLedValue);

            Deactivate();

            for (int i = 0; i < lightsAmount; i++) {
                Lights[i].Activate();
            }
        }
    }
}