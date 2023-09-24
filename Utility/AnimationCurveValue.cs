using UnityEngine;

namespace SombraStudios.Utility
{
    /// <summary>
    /// Scriptable object to use Animation Curves as values
    /// </summary>
    [CreateAssetMenu(fileName = "New Animation Curve Value", menuName = "Sombra Studios/Curve Value", order = 51)]
    public class AnimationCurveValue : ScriptableObject
    {
        [SerializeField] private AnimationCurve _curve;

        [SerializeField] private WrapMode _preWrapMode = WrapMode.PingPong;
        [SerializeField] private WrapMode _postWrapMode = WrapMode.PingPong;

        [Range(-1f, 1f)]
        [SerializeField] private float _curveSpeed = 0f;

        public AnimationCurve Curve { get => _curve; set => _curve = value; }


        private void OnValidate()
        {
            _curve.preWrapMode = _preWrapMode;
            _curve.postWrapMode = _postWrapMode;
        }

        public float GetValueTimed()
        {
            return _curve.Evaluate(Time.time * _curveSpeed);
        }

        public float GetValueExact(float time)
        {
            return _curve.Evaluate(time);
        }

        /// <summary>
        /// Get value from the curve by a normalized time parameter 0 to 1.
        /// </summary>
        /// <param name="normalizedTime"></param>
        /// <returns></returns>
        public float GetValueNormalized(float normalizedTime)
        {
            if (normalizedTime < 0f || normalizedTime > 1f)
            {
                return 0f;
            }
            else
            {
                return _curve.Evaluate((_curve.keys[_curve.length - 1].time - _curve.keys[0].time) * normalizedTime + _curve.keys[0].time);
            }
        }
    }
}
