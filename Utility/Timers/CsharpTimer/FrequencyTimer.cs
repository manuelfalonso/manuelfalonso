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
        /// Gets the number of ticks per second.
        /// </summary>
        public int TicksPerSecond { get; private set; }

        /// <summary>
        /// Event triggered on each tick.
        /// </summary>
        public Action OnTick = delegate { };

        float timeThreshold;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrequencyTimer"/> class.
        /// </summary>
        /// <param name="ticksPerSecond">The number of ticks per second.</param>
        public FrequencyTimer(int ticksPerSecond) : base(0)
        {
            CalculateTimeThreshold(ticksPerSecond);
        }

        /// <summary>
        /// Updates the timer and triggers the tick event if the time threshold is reached.
        /// </summary>
        public override void Tick()
        {
            if (IsRunning && CurrentTime >= timeThreshold)
            {
                CurrentTime -= timeThreshold;
                OnTick.Invoke();
            }

            if (IsRunning && CurrentTime < timeThreshold)
            {
                CurrentTime += Time.deltaTime;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the timer has finished.
        /// </summary>
        public override bool IsFinished => !IsRunning;

        /// <summary>
        /// Resets the timer to zero.
        /// </summary>
        public override void Reset()
        {
            CurrentTime = 0;
        }

        /// <summary>
        /// Resets the timer to zero and sets a new tick frequency.
        /// </summary>
        /// <param name="newTicksPerSecond">The new number of ticks per second.</param>
        public void Reset(int newTicksPerSecond)
        {
            CalculateTimeThreshold(newTicksPerSecond);
            Reset();
        }

        /// <summary>
        /// Calculates the time threshold based on the number of ticks per second.
        /// </summary>
        /// <param name="ticksPerSecond">The number of ticks per second.</param>
        private void CalculateTimeThreshold(int ticksPerSecond)
        {
            TicksPerSecond = ticksPerSecond;
            timeThreshold = 1f / TicksPerSecond;
        }
    }
}