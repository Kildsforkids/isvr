namespace ISVR.Core.Devices {
    public class Wiretapper : Electronic {
        
        private void Start() {
            GameState.Instance.AddWiretapper(this);
        }
    }
}
