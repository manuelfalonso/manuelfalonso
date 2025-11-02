using UnityEngine;

namespace SombraStudios.Shared.Utility.Timers.Core
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
        public CountdownTimer(float value) : base(value)
        {
            if (value < 0f)
            {
                throw new System.ArgumentOutOfRangeException(nameof(value),
                    "Countdown timer value must be greater than or equal to 0.");
            }
        }

        /// <summary>
        /// Updates the timer by subtracting the delta time from the current time.
        /// </summary>
        public override void Tick()
        {
            if (!IsRunning) return;

            CurrentTime -= Time.deltaTime;

            if (CurrentTime <= 0f)
            {
                CurrentTime = 0f;
                Stop();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the timer has finished.
        /// </summary>
        public override bool IsFinished => CurrentTime <= 0f;

        /// <summary>
        /// Gets the remaining time as a percentage of the initial time.
        /// </summary>
        /// <value>
        /// A value between 0 and 1, where 1 represents full time remaining and 0 represents no time remaining.
        /// </value>
        public float RemainingTimePercentage => _initialTime > 0f ? Mathf.Clamp01(CurrentTime / _initialTime) : 0f;

        /// <summary>
        /// Gets the elapsed time since the countdown started.
        /// </summary>
        /// <value>
        /// The amount of time that has passed since the countdown began.
        /// </value>
        public float ElapsedTime => _initialTime - CurrentTime;

        /// <summary>
        /// Gets the elapsed time as a percentage of the initial time.
        /// </summary>
        /// <value>
        /// A value between 0 and 1, where 0 represents no time elapsed and 1 represents all time elapsed.
        /// </value>
        public float ElapsedTimePercentage => _initialTime > 0f ? Mathf.Clamp01(ElapsedTime / _initialTime) : 1f;
    }
}