using UnityEngine;

namespace SombraStudios.Shared.Utility.Timers.CsharpTimer
{
    /// <summary>
    /// Timer that counts down from a specific value to zero.
    /// </summary>
    public class CountdownTimer : Timer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountdownTimer"/> class.
        /// </summary>
        /// <param name="value">The initial time value for the countdown.</param>
        public CountdownTimer(float value) : base(value) { }

        /// <summary>
        /// Updates the timer by subtracting the delta time from the current time.
        /// </summary>
        public override void Tick()
        {
            if (IsRunning && CurrentTime > 0)
            {
                CurrentTime -= Time.deltaTime;
            }

            if (IsRunning && CurrentTime <= 0)
            {
                Stop();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the timer has finished.
        /// </summary>
        public override bool IsFinished => CurrentTime <= 0;
    }
}