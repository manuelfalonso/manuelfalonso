using System;
using UnityEngine;

namespace SombraStudios.Shared.Utility.Timers.CsharpTimer
{
    /// <summary>
    /// Timer that ticks at a specific frequency. (N times per second)
    /// </summary>
    public class FrequencyTimer : Timer
    {
        /// <summary>
        /// Gets the current frequency as ticks per second.
        /// </summary>
        /// <value>The number of times the timer will tick per second.</value>
        public float Frequency { get; private set; }

        /// <summary>
        /// Gets the time interval between ticks in seconds.
        /// </summary>
        /// <value>The duration in seconds between each tick.</value>
        public float TickInterval => _timeThreshold;

        /// <summary>
        /// Event triggered on each tick.
        /// </summary>
        public Action OnTick;

        private float _timeThreshold;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrequencyTimer"/> class.
        /// </summary>
        /// <param name="ticksPerSecond">The number of ticks per second.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when ticksPerSecond is less than or equal to 0.
        /// </exception>
        public FrequencyTimer(int ticksPerSecond) : base(0f)
        {
            if (ticksPerSecond <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ticksPerSecond),
                    "Ticks per second must be greater than 0.");
            }
            CalculateTimeThreshold(ticksPerSecond);
        }

        /// <summary>
        /// Updates the timer and triggers the tick event if the time threshold is reached.
        /// </summary>
        public override void Tick()
        {
            if (!IsRunning) return;

            CurrentTime += Time.deltaTime;

            if (CurrentTime >= _timeThreshold)
            {
                CurrentTime -= _timeThreshold;
                OnTick?.Invoke();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the timer has finished.
        /// A frequency timer never finishes as it runs continuously when started.
        /// </summary>
        /// <value>Always returns <c>false</c> since frequency timers run indefinitely.</value>
        public override bool IsFinished => false;

        /// <summary>
        /// Resets the timer to zero.
        /// </summary>
        public override void Reset()
        {
            CurrentTime = 0f;
        }

        /// <summary>
        /// Resets the timer to zero and sets a new tick frequency.
        /// </summary>
        /// <param name="newTicksPerSecond">The new number of ticks per second.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when newTicksPerSecond is less than or equal to 0.
        /// </exception>
        public void Reset(int newTicksPerSecond)
        {
            if (newTicksPerSecond <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newTicksPerSecond),
                    "Ticks per second must be greater than 0.");
            }
            CalculateTimeThreshold(newTicksPerSecond);
            Reset();
        }

        /// <summary>
        /// Calculates the time threshold based on the number of ticks per second.
        /// </summary>
        /// <param name="ticksPerSecond">The number of ticks per second.</param>
        private void CalculateTimeThreshold(int ticksPerSecond)
        {
            Frequency = ticksPerSecond;
            _timeThreshold = 1f / Frequency;
        }
    }
}