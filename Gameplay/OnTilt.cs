using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Gameplay
{
    /// <summary>
    /// Runs functionality when an object is tilted.
    /// Used with grabbable objects for pouring.
    /// </summary>
    public class OnTilt : MonoBehaviour
    {
        /// <summary>
        /// Extra angle value that is added/removed from the threshold to events from rapid-fire triggering on and off.
        /// </summary>
        private const float _angleBuffer = 0.05f;

        [SerializeField]
        [Tooltip("Tilt range, 0 - 180 degrees.")]
        [Range(_angleBuffer * 2f, (1 - _angleBuffer * 2f))]
        private float _threshold = 0.5f;

        [SerializeField]
        [Tooltip("The transform to check for tilt. Will default to this object if not set.")]
        private Transform _target;

        [SerializeField]
        [Tooltip("The transform to get as the source of the 'up' direction. Will default to world up if not set.")]
        private Transform _upSource;

        [SerializeField]
        [Tooltip("Event to trigger when tilting goes over the threshold.")]
        private UnityEvent _onBegin = new UnityEvent();

        [SerializeField]
        [Tooltip("Event to trigger when tilting returns from the threshold.")]
        private UnityEvent _onEnd = new UnityEvent();

        /// <summary>
        /// Event to trigger when tilting goes over the threshold.
        /// </summary>
        public UnityEvent OnBegin => _onBegin;

        /// <summary>
        /// Event to trigger when tilting returns from the threshold.
        /// </summary>
        public UnityEvent OnEnd => _onEnd;

        private bool _withinThreshold;


        private void Update()
        {
            CheckOrientation();
        }


        private void CheckOrientation()
        {
            var targetUp = _target != null ? _target.up : transform.up;
            var baseUp = _upSource != null ? _upSource.up : Vector3.up;

            var similarity = Vector3.Dot(-targetUp, baseUp);
            similarity = Mathf.InverseLerp(-1, 1, similarity);

            if (_withinThreshold)
                similarity += _angleBuffer;
            else
                similarity -= _angleBuffer;

            var thresholdCheck = (similarity >= _threshold);

            if (_withinThreshold != thresholdCheck)
            {
                _withinThreshold = thresholdCheck;

                if (_withinThreshold)
                {
                    _onBegin.Invoke();
                }
                else
                {
                    _onEnd.Invoke();
                }
            }
        }
    }
}
