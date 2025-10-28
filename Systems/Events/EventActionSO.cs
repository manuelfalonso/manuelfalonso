using SombraStudios.Shared.ScriptableObjects.Conditions;
using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Events
{
    /// <summary>
    /// Abstract base class for defining event actions with configurable execution, conditions, and logging.
    /// </summary>
    public abstract class EventActionSO : ScriptableObject, ISerializationCallbackReceiver
    {
        [Header("Base Settings")]
        /// <summary>
        /// Indicates if the action is active to toggle the execution.
        /// </summary>
        [Tooltip("Indicates if the action is active to toggle the execution.")]
        [SerializeField] protected bool _active = true;

        /// <summary>
        /// Indicates if the action can be skipped using a method call.
        /// </summary>
        [Tooltip("Indicates if the action can be skipped using a method call.")]
        [SerializeField] protected bool _isSkippable = true;

        /// <summary>
        /// Indicates if the action is optional using a condition check.
        /// </summary>
        [Tooltip("Indicates if the action is optional using a condition check.")]
        [SerializeField] private bool _isOptional = false;

        /// <summary>
        /// Optional condition that determines if the action should execute.
        /// </summary>
        [Tooltip("Optional condition that determines if the action should execute.")]
        [SerializeField] private ConditionSO _condition;

        /// <summary>
        /// Indicates if the action can run in parallel with other actions.
        /// </summary>
        [Tooltip("Indicates if the action can run in parallel with other actions.")]
        [SerializeField] private bool _canRunInParallel = false;

        /// <summary>
        /// Indicates if the sequencer should automatically advance to the next action after completion.
        /// </summary>
        [Tooltip("Indicates if the sequencer should automatically advance to the next action after completion.")]
        [SerializeField] private bool _autoAdvanceToNextAction = true;

        [Header(EventSequencer.DEBUG_TITLE)]
        /// <summary>
        /// Shows debug logs if enabled.
        /// </summary>
        [Tooltip("Shows debug logs if enabled.")]
        [SerializeField] protected bool _showLogs = false;

        /// <summary>
        /// Indicates if the action has been completed.
        /// </summary>
        public bool IsCompleted { get; protected set; }

        /// <summary>
        /// Indicates if the action can be skipped.
        /// </summary>
        public bool IsSkippable => _isSkippable;

        /// <summary>
        /// Indicates if the action is optional.
        /// </summary>
        public bool IsOptional => _isOptional;

        /// <summary>
        /// Gets the condition that determines if the action should execute.
        /// </summary>
        public ConditionSO Condition => _condition;

        /// <summary>
        /// Indicates if the action can run in parallel with other actions.
        /// </summary>
        public bool CanRunInParallel => _canRunInParallel;

        /// <summary>
        /// Indicates if the sequencer should automatically advance to the next action after completion.
        /// </summary>
        public bool AutoAdvanceToNextAction => _autoAdvanceToNextAction;

        /// <summary>
        /// Starts the event action.
        /// </summary>
        /// <returns>IEnumerator for action execution.</returns>
        public abstract IEnumerator StartAction();

        /// <summary>
        /// Pauses or resumes the event action.
        /// </summary>
        /// <param name="pause">True to pause, false to resume.</param>
        public abstract void PauseAction(bool pause);

        /// <summary>
        /// Resets the action state.
        /// </summary>
        public virtual void ResetAction()
        {
            IsCompleted = false;
        }

        /// <summary>
        /// Marks the action as completed.
        /// </summary>
        public virtual void CompleteAction()
        {
            IsCompleted = true;
        }

        /// <summary>
        /// Initializes the internal state of the action.
        /// </summary>
        internal virtual void Initialize()
        {
            IsCompleted = false;
        }

        #region Logging
        /// <summary>
        /// Logs a message if logging is enabled.
        /// </summary>
        /// <param name="message">Message to log.</param>
        protected void Log(string message)
        {
            if (_showLogs)
                Utility.Loggers.Logger.Log(message, this);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">Error message to log.</param>
        protected void LogError(string message)
        {
            Utility.Loggers.Logger.LogError(message, this);
        }
        #endregion

        #region ISerializationCallbackReceiver
        /// <summary>
        /// Called after deserialization to restore initial values.
        /// </summary>
        public virtual void OnAfterDeserialize()
        {
            Initialize();
        }

        /// <summary>
        /// Called before serialization.
        /// </summary>
        public virtual void OnBeforeSerialize() { }
        #endregion
    }
}
