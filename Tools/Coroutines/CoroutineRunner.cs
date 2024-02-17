using System;
using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.Tools.Coroutines
{
    /// <summary>
    /// Unity Coroutine runner that allows game object decoupled, scene 
    /// persistent and single or multi coroutine execution.
    /// 
    /// Events:
    /// OnCoroutineStarted
    /// OnCoroutineFinished
    /// </summary>
    public class CoroutineRunner
    {
        /// <summary>
        /// The client script associated with the CoroutineRunner.
        /// </summary>
        private readonly MonoBehaviour _myMonobehaviour = null;
        /// <summary>
        /// Determines whether the coroutine starts from an external object that will not stop the coroutine if the 
        /// client GameObject is deactivated or destroyed.
        /// </summary>
        private readonly bool _isGameObjectDecoupled = false;
        /// <summary>
        /// Determines whether the StartCoroutine method lets run different routines simultaneously.
        /// </summary>
        private readonly bool _isMultiExecution = false;

        // Coroutine data
        /// <summary>
        /// The current coroutine instance.
        /// </summary>
        private Coroutine _coroutine = null;
        /// <summary>
        /// The IEnumerator routine associated with the coroutine.
        /// </summary>
        private IEnumerator _routine = null;
        /// <summary>
        /// The time to wait before starting the coroutine.
        /// </summary>
        private float _preWaitTimeInSeconds = 0f;
        /// <summary>
        /// The time to wait after the coroutine finishes.
        /// </summary>
        private float _postWaitTimeInSeconds = 0f;

        /// <summary>
        /// Event invoked when the coroutine starts.
        /// </summary>
        private event Action OnCoroutineStarted = null;
        /// <summary>
        /// Event invoked when the coroutine finishes.
        /// </summary>
        private event Action OnCoroutineFinished = null;


        /// <summary>
        /// Initializes a new instance of the CoroutineRunner class.
        /// </summary>
        /// <param name="myMonobehaviour">The client script.</param>
        /// <param name="isGameObjectDecoupled">Determines whether the coroutine starts from an external object that 
        /// will not stop the coroutine if the client GameObject is deactivated or destroyed.</param>
        /// <param name="isMultiExecution">Determines whether the StartCoroutine method lets run different routines 
        /// simultaneously.</param>
        public CoroutineRunner(MonoBehaviour myMonobehaviour, bool isGameObjectDecoupled = false, bool isMultiExecution = false)
        {
            _myMonobehaviour = myMonobehaviour;
            _isGameObjectDecoupled = isGameObjectDecoupled;
            _isMultiExecution = isMultiExecution;

            if (!_isGameObjectDecoupled && _myMonobehaviour == null)
            {
                Debug.LogError($"{this} => If isGameObjectDecoupled is false, myMonobehaviour must be assigned!");
            }
        }


        #region Public Methods
        /// <summary>
        /// Starts a coroutine with optional pre and post wait times.
        /// </summary>
        public void StartCoroutine(IEnumerator routine, float preWaitTime = 0f, float postWaitTime = 0f)
        {
            _routine = routine;
            _preWaitTimeInSeconds = preWaitTime;
            _postWaitTimeInSeconds = postWaitTime;

            Start();
        }

        /// <summary>
        /// Attempts to stop the current coroutine.
        /// </summary>
        /// <returns>Returns true if the coroutine was successfully stopped; otherwise, false.</returns>
        public bool TryStopCoroutine()
        {
            if (CanStop())
            {
                Stop();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Stops all coroutines.
        /// </summary>
        public void StopAll()
        {
            if (_isGameObjectDecoupled)
            {
                Debug.LogWarning($"{this} => Stopping all decoupled coroutines!");
                CoroutineManager.Instance.StopAllCoroutines();
            }
            else
            {
                _myMonobehaviour.StopAllCoroutines();
            }
        }

        /// <summary>
        /// Attempts to restart the coroutine.
        /// </summary>
        /// <returns>Returns true if the coroutine was successfully restarted; otherwise, false.</returns>
        public bool TryRestart()
        {
            if (CanRestart())
            {
                Restart();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Subscribes to the coroutine started event.
        /// </summary>
        public void SubscribeOnCoroutineStarted(Action callback)
        {
            OnCoroutineStarted -= callback;
            OnCoroutineStarted += callback;
        }

        /// <summary>
        /// Unsubscribes from the coroutine started event.
        /// </summary>
        public void UnsubscribeOnCoroutineStarted(Action callback)
        {
            OnCoroutineStarted -= callback;
        }

        /// <summary>
        /// Subscribes to the coroutine finished event.
        /// </summary>
        public void SubscribeOnCoroutineFinished(Action callback)
        {
            OnCoroutineFinished -= callback;
            OnCoroutineFinished += callback;
        }

        /// <summary>
        /// Unsubscribes from the coroutine finished event.
        /// </summary>
        public void UnsubscribeOnCoroutineFinished(Action callback)
        {
            OnCoroutineFinished -= callback;
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Starts the coroutine.
        /// </summary>
        private void Start()
        {
            if (!_isMultiExecution) { Stop(); }

            if (_isGameObjectDecoupled)
            {
                _coroutine = CoroutineManager.Instance.StartCoroutine(Routine());
            }
            else
            {
                _coroutine = _myMonobehaviour.StartCoroutine(Routine());
            }
        }

        /// <summary>
        /// The main coroutine routine.
        /// </summary>
        private IEnumerator Routine()
        {
            if (_preWaitTimeInSeconds != 0f) { yield return new WaitForSeconds(_preWaitTimeInSeconds); }

            OnCoroutineStarted?.Invoke();

            yield return _routine;

            OnCoroutineFinished?.Invoke();

            if (_postWaitTimeInSeconds != 0f) { yield return new WaitForSeconds(_postWaitTimeInSeconds); }
        }

        /// <summary>
        /// Checks if the coroutine can be stopped.
        /// </summary>
        /// <returns>Returns true if the coroutine can be stopped; otherwise, false.</returns>
        private bool CanStop()
        {
            bool canStop = true;

            if (_routine == null)
            {
                Debug.LogError($"{this} => Before stopping, start a coroutine!");
                canStop = false;
            }

            return canStop;
        }

        /// <summary>
        /// Stops the current coroutine.
        /// </summary>
        private void Stop()
        {
            if (_isGameObjectDecoupled)
            {
                CoroutineManager.Instance.StopCoroutine(_coroutine);
            }
            else
            {
                _myMonobehaviour.StopCoroutine(_coroutine);
            }

            _coroutine = null;
        }

        /// <summary>
        /// Checks if the coroutine can be restarted.
        /// </summary>
        /// <returns>Returns true if the coroutine can be restarted; otherwise, false.</returns>
        private bool CanRestart()
        {
            bool canRestart = true;
            if (_routine == null)
            {
                Debug.LogError($"{this} => Before restarting, start a coroutine!");
                canRestart = false;
            }
            return canRestart;
        }

        /// <summary>
        /// Restarts the coroutine.
        /// </summary>
        private void Restart()
        {
            if (!_isMultiExecution) { Stop(); }

            Start();
        }
        #endregion
    }
}
