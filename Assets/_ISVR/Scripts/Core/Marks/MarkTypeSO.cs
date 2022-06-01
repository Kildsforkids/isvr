using UnityEngine;

namespace ISVR.Core.Marks {

    [CreateAssetMenu(menuName = "Marks/MarkType", fileName = "New MarkType")]
    public class MarkTypeSO : ScriptableObject {

        public MarkType Type;
        public Sprite Sprite;
        public Color Color;
    }

    public enum MarkType {
        None,
        Electronic,
        Question,
        Wiretapper
    }
}