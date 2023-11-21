using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SombraStudios.Shared.UI
{
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
        private Text _legacyText = default;
        [SerializeField]
        private TextMeshProUGUI _tMProText = default;
        [SerializeField]
        private TextMeshProUGUI _targetFrameRateText = default;
        [SerializeField]
        private TextMeshProUGUI _screenRefreshRateText = default;

        [Header("Config")]
        [SerializeField]
        private float _updateInterval = 0.5F;
        [SerializeField]
        private bool _debugMode = false;

        private float _accum = 0; // FPS accumulated over the interval
        private int _frames = 0; // Frames drawn over the interval
        private float _timeleft; // Left time for current interval


        void Start()
        {
            if (_legacyText == null && _tMProText == null)
            {
                if (_debugMode)
                    Debug.LogError($"{this} => needs a Text component!");
                enabled = false;
                return;
            }

            SetTargetFrameRateText();

            SetScreenResolutionRefreshRateText();

            _timeleft = _updateInterval;
        }

        void Update()
        {
            CalculateFPS();
        }


        private void SetTargetFrameRateText()
        {
            if (_targetFrameRateText != null)
            {
                var frameRate = Application.targetFrameRate.ToString();
                _targetFrameRateText.text = $"Target Frame Rate: {frameRate}";
            }
        }

        private void SetScreenResolutionRefreshRateText()
        {
            if (_screenRefreshRateText != null)
            {
                var refreshRate = Screen.currentResolution.refreshRate.ToString();
                _screenRefreshRateText.text = $"Screen Refresh Rate: {refreshRate}";
            }
        }

        private void CalculateFPS()
        {
            _timeleft -= Time.deltaTime;
            _accum += Time.timeScale / Time.deltaTime;
            ++_frames;

            // Interval ended - update GUI text and start new interval
            if (_timeleft <= 0.0)
            {
                // display two fractional digits (f2 format)
                float fps = _accum / _frames;
                string format = string.Format("{0:F0} FPS", fps);

                if (_legacyText != null)
                {
                    _legacyText.text = format;
                    _legacyText.color = GetTextColor(fps);
                }

                if (_tMProText != null)
                {
                    _tMProText.text = format;
                    _tMProText.color = GetTextColor(fps);
                }

                _timeleft = _updateInterval;
                _accum = 0.0F;
                _frames = 0;
            }
        }

        private Color GetTextColor(float fps)
        {
            Color newColor;

            if (fps < 30)
                newColor = Color.yellow;
            else
                if (fps < 10)
                newColor = Color.red;
            else
                newColor = Color.green;

            return newColor;
        }
    }
}