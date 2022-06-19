namespace ISVR.UI {

    public class LedLightGroupToggler : LedLightGroup {

        private int _index;

        public void Next() {
            Lights[_index++].Deactivate();
            _index = (_index > LastIndex) ? 0 : _index;
            Lights[_index].Activate();
        }

        public void Previous() {
            Lights[_index--].Deactivate();
            _index = (_index < 0) ? LastIndex : _index;
            Lights[_index].Activate();
        }

        public void NextLocked() {
            int previousIndex = _index++;
            if (_index > LastIndex) {
                _index = previousIndex;
            } else {
                Lights[previousIndex].Deactivate();
                Lights[_index].Activate();
            }
        }

        public void PreviousLocked() {
            int previousIndex = _index--;
            if (_index < 0) {
                _index = previousIndex;
            } else {
                Lights[previousIndex].Deactivate();
                Lights[_index].Activate();
            }
        }
    }
}