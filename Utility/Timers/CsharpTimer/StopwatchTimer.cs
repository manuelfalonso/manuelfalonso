using UnityEngine;

namespace SombraStudios.Shared.Utility.Timers.CsharpTimer
{
    /// <summary>
    /// Timer that counts up from zero to infinity. Great for measuring durations.
    /// </summary>
    public class StopwatchTimer : Timer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StopwatchTimer"/> class.
        /// </summary>
        public StopwatchTimer() : base(0) { }

        /// <summary>
        /// Updates the timer by adding the delta time to the current time.
        /// </summary>
        public override void Tick()
        {
            if (IsRunning)
            {
                CurrentTime += Time.deltaTime;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the timer has finished.
        /// </summary>
        public override bool IsFinished => false;
    }
}