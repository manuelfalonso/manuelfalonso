using UnityEngine;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.ScriptableObjects.Conditions
{
    /// <summary>
    /// Represents a condition that evaluates whether the application is running in a development build.
    /// On Editor it will always return true.
    /// </summary>
    /// <remarks>This condition checks the value of <see cref="Debug.isDebugBuild"/> to determine if the
    /// application is running in a development build. If logging is enabled, a message indicating the result of the
    /// evaluation is logged to the console.</remarks>
    [CreateAssetMenu(fileName = "NewDevelopmentBuildCondition", menuName = "Sombra Studios/Conditions/Development Build Condition")]
    public class DevelopmentBuildConditionSO : ConditionSO
    {
        public override bool IsValid()
        {
            bool isDevelopmentBuild = Debug.isDebugBuild;
            if (_showLogs)
            {
                Logger.Log(LOG_CATEGORY, $"Condition met: {isDevelopmentBuild}", this);
            }
            return isDevelopmentBuild;
        }
    }
}