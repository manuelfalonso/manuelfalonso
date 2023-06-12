using UnityEngine;

namespace SombraStudios.Utility
{
    /// <summary>
    /// Scriptable object to use Animation Curves as values
    /// </summary>
    [CreateAssetMenu(fileName = "New Animation Curve Value", menuName = "Curve Value", order = 51)]
    public class AnimationCurveValue : ScriptableObject
    {
        [SerializeField] private AnimationCurve _curve;

        [SerializeField] private WrapMode _preWrapMode = WrapMode.PingPong;
        [SerializeField] private WrapMode _postWrapMode = WrapMode.PingPong;

        [Range(-1f, 1f)]
        [SerializeField] private float _curveSpeed = 0f;

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
    }
}
