using UnityEngine;
using TMPro;

namespace ISVR {

    public class CommandPanel : MonoBehaviour {
        
        [SerializeField] private TextMeshProUGUI debugText;

        public void UpdateDebugText(string text) {
            debugText.text = text;
        }
    }
}