using System;
using UnityEngine;

namespace SombraStudios.Shared.Utility.Timers.CsharpTimer
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

        protected float initialTime;

        /// <summary>
        /// Gets the progress of the timer as a value between 0 and 1.
        /// </summary>
        public float Progress => Mathf.Clamp(CurrentTime / initialTime, 0, 1);

        /// <summary>
        /// Event triggered when the timer starts.
        /// </summary>
        public Action OnTimerStart = delegate { };

        /// <summary>
        /// Event triggered when the timer stops.
        /// </summary>
        public Action OnTimerStop = delegate { };

        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class.
        /// </summary>
        /// <param name="value">The initial time value for the timer.</param>
        protected Timer(float value)
        {
            initialTime = value;
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
            CurrentTime = initialTime;
            if (!IsRunning)
            {
                IsRunning = true;
                OnTimerStart.Invoke();
            }
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                OnTimerStop.Invoke();
            }
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
        public virtual void Reset() => CurrentTime = initialTime;

        /// <summary>
        /// Resets the timer to a new initial time.
        /// </summary>
        /// <param name="newTime">The new initial time value for the timer.</param>
        public virtual void Reset(float newTime)
        {
            initialTime = newTime;
            Reset();
        }
    }
}