using System;
using System.Collections;
using UnityEngine;

namespace SombraStudios.Systems
{
    /// <summary>
    /// Manages the state of the reloading system.
    /// Invokes events with the changes of state
    /// </summary>
    public class AmmoReloadSystem : MonoBehaviour
    {
        public event EventHandler ReloadingStarted;
        public event EventHandler ReloadingCanceled;
        public event EventHandler ReloadingFinished;

        private float _reloadTime;

        private bool _isReloading = false;

        [SerializeField]
        private bool _debugMode;

        private IEnumerator _reloadCoroutine;

        public void Initialize(float reloadTime)
        {
            _reloadTime = reloadTime;
            _isReloading = false;
        }

        public void Reload()
        {
            // Set reference so we can cancel coroutine reference if necessary
            _reloadCoroutine = Reloading();
            StartCoroutine(_reloadCoroutine);
        }

        private IEnumerator Reloading()
        {
            if (_isReloading)
                yield break;

            // Reloading
            _isReloading = true;
            ReloadingStarted?.Invoke(this, EventArgs.Empty);
            if (_debugMode) Debug.Log($"isReloading");

            yield return new WaitForSeconds(_reloadTime);

            // Reloaded
            _isReloading = false;
            ReloadingFinished?.Invoke(this, EventArgs.Empty);
            if (_debugMode) Debug.Log($"isReloaded");
        }

        public void CancelReload()
        {
            if (!_isReloading)
                return;

            // Reloading canceled
            StopCoroutine(_reloadCoroutine);
            ReloadingCanceled?.Invoke(this, EventArgs.Empty);
            if (_debugMode) Debug.Log($"CancelReload");
        }
    }
}
