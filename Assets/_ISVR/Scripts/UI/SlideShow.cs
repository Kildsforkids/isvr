using UnityEngine;

namespace ISVR.UI {
    public class SlideShow : MonoBehaviour {
        
        private Slide[] _slides;
        private int _slideIndex;
        private int _previousSlideIndex;
        private int _slidesCount;

        private void Start() {
            _slides = GetComponentsInChildren<Slide>();
            _slidesCount = _slides.Length;
        }

        public void NextSlide() {
            if (_slideIndex >= _slidesCount - 1) return;
            _previousSlideIndex = _slideIndex;
            _slideIndex++;
            ShowSlide();
        }

        public void PreviousSlide() {
            if (_slideIndex <= 0) return;
            _previousSlideIndex = _slideIndex;
            _slideIndex--;
            ShowSlide();
        }

        private void ShowSlide() {
            Debug.Log($"Showing slide {_slideIndex}, previous slide {_previousSlideIndex}");
        }
    }
}
