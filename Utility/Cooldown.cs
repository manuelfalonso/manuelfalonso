using SombraStudios.Shared.Attributes;
using SombraStudios.Shared.Tools.Coroutines;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility
{
    /// <summary>
    /// It can be used as a waiting period between using specific actions in a game/app, 
    /// ensuring they can't be spammed and adding strategic depth to gameplay.
    /// </summary>
    public class Cooldown : MonoBehaviour
    {
        [Tooltip("In Seconds")]
        [SerializeField] protected float _time = 0f;
        [Tooltip("If true, the cooldown will still run if the object gets disabled")]
        [SerializeField] private bool _isGameObjectDecoupled = false;

        [Header("Debug")]
        [SerializeField] private bool _isActive = true;
        [SerializeField, ReadOnly] private bool _isInCooldown = false;

        public float Time { get => _time; private set => _time = value; }
        public bool IsActive { get => _isActive; set => _isActive = value; }
        public bool IsInCooldown { get => _isInCooldown; private set => _isInCooldown = value; }

        protected WaitForSeconds _waitForSeconds;
        protected CoroutineRunner _coroutineRunner;

        public UnityEvent CooldownStarted = new UnityEvent();
        [Tooltip("Calls when the Cooldown was manually stopped")]
        public UnityEvent CooldownStopped = new UnityEvent();
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
            if (_coroutineRunner.StopCoroutine()) { CooldownStopped?.Invoke(); }
            _isInCooldown = false;
        }


        private IEnumerator CooldownRoutine()
        {
            CooldownStarted?.Invoke();
            yield return _waitForSeconds;
            CooldownFinished?.Invoke();
            _isInCooldown = false;
            yield break;
        }

        public void Log(string text)
        {
            Debug.Log(text);
        }
    }
}
