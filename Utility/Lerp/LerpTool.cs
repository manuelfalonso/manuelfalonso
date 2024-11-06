using SombraStudios.Shared.Utility.Coroutines;
using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.Utility.Lerp
{
    public static class LerpTool
    {
        private static CoroutineRunner CoroutineRunner;

        #region Public Methods
        public static void LerpValue(ref float value, LerpValueData data)
        {
            if (CoroutineRunner == null)
            {
                CoroutineRunner = new CoroutineRunner(null, true, true);
            }
            FloatWrapper wrapper = new FloatWrapper(ref value);
            CoroutineRunner.StartCoroutine(LerpValueCoroutine(wrapper, data));
        }
        #endregion

         
        #region Private Methods
        private static IEnumerator LerpValueCoroutine(FloatWrapper valueWrapper, LerpValueData data)
        {
            float time = 0f;
            while (time < data.Duration)
            {
                valueWrapper.value = Mathf.Lerp(data.Start, data.Target, data.Curve.Evaluate(time / data.Duration));
                Debug.Log(valueWrapper.value);
                time += Time.deltaTime;
                yield return null;
            }
            valueWrapper.value = data.Target;            
        }
        #endregion

        private class FloatWrapper
        {
            public float value;

            public FloatWrapper(ref float val)
            {
                value = val;
            }
        }
    }

    public struct LerpValueData
    {
        public float Start;
        public float Target;
        public float Duration;
        public AnimationCurve Curve;
    }

    public struct LerpVectorData
    {
        public Vector3 Start;
        public Vector3 Target;
        public float Duration;
        public AnimationCurve Curve;
    }

    public struct LerpColorData
    {
        public Color Start;
        public Color Target;
        public float Duration;
        public AnimationCurve Curve;
    }

    public struct LerpQuaternionData
    {
        public Quaternion Start;
        public Quaternion Target;
        public float Duration;
        public AnimationCurve Curve;
    }
}
