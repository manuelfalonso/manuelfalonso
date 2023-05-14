namespace SombraStudios.Utils
{

    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Unity Coroutine runner that allows decoupled scene persistent 
    /// Coroutines and single or multi coroutine execution validation.
    /// 
    /// Events:
    /// OnCoroutineStarted
    /// OnCoroutineFinished
    /// </summary>
    public class CoroutineRunner
    {
        private MonoBehaviour _myMonobehaviour = null;
        private bool _decoupled = false;
        private bool _multiCoroutine = false;

        // Coroutine data
        private Coroutine _coroutine = null;
        private IEnumerator _routine = null;
        private float _preWaitTime = 0f;
        private float _postWaitTime = 0f;

        private System.Action OnCoroutineStarted = null;
        private System.Action OnCoroutineFinished = null;


        /// <param name="myMonobehaviour">Client script</param>
        /// <param name="decoupled">Starts coroutine from an external object that will not stop the coroutine if client gameobject is deactivated or destroy</param>
        /// <param name="multiCoroutine">StartCoroutine method let run different routines simultaneously</param>
        public CoroutineRunner(MonoBehaviour myMonobehaviour, bool decoupled, bool multiCoroutine = false)
        {
            _myMonobehaviour = myMonobehaviour;
            _decoupled = decoupled;
            _multiCoroutine = multiCoroutine;
        }


        // Start
        public void StartCoroutine(IEnumerator routine, float preWaitTime = 0, float postWaitTime = 0)
        {
            _routine = routine;
            _preWaitTime = preWaitTime;
            _postWaitTime = postWaitTime;

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
            if (_decoupled)
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

            if (_routine != null && !_multiCoroutine)
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
            if (!_multiCoroutine)
                Stop();

            if (_decoupled)
                _coroutine = CoroutineManager.Instance.StartCoroutine(Routine());
            else
                _coroutine = _myMonobehaviour.StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            if (_preWaitTime != 0)
                yield return new WaitForSeconds(_preWaitTime);

            OnCoroutineStarted?.Invoke();

            yield return _routine;

            OnCoroutineFinished?.Invoke();

            if (_postWaitTime != 0)
                yield return new WaitForSeconds(_postWaitTime);
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

                if (_decoupled)
                    CoroutineManager.Instance.StopCoroutine(_coroutine);
                else
                    _myMonobehaviour.StopCoroutine(_coroutine);

                _coroutine = null;
            }

            return wasRunning;
        }
    }

    public class CoroutineManager : MonoBehaviour
    {
        private static CoroutineManager _instance;
        public static CoroutineManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject(nameof(CoroutineManager)).AddComponent<CoroutineManager>();
                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }
    }
}
