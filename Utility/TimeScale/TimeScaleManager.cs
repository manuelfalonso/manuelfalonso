using SombraStudios.Shared.Patterns.Creational.Singleton;
using System;
using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.Utility.TimeScale
{
    public class TimeScaleManager : Singleton<TimeScaleManager>
    {
        [Header("Debug")]
        [SerializeField] private bool _showLogs = false;

        public int UpdatesPerSecond { get; set; } = 10;
        public bool IsTimeScaleChanging { get; private set; }
        /// <summary>
        /// Is the behaviour of this script running
        /// </summary>
        public bool IsActive { get; set; } = true;
        public bool IsPaused => Time.timeScale == 0f;

        public event Action<float> TimeScaleChanged;
        public event Action Paused;
        public event Action Resumed;

        /// <summary>
        /// Set TimeScale to 0
        /// </summary>
        public void Pause()
        {
            if (!IsActive) { return; }

            Time.timeScale = 0f;

            Paused?.Invoke();
        }

        /// <summary>
        /// Set TimeScale to 1
        /// </summary>
        public void Resume()
        {
            if (!IsActive) { return; }

            Time.timeScale = 1f;

            Resumed?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeScaleData">TimeScale changed succesfully</param>
        /// <returns></returns>
        public bool SetTimeScale(AnimationCurveValue timeScaleData)
        {
            var timeScaleSetted = false;

            if (!IsActive) { return timeScaleSetted; }

            if (IsTimeScaleChanging) { return timeScaleSetted; }

            IsTimeScaleChanging = true;

            StartCoroutine(RunTimeScale(timeScaleData));

            timeScaleSetted = true;

            return timeScaleSetted;
        }


        private IEnumerator RunTimeScale(AnimationCurveValue data)
        {
            //float progression = 0f;
            float firstKeyTime = data.Curve.keys[0].time;
            float lastKetTime = data.Curve.keys[^1].time;

            float progression = firstKeyTime;
            var progressionRate = 1f / UpdatesPerSecond;
            // Use realtime as this script is also afected by TimeScale
            var waitTime = new WaitForSecondsRealtime(progressionRate);

            while (progression <= lastKetTime)
            {
                UpdateTimeScale(data, progression);
                progression += progressionRate;
                yield return waitTime;
            }

            UpdateTimeScale(data, lastKetTime);

            IsTimeScaleChanging = false;
            TimeScaleChanged?.Invoke(Time.timeScale);
        }

        private void UpdateTimeScale(AnimationCurveValue data, float progression)
        {
            var newValue = data.GetValueExact(progression);
            if (newValue <= 0)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = newValue;
            }
            Utility.Logger.Log(_showLogs, $"New Time Scale setted: {Time.timeScale}", this);
        }
    }
}
