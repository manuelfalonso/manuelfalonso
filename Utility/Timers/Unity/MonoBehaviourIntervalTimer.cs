using SombraStudios.Shared.Utility.Timers.Core;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.Timers.Unity
{
    public class MonoBehaviourIntervalTimer : MonoBehaviourTimer<IntervalTimer>
    {
        [SerializeField] private float _initialTime = 5f;

        [Header("Settings")]
        [SerializeField] private float _interval = 1f;

        [Header("Events")]
        public UnityEvent OnInterval = new();

        protected override string StartTimerLog => $"{GetType().Name} started with initial time: {_initialTime} seconds and interval: {_interval} seconds.";
        protected override string StopTimerLog => $"{GetType().Name} stopped at current time: {CurrentTime} seconds.";
        protected override string ResetTimerLog => $"{GetType().Name} reset to initial time: {_initialTime} seconds and interval: {_interval} seconds.";
        
        protected override void InitializeTimer()
        {
            _timer = new IntervalTimer(_initialTime, _interval);
        }

        protected override void SubscribeListeners()
        {
            base.SubscribeListeners();
            if (_timer == null) { return; }
            _timer.OnInterval += () => OnInterval?.Invoke();
        }

        protected override void UnsubscribeListeners()
        {
            base.UnsubscribeListeners();
            if (_timer == null) { return; }
            _timer.OnInterval -= () => OnInterval?.Invoke();
        }
    }
}
