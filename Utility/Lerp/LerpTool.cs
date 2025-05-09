using SombraStudios.Shared.Utility.Coroutines;
using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.Utility.Lerp
{
    /// <summary>  
    /// Utility class for performing various types of Lerp operations.  
    /// </summary>  
    public static class LerpTool
    {
        private static CoroutineRunner _coroutineRunner;

        #region Public Methods  

        /// <summary>  
        /// Performs a Lerp operation on a float value over time.  
        /// </summary>  
        /// <param name="onValueChanged">Callback to update the value during the Lerp.</param>  
        /// <param name="data">Data containing Lerp parameters.</param>  
        public static void LerpValue(System.Action<float> onValueChanged, LerpValueData data)
        {
            EnsureCoroutineRunnerInitialized();
            _coroutineRunner.StartCoroutine(LerpValueCoroutine(onValueChanged, data));
        }

        /// <summary>  
        /// Performs a Lerp operation on a Vector2 value over time.  
        /// </summary>  
        /// <param name="onValueChanged">Callback to update the value during the Lerp.</param>  
        /// <param name="data">Data containing Lerp parameters.</param>  
        public static void LerpVector2(System.Action<Vector2> onValueChanged, LerpVector2Data data)
        {
            EnsureCoroutineRunnerInitialized();
            _coroutineRunner.StartCoroutine(LerpVector2Coroutine(onValueChanged, data));
        }

        /// <summary>  
        /// Performs a Lerp operation on a Vector3 value over time.  
        /// </summary>  
        /// <param name="onValueChanged">Callback to update the value during the Lerp.</param>  
        /// <param name="data">Data containing Lerp parameters.</param>  
        public static void LerpVector3(System.Action<Vector3> onValueChanged, LerpVector3Data data)
        {
            EnsureCoroutineRunnerInitialized();
            _coroutineRunner.StartCoroutine(LerpVector3Coroutine(onValueChanged, data));
        }

        /// <summary>  
        /// Performs a Lerp operation on a Color value over time.  
        /// </summary>  
        /// <param name="onValueChanged">Callback to update the value during the Lerp.</param>  
        /// <param name="data">Data containing Lerp parameters.</param>  
        public static void LerpColor(System.Action<Color> onValueChanged, LerpColorData data)
        {
            EnsureCoroutineRunnerInitialized();
            _coroutineRunner.StartCoroutine(LerpColorCoroutine(onValueChanged, data));
        }

        /// <summary>  
        /// Performs a Lerp operation on a Quaternion value over time.  
        /// </summary>  
        /// <param name="onValueChanged">Callback to update the value during the Lerp.</param>  
        /// <param name="data">Data containing Lerp parameters.</param>  
        public static void LerpQuaternion(System.Action<Quaternion> onValueChanged, LerpQuaternionData data)
        {
            EnsureCoroutineRunnerInitialized();
            _coroutineRunner.StartCoroutine(LerpQuaternionCoroutine(onValueChanged, data));
        }

        #endregion

        #region Private Methods  

        /// <summary>  
        /// Ensures that the CoroutineRunner is initialized.  
        /// </summary>  
        private static void EnsureCoroutineRunnerInitialized()
        {
            if (_coroutineRunner == null)
            {
                _coroutineRunner = new CoroutineRunner(null, true, true);
            }
        }

        /// <summary>  
        /// Coroutine for performing a Lerp operation on a float value.  
        /// </summary>  
        private static IEnumerator LerpValueCoroutine(System.Action<float> onValueChanged, LerpValueData data)
        {
            float time = 0f;
            while (time < data.Duration)
            {
                float value = Mathf.Lerp(data.Start, data.Target, data.Curve.Evaluate(time / data.Duration));
                onValueChanged?.Invoke(value);
                time += Time.deltaTime;
                yield return null;
            }
            onValueChanged?.Invoke(data.Target);
        }

        /// <summary>  
        /// Coroutine for performing a Lerp operation on a Vector2 value.  
        /// </summary>  
        private static IEnumerator LerpVector2Coroutine(System.Action<Vector2> onValueChanged, LerpVector2Data data)
        {
            float time = 0f;
            while (time < data.Duration)
            {
                Vector2 value = Vector2.Lerp(data.Start, data.Target, data.Curve.Evaluate(time / data.Duration));
                onValueChanged?.Invoke(value);
                time += Time.deltaTime;
                yield return null;
            }
            onValueChanged?.Invoke(data.Target);
        }

        /// <summary>  
        /// Coroutine for performing a Lerp operation on a Vector3 value.  
        /// </summary>  
        private static IEnumerator LerpVector3Coroutine(System.Action<Vector3> onValueChanged, LerpVector3Data data)
        {
            float time = 0f;
            while (time < data.Duration)
            {
                Vector3 value = Vector3.Lerp(data.Start, data.Target, data.Curve.Evaluate(time / data.Duration));
                onValueChanged?.Invoke(value);
                time += Time.deltaTime;
                yield return null;
            }
            onValueChanged?.Invoke(data.Target);
        }

        /// <summary>  
        /// Coroutine for performing a Lerp operation on a Color value.  
        /// </summary>  
        private static IEnumerator LerpColorCoroutine(System.Action<Color> onValueChanged, LerpColorData data)
        {
            float time = 0f;
            while (time < data.Duration)
            {
                Color value = Color.Lerp(data.Start, data.Target, data.Curve.Evaluate(time / data.Duration));
                onValueChanged?.Invoke(value);
                time += Time.deltaTime;
                yield return null;
            }
            onValueChanged?.Invoke(data.Target);
        }

        /// <summary>  
        /// Coroutine for performing a Lerp operation on a Quaternion value.  
        /// </summary>  
        private static IEnumerator LerpQuaternionCoroutine(System.Action<Quaternion> onValueChanged, LerpQuaternionData data)
        {
            float time = 0f;
            while (time < data.Duration)
            {
                Quaternion value = Quaternion.Slerp(data.Start, data.Target, data.Curve.Evaluate(time / data.Duration));
                onValueChanged?.Invoke(value);
                time += Time.deltaTime;
                yield return null;
            }
            onValueChanged?.Invoke(data.Target);
        }

        #endregion
    }

    /// <summary>  
    /// Data structure for Lerp operations on float values.  
    /// </summary>  
    public struct LerpValueData
    {
        public float Start;
        public float Target;
        public float Duration;
        public AnimationCurve Curve;
    }

    /// <summary>  
    /// Data structure for Lerp operations on Vector2 values.  
    /// </summary>  
    public struct LerpVector2Data
    {
        public Vector2 Start;
        public Vector2 Target;
        public float Duration;
        public AnimationCurve Curve;
    }

    /// <summary>  
    /// Data structure for Lerp operations on Vector3 values.  
    /// </summary>  
    public struct LerpVector3Data
    {
        public Vector3 Start;
        public Vector3 Target;
        public float Duration;
        public AnimationCurve Curve;
    }

    /// <summary>  
    /// Data structure for Lerp operations on Color values.  
    /// </summary>  
    public struct LerpColorData
    {
        public Color Start;
        public Color Target;
        public float Duration;
        public AnimationCurve Curve;
    }

    /// <summary>  
    /// Data structure for Lerp operations on Quaternion values.  
    /// </summary>  
    public struct LerpQuaternionData
    {
        public Quaternion Start;
        public Quaternion Target;
        public float Duration;
        public AnimationCurve Curve;
    }
}
