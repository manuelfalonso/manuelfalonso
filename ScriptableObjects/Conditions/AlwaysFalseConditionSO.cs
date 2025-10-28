using UnityEngine;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.ScriptableObjects.Conditions
{
    /// <summary>
    /// Represents a condition that is always evaluated as <see langword="false"/>.
    /// </summary>
    /// <remarks>This condition is primarily used in scenarios where a condition must explicitly fail. If
    /// logging is enabled, a debug message will be logged whenever the condition is evaluated.</remarks>
    [CreateAssetMenu(fileName = "NewAlwaysFalseCondition", menuName = "Sombra Studios/Conditions/Always False Condition")]
    public class AlwaysFalseConditionSO : ConditionSO
    {
        public override bool IsValid()
        {
            if (_showLogs)
            {
                Logger.Log(LOG_CATEGORY, $"Condition never met.", this);
            }
            return false;
        }
    }
}