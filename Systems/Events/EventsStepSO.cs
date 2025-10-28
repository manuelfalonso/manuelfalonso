using SombraStudios.Shared.ScriptableObjects.Conditions;
using SombraStudios.Shared.Utility.Coroutines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Events
{
    /// <summary>
    /// ScriptableObject representing a step in an event sequence.
    /// Manages a list of event actions, their execution, and step state.
    /// </summary>
    [CreateAssetMenu(
        fileName = "EventsStepSO",
        menuName = "Sombra Studios/Systems/Events/Events Step",
        order = 0)]
    public class EventsStepSO : ScriptableObject, ISerializationCallbackReceiver
    {
        [Header(EventSequencer.SETTINGS_TITLE)]
        /// <summary>
        /// Description of the step for editor reference.
        /// </summary>
        [Tooltip("Description of the step for editor reference.")]
        [SerializeField, TextArea] private string _description;

        /// <summary>
        /// Indicates if the step is active and should be executed.
        /// </summary>
        [Tooltip("Indicates if the step is active and should be executed.")]
        [SerializeField] private bool _isActive = true;

        /// <summary>
        /// Indicates if the step can be skipped.
        /// </summary>
        [Tooltip("Indicates if the step can be skipped.")]
        [SerializeField] private bool _isSkippable = true;

        /// <summary>
        /// Indicates if the step is optional and can be conditionally executed.
        /// </summary>
        [Tooltip("Indicates if the step is optional and can be conditionally executed.")]
        [SerializeField] private bool _isOptional = false;

        /// <summary>
        /// Optional condition that determines if the step should execute.
        /// </summary>
        [Tooltip("Optional condition that determines if the step should execute.")]
        [SerializeField] private ConditionSO _condition;

        /// <summary>
        /// Indicates if the step can run in parallel with other steps.
        /// </summary>
        [Tooltip("Indicates if the step can run in parallel with other steps.")]
        [SerializeField] private bool _canRunInParallel = false;

        /// <summary>
        /// Indicates if the sequencer should automatically advance to the next step after completion.
        /// </summary>
        [Tooltip("Indicates if the sequencer should automatically advance to the next step after completion.")]
        [SerializeField] private bool _autoAdvanceToNextStep = true;

        /// <summary>
        /// List of actions to execute in this step.
        /// </summary>
        [Tooltip("List of actions to execute in this step.")]
        [SerializeField] private List<EventActionSO> _actions;

        [Header(EventSequencer.DEBUG_TITLE)]
        /// <summary>
        /// Enables debug logging for this step.
        /// </summary>
        [Tooltip("Enables debug logging for this step.")]
        [SerializeField] private bool _showLogs = false;

        /// <summary>
        /// Gets whether the step can be skipped.
        /// </summary>
        public bool IsSkippable => _isSkippable;

        /// <summary>
        /// Gets whether the step is optional.
        /// </summary>
        public bool IsOptional => _isOptional;

        /// <summary>
        /// Gets the condition that determines if the step should execute.
        /// </summary>
        public ConditionSO Condition => _condition;

        /// <summary>
        /// Gets whether the step can run in parallel with other steps.
        /// </summary>
        public bool CanRunInParallel => _canRunInParallel;

        /// <summary>
        /// Gets whether the sequencer should automatically advance to the next step after completion.
        /// </summary>
        public bool AutoAdvanceToNextStep => _autoAdvanceToNextStep;

        /// <summary>
        /// Gets whether the step is currently paused.
        /// </summary>
        public bool IsPaused => _isPaused;

        /// <summary>
        /// Gets whether the step has been completed.
        /// </summary>
        public bool IsCompleted => _isCompleted;

        private int _currentActionIndex = 0;
        private bool _isRunning = false;
        private bool _isPaused = false;
        private bool _isCompleted = false;

        #region Internal Methods
        /// <summary>
        /// Initializes the step and all contained actions.
        /// </summary>
        internal void Initialize()
        {
            _currentActionIndex = 0;
            _isRunning = false;
            _isPaused = false;
            _isCompleted = false;

            if (_actions != null)
            {
                foreach (var action in _actions)
                {
                    if (action != null)
                        action.Initialize();
                    else
                        LogError($"{name}: Action is null during initialization.");
                }
            }
            else
            {
                LogError($"{name}: Actions list is null during initialization.");
            }
        }

        /// <summary>
        /// Starts the execution of the step and its actions.
        /// </summary>
        /// <returns>Coroutine enumerator for step execution.</returns>
        internal IEnumerator StartStep()
        {
            if (!_isActive)
            {
                Log($"{name}: Step is not active, skipping.");
                yield break;
            }

            if (_isCompleted)
            {
                Log($"{name}: Step is already completed, skipping.");
                yield break;
            }

            Log($"{name}: Step Started");
            _isRunning = true;

            int actionsCount = _actions != null ? _actions.Count : 0;
            for (; _currentActionIndex < actionsCount; _currentActionIndex++)
            {
                var action = _actions[_currentActionIndex];
                if (action == null)
                {
                    LogError($"{name}: Action {_currentActionIndex + 1}/{actionsCount} is null, skipping.");
                    continue;
                }

                Log($"{action.name} Started");

                if (action.IsCompleted)
                {
                    Log($"{action.name} is already completed, skipping.");
                    continue;
                }

                if (action.IsOptional)
                {
                    if (action.Condition == null)
                    {
                        LogError($"{action.name} is optional but has no condition defined, skipping action.");
                        continue;
                    }

                    if (action.Condition.IsValid())
                    {
                        Log($"{action.name} is optional and condition is valid, starting action.");
                    }
                    else
                    {
                        Log($"{action.name} is optional but condition is not valid, skipping action.");
                        continue;
                    }
                }

                if (action.CanRunInParallel)
                {
                    CoroutineManager.Instance.StartCoroutine(action.StartAction());
                }
                else
                {
                    yield return action.StartAction();
                }

                if (!action.AutoAdvanceToNextAction)
                {
                    Log($"{action.name} is not set to auto-advance, stopping step execution.");
                    _currentActionIndex++;
                    _isRunning = false;
                    yield break;
                }

                Log($"{action.name} Finished");
            }

            _isRunning = false;
            _isCompleted = true;

            Log($"{name} Step Finished");

            yield return null;
        }

        /// <summary>
        /// Stops the execution of the step.
        /// </summary>
        internal void StopStep()
        {
            if (!_isRunning)
            {
                LogError($"{name}: Cannot stop step, it is not currently running.");
                return;
            }

            Log($"{name}: Step Stopped");

            _isRunning = false;
        }

        /// <summary>
        /// Pauses or resumes the current action in the step.
        /// </summary>
        /// <param name="pause">True to pause, false to resume.</param>
        internal void PauseStep(bool pause)
        {
            Log($"{name}: Step {(pause ? "Paused" : "Resumed")}");

            if (_isRunning && _actions != null && _currentActionIndex < _actions.Count && _actions[_currentActionIndex] != null)
            {
                _actions[_currentActionIndex].PauseAction(pause);
            }
            else
            {
                LogError($"{name}: Cannot pause step, no action is currently running or actions list is null.");
            }
        }

        /// <summary>
        /// Resets the step and all contained actions to their initial state.
        /// </summary>
        internal void ResetStep()
        {
            Log($"{name}: Step Reset");

            _currentActionIndex = 0;
            _isRunning = false;
            _isCompleted = false;

            if (_actions == null)
            {
                LogError($"{name}: Actions list is null, cannot reset actions.");
                return;
            }

            int count = _actions.Count;
            for (int i = 0; i < count; i++)
            {
                if (_actions[i] != null)
                    _actions[i].ResetAction();
                else
                    LogError($"{name}: Action at index {i} is null, cannot reset action.");
            }
        }

        /// <summary>
        /// Attempts to skip the current action if possible.
        /// </summary>
        /// <returns>True if the action was skipped, false otherwise.</returns>
        internal bool TrySkipAction()
        {
            if (_actions == null || _currentActionIndex >= _actions.Count)
            {
                LogError($"{name}: Cannot skip action, either actions list is null or current action index is out of bounds.");
                return false;
            }

            var action = _actions[_currentActionIndex];
            if (action == null)
            {
                LogError($"{name}: Action at index {_currentActionIndex} is null, skipping.");
                _currentActionIndex++;
                return true;
            }

            if (action.IsCompleted)
            {
                Log($"{name}: Action {action.name} is already completed, skipping.");
                _currentActionIndex++;
                return true;
            }

            if (!action.IsSkippable)
            {
                LogError($"{name}: Action {action.name} is not skippable, cannot skip.");
                return false;
            }

            Log($"{name}: Skipping Action {action.name}");

            action.CompleteAction();
            _currentActionIndex++;

            return true;
        }
        #endregion

        #region Logging
        /// <summary>
        /// Logs a message if logging is enabled.
        /// </summary>
        /// <param name="message">The message to log.</param>
        private void Log(string message)
        {
            if (_showLogs)
                Utility.Loggers.Logger.Log(message, this);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        private void LogError(string message)
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
