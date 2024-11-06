using SombraStudios.Shared.Attributes;
using SombraStudios.Shared.Utility.Coroutines;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.Cooldown
{
    /// <summary>
    /// It can be used as a waiting period between using specific actions in a game/app, 
    /// ensuring they can't be spammed and adding strategic depth to gameplay.
    /// </summary>
    public class Cooldown : MonoBehaviour, ICooldown
    {
        /// <summary>
        /// The duration of the cooldown in seconds.
        /// </summary>
        [Tooltip("The duration of the cooldown in seconds.")]
        [SerializeField] protected float _time = 0f;
        /// <summary>
        /// If true, the cooldown will still run if the object gets disabled.
        /// </summary>
        [Tooltip("If true, the cooldown will still run if the object gets disabled")]
        [SerializeField] private bool _isGameObjectDecoupled = false;

        [Header("Debug")]
        /// <summary>
        /// Indicates whether the cooldown is currently active.
        /// </summary>
        [Tooltip("Indicates whether the cooldown is currently active.")]
        [SerializeField] private bool _isActive = true;
        /// <summary>
        /// Indicates whether the cooldown is currently in progress.
        /// </summary>
        [Tooltip("Indicates whether the cooldown is currently in progress.")]
        [SerializeField, ReadOnly] private bool _isInCooldown = false;

        public float Time { get => _time; private set => _time = value; }
        public bool IsActive { get => _isActive; set => _isActive = value; }
        public bool IsInCooldown { get => _isInCooldown; private set => _isInCooldown = value; }

        /// <summary>
        /// The WaitForSeconds instance used in the cooldown routine.
        /// </summary>
        protected WaitForSeconds _waitForSeconds;
        /// <summary>
        /// The CoroutineRunner instance used to manage coroutines for the cooldown.
        /// </summary>
        protected CoroutineRunner _coroutineRunner;

        /// <summary>
        /// UnityEvent triggered when the cooldown starts.
        /// </summary>        
        [Tooltip("UnityEvent triggered when the cooldown starts.")]
        public UnityEvent CooldownStarted = new UnityEvent();
        /// <summary>
        /// UnityEvent triggered when the cooldown is manually stopped.
        /// </summary>
        [Tooltip("UnityEvent triggered when the cooldown is manually stopped.")]
        public UnityEvent CooldownStopped = new UnityEvent();
        /// <summary>
        /// UnityEvent triggered when the cooldown finishes.
        /// </summary>
        [Tooltip("UnityEvent triggered when the cooldown finishes.")]
        public UnityEvent CooldownFinished = new UnityEvent();


        private void Start()
        {
            _waitForSeconds = new WaitForSeconds(_time);
            _coroutineRunner = new CoroutineRunner(this, _isGameObjectDecoupled);
        }


        public void StartCooldown()
        {
            if (!_isActive) { return; }
            if (_isInCooldown) { return; }
            _coroutineRunner.StartCoroutine(CooldownRoutine());
            _isInCooldown = true;
        }

        public void StopCooldown()
        {
            if (!_isActive) { return; }
            if (!_isInCooldown) { return; }
            if (_coroutineRunner.TryStopCoroutine()) { CooldownStopped?.Invoke(); }
            _isInCooldown = false;
        }


        /// <summary>
        /// Coroutine for handling the cooldown logic.
        /// </summary>
        private IEnumerator CooldownRoutine()
        {
            CooldownStarted?.Invoke();
            yield return _waitForSeconds;
            CooldownFinished?.Invoke();
            _isInCooldown = false;
            yield break;
        }
    }
}
