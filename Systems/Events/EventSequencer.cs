using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Events
{
    /// <summary>
    /// Manages the execution of a sequence of event steps, allowing for starting, stopping, pausing, resetting,
    /// and skipping steps or actions. Integrates with the Unity lifecycle and supports editor configuration.
    /// </summary>
    public class EventSequencer : MonoBehaviour
    {
        internal const string LOG_CATEGORY = "[EventSequencer]";
        internal const string SETTINGS_TITLE = "Settings";
        internal const string DEBUG_TITLE = "Debug";

        [Header(SETTINGS_TITLE)]
        /// <summary>
        /// List of event steps to execute in sequence.
        /// </summary>
        [Tooltip("List of event steps to execute in sequence. Steps will be executed in the order they are added.")]
        [SerializeField] private List<EventsStepSO> _eventsSteps = new();
        /// <summary>
        /// If true, the sequence will start automatically on Start().
        /// </summary>
        [Tooltip("If true, the sequence will start automatically on Start()." +
            " If false, you need to call StartSequence() manually to start the sequence.")]
        [SerializeField] private bool _autoStart = true;
        /// <summary>
        /// Wait time in seconds before starting the sequence.
        /// </summary>
        [Tooltip("Wait time in seconds before starting the sequence.")]
        [SerializeField] private float _startWaitTime = 0f;

        [Header(DEBUG_TITLE)]
        /// <summary>
        /// Enables debug logging for the sequencer.
        /// </summary>
        [Tooltip("Enables debug logging for the sequencer. " +
            "Logs will be printed to the console when enabled, useful for debugging and tracking sequence execution.")]
        [SerializeField] private bool _showLogs = false;

        /// <summary>
        /// Gets the index of the current step.
        /// </summary>
        public int CurrentStepIndex() => _currentStepIndex;
        /// <summary>
        /// Returns true if the sequence is currently running.
        /// </summary>
        public bool IsRunning() => _isRunning;
        /// <summary>
        /// Returns true if the sequence is currently paused.
        /// </summary>
        public bool IsPaused() => _isPaused;
        /// <summary>
        /// Returns true if the sequence has completed all steps.
        /// </summary>
        public bool IsCompleted() => _isCompleted;

        private int _currentStepIndex = 0;
        private bool _isRunning = false;
        private bool _isPaused = false;
        private bool _isCompleted = false;

        #region Unity Messages
        /// <summary>
        /// Initializes the sequencer and all steps on Awake.
        /// </summary>
        private void Awake()
        {
            Initialize();
        }

        /// <summary>
        /// Starts the sequence automatically if _autoStart is enabled.
        /// </summary>
        private void Start()
        {
            if (_autoStart)
                StartSequence();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Starts the event sequence if not already running.
        /// </summary>
        public void StartSequence()
        {
            if (_isRunning)
            {
                Log($"{LOG_CATEGORY} Sequence is already running.");
                return;
            }

            StartCoroutine(InternalStartSequence(_startWaitTime));
        }

        /// <summary>
        /// Stops the event sequence and the current step.
        /// </summary>
        public void StopSequence()
        {
            if (!_isRunning)
            {
                Log($"{LOG_CATEGORY} Sequence is not running, cannot stop.");
                return;
            }

            Log($"{LOG_CATEGORY} Stopped.");
            StopAllCoroutines();
            _isRunning = false;

            if (_currentStepIndex < _eventsSteps.Count && _eventsSteps[_currentStepIndex] != null)
                _eventsSteps[_currentStepIndex].StopStep();
            else
                LogError($"{LOG_CATEGORY} Current step index {_currentStepIndex} is out of bounds or step is null.");
        }

        /// <summary>
        /// Pauses or resumes the event sequence and the current step.
        /// </summary>
        /// <param name="pause">True to pause, false to resume.</param>
        public void PauseSequence(bool pause)
        {
            if (pause && !_isRunning)
            {
                Log($"{LOG_CATEGORY} Sequence is not running, cannot pause.");
                return;
            }
            else if (!pause && !_isPaused)
            {
                Log($"{LOG_CATEGORY} Sequence is not paused, cannot resume.");
                return;
            }

            if (pause)
            {
                _isPaused = true;
                Log($"{LOG_CATEGORY} Paused.");
                StopSequence();
            }
            else
            {
                _isPaused = false;
                Log($"{LOG_CATEGORY} Resumed.");
                StartCoroutine(InternalStartSequence(0f));
            }

            if (_currentStepIndex < _eventsSteps.Count && _eventsSteps[_currentStepIndex] != null)
                _eventsSteps[_currentStepIndex].PauseStep(pause);
            else
                LogError($"{LOG_CATEGORY} Current step index {_currentStepIndex} is out of bounds or step is null.");
        }

        /// <summary>
        /// Resets the sequence and all steps to their initial state.
        /// </summary>
        public void ResetSequence()
        {
            if (_isRunning)
            {
                Log($"{LOG_CATEGORY} Sequence reset.");
                StopAllCoroutines();
            }

            foreach (var step in _eventsSteps)
            {
                if (step == null)
                {
                    LogError($"{LOG_CATEGORY} Step is null, cannot reset.");
                    continue;
                }
                step.ResetStep();
            }

            _currentStepIndex = 0;
            _isRunning = false;
            _isCompleted = false;
        }

        /// <summary>
        /// Skips the current step if possible and advances to the next step.
        /// </summary>
        public void SkipStep()
        {
            if (_currentStepIndex >= _eventsSteps.Count)
            {
                Log($"{LOG_CATEGORY} No more steps to skip.");
                return;
            }

            var step = _eventsSteps[_currentStepIndex];
            if (step == null)
            {
                Log($"{LOG_CATEGORY} Step {_currentStepIndex + 1}/{_eventsSteps.Count} is null, skipping.");
                _currentStepIndex++;
                return;
            }

            if (!step.IsSkippable)
            {
                Log($"{LOG_CATEGORY} Step {_currentStepIndex + 1}/{_eventsSteps.Count} is not skippable.");
            }

            Log($"{LOG_CATEGORY} Skipping step {_currentStepIndex + 1}/{_eventsSteps.Count}.");
            step.StopStep();
            StopSequence();
            _currentStepIndex++;
            if (_currentStepIndex >= _eventsSteps.Count)
            {
                Log($"{LOG_CATEGORY} All steps completed, sequence finished.");
                return;
            }
            else
            {
                // Restart the sequence to continue with the next step
                StartCoroutine(InternalStartSequence(0f));
            }
        }

        /// <summary>
        /// Attempts to skip the current action in the current step.
        /// </summary>
        public void SkipAction()
        {
            if (_eventsSteps == null || _eventsSteps.Count == 0)
            {
                LogError($"{LOG_CATEGORY} No events steps found, cannot skip action.");
                return;
            }

            var step = _eventsSteps[_currentStepIndex];
            if (step == null)
            {
                LogError($"{LOG_CATEGORY} Step {_currentStepIndex + 1}/{_eventsSteps.Count} is null, cannot skip action.");
                return;
            }

            if (step.TrySkipAction())
            {
                Log($"{LOG_CATEGORY} Skipping action in step {_currentStepIndex + 1}/{_eventsSteps.Count}.");
                // Restart the sequence to continue with the next action
                StopSequence();
                StartCoroutine(InternalStartSequence(0f));
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes all steps in the sequence.
        /// </summary>
        private void Initialize()
        {
            foreach (var step in _eventsSteps)
            {
                if (step == null)
                {
                    LogError($"{LOG_CATEGORY} Found a null step in the events steps list, please check your configuration.");
                    continue;
                }
                step.Initialize();
            }
        }

        /// <summary>
        /// Coroutine that manages the execution of the event sequence, including delays and step transitions.
        /// </summary>
        /// <param name="delayTime">Delay in seconds before starting the sequence.</param>
        /// <returns>IEnumerator for coroutine execution.</returns>
        private IEnumerator InternalStartSequence(float delayTime)
        {
            if (_eventsSteps == null || _eventsSteps.Count == 0)
            {
                LogError($"{LOG_CATEGORY} No events steps found.");
                yield break;
            }

            if (_isCompleted)
            {
                Log($"{LOG_CATEGORY} Sequence is already completed, reset it before starting it again.");
                yield break;
            }

            Log($"{LOG_CATEGORY} Started");

            _isRunning = true;

            if (delayTime > 0f)
                yield return new WaitForSeconds(delayTime);

            int stepsCount = _eventsSteps.Count;
            for (; _currentStepIndex < stepsCount; _currentStepIndex++)
            {
                var step = _eventsSteps[_currentStepIndex];
                if (step == null)
                {
                    LogError($"{LOG_CATEGORY} Step {_currentStepIndex + 1}/{stepsCount} is null, skipping.");
                    continue;
                }

                if (step.IsCompleted)
                {
                    Log($"{LOG_CATEGORY} Step {_currentStepIndex + 1}/{stepsCount} is already completed, skipping.");
                    continue;
                }

                if (step.IsOptional)
                {
                    if (step.Condition == null)
                    {
                        LogError($"{LOG_CATEGORY} Step {_currentStepIndex + 1}/{stepsCount} is optional but has no condition set, skipping step.");
                        continue;
                    }

                    if (step.Condition.IsValid())
                    {
                        Log($"{LOG_CATEGORY} Step {_currentStepIndex + 1}/{stepsCount} is optional and condition is valid, starting step.");
                    }
                    else
                    {
                        Log($"{LOG_CATEGORY} Step {_currentStepIndex + 1}/{stepsCount} is optional but condition is not valid, skipping step.");
                        continue;
                    }
                }

                if (step.CanRunInParallel)
                {
                    StartCoroutine(step.StartStep());
                }
                else
                {
                    yield return step.StartStep();
                }

                if (!step.AutoAdvanceToNextStep)
                {
                    Log($"{LOG_CATEGORY} Step {_currentStepIndex + 1}/{stepsCount} is not set to auto-advance.");
                    _currentStepIndex++;
                    _isRunning = false;
                    yield break;
                }
            }

            _isRunning = false;
            _isCompleted = true;

            Log($"{LOG_CATEGORY} Finished");
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
    }
}
