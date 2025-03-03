using UnityEngine;

namespace SombraStudios.Shared.Optimization.FrameRate
{
    /// <summary>
    /// Limit FPS to optimize performance to the maximum refresh rate of the monitor
    /// This script don't need to and can't be attached to any Game Object
    /// </summary>
    public static class FrameRateLimiter
    {
        private static bool _isActive = true;

        [RuntimeInitializeOnLoadMethod]
        public static void LimitFrameRate()
        {
            if (!_isActive) { return; }

            Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
        }
    }
}
