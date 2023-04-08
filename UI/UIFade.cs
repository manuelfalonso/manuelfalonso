using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Simple CanvasGroup UI Fade script managed by an Animation Cruve
/// Events:
/// OnStartFade
/// OnEndFade
/// </summary>
public class UIFade : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField]
    private CanvasGroup _canvasGroup;

    [Header("Config")]
    [SerializeField]
    private AnimationCurve _fadeCurve = new AnimationCurve();
    [SerializeField]
    private float _preDelayTime = 0f;
    [SerializeField]
    private float _postDelayTime = 0f;

    [Header("Events")]
    public UnityEvent OnStartFade = new UnityEvent();
    public UnityEvent OnEndFade = new UnityEvent();

    [Header("Debug mode")]
    [SerializeField]
    private bool _debugMode = false;

    private Coroutine _coroutine = null;
    private WaitForSeconds _preDelay = default;
    private WaitForSeconds _postDelay = default;


    private void Awake()
    {
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
        
        if (_debugMode)
            Debug.Log($"Fade started!");
    }

    public void StopFade()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        if (_debugMode)
            Debug.Log($"Fade stopped!");
    }


    private IEnumerator Fade()
    {
        OnStartFade?.Invoke();
        yield return _preDelay;

        // Set fadeTime as the Animiation Curve initial time
        // Set new clamped alpha
        // Add deltaTime to fadeTime
        // Until fadeTime is equal or greater than the Animiation Curve last time
        for (float _fadeTime = _fadeCurve.keys[0].time; 
            _fadeTime <= _fadeCurve.keys[_fadeCurve.length - 1].time;
            _fadeTime += Time.deltaTime)
        {
            _canvasGroup.alpha = Mathf.Clamp01(_fadeCurve.Evaluate(_fadeTime));

            if (_debugMode)
                Debug.Log($"New Canvas Group Alpha setted: {_canvasGroup.alpha}");
            
            yield return null;
        }

        yield return _postDelay;
        OnEndFade?.Invoke();

        StopFade();
    }
}
