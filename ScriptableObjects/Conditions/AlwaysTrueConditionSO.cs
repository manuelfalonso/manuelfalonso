using UnityEngine;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.ScriptableObjects.Conditions
{
    /// <summary>
    /// Represents a condition that is always evaluated as <see langword="true"/>.
    /// </summary>
    /// <remarks>This condition is useful in scenarios where a condition is required but should always
    /// succeed. If logging is enabled, a debug message will be logged whenever the condition is evaluated.</remarks>
    [CreateAssetMenu(fileName = "NewAlwaysTrueCondition", menuName = "Sombra Studios/Conditions/Always True Condition")]
    public class AlwaysTrueConditionSO : ConditionSO
    {
        public override bool IsValid()
        {
            if (_showLogs)
            {
                Logger.Log(LOG_CATEGORY, $"Condition always met.", this);
            }
            return true;
        }
    }
}