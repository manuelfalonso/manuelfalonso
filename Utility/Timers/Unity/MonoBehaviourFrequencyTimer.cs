using SombraStudios.Shared.Utility.Timers.Core;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.Timers.Unity
{
    public class MonoBehaviourFrequencyTimer : MonoBehaviourTimer<FrequencyTimer>
    {
        [Header("Settings")]
        [SerializeField] private int _ticksPerSecond = 1;

        [Header("Events")]
        public UnityEvent OnTick = new();

        public float Frequency => _timer?.Frequency ?? 0f;
        public float TickInterval => _timer?.TickInterval ?? 0f;

        protected override string StopTimerLog => $"{GetType().Name} stopped.";

        protected override void InitializeTimer()
        {
            _timer = new FrequencyTimer(_ticksPerSecond);
        }

        protected override void SubscribeListeners()
        {
            base.SubscribeListeners();
            if (_timer == null) { return; }
            _timer.OnTick += () => OnTick?.Invoke();
        }

        protected override void UnsubscribeListeners()
        {
            base.UnsubscribeListeners();
            if (_timer == null) { return; }
            _timer.OnTick -= () => OnTick?.Invoke();
        }
    }
}
