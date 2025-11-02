using SombraStudios.Shared.Interfaces;
using SombraStudios.Shared.Utility.Timers.Core;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.Timers.Unity
{
    public abstract class MonoBehaviourTimer<T> : MonoBehaviour, IInitializable where T : Timer
    {
        [Header("Base Events")]
        public UnityEvent OnTimerStart = new();
        public UnityEvent OnTimerStop = new();

        [Header("Debug")]
        [SerializeField] private bool _showLogs;

        public bool IsInitialized => _isInitialized;
        public bool IsRunning => _timer?.IsRunning ?? false;
        public bool IsFinished => _timer?.IsFinished ?? false;
        public float CurrentTime => _timer?.CurrentTime ?? 0f;
        public float Progress => _timer?.Progress ?? 0f;

        protected virtual string StartTimerLog => $"{GetType().Name} started.";
        protected virtual string StopTimerLog => $"{GetType().Name} stopped at current time: {CurrentTime} seconds.";
        protected virtual string ResumeTimerLog => $"{GetType().Name} resumed at current time: {CurrentTime} seconds.";
        protected virtual string PauseTimerLog => $"{GetType().Name} paused at current time: {CurrentTime} seconds.";
        protected virtual string ResetTimerLog => $"{GetType().Name} reseted.";
        protected virtual string ResetTimerWithNewTimeLog(float newTime) => $"{GetType().Name} reset to new initial time: {newTime} seconds.";

        protected T _timer;

        private bool _isInitialized = false;
        private event Action _onInitialized;

        #region Unity Messages
        protected virtual void Awake()
        {
            Initialize();
        }

        protected virtual void OnEnable()
        {
            UnsubscribeListeners();
            SubscribeListeners();
        }

        protected virtual void Update()
        {
            _timer?.Tick();
        }

        protected virtual void OnDisable()
        {
            UnsubscribeListeners();
        }
        #endregion

        #region Public Methods
        public void WhenInitialized(Action action)
        {
            if (IsInitialized)
            {
                action?.Invoke();
            }
            else
            {
                _onInitialized += action;                
            }
        }

        public void StartTimer()
        {
            _timer.Start();
            DebugLog(StartTimerLog);
        }

        public void StopTimer()
        {
            _timer.Stop();
            DebugLog(StopTimerLog);
        }

        public void ResumeTimer()
        {
            _timer.Resume();
            DebugLog(ResumeTimerLog);
        }

        public void PauseTimer()
        {
            _timer.Pause();
            DebugLog(PauseTimerLog);
        }

        public void ResetTimer()
        {
            _timer.Reset();
            DebugLog(ResetTimerLog);
        }

        public void ResetTimer(float newInitialTime)
        {
            _timer.Reset(newInitialTime);
            DebugLog(ResetTimerWithNewTimeLog(newInitialTime));
        }
        #endregion

        #region Protected Methods
        protected abstract void InitializeTimer();

        protected virtual void SubscribeListeners()
        {
            if (_timer == null) { return; }

            _timer.OnTimerStart += () => OnTimerStart?.Invoke();
            _timer.OnTimerStop += () => OnTimerStop?.Invoke();
        }

        protected virtual void UnsubscribeListeners()
        {
            if (_timer == null) { return; }

            _timer.OnTimerStart -= () => OnTimerStart?.Invoke();
            _timer.OnTimerStop -= () => OnTimerStop?.Invoke();
        }
        #endregion

        #region Private Methods
        private void Initialize()
        {
            if (_isInitialized) return;
            _isInitialized = true;
            InitializeTimer();
            _onInitialized?.Invoke();
            _onInitialized = null;
        }

        private void DebugLog(string message)
        {
            if (_showLogs)
            {
                Debug.Log(message, this);
            }
        }
        #endregion
    }
}
