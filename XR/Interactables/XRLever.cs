#if UNITY_XR_INTERACTION_TOOLKIT
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
#if UNITY_6000_0_OR_NEWER
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
#endif

namespace SombraStudios.Shared.XR.Interactables
{
    /// <summary>
    /// An interactable lever that snaps into an on or off position by a direct interactor
    /// </summary>
    public class XRLever : XRBaseInteractable
    {
        const float _leverDeadZone = 0.1f; // Prevents rapid switching between on and off states when right in the middle

        [Tooltip("The object that is visually grabbed and manipulated")]
        [SerializeField] private Transform _handle = null;

        [Tooltip("The value of the lever")]
        [SerializeField] private bool _value = false;

        [Tooltip("If enabled, the lever will snap to the value position when released")]
        [SerializeField] private bool _lockToValue;

        [Tooltip("Angle of the lever in the 'on' position")]
        [Range(-90.0f, 90.0f)]
        [SerializeField] private float _maxAngle = 90.0f;

        [Tooltip("Angle of the lever in the 'off' position")]
        [Range(-90.0f, 90.0f)]
        [SerializeField] private float _minAngle = -90.0f;

        [Tooltip("Events to trigger when the lever activates")]
        [SerializeField] private UnityEvent _onLeverActivate = new UnityEvent();
        
        [Tooltip("Events to trigger when the lever deactivates")]
        [SerializeField] private UnityEvent _onLeverDeactivate = new UnityEvent();

        private IXRSelectInteractor _interactor;

        /// <summary>
        /// The object that is visually grabbed and manipulated
        /// </summary>
        public Transform Handle
        {
            get => _handle;
            set => _handle = value;
        }

        /// <summary>
        /// The value of the lever
        /// </summary>
        public bool Value
        {
            get => _value;
            set => SetValue(value, true);
        }

        /// <summary>
        /// If enabled, the lever will snap to the value position when released
        /// </summary>
        public bool LockToValue { get; set; }

        /// <summary>
        /// Angle of the lever in the 'on' position
        /// </summary>
        public float MaxAngle
        {
            get => _maxAngle;
            set => _maxAngle = value;
        }

        /// <summary>
        /// Angle of the lever in the 'off' position
        /// </summary>
        public float MinAngle
        {
            get => _minAngle;
            set => _minAngle = value;
        }

        /// <summary>
        /// Events to trigger when the lever activates
        /// </summary>
        public UnityEvent OnLeverActivate => _onLeverActivate;

        /// <summary>
        /// Events to trigger when the lever deactivates
        /// </summary>
        public UnityEvent OnLeverDeactivate => _onLeverDeactivate;


        #region Unity Methods
        void Start()
        {
            SetValue(_value, true);
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
            var angleStartPoint = transform.position;

            if (_handle != null)
                angleStartPoint = _handle.position;

            const float k_AngleLength = 0.25f;

            var angleMaxPoint = angleStartPoint + transform.TransformDirection(Quaternion.Euler(_maxAngle, 0.0f, 0.0f) * Vector3.up) * k_AngleLength;
            var angleMinPoint = angleStartPoint + transform.TransformDirection(Quaternion.Euler(_minAngle, 0.0f, 0.0f) * Vector3.up) * k_AngleLength;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(angleStartPoint, angleMaxPoint);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(angleStartPoint, angleMinPoint);
        }

        void OnValidate()
        {
            SetHandleAngle(_value ? _maxAngle : _minAngle);
        }
        #endregion


        void StartGrab(SelectEnterEventArgs args)
        {
            _interactor = args.interactorObject;
        }

        void EndGrab(SelectExitEventArgs args)
        {
            SetValue(_value, true);
            _interactor = null;
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);

            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                if (isSelected)
                {
                    UpdateValue();
                }
            }
        }

        Vector3 GetLookDirection()
        {
            Vector3 direction = _interactor.GetAttachTransform(this).position - _handle.position;
            direction = transform.InverseTransformDirection(direction);
            direction.x = 0;

            return direction.normalized;
        }

        void UpdateValue()
        {
            var lookDirection = GetLookDirection();
            var lookAngle = Mathf.Atan2(lookDirection.z, lookDirection.y) * Mathf.Rad2Deg;

            if (_minAngle < _maxAngle)
                lookAngle = Mathf.Clamp(lookAngle, _minAngle, _maxAngle);
            else
                lookAngle = Mathf.Clamp(lookAngle, _maxAngle, _minAngle);

            var maxAngleDistance = Mathf.Abs(_maxAngle - lookAngle);
            var minAngleDistance = Mathf.Abs(_minAngle - lookAngle);

            if (_value)
                maxAngleDistance *= (1.0f - _leverDeadZone);
            else
                minAngleDistance *= (1.0f - _leverDeadZone);

            var newValue = (maxAngleDistance < minAngleDistance);

            SetHandleAngle(lookAngle);

            SetValue(newValue);
        }

        void SetValue(bool isOn, bool forceRotation = false)
        {
            if (_value == isOn)
            {
                if (forceRotation)
                    SetHandleAngle(_value ? _maxAngle : _minAngle);

                return;
            }

            _value = isOn;

            if (_value)
            {
                _onLeverActivate.Invoke();
            }
            else
            {
                _onLeverDeactivate.Invoke();
            }

            if (!isSelected && (_lockToValue || forceRotation))
                SetHandleAngle(_value ? _maxAngle : _minAngle);
        }

        void SetHandleAngle(float angle)
        {
            if (_handle != null)
                _handle.localRotation = Quaternion.Euler(angle, 0.0f, 0.0f);
        }
    }
}
#endif