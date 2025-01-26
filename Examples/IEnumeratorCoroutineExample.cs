using UnityEngine;
using System.Collections; // Required for IEnumerator

namespace SombraStudios.Shared.Examples
{

    /// <summary>
    /// IEnumerator Coroutines examples
    /// 
    /// Coroutine Documentation:
    /// https://docs.unity3d.com/Manual/Coroutines.html
    /// Coroutine Script Documentation:
    /// https://docs.unity3d.com/ScriptReference/Coroutine.html
    /// YieldInstruction Documentation:
    /// https://docs.unity3d.com/ScriptReference/YieldInstruction.html
    /// IEnumerator Interface (C#) Documentation:
    /// https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerator?view=net-7.0
    /// </summary>
    public class IEnumeratorCoroutineExample : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField]
        private float _waitTimeInSeconds = 1f;

        [Header("Examples")]
        [SerializeField]
        private bool _startStopStopAllExample = false;
        [SerializeField]
        private bool _yieldReturnExample = false;
        [SerializeField]
        private bool _breakExample = false;
        [SerializeField]
        private bool _waitUntilWhileExample = false;

        private WaitForSeconds _waitTime = default;


        void Start()
        {
            _waitTime = new WaitForSeconds(_waitTimeInSeconds);

            RunExamples();
        }


        private void RunExamples()
        {
            if (_startStopStopAllExample)
                StartStopStopAll();
            if (_yieldReturnExample)
                YieldReturn();
            if (_breakExample)
                Break();
            if (_waitUntilWhileExample)
                WaitUntilWhile();
        }


        #region StartStopStopAll
        // Private fields
        private Coroutine _coroutineOne = default;
        private Coroutine _coroutineTwo = default;
        private Coroutine _coroutineThree = default;

        // Note: You can stop a coroutine using MonoBehaviour.StopCoroutine
        // and MonoBehaviour.StopAllCoroutines.
        private void StartStopStopAll()
        {
            Log($"StartStopStopAll", this);

            var routineOne = RoutineOne();
            var routineTwo = RoutineTwo();
            var routineThree = RoutineThree();

            _coroutineOne = StartCoroutine(routineOne);
            _coroutineTwo = StartCoroutine(routineTwo);
            _coroutineThree = StartCoroutine(routineThree);
        }


        private IEnumerator RoutineOne()
        {
            Log("RoutineOne", this);

            yield return _waitTime;

            Log("RoutineOne: Stopping Coroutine One", this);
            // Note: Do not mix the three arguments. If a string is used as the
            // argument in StartCoroutine, use the string in StopCoroutine.
            // Similarly, use the IEnumerator in both StartCoroutine and
            // StopCoroutine. Finally, use StopCoroutine with the Coroutine
            // used for creation
            StopCoroutine(_coroutineOne);
            Log("RoutineOne: Stopping Coroutine Two", this);
            StopCoroutine(_coroutineTwo);

            yield return null;
            // The yield return is required to stop the execution of the method
            // if the stop is for its own coroutine.
            // In other words, if a coroutine is running during its own stop call,
            // it will continue running its logic until a yield or the end of 
            // the method is reached.
            Log("RoutineOne: This message should not be printed", this);
        }

        private IEnumerator RoutineTwo()
        {
            Log("RoutineTwo", this);

            yield return _waitTime;

            Log("RoutineTwo: This message should not be printed", this);
        }

        // Coroutines are also stopped when the MonoBehaviour
        // is destroyed or if the GameObject the MonoBehaviour
        // is attached to is disabled. Coroutines are not stopped
        // when a MonoBehaviour is disabled.
        private IEnumerator RoutineThree()
        {
            Log("RoutineThree", this);

            yield return _waitTime;

            Log("RoutineThree: Disabling the Monobehaviour", this);
            enabled = false;

            yield return _waitTime;

            Log("RoutineThree: Continue running with Monobehaviour disabled", this);
            Log("RoutineThree: Disabling Gameobject (this will stop the coroutine)", this);
            gameObject.SetActive(false);

            yield return _waitTime;

            Log("RoutineThree: This message should not be printed", this);
        }
        #endregion

        #region YieldReturn
        // Private fields
        private Coroutine _yieldReturnCoroutine = default;

        private void YieldReturn()
        {
            Log($"YieldReturn", this);

            var routine = YieldReturnRoutine();

            _yieldReturnCoroutine = StartCoroutine(routine);
        }

        private IEnumerator YieldReturnRoutine()
        {
            Log($"Frame {Time.frameCount}-Time {Time.time}: YieldReturnRoutine called", this);

            // 1 frame wait
            yield return null;
            Log($"Frame {Time.frameCount}-Time {Time.time}: Yield return null", this);

            // 1 frame wait
            yield return 999;
            Log($"Frame {Time.frameCount}-Time {Time.time}: Yield return \"anything\"", this);

            yield return AnotherCoroutine();
            Log($"Frame {Time.frameCount}-Time {Time.time}: Yield return another Coroutine", this);

            // Waits until the end of the frame after Unity has rendererd
            // every Camera and GUI, just before displaying the frame on screen.
            yield return new WaitForEndOfFrame();
            Log($"Frame {Time.frameCount}-Time {Time.time}: Yield return WaitForEndOfFrame", this);

            // Waits until next fixed frame rate update function.
            yield return new WaitForFixedUpdate();
            Log($"Frame {Time.frameCount}-Time {Time.time}: Yield return WaitForFixedUpdate", this);

            // Uses scaled time. Its the time given divded by Time.timeScale
            yield return new WaitForSeconds(1f);
            Log($"Frame {Time.frameCount}-Time {Time.time}: Yield return WaitForSeconds 1", this);

            // Uses unscaled time
            yield return new WaitForSecondsRealtime(1f);
            Log($"Frame {Time.frameCount}-Time {Time.time}: Yield return WaitForSecondsRealtime 1", this);
        }

        private IEnumerator AnotherCoroutine()
        {
            Log($"AnotherCoroutine called", this);
            // Requieres at least one yield return to be a IEnumerator
            // 1 frame wait
            yield return null;
        }
        #endregion

        #region Break
        // Private fields
        private Coroutine _breakCoroutine = default(Coroutine);

        private void Break()
        {
            Log($"Break", this);

            var routine = BreakRoutine();
            _breakCoroutine = StartCoroutine(routine);
        }

        private IEnumerator BreakRoutine()
        {
            // normal break
            while (true)
            {
                Log($"break; stops loop", this);
                break;
            }

            // yield break stops coroutine
            Log($"yield break; stops coroutine", this);
            yield break;
            
#pragma warning disable CS0162 // Disable warning about unreachable code detected
            // Innacesible code
            Log("RoutineOne: This message should not be printed", this);
#pragma warning restore CS0162 // Re-enable warning for unreachable code detected
        }
        #endregion

        #region WaitUntilWhile
        // Private Fields
        private Coroutine _waitUntilWhileCoroutine = default(Coroutine);

        private void WaitUntilWhile()
        {
            Log($"WaitUntilWhile", this);

            var routine = WaitUntilWhileRoutine();
            _waitUntilWhileCoroutine = StartCoroutine(routine);
        }

        private IEnumerator WaitUntilWhileRoutine()
        {
            Log($"Frame {Time.frameCount}: WaitUntilWhileRoutine started", this);

            yield return new WaitUntil(() => Time.frameCount == 3);
            Log($"Frame {Time.frameCount}: Wait until Frame == 3", this);

            yield return new WaitWhile(() => Time.frameCount < 5);
            Log($"Frame {Time.frameCount}: Wait while Frame < 5", this);
        }
        #endregion


        // Logger
        private void Log(object message, UnityEngine.Object sender)
        {
            Debug.Log($"{this} => {message}", sender);
        }
    }
}
