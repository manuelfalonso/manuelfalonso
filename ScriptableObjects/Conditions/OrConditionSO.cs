using UnityEngine;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.ScriptableObjects.Conditions
{
    /// <summary>
    /// Represents a condition that evaluates to <see langword="true"/> if any of the specified sub-conditions are
    /// valid.
    /// </summary>
    /// <remarks>This condition checks a list of sub-conditions and returns <see langword="true"/> as soon as
    /// one of them evaluates to <see langword="true"/>. If the list of sub-conditions is empty, or if none of the
    /// sub-conditions are valid, this condition will return <see langword="false"/>. A warning is logged if the
    /// condition list contains a reference to itself, and such references are ignored during evaluation.</remarks>
    [CreateAssetMenu(fileName = "NewOrCondition", menuName = "Sombra Studios/Conditions/Or Condition")]
    public class OrConditionSO : ConditionSO
    {
        [Header(PROPERTIES_TITLE)]
        [Tooltip("List of conditions to be checked.")]
        [SerializeField] private ConditionSO[] _conditions;

        public override bool IsValid()
        {
            foreach (var condition in _conditions)
            {
                if (condition == this)
                {
                    Logger.LogWarning(LOG_CATEGORY, "Condition cannot contain itself!", this);
                    continue;
                }
                if (condition.IsValid())
                    return true;
            }
            return false;
        }
    }
}