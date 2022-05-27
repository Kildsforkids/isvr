using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ISVR.UI {
    public class IconChanger : MonoBehaviour {

        [SerializeField] private Image image;
        [SerializeField] private List<Sprite> icons;

        private int _index;
        private int _maxIndex;

        private void Start() {
            _maxIndex = icons.Count - 1;
        }
        
        public void NextIcon() {
            _index++;
            if (_index > _maxIndex) {
                _index = 0;
            }
            ChooseIcondByIndex(_index);
        }

        private void ChooseIcondByIndex(int index) {
            image.sprite = icons[index];
        }
    }
}
