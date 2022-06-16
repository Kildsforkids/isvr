namespace ISVR.UI {

    public class LedLightGroupToggler : LedLightGroup {

        private int _index;

        public void Next() {
            Lights[_index++].Deactivate();
            _index = (_index > LastIndex) ? 0 : _index;
            Lights[_index].Activate();
        }
    }
}