namespace SombraStudios.Tools.Coroutines
{

    using System.Collections;
    using UnityEngine;

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
        private MonoBehaviour _myMonobehaviour = null;
        private bool _isGameObjectDecoupled = false;
        private bool _isMultiExecution = false;

        // Coroutine data
        private Coroutine _coroutine = null;
        private IEnumerator _routine = null;
        private float _preWaitTimeInSeconds = 0f;
        private float _postWaitTimeInSeconds = 0f;

        private System.Action OnCoroutineStarted = null;
        private System.Action OnCoroutineFinished = null;


        /// <param name="myMonobehaviour">Client script</param>
        /// <param name="isGameObjectDecoupled">Starts coroutine from an external object that will not stop the coroutine if client gameobject is deactivated or destroy</param>
        /// <param name="isMultiExecution">StartCoroutine method let run different routines simultaneously</param>
        public CoroutineRunner(MonoBehaviour myMonobehaviour, bool isGameObjectDecoupled = false, bool isMultiExecution = false)
        {
            _myMonobehaviour = myMonobehaviour;
            _isGameObjectDecoupled = isGameObjectDecoupled;
            _isMultiExecution = isMultiExecution;
        }


        // Start
        public void StartCoroutine(IEnumerator routine, float preWaitTime = 0, float postWaitTime = 0)
        {
            _routine = routine;
            _preWaitTimeInSeconds = preWaitTime;
            _postWaitTimeInSeconds = postWaitTime;

            Start();
        }

        // Stop
        public bool StopCoroutine()
        {
            return Stop();
        }

        // Stop All
        /// <summary>
        /// Warning: If decoupled, this method will stop ALL decoupled Coroutines.
        /// </summary>
        public void StopAll()
        {
            if (_isGameObjectDecoupled)
                CoroutineManager.Instance.StopAllCoroutines();
            else
                _myMonobehaviour.StopAllCoroutines();
        }

        // Restart
        public void Restart()
        {
            if (_routine == null)
            {
                Debug.LogError($"{this} => Before restarting, start a coroutine!");
                return;
            }

            if (_routine != null && !_isMultiExecution)
                Stop();

            Start();
        }

        // Subscribe On Started
        public void SubscribeOnCoroutineStarted(System.Action callback)
        {
            OnCoroutineStarted -= callback;
            OnCoroutineStarted += callback;
        }

        // Subscribe On Finished
        public void SubscribeOnCoroutineFinished(System.Action callback)
        {
            OnCoroutineFinished -= callback;
            OnCoroutineFinished += callback;
        }


        private void Start()
        {
            if (!_isMultiExecution)
                Stop();

            if (_isGameObjectDecoupled)
                _coroutine = CoroutineManager.Instance.StartCoroutine(Routine());
            else
                _coroutine = _myMonobehaviour.StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            if (_preWaitTimeInSeconds != 0)
                yield return new WaitForSeconds(_preWaitTimeInSeconds);

            OnCoroutineStarted?.Invoke();

            yield return _routine;

            OnCoroutineFinished?.Invoke();

            if (_postWaitTimeInSeconds != 0)
                yield return new WaitForSeconds(_postWaitTimeInSeconds);
        }

        private bool Stop()
        {
            bool wasRunning = false;

            if (_routine == null)
            {
                Debug.LogError($"{this} => Before stopping, start a coroutine!");
                return wasRunning;
            }

            if (_coroutine != null)
            {
                wasRunning = true;

                if (_isGameObjectDecoupled)
                    CoroutineManager.Instance.StopCoroutine(_coroutine);
                else
                    _myMonobehaviour.StopCoroutine(_coroutine);

                _coroutine = null;
            }

            return wasRunning;
        }
    }
}
