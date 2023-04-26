using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Attach this to a GUIText to make a frames/second indicator with colors.
///
/// It calculates frames/second over each updateInterval,
/// so the display does not keep changing wildly.
///
/// It is also fairly accurate at very low FPS counts (<10).
/// We do this not by simply counting frames per interval, but
/// by accumulating FPS for each frame. This way we end up with
/// correct overall FPS even if the interval renders something like
/// 5.5 frames.
/// </summary>
public class HUDFPS : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private Text _legacyText;
    [SerializeField]
    private TextMeshProUGUI _tMProText;

    [Header("Config")]
    [SerializeField]
    private float _updateInterval = 0.5F;

    private float _accum = 0; // FPS accumulated over the interval
    private int _frames = 0; // Frames drawn over the interval
    private float _timeleft; // Left time for current interval


    void Start()
    {
        if (TryGetComponent(out Text legacyText))
            _legacyText = legacyText;

        if (TryGetComponent(out TextMeshProUGUI tMProText))
            _tMProText = tMProText;

        if (_legacyText == null && _tMProText == null)
        {
            Debug.LogError($"{this}: needs a Text component!");
            enabled = false;
            return;
        }

        _timeleft = _updateInterval;
    }

    void Update()
    {
        _timeleft -= Time.deltaTime;
        _accum += Time.timeScale / Time.deltaTime;
        ++_frames;

        // Interval ended - update GUI text and start new interval
        if (_timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            float fps = _accum / _frames;
            string format = string.Format("{0:F2} FPS", fps);

            if (_legacyText != null)
                SetLegacyText(fps, format);

            if (_tMProText != null)
                SetTMProText(fps, format);

            _timeleft = _updateInterval;
            _accum = 0.0F;
            _frames = 0;
        }
    }


    private void SetLegacyText(float fps, string format)
    {
        _legacyText.text = format;

        if (fps < 30)
            _legacyText.color = Color.yellow;
        else
            if (fps < 10)
            _legacyText.color = Color.red;
        else
            _legacyText.color = Color.green;
    }

    private void SetTMProText(float fps, string format)
    {
        _tMProText.text = format;

        if (fps < 30)
            _tMProText.color = Color.yellow;
        else
            if (fps < 10)
            _tMProText.color = Color.red;
        else
            _tMProText.color = Color.green;
    }
}