using SombraStudios.Shared.Utility.Timers.Core;
using UnityEngine;

namespace SombraStudios.Shared.Utility.Timers.Unity
{
    public class MonoBehaviourCountdownTimer : MonoBehaviourTimer<CountdownTimer>
    {
        [SerializeField] private float _initialTime = 5f;

        public float RemainingTimePercentage => _timer?.RemainingTimePercentage ?? 0f;
        public float ElapsedTime => _timer?.ElapsedTime ?? 0f;
        public float ElapsedTimePercentage => _timer?.RemainingTimePercentage ?? 0f;

        protected override string StartTimerLog => $"{GetType().Name} started with initial time: {_initialTime} seconds.";
        protected override string ResetTimerLog => $"{GetType().Name} reset with initial time: {_initialTime} seconds.";


        protected override void InitializeTimer()
        {
            _timer = new CountdownTimer(_initialTime);
        }
    }
}
