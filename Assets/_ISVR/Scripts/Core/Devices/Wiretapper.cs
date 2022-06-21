namespace ISVR.Core.Devices {
    public class Wiretapper : Emitter {
        
        private void Start() {
            GameState.Instance.AddWiretapper(this);
        }
    }
}
