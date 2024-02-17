#if UNITY_XR_INTERACTION_TOOLKIT
using System;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace UnityEngine.XR.Content.Interaction
{
    /// <summary>
    /// An interactable knob that follows the rotation of the interactor
    /// </summary>
    public class XRKnob : XRBaseInteractable
    {
        const float _modeSwitchDeadZone = 0.1f; // Prevents rapid switching between the different rotation tracking modes
                
        [Serializable]
        public class ValueChangeEvent : UnityEvent<float> { }

        [Tooltip("The object that is visually grabbed and manipulated")]
        [SerializeField] private Transform _handle = null;

        [Tooltip("The value of the knob")]
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _value = 0.5f;

        [Tooltip("Whether this knob's rotation should be clamped by the angle limits")]
        [SerializeField] private bool _clampedMotion = true;

        [Tooltip("Rotation of the knob at value '1'")]
        [SerializeField] private float _maxAngle = 90.0f;

        [Tooltip("Rotation of the knob at value '0'")]
        [SerializeField] private float _minAngle = -90.0f;

        [Tooltip("Angle increments to support, if greater than '0'")]
        [SerializeField] private float _angleIncrement = 0.0f;

        [Tooltip("The position of the interactor controls rotation when outside this radius")]
        [SerializeField] private float _positionTrackedRadius = 0.1f;

        [Tooltip("How much controller rotation ")]
        [SerializeField] private float _twistSensitivity = 1.5f;
        
        [Tooltip("Events to trigger when the knob is rotated")]
        [SerializeField] private ValueChangeEvent _onValueChange = new ValueChangeEvent();

        IXRSelectInteractor _interactor;

        private bool _positionDriven = false;
        private bool _upVectorDriven = false;

        private TrackedRotation _positionAngles = new TrackedRotation();
        private TrackedRotation _upVectorAngles = new TrackedRotation();
        private TrackedRotation _forwardVectorAngles = new TrackedRotation();

        private float _baseKnobRotation = 0.0f;

        /// <summary>
        /// The object that is visually grabbed and manipulated
        /// </summary>
        public Transform Handle
        {
            get => _handle;
            set => _handle = value;
        }

        /// <summary>
        /// The value of the knob
        /// </summary>
        public float Value
        {
            get => _value;
            set
            {
                SetValue(value);
                SetKnobRotation(ValueToRotation());
            }
        }

        /// <summary>
        /// Whether this knob's rotation should be clamped by the angle limits
        /// </summary>
        public bool ClampedMotion
        {
            get => _clampedMotion;
            set => _clampedMotion = value;
        }

        /// <summary>
        /// Rotation of the knob at value '1'
        /// </summary>
        public float MaxAngle
        {
            get => _maxAngle;
            set => _maxAngle = value;
        }

        /// <summary>
        /// Rotation of the knob at value '0'
        /// </summary>
        public float MinAngle
        {
            get => _minAngle;
            set => _minAngle = value;
        }

        /// <summary>
        /// The position of the interactor controls rotation when outside this radius
        /// </summary>
        public float PositionTrackedRadius
        {
            get => _positionTrackedRadius;
            set => _positionTrackedRadius = value;
        }

        /// <summary>
        /// Events to trigger when the knob is rotated
        /// </summary>
        public ValueChangeEvent OnValueChange => _onValueChange;


        #region Unity Methods
        void Start()
        {
            SetValue(_value);
            SetKnobRotation(ValueToRotation());
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            selectEntered.AddListener(StartGrab);
            selectExited.AddListener(EndGrab);
        }

        protected override void OnDisable()
        {
            selectEntered.RemoveListener(StartGrab);
            selectExited.RemoveListener(EndGrab);
            base.OnDisable();
        }

        void OnDrawGizmosSelected()
        {
            const int k_CircleSegments = 16;
            const float k_SegmentRatio = 1.0f / k_CircleSegments;

            // Nothing to do if position radius is too small
            if (_positionTrackedRadius <= Mathf.Epsilon)
                return;

            // Draw a circle from the handle point at size of position tracked radius
            var circleCenter = transform.position;

            if (_handle != null)
                circleCenter = _handle.position;

            var circleX = transform.right;
            var circleY = transform.forward;

            Gizmos.color = Color.green;
            var segmentCounter = 0;
            while (segmentCounter < k_CircleSegments)
            {
                var startAngle = (float)segmentCounter * k_SegmentRatio * 2.0f * Mathf.PI;
                segmentCounter++;
                var endAngle = (float)segmentCounter * k_SegmentRatio * 2.0f * Mathf.PI;

                Gizmos.DrawLine(circleCenter + (Mathf.Cos(startAngle) * circleX + Mathf.Sin(startAngle) * circleY) * _positionTrackedRadius,
                    circleCenter + (Mathf.Cos(endAngle) * circleX + Mathf.Sin(endAngle) * circleY) * _positionTrackedRadius);
            }
        }

        void OnValidate()
        {
            if (_clampedMotion)
                _value = Mathf.Clamp01(_value);

            if (_minAngle > _maxAngle)
                _minAngle = _maxAngle;

            SetKnobRotation(ValueToRotation());
        }
        #endregion


        private void StartGrab(SelectEnterEventArgs args)
        {
            _interactor = args.interactorObject;

            _positionAngles.Reset();
            _upVectorAngles.Reset();
            _forwardVectorAngles.Reset();

            UpdateBaseKnobRotation();
            UpdateRotation(true);
        }

        private void EndGrab(SelectExitEventArgs args)
        {
            _interactor = null;
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);

            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                if (isSelected)
                {
                    UpdateRotation();
                }
            }
        }

        private void UpdateRotation(bool freshCheck = false)
        {
            // Are we in position offset or direction rotation mode?
            var interactorTransform = _interactor.GetAttachTransform(this);

            // We cache the three potential sources of rotation - the position offset, the forward vector of the controller, and up vector of the controller
            // We store any data used for determining which rotation to use, then flatten the vectors to the local xz plane
            var localOffset = transform.InverseTransformVector(interactorTransform.position - _handle.position);
            localOffset.y = 0.0f;
            var radiusOffset = transform.TransformVector(localOffset).magnitude;
            localOffset.Normalize();

            var localForward = transform.InverseTransformDirection(interactorTransform.forward);
            var localY = Math.Abs(localForward.y);
            localForward.y = 0.0f;
            localForward.Normalize();

            var localUp = transform.InverseTransformDirection(interactorTransform.up);
            localUp.y = 0.0f;
            localUp.Normalize();


            if (_positionDriven && !freshCheck)
                radiusOffset *= (1.0f + _modeSwitchDeadZone);

            // Determine when a certain source of rotation won't contribute - in that case we bake in the offset it has applied
            // and set a new anchor when they can contribute again
            if (radiusOffset >= _positionTrackedRadius)
            {
                if (!_positionDriven || freshCheck)
                {
                    _positionAngles.SetBaseFromVector(localOffset);
                    _positionDriven = true;
                }
            }
            else
                _positionDriven = false;

            // If it's not a fresh check, then we weight the local Y up or down to keep it from flickering back and forth at boundaries
            if (!freshCheck)
            {
                if (!_upVectorDriven)
                    localY *= (1.0f - (_modeSwitchDeadZone * 0.5f));
                else
                    localY *= (1.0f + (_modeSwitchDeadZone * 0.5f));
            }

            if (localY > 0.707f)
            {
                if (!_upVectorDriven || freshCheck)
                {
                    _upVectorAngles.SetBaseFromVector(localUp);
                    _upVectorDriven = true;
                }
            }
            else
            {
                if (_upVectorDriven || freshCheck)
                {
                    _forwardVectorAngles.SetBaseFromVector(localForward);
                    _upVectorDriven = false;
                }
            }

            // Get angle from position
            if (_positionDriven)
                _positionAngles.SetTargetFromVector(localOffset);

            if (_upVectorDriven)
                _upVectorAngles.SetTargetFromVector(localUp);
            else
                _forwardVectorAngles.SetTargetFromVector(localForward);

            // Apply offset to base knob rotation to get new knob rotation
            var knobRotation = _baseKnobRotation - ((_upVectorAngles.TotalOffset + _forwardVectorAngles.TotalOffset) * _twistSensitivity) - _positionAngles.TotalOffset;

            // Clamp to range
            if (_clampedMotion)
                knobRotation = Mathf.Clamp(knobRotation, _minAngle, _maxAngle);

            SetKnobRotation(knobRotation);

            // Reverse to get value
            var knobValue = (knobRotation - _minAngle) / (_maxAngle - _minAngle);
            SetValue(knobValue);
        }

        private void SetKnobRotation(float angle)
        {
            if (_angleIncrement > 0)
            {
                var normalizeAngle = angle - _minAngle;
                angle = (Mathf.Round(normalizeAngle / _angleIncrement) * _angleIncrement) + _minAngle;
            }

            if (_handle != null)
                _handle.localEulerAngles = new Vector3(0.0f, angle, 0.0f);
        }

        private void SetValue(float value)
        {
            if (_clampedMotion)
                value = Mathf.Clamp01(value);

            if (_angleIncrement > 0)
            {
                var angleRange = _maxAngle - _minAngle;
                var angle = Mathf.Lerp(0.0f, angleRange, value);
                angle = Mathf.Round(angle / _angleIncrement) * _angleIncrement;
                value = Mathf.InverseLerp(0.0f, angleRange, angle);
            }

            _value = value;
            _onValueChange.Invoke(_value);
        }

        private float ValueToRotation()
        {
            return _clampedMotion ? Mathf.Lerp(_minAngle, _maxAngle, _value) : Mathf.LerpUnclamped(_minAngle, _maxAngle, _value);
        }

        private void UpdateBaseKnobRotation()
        {
            _baseKnobRotation = Mathf.LerpUnclamped(_minAngle, _maxAngle, _value);
        }

        private static float ShortestAngleDistance(float start, float end, float max)
        {
            var angleDelta = end - start;
            var angleSign = Mathf.Sign(angleDelta);

            angleDelta = Math.Abs(angleDelta) % max;
            if (angleDelta > (max * 0.5f))
                angleDelta = -(max - angleDelta);

            return angleDelta * angleSign;
        }


        /// <summary>
        /// Helper class used to track rotations that can go beyond 180 degrees while minimizing accumulation error
        /// </summary>
        struct TrackedRotation
        {
            /// <summary>
            /// The anchor rotation we calculate an offset from
            /// </summary>
            float _baseAngle;

            /// <summary>
            /// The target rotate we calculate the offset to
            /// </summary>
            float _currentOffset;

            /// <summary>
            /// Any previous offsets we've added in
            /// </summary>
            float _accumulatedAngle;

            /// <summary>
            /// The total rotation that occurred from when this rotation started being tracked
            /// </summary>
            public float TotalOffset => _accumulatedAngle + _currentOffset;

            /// <summary>
            /// Resets the tracked rotation so that total offset returns 0
            /// </summary>
            public void Reset()
            {
                _baseAngle = 0.0f;
                _currentOffset = 0.0f;
                _accumulatedAngle = 0.0f;
            }

            /// <summary>
            /// Sets a new anchor rotation while maintaining any previously accumulated offset
            /// </summary>
            /// <param name="direction">The XZ vector used to calculate a rotation angle</param>
            public void SetBaseFromVector(Vector3 direction)
            {
                // Update any accumulated angle
                _accumulatedAngle += _currentOffset;

                // Now set a new base angle
                _baseAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
                _currentOffset = 0.0f;
            }

            public void SetTargetFromVector(Vector3 direction)
            {
                // Set the target angle
                var targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

                // Return the offset
                _currentOffset = ShortestAngleDistance(_baseAngle, targetAngle, 360.0f);

                // If the offset is greater than 90 degrees, we update the base so we can rotate beyond 180 degrees
                if (Mathf.Abs(_currentOffset) > 90.0f)
                {
                    _baseAngle = targetAngle;
                    _accumulatedAngle += _currentOffset;
                    _currentOffset = 0.0f;
                }
            }
        }
    }
}
#endif