using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Simple Splash screen controller that activates and deactivates objects
/// Each object can set its own show duration time
/// </summary>
public class SplashScreenController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private bool _playOnStart = true;
    [SerializeField]
    private float _preDelayTime = 0f;
    [SerializeField]
    private float _betweenLogosTime = 0f;
    [SerializeField]
    private float _postDelayTime = 0f;

    [Space]
    [SerializeField]
    private List<SplashScreenLogo> _logos = new List<SplashScreenLogo>();

    [Header("Events")]
    public UnityEvent OnSplashScreenCompleted = new UnityEvent();

    [Header("Debug")]
    [SerializeField]
    private bool _debugMode = false;


    private Coroutine _coroutine = null;
    private WaitForSeconds _delayBetweenLogos = default;


    private void Awake()
    {
        _delayBetweenLogos = new WaitForSeconds(_betweenLogosTime);
    }

    void Start()
    {
        if (!_playOnStart)
            return;

        RunSplashScreen();
    }


    public void RunSplashScreen()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(Play());
        else
            Debug.LogWarning($"Splash screen logos already running");
    }


    private IEnumerator Play()
    {
        if (_debugMode)
            Debug.Log($"Splash screen running");

        yield return new WaitForSeconds(_preDelayTime);

        for (int _currentIndex = 0; _currentIndex < _logos.Count; _currentIndex++)
        {
            if (_debugMode)
                Debug.Log($"Running logo index {_currentIndex}");

            _logos[_currentIndex].gameObject.SetActive(true);
            yield return new WaitForSeconds(_logos[_currentIndex].logoDurationTime);
            _logos[_currentIndex].gameObject.SetActive(false);
            yield return _delayBetweenLogos;
        }

        yield return new WaitForSeconds(_postDelayTime);

        OnSplashScreenCompleted?.Invoke();
    }
}
