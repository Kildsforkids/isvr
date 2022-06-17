using UnityEngine;

namespace ISVR.Core.Devices {
    public abstract class Emitter : MonoBehaviour {

        [SerializeField] private float secondHarmonicValue;
        [SerializeField] private float thirdHarmonicValue;

        public float SecondHarmonicValue => secondHarmonicValue;
        public float ThirdHarmonicValue => thirdHarmonicValue;
    }
}