using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Physics
{
    /// <summary>
    /// Calls events for when the velocity of this objects breaks the begin and end threshold.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class OnVelocity : MonoBehaviour
    {
        /// <summary>
        /// The speed that will trigger the begin event.
        /// </summary>
        [SerializeField] [Tooltip("The speed that will trigger the begin event.")]
        private float _beginThreshold = 1.25f;

        /// <summary>
        /// The speed that will trigger the end event.
        /// </summary>
        [SerializeField] [Tooltip("The speed that will trigger the end event.")]
        private float _endThreshold = 0.25f;

        /// <summary>
        /// Event that triggers when speed meets the begin threshold
        /// </summary>
        [SerializeField] [Tooltip("Event that triggers when speed meets the begin threshold.")]
        private UnityEvent _onBegin = new UnityEvent();

        /// <summary>
        /// Event that triggers when the speed dips below the end threshold.
        /// </summary>
        [SerializeField] [Tooltip("Event that triggers when the speed dips below the end threshold.")]
        private UnityEvent _onEnd = new UnityEvent();

        /// <summary>
        /// Event that triggers when speed meets the begin threshold.
        /// </summary>
        public UnityEvent OnBegin => _onBegin;

        /// <summary>
        /// Event that triggers when the speed dips below the end threshold.
        /// </summary>
        public UnityEvent OnEnd => _onEnd;

        private Rigidbody _rigidBody;
        private bool _hasBegun;


        #region Unity Messages

        void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            CheckVelocity();
        }

        #endregion


        #region Private Methods

        private void CheckVelocity()
        {
#if UNITY_6000_0_OR_NEWER
            var speed = _rigidBody.linearVelocity.magnitude;
#else
            var speed = _rigidBody.velocity.magnitude;
#endif
            _hasBegun = HasVelocityBegun(speed);

            if (HasVelocityEnded(speed))
                _hasBegun = false;
        }

        private bool HasVelocityBegun(float speed)
        {
            if (_hasBegun)
                return true;

            var beginCheck = speed > _beginThreshold;

            if (beginCheck)
                _onBegin.Invoke();

            return beginCheck;
        }

        private bool HasVelocityEnded(float speed)
        {
            if (!_hasBegun)
                return false;

            var endCheck = speed < _endThreshold;

            if (endCheck)
                _onEnd.Invoke();

            return endCheck;
        }

        #endregion
    }
}