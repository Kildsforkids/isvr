namespace ISVR {

    public class Bug : Electrical {

        private void Start() {
            GameSetup.Instance.AddBug(this);
        }
    }
}