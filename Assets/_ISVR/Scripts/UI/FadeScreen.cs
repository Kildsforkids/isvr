using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ISVR.UI {
    [RequireComponent(typeof(MeshRenderer))]
    public class FadeScreen : MonoBehaviour {
        
        [SerializeField] private float fadeDuration;
        [SerializeField] private float minAlpha = 0f;
        [SerializeField] private float maxAlpha = 1f;

        public UnityEvent OnFadeIn;
        public UnityEvent OnFadeOut;

        private MeshRenderer _meshRenderer;
        private Color _fadeColor;
        private Coroutine _fadeCoroutine;

        private void Awake() {
            _meshRenderer = GetComponent<MeshRenderer>();
            _fadeColor = _meshRenderer.material.color;
        }

        public void FadeOut() {
            if (_fadeCoroutine != null) {
                StopCoroutine(_fadeCoroutine);
            }
            _fadeCoroutine = StartCoroutine(FadeCoroutine(minAlpha, maxAlpha));
        }

        public void FadeIn() {
            if (_fadeCoroutine != null) {
                StopCoroutine(_fadeCoroutine);
            }
            _fadeCoroutine = StartCoroutine(FadeCoroutine(maxAlpha, minAlpha));
        }

        public void FadeInOut() {
            if (_fadeCoroutine != null) {
                StopCoroutine(_fadeCoroutine);
            }
            _fadeCoroutine = StartCoroutine(FadeInOutCoroutine());
        }

        private IEnumerator FadeCoroutine(float alphaIn, float alphaOut) {
            float timer = 0;
            while (timer <= fadeDuration) {
                _fadeColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

                _meshRenderer.material.color = _fadeColor;

                timer += Time.deltaTime;
                yield return null;
            }
            _fadeColor.a = alphaOut;
            _meshRenderer.material.color = _fadeColor;
        }

        private IEnumerator FadeInOutCoroutine() {
            yield return FadeCoroutine(minAlpha, maxAlpha);
            OnFadeIn?.Invoke();
            yield return FadeCoroutine(maxAlpha, minAlpha);
            OnFadeOut?.Invoke();
        }
    }
}
