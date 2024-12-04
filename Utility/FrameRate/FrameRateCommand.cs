using SombraStudios.Shared.Patterns.Behavioural.Command;
using UnityEngine;

namespace SombraStudios.Shared.Utility.FrameRate
{
    /// <summary>
    /// Command to set the target frame rate of the application.
    /// </summary>
    [CreateAssetMenu(fileName = "FrameRateCommand", menuName = "Sombra Studios/Commands/Frame Rate Command")]
    public class FrameRateCommand : CommandSO
    {
        /// <summary>
        /// The target frame rate to set.
        /// </summary>
        [Tooltip("The target frame rate to set.")]
        public int TargetFrameRate;

        /// <summary>
        /// Whether to show logs when the frame rate is set.
        /// </summary>
        [Tooltip("Whether to show logs when the frame rate is set.")]
        public bool ShowLogs;

        /// <summary>
        /// Determines whether the command can be executed.
        /// </summary>
        /// <returns>True if the command can be executed, otherwise false.</returns>
        protected override bool CanExecute()
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
        protected override void Execute()
        {
            Application.targetFrameRate = TargetFrameRate;

            if (ShowLogs)
            {
                Debug.Log($"Frame rate set to {TargetFrameRate}");
            }
        }
    }
}
