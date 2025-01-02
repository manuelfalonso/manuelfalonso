#if UNITY_XR_INTERACTION_TOOLKIT
using System;
using System.Collections.Generic;
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
    /// An interactable that can be pushed by a direct interactor's movement
    /// </summary>
    public class XRPushButton : XRBaseInteractable
    {
        class PressInfo
        {
            internal IXRHoverInteractor _interactor;
            internal bool _inPressRegion = false;
            internal bool _wrongSide = false;
        }

        [Serializable] public class ValueChangeEvent : UnityEvent<float> { }
                
        [Tooltip("The object that is visually pressed down")]
        [SerializeField] private Transform _button = null;
                
        [Tooltip("The distance the button can be pressed")]
        [SerializeField] private float _pressDistance = 0.1f;
                
        [Tooltip("Extra distance for clicking the button down")]
        [SerializeField] private float _pressBuffer = 0.01f;

        [Tooltip("Offset from the button base to start testing for push")]
        [SerializeField] private float _buttonOffset = 0.0f;

        [Tooltip("How big of a surface area is available for pressing the button")]
        [SerializeField] private float _buttonSize = 0.1f;

        [Tooltip("Treat this button like an on/off toggle")]
        [SerializeField] private bool _toggleButton = false;

        [Tooltip("Events to trigger when the button is pressed")]
        [SerializeField] private UnityEvent _onPress;

        [Tooltip("Events to trigger when the button is released")]
        [SerializeField] private UnityEvent _onRelease;

        [Tooltip("Events to trigger when the button pressed value is updated. Only called when the button is pressed")]
        [SerializeField] private ValueChangeEvent _onValueChange;

        private bool _pressed = false;
        private bool _toggled = false;
        private float _value = 0f;
        private Vector3 _baseButtonPosition = Vector3.zero;

        private Dictionary<IXRHoverInteractor, PressInfo> m_HoveringInteractors = new ();

        /// <summary>
        /// The object that is visually pressed down
        /// </summary>
        public Transform Button
        {
            get => _button;
            set => _button = value;
        }

        /// <summary>
        /// The distance the button can be pressed
        /// </summary>
        public float PressDistance
        {
            get => _pressDistance;
            set => _pressDistance = value;
        }

        /// <summary>
        /// The distance (in percentage from 0 to 1) the button is currently being held down
        /// </summary>
        public float Value => _value;

        /// <summary>
        /// Events to trigger when the button is pressed
        /// </summary>
        public UnityEvent OnPress => _onPress;

        /// <summary>
        /// Events to trigger when the button is released
        /// </summary>
        public UnityEvent OnRelease => _onRelease;

        /// <summary>
        /// Events to trigger when the button distance value is changed. Only called when the button is pressed
        /// </summary>
        public ValueChangeEvent OnValueChange => _onValueChange;

        /// <summary>
        /// Whether or not a toggle button is in the locked down position
        /// </summary>
        public bool ToggleValue
        {
            get => _toggleButton && _toggled;
            set
            {
                if (!_toggleButton) { return; }                    

                _toggled = value;
                if (_toggled)
                    SetButtonHeight(-_pressDistance);
                else
                    SetButtonHeight(0.0f);
            }
        }


        #region Unity Methods
        protected override void OnEnable()
        {
            base.OnEnable();

            if (_toggled)
                SetButtonHeight(-_pressDistance);
            else
                SetButtonHeight(0.0f);

            hoverEntered.AddListener(StartHover);
            hoverExited.AddListener(EndHover);
        }

        void Start()
        {
            if (_button != null)
                _baseButtonPosition = _button.position;
        }

        protected override void OnDisable()
        {
            hoverEntered.RemoveListener(StartHover);
            hoverExited.RemoveListener(EndHover);
            base.OnDisable();
        }

        private void OnDrawGizmosSelected()
        {
            var pressStartPoint = Vector3.zero;

            if (_button != null)
            {
                pressStartPoint = _button.localPosition;
            }

            pressStartPoint.y += _buttonOffset - (_pressDistance * 0.5f);

            Gizmos.color = Color.green;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(pressStartPoint, new Vector3(_buttonSize, _pressDistance, _buttonSize));
        }

        private void OnValidate()
        {
            SetButtonHeight(0.0f);
        }
        #endregion


        public override bool IsHoverableBy(IXRHoverInteractor interactor)
        {
            if (interactor is XRRayInteractor)
                return false;

            return base.IsHoverableBy(interactor);
        }


        private void StartHover(HoverEnterEventArgs args)
        {
            m_HoveringInteractors.Add(args.interactorObject, new PressInfo { _interactor = args.interactorObject });
        }

        private void EndHover(HoverExitEventArgs args)
        {
            m_HoveringInteractors.Remove(args.interactorObject);

            if (m_HoveringInteractors.Count == 0)
            {
                if (_toggleButton && _toggled)
                    SetButtonHeight(-_pressDistance);
                else
                    SetButtonHeight(0.0f);
            }
        }


        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);

            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                if (m_HoveringInteractors.Count > 0)
                {
                    UpdatePress();
                }
            }
        }


        private void UpdatePress()
        {
            var minimumHeight = 0.0f;

            if (_toggleButton && _toggled)
                minimumHeight = -_pressDistance;

            // Go through each interactor
            foreach (var pressInfo in m_HoveringInteractors.Values)
            {
                var interactorTransform = pressInfo._interactor.GetAttachTransform(this);
                var localOffset = transform.InverseTransformVector(interactorTransform.position - _baseButtonPosition);

                var withinButtonRegion = (Mathf.Abs(localOffset.x) < _buttonSize && Mathf.Abs(localOffset.z) < _buttonSize);
                if (withinButtonRegion)
                {
                    if (!pressInfo._inPressRegion)
                    {
                        pressInfo._wrongSide = (localOffset.y < _buttonOffset);
                    }

                    if (!pressInfo._wrongSide)
                        minimumHeight = Mathf.Min(minimumHeight, localOffset.y - _buttonOffset);
                }

                pressInfo._inPressRegion = withinButtonRegion;
            }

            minimumHeight = Mathf.Max(minimumHeight, -(_pressDistance + _pressBuffer));

            // If button height goes below certain amount, enter press mode
            var pressed = _toggleButton ? (minimumHeight <= -(_pressDistance + _pressBuffer)) : (minimumHeight < -_pressDistance);

            var currentDistance = Mathf.Max(0f, -minimumHeight - _pressBuffer);
            _value = currentDistance / _pressDistance;

            if (_toggleButton)
            {
                if (pressed)
                {
                    if (!_pressed)
                    {
                        _toggled = !_toggled;

                        if (_toggled)
                            _onPress.Invoke();
                        else
                            _onRelease.Invoke();
                    }
                }
            }
            else
            {
                if (pressed)
                {
                    if (!_pressed)
                        _onPress.Invoke();
                }
                else
                {
                    if (_pressed)
                        _onRelease.Invoke();
                }
            }
            _pressed = pressed;

            // Call value change event
            if (_pressed)
                _onValueChange.Invoke(_value);

            SetButtonHeight(minimumHeight);
        }

        private void SetButtonHeight(float height)
        {
            if (_button == null)
                return;

            Vector3 newPosition = _button.localPosition;
            newPosition.y = height;
            _button.localPosition = newPosition;
        }
    }
}
#endif