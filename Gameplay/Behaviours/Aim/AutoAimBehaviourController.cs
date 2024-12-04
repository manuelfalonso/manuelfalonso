#if DOTWEEN
using DG.Tweening;
#endif
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Gameplay.Behaviours.Aim
{
    /// <summary>
    /// Controls the auto-aim behaviour of a game object.
    /// </summary>
    public class AutoAimBehaviourController : MonoBehaviour, IBehaviour
    {
        [Tooltip("The target to aim at.")]
        [SerializeField] private Transform _target;

        [Header("Settings")]
        [Tooltip("Settings for the auto-aim behaviour.")]
        [SerializeField] private AutoAimBehaviourSO _data;
        [Tooltip("Show debug rays in the editor.")]
        [SerializeField] private bool _showDebugRays = false;

        [Header("Debug")]
        [Tooltip("Indicates whether the behaviour is enabled.")]
        [SerializeField] private bool _isEnabled;
        [Tooltip("Indicates whether the object is currently aiming.")]
        [SerializeField] private bool _isAiming = false;
        [Tooltip("Indicates whether the target is within the deadzone.")]
        [SerializeField] private bool _isInDeadzone = false;

        [Header("Events")]
        public UnityEvent AimTargetChanged;
        public UnityEvent AimTargetRemoved;
        public UnityEvent AimTargetLocked;
        public UnityEvent AimStarted;
        public UnityEvent AimStopped;

        /// <summary>
        /// Gets or sets the target to aim at.
        /// </summary>
        public Transform Target
        {
            get => _target;
            set
            {
                if (_target == value)
                    return;
                else
                {
                    if (value == null)
                    {
                        AimTargetRemoved?.Invoke();
                    }
                    else
                    {
                        AimTargetChanged?.Invoke();
                    }
                }
                _target = value;
                SetTargetRigidbody();
            }
        }

        /// <summary>
        /// Gets or sets the auto-aim behaviour settings.
        /// </summary>
        public AutoAimBehaviourSO Data { get => _data; set => _data = value; }

        /// <summary>
        /// Gets a value indicating whether the object is currently aiming.
        /// </summary>
        public bool IsAiming
        {
            get => _isAiming;
            private set
            {
                if (_isAiming == value)
                    return;
                else
                {
                    if (value == true)
                    {
                        AimStarted?.Invoke();
                    }
                    else
                    {
                        AimStopped?.Invoke();
                    }
                }
                _isAiming = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the target is within the deadzone.
        /// </summary>
        public bool IsInDeadzone
        {
            get => _isInDeadzone;
            set
            {
                if (_isInDeadzone == value)
                    return;
                else
                {
                    if (value == true)
                    {
                        AimTargetLocked?.Invoke();
                        IsAiming = false;
                    }
                    else
                    {
                        IsAiming = true;
                    }
                }
                _isInDeadzone = value;
            }
        }

        public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }
        
        public void ToggleBehaviour() => IsEnabled = !IsEnabled;

        private Quaternion _currentRotation;
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;
        private Rigidbody _targetRigidbody;
        private Vector3 _targetDirection;
        private float _currentNoiseTime;
        private Vector3 _currentDirection;
        private float _outOfDeadzoneTime;

        #region Unity Messages
        /// <summary>
        /// Unity's Awake method.
        /// </summary>
        private void Awake()
        {
            if (_target != null)
            {
                SetTargetRigidbody();
            }
        }

        /// <summary>
        /// Unity's Update method.
        /// </summary>
        private void Update()
        {
            ExecuteBehaviour();
        }

        /// <summary>
        /// Draws debug gizmos in the editor.
        /// </summary>
        private void OnDrawGizmos()
        {
            if (!_showDebugRays) return;

            if (_target == null) return;

            if (_data == null) return;

            if (_targetDirection == null) return;

            // Target position with offset
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, _target.position + _data.TargetOffset);

            // Transform forward
            if (_isInDeadzone)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * _targetDirection.magnitude);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets the Rigidbody component of the target.
        /// </summary>
        private void SetTargetRigidbody()
        {
            if (_target == null)
            {
                return;
            }

            if (_target.TryGetComponent(out Rigidbody targetRigidbody))
            {
                _targetRigidbody = targetRigidbody;
            }
        }

        /// <summary>
        /// Handles the auto-aim logic.
        /// </summary>
        public void ExecuteBehaviour()
        {
            if (_target == null) return;

            if (_data == null) return;

            // Set initial data
            _currentRotation = transform.rotation;
            _targetPosition = _target.position;

            ApplyPrediction();

            ApplyOffset();

            // Smooth rotation logic
            _targetRotation = Quaternion.LookRotation(_targetDirection);

            ApplyNoise();

            if (ApplyDeadzone()) return;

            ApplyConstraints();

            // Apply new rotation with interpolation
            transform.rotation = Quaternion.Slerp(_currentRotation, _targetRotation, _data.AimSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Applies prediction to the target position based on its velocity.
        /// </summary>
        private void ApplyPrediction()
        {
            if (!_data.UsePrediction) return;

            if (_targetRigidbody != null)
            {
                _targetPosition += _targetRigidbody.velocity * _data.PredictionVelocityFactor;
            }
        }

        /// <summary>
        /// Applies the offset to the target direction.
        /// </summary>
        private void ApplyOffset()
        {
            _targetDirection = _targetPosition + _data.TargetOffset - transform.position;
        }

        /// <summary>
        /// Applies noise to the aim using DoTween.
        /// </summary>
        private void ApplyNoise()
        {
#if DOTWEEN
            if (!_data.UseNoise) return;

            _currentNoiseTime += Time.deltaTime;
            if (_currentNoiseTime > _data.NoiseFrequency)
            {
                _currentNoiseTime = 0;
                transform.DOShakeRotation(
                    _data.NoiseFrequency,
                    _data.NoiseStrength,
                    _data.NoiseVibrato,
                    _data.NoiseRandomness,
                    _data.NoiseFadeOut,
                    _data.NoiseRandomnessMode);
            }
#endif
        }

        /// <summary>
        /// Applies the deadzone logic.
        /// </summary>
        /// <returns>True if the target is within the deadzone, otherwise false.</returns>
        private bool ApplyDeadzone()
        {
            // Apply deadzone
            _currentDirection = transform.forward * _targetDirection.magnitude;
            if ((_currentDirection - _targetDirection).magnitude < _data.DeadzoneDistance)
            {
                IsInDeadzone = true;
                return true;
            }
            else
            {
                if (_isInDeadzone)
                {
                    _outOfDeadzoneTime = 0;
                }

                _outOfDeadzoneTime += Time.deltaTime;
                IsInDeadzone = false;
            }

            if (_outOfDeadzoneTime < _data.OutOfDeadzoneDelay)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Applies constraints to the target rotation.
        /// </summary>
        private void ApplyConstraints()
        {
            if (!_data.UseConstraints) return;

            if (_data.XConstraint) _targetRotation.x = 0;
            if (_data.YConstraint) _targetRotation.y = 0;
            if (_data.ZConstraint) _targetRotation.z = 0;
        }
        #endregion
    }
}
