using UnityEngine;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.ScriptableObjects.Conditions
{
    /// <summary>
    /// Represents a condition that negates the result of another condition.
    /// </summary>
    /// <remarks>This class is used to invert the validity of a specified condition.  If the specified
    /// condition is valid, this condition will be invalid, and vice versa.</remarks>
    [CreateAssetMenu(fileName = "NewNotCondition", menuName = "Sombra Studios/Conditions/Not Condition")]
    public class NotConditionSO : ConditionSO
    {
        [Header(PROPERTIES_TITLE)]
        [Tooltip("The condition to be negated.")]
        [SerializeField] private ConditionSO _condition;

        public override bool IsValid()
        {
            if (_condition == this)
            {
                Logger.LogWarning(LOG_CATEGORY, "Condition cannot contain itself!", this);
                return false;
            }
            return !_condition.IsValid();
        }
    }
}