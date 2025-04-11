using UnityEngine;

namespace SombraStudios.Shared.Utility
{
    /// <summary>
    /// Scriptable object to use Animation Curves as values
    /// </summary>
    [CreateAssetMenu(fileName = "New Animation Curve Value", menuName = "Sombra Studios/Curve Value", order = 51)]
    public class AnimationCurveValueSO : ScriptableObject
    {
        [SerializeField] private AnimationCurve _curve;

        [SerializeField] private WrapMode _preWrapMode = WrapMode.PingPong;
        [SerializeField] private WrapMode _postWrapMode = WrapMode.PingPong;

        //[Range(-1f, 1f)]
        //[SerializeField] private float _curveSpeed = 0f;

        public AnimationCurve Curve
        {
            get => _curve;
            set
            {
                _curve = value;
                ApplyWrapModes();
            }
        }

        private void OnValidate()
        {
            ApplyWrapModes();
        }

        private void ApplyWrapModes()
        {
            if (_curve != null)
            {
                _curve.preWrapMode = _preWrapMode;
                _curve.postWrapMode = _postWrapMode;
            }
        }

        public float GetValueExact(float time)
        {
            return _curve?.Evaluate(time) ?? 0f;
        }

        /// <summary>
        /// Get value from the curve by a normalized time parameter 0 to 1.
        /// </summary>
        /// <param name="normalizedTime"></param>
        /// <returns></returns>
        public float GetValueNormalized(float normalizedTime)
        {
            if (_curve == null || normalizedTime < 0f || normalizedTime > 1f)
            {
                return 0f;
            }

            float startTime = _curve.keys[0].time;
            float endTime = _curve.keys[_curve.length - 1].time;
            float evaluatedTime = Mathf.Lerp(startTime, endTime, normalizedTime);
            return _curve.Evaluate(evaluatedTime);
        }
    }
}
