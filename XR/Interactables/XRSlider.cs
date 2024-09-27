#if UNITY_XR_INTERACTION_TOOLKIT
using System;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

namespace SombraStudios.Shared.XR.Interactables
{
    /// <summary>
    /// An interactable that follows the position of the interactor on a single axis
    /// </summary>
    public class XRSlider : XRBaseInteractable
    {
        [Serializable] public class ValueChangeEvent : UnityEvent<float> { }
                
        [Tooltip("The object that is visually grabbed and manipulated")]
        [SerializeField] private Transform _handle = null;
                
        [Tooltip("The value of the slider")]
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _value = 0.5f;
        
        [Tooltip("The offset of the slider at value '1'")]
        [SerializeField] private float _maxPosition = 0.5f;
        
        [Tooltip("The offset of the slider at value '0'")]
        [SerializeField] private float _minPosition = -0.5f;
        
        [Tooltip("Events to trigger when the slider is moved")]
        [SerializeField] private ValueChangeEvent _onValueChange = new ValueChangeEvent();

        IXRSelectInteractor _interactor;

        /// <summary>
        /// The value of the slider
        /// </summary>
        public float Value
        {
            get => _value;
            set
            {
                SetValue(value);
                SetSliderPosition(value);
            }
        }

        /// <summary>
        /// Events to trigger when the slider is moved
        /// </summary>
        public ValueChangeEvent OnValueChange => _onValueChange;


        #region Unity Methods
        private void Start()
        {
            SetValue(_value);
            SetSliderPosition(_value);
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

        private void OnDrawGizmosSelected()
        {
            var sliderMinPoint = transform.TransformPoint(new Vector3(0.0f, 0.0f, _minPosition));
            var sliderMaxPoint = transform.TransformPoint(new Vector3(0.0f, 0.0f, _maxPosition));

            Gizmos.color = Color.green;
            Gizmos.DrawLine(sliderMinPoint, sliderMaxPoint);
        }

        private void OnValidate()
        {
            SetSliderPosition(_value);
        }
        #endregion


        private void StartGrab(SelectEnterEventArgs args)
        {
            _interactor = args.interactorObject;
            UpdateSliderPosition();
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
                    UpdateSliderPosition();
                }
            }
        }


        private void UpdateSliderPosition()
        {
            // Put anchor position into slider space
            var localPosition = transform.InverseTransformPoint(_interactor.GetAttachTransform(this).position);
            var sliderValue = Mathf.Clamp01((localPosition.z - _minPosition) / (_maxPosition - _minPosition));
            SetValue(sliderValue);
            SetSliderPosition(sliderValue);
        }

        private void SetSliderPosition(float value)
        {
            if (_handle == null)
                return;

            var handlePos = _handle.localPosition;
            handlePos.z = Mathf.Lerp(_minPosition, _maxPosition, value);
            _handle.localPosition = handlePos;
        }

        private void SetValue(float value)
        {
            _value = value;
            _onValueChange.Invoke(_value);
        }
    }
}
#endif