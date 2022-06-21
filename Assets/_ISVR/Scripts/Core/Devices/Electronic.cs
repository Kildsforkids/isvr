namespace ISVR.Core.Devices {
    public class Electronic : Emitter {

        private void Start() {
            GameState.Instance.AddElectronic(this);
        }
    }
}
