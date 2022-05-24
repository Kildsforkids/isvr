namespace ISVR.Core.Bugs {

    public class Bug : Electrical {

        private void Start() {
            GameSetup.Instance.AddBug(this);
        }
    }
}