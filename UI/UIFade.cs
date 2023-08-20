using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.UI
{
    /// <summary>
    /// Optimized and customizable CanvasGroup UI Fade script managed by an Animation Cruve
    /// Events:
    /// OnStartFade
    /// OnEndFade
    /// </summary>
    public class UIFade : MonoBehaviour
    {
        [Header("Canvas")]
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Config")]
        [SerializeField] private AnimationCurve _fadeCurve = new AnimationCurve();
        [SerializeField, Min(0), Tooltip("In seconds")] private float _updateRateTime = 0.05f;
        [SerializeField, Min(0), Tooltip("In seconds")] private float _preDelayTime = 0f;
        [SerializeField, Min(0), Tooltip("In seconds")] private float _postDelayTime = 0f;

        [Header("Events")]
        public UnityEvent OnStartFade = new UnityEvent();
        public UnityEvent OnEndFade = new UnityEvent();

        [Header("Debug mode")]
        [SerializeField] private bool _showLogs = false;

        private Coroutine _coroutine = null;
        private WaitForSeconds _updateRate = default;
        private WaitForSeconds _preDelay = default;
        private WaitForSeconds _postDelay = default;


        private void Awake()
        {
            _updateRate = new WaitForSeconds(_updateRateTime);
            _preDelay = new WaitForSeconds(_preDelayTime);
            _postDelay = new WaitForSeconds(_postDelayTime);
        }


        public void StartFade()
        {
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(Fade());
            }
            else
            {
                Debug.LogWarning($"Fade in progress!");
                return;
            }

            if (_showLogs) { Debug.Log($"Fade started!"); }
        }

        public void StopFade()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            if (_showLogs) { Debug.Log($"Fade stopped!"); }
        }


        private IEnumerator Fade()
        {
            OnStartFade?.Invoke();
            yield return _preDelay;

            float _fadeTime = _fadeCurve.keys[0].time;

            do
            {
                _fadeTime += _updateRateTime;
                _canvasGroup.alpha = Mathf.Clamp01(_fadeCurve.Evaluate(_fadeTime));

                if (_showLogs) { Debug.Log($"New Canvas Group Alpha setted: {_canvasGroup.alpha}"); }

                yield return _updateRate;

            } while (_fadeTime < _fadeCurve.keys[_fadeCurve.length - 1].time);

            yield return _postDelay;
            OnEndFade?.Invoke();

            StopFade();
        }
    }
}
