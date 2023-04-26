using UnityEngine;

/// <summary>
/// Singleton Frame Rate Limiter Class
/// Can set Frame Rate equal to Screen Refresh Rate or a defined by user
/// </summary>
public class FrameRateLimiterManager : Singleton<FrameRateLimiterManager>
{
    public bool MatchScreenRefreshRate { 
        get 
        { 
            return _matchScreenRefreshRate; 
        }
        set 
        {
            if (value)
                Application.targetFrameRate = Screen.currentResolution.refreshRate;
            else
                Application.targetFrameRate = _frameRate;
        } 
    }

    [Header("Settings")]

    [SerializeField] private bool _matchScreenRefreshRate = false;
    [Tooltip("If 0 will be the maximum the device can render")]
    [SerializeField] private int _frameRate = 60;

    // Start is called before the first frame update
    void Start()
    {
        MatchScreenRefreshRate = _matchScreenRefreshRate;
    }

    public void SetFrameRate(int frameRate)
    {
        Application.targetFrameRate = frameRate;
    }
}
