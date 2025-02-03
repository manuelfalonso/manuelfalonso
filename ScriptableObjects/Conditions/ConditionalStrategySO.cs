using SombraStudios.Shared.Patterns.Behavioural.Strategy;
using UnityEngine;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.ScriptableObjects.Conditions
{
    /// <summary>
    /// A strategy that executes different actions based on whether a specified condition is met.
    /// </summary>
    /// <remarks>
    /// If the condition is met, the <see cref="ConditionMetStrategy"/> is executed.
    /// If the condition is not met and <see cref="ApplyUnmetConditionEffect"/> is enabled, 
    /// the <see cref="UnmetConditionStrategy"/> is executed instead.
    /// </remarks>
    public abstract class ConditionalStrategySO<T> : StrategySO<T>
    {
        [Header(PROPERTIES_TITLE)]
        /// <summary>
        /// The condition that determines which strategy should be executed.
        /// </summary>
        [Tooltip("The condition that determines which strategy should be executed.")]
        public ConditionSO<T> Condition;
        /// <summary>
        /// The strategy executed when the condition is met.
        /// </summary>
        [Tooltip("The strategy executed when the condition is met.")]
        public StrategySO<T> ConditionMetStrategy;
        /// <summary>
        /// Whether to execute an alternative strategy if the condition is not met.
        /// </summary>
        [Tooltip("Whether to execute an alternative strategy if the condition is not met.")]
        public bool ApplyUnmetConditionEffect;
        /// <summary>
        /// The strategy executed when the condition is not met, provided that 
        /// <see cref="ApplyUnmetConditionEffect"/> is enabled.
        /// </summary>
        [Tooltip("The strategy executed when the condition is not met, provided that " +
            "'Apply Unmet Condition Effect' is enabled.")]
        public StrategySO<T> UnmetConditionStrategy;

        public override bool CanExecute(T data)
        {
            if (Condition == null)
            {
                Logger.LogError(LOG_CATEGORY, $"Condition is null in {name}.", this);
                return false;
            }

            if (ConditionMetStrategy == null)
            {
                Logger.LogError(LOG_CATEGORY, $"ConditionMetStrategy is null in {name}.", this);
                return false;
            }

            if (ApplyUnmetConditionEffect && UnmetConditionStrategy == null)
            {
                Logger.LogError(LOG_CATEGORY, $"UnmetConditionStrategy is null in {name}.", this);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Executes the appropriate strategy based on whether the condition is met.
        /// </summary>
        public override void Execute(T data)
        {
            if (Condition.IsValid(data))
            {
                if (_showLogs)
                {
                    Logger.Log(LOG_CATEGORY, $"Condition met in {name}.", this);
                }
                ConditionMetStrategy.Execute(data);
            }
            else if (ApplyUnmetConditionEffect)
            {
                if (_showLogs)
                {
                    Logger.Log(LOG_CATEGORY, $"Condition not met in {name}.", this);
                }
                UnmetConditionStrategy.Execute(data);
            }
        }
    }

    /// <summary>
    /// A strategy that executes different actions based on whether a specified condition is met.
    /// </summary>
    /// <remarks>
    /// If the condition is met, the <see cref="ConditionMetStrategy"/> is executed.
    /// If the condition is not met and <see cref="ApplyUnmetConditionEffect"/> is enabled, 
    /// the <see cref="UnmetConditionStrategy"/> is executed instead.
    /// </remarks>
    [CreateAssetMenu(fileName = "NewConditionalEffect", menuName = "Sombra Studios/Strategies/Conditional Strategy")]
    public class ConditionalStrategySO : StrategySO
    {
        [Header(PROPERTIES_TITLE)]
        /// <summary>
        /// The condition that determines which strategy should be executed.
        /// </summary>
        [Tooltip("The condition that determines which strategy should be executed.")]
        public ConditionSO Condition;
        /// <summary>
        /// The strategy executed when the condition is met.
        /// </summary>
        [Tooltip("The strategy executed when the condition is met.")]
        public StrategySO ConditionMetStrategy;
        /// <summary>
        /// Whether to execute an alternative strategy if the condition is not met.
        /// </summary>
        [Tooltip("Whether to execute an alternative strategy if the condition is not met.")]
        public bool ApplyUnmetConditionEffect;
        /// <summary>
        /// The strategy executed when the condition is not met, provided that 
        /// <see cref="ApplyUnmetConditionEffect"/> is enabled.
        /// </summary>
        [Tooltip("The strategy executed when the condition is not met, provided that " +
            "'Apply Unmet Condition Effect' is enabled.")]
        public StrategySO UnmetConditionStrategy;

        public override bool CanExecute()
        {
            if (Condition == null)
            {
                Logger.LogError(LOG_CATEGORY, $"Condition is null in {name}.", this);
                return false;
            }

            if (ConditionMetStrategy == null)
            {
                Logger.LogError(LOG_CATEGORY, $"ConditionMetStrategy is null in {name}.", this);
                return false;
            }

            if (ApplyUnmetConditionEffect && UnmetConditionStrategy == null)
            {
                Logger.LogError(LOG_CATEGORY, $"UnmetConditionStrategy is null in {name}.", this);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Executes the appropriate strategy based on whether the condition is met.
        /// </summary>
        public override void Execute()
        {
            if (Condition.IsValid())
            {
                if (_showLogs)
                {
                    Logger.Log(LOG_CATEGORY, $"Condition met in {name}.", this);
                }
                ConditionMetStrategy.Execute();
            }
            else if (ApplyUnmetConditionEffect)
            {
                if (_showLogs)
                {
                    Logger.Log(LOG_CATEGORY, $"Condition not met in {name}.", this);
                }
                UnmetConditionStrategy.Execute();
            }
        }
    }
}
