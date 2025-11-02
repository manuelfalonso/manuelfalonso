using System;
using UnityEngine;

namespace SombraStudios.Shared.Utility.Timers.Core
{
    /// <summary>
    /// Countdown timer that triggers events at specified intervals until completion.
    /// </summary>
    public class IntervalTimer : Timer
    {
        public Action OnInterval;

        private readonly float _interval;
        private float _nextIntervalTime;

        private const string ErrorMessage = "Interval must be greater than 0 and less than or equal to total time.";

        /// <summary>
        /// Initializes a new instance of the <see cref="IntervalTimer"/> class.
        /// </summary>
        /// <param name="totalTime">The total duration of the timer in seconds.</param>
        /// <param name="interval">The interval duration between events in seconds.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when interval is less than or equal to 0, or greater than totalTime.
        /// </exception>
        public IntervalTimer(float totalTime, float interval) : base(totalTime)
        {
            if (interval <= 0f || interval > totalTime)
            {
                throw new ArgumentOutOfRangeException(nameof(interval), ErrorMessage);
            }
            _interval = interval;
            _nextIntervalTime = totalTime - interval;
        }

        /// <summary>
        /// Updates the timer, triggering interval events as needed and checking for completion.
        /// This method should be called every frame to maintain accurate timing.
        /// </summary>
        public override void Tick()
        {
            if (!IsRunning || CurrentTime <= 0f) return;

            CurrentTime -= Time.deltaTime;

            // Check if we've reached the next interval
            while (CurrentTime <= _nextIntervalTime && _nextIntervalTime >= 0f)
            {
                OnInterval?.Invoke();
                _nextIntervalTime -= _interval;
            }

            if (CurrentTime <= 0f)
            {
                CurrentTime = 0f;
                Stop();
            }
        }

        public override bool IsFinished => CurrentTime <= 0f;

        public override void Reset()
        {
            base.Reset();
            _nextIntervalTime = _initialTime - _interval;
        }

        public override void Reset(float newTime)
        {
            if (_interval > newTime)
            {
                throw new ArgumentOutOfRangeException(nameof(newTime), ErrorMessage);
            }
            base.Reset(newTime);
            _nextIntervalTime = newTime - _interval;
        }
    }
}
