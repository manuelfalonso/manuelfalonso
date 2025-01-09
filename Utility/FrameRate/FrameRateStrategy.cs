using SombraStudios.Shared.Patterns.Behavioural.Strategy;
using UnityEngine;

namespace SombraStudios.Shared.Utility.FrameRate
{
    /// <summary>
    /// Command to set the target frame rate of the application.
    /// </summary>
    [CreateAssetMenu(fileName = "FrameRateStrategy", menuName = "Sombra Studios/Strategies/Frame Rate Strategy")]
    public class FrameRateStrategy : StrategySO
    {
        [Header(PROPERTIES_TITLE)]
        /// <summary>
        /// The target frame rate to set.
        /// </summary>
        [Tooltip("The target frame rate to set.")]
        public int TargetFrameRate;

        /// <summary>
        /// Determines whether the command can be executed.
        /// </summary>
        /// <returns>True if the command can be executed, otherwise false.</returns>
        public override bool CanExecute()
        {
            if (Application.targetFrameRate == TargetFrameRate)
            {
                return false;
            }

            if (TargetFrameRate < 0)
            {
                return false;
            }

            if (TargetFrameRate > Screen.currentResolution.refreshRateRatio.value)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Executes the command to set the target frame rate.
        /// </summary>
        public override void Execute()
        {
            Application.targetFrameRate = TargetFrameRate;

            if (_showLogs)
            {
                Debug.Log($"Frame rate set to {TargetFrameRate}");
            }
        }
    }
}
