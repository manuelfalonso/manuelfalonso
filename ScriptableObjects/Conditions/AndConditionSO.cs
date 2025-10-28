using UnityEngine;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.ScriptableObjects.Conditions
{
    /// <summary>
    /// Represents a composite condition that evaluates to <see langword="true"/> if all contained conditions are valid.
    /// </summary>
    /// <remarks>This condition checks a list of other conditions and returns <see langword="true"/> only if
    /// all of them are valid. If the list contains a reference to itself, a warning is logged, and the self-reference
    /// is ignored during evaluation.</remarks>
    [CreateAssetMenu(fileName = "NewAndCondition", menuName = "Sombra Studios/Conditions/And Condition")]
    public class AndConditionSO : ConditionSO
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
                if (!condition.IsValid())
                    return false;
            }
            return true;
        }
    }
}