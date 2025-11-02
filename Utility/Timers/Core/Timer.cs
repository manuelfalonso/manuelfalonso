using System;
using UnityEngine;

namespace SombraStudios.Shared.Utility.Timers.Core
{
    /// <summary>
    /// Abstract base class for a timer.
    /// </summary>
    public abstract class Timer
    {
        /// <summary>
        /// Gets the current time of the timer.
        /// </summary>
        public float CurrentTime { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether the timer is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        protected float _initialTime;

        /// <summary>
        /// Gets the progress of the timer as a value between 0 and 1.
        /// </summary>
        public float Progress => _initialTime > 0f ? Mathf.Clamp(CurrentTime / _initialTime, 0, 1) : 0f;

        /// <summary>
        /// Event triggered when the timer starts.
        /// </summary>
        public Action OnTimerStart;

        /// <summary>
        /// Event triggered when the timer stops.
        /// </summary>
        public Action OnTimerStop;

        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class.
        /// </summary>
        /// <param name="value">The initial time value for the timer.</param>
        protected Timer(float value)
        {
            _initialTime = value;
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
            CurrentTime = _initialTime;
            if (IsRunning) return;

            IsRunning = true;
            OnTimerStart?.Invoke();
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Stop()
        {
            if (!IsRunning) return;

            IsRunning = false;
            OnTimerStop?.Invoke();
        }

        /// <summary>
        /// Abstract method to be implemented by derived classes to update the timer.
        /// </summary>
        public abstract void Tick();

        /// <summary>
        /// Gets a value indicating whether the timer has finished.
        /// </summary>
        public abstract bool IsFinished { get; }

        /// <summary>
        /// Resumes the timer.
        /// </summary>
        public void Resume() => IsRunning = true;

        /// <summary>
        /// Pauses the timer.
        /// </summary>
        public void Pause() => IsRunning = false;

        /// <summary>
        /// Resets the timer to its initial time.
        /// </summary>
        public virtual void Reset() => CurrentTime = _initialTime;

        /// <summary>
        /// Resets the timer to a new initial time.
        /// </summary>
        /// <param name="newTime">The new initial time value for the timer.</param>
        public virtual void Reset(float newTime)
        {
            _initialTime = newTime;
            CurrentTime = newTime;
        }
    }
}