using ISVR.Core.Devices;

namespace ISVR.Core.Bugs {

    public class Bug : Electronic {

        private void Start() {
            GameSetup.Instance.AddBug(this);
        }
    }
}