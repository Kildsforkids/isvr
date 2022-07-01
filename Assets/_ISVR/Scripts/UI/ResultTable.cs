using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ISVR.UI {

    public class ResultTable : MonoBehaviour {

        [SerializeField] private int rowLength;
        [SerializeField] private List<TextMeshProUGUI> values;

        public void UpdateValue(int row, int col, int value) {
            int index = row * rowLength + col;
            values[index].text = value.ToString();
        }
    }
}
