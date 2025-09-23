using UnityEngine;

namespace SombraStudios.Shared.Utility.Timers.CsharpTimer
{
    /// <summary>
    /// Timer that counts up from zero to infinity. Great for measuring durations.
    /// </summary>
    public class StopwatchTimer : Timer
    {
        public StopwatchTimer() : base(0f) { }

        /// <summary>
        /// Updates the timer by adding the delta time to the current time.
        /// Only runs when the timer is active.
        /// </summary>
        public override void Tick()
        {
            if (!IsRunning) return;

            CurrentTime += Time.deltaTime;
        }

        public override bool IsFinished => false;

        /// <summary>
        /// Gets the elapsed time since the stopwatch was started.
        /// This is equivalent to CurrentTime for a stopwatch timer.
        /// </summary>
        public float ElapsedTime => CurrentTime;

        /// <summary>
        /// Resets the stopwatch timer back to zero.
        /// </summary>
        public override void Reset()
        {
            CurrentTime = 0f;
        }

        /// <summary>
        /// Resets the stopwatch timer to a specific starting value.
        /// This can be useful for resuming from a previously saved time.
        /// </summary>
        /// <param name="startTime">The starting time value for the stopwatch in seconds.</param>
        public override void Reset(float startTime)
        {
            CurrentTime = startTime;
        }
    }
}