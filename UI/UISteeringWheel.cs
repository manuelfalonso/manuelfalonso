using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SombraStudios.Shared.UI
{
    /// <summary>
    /// Represents a UI steering wheel control.
    /// Credits to yasirkula for the original script.
    /// https://forum.unity.com/threads/touchscreen-steering-wheel-rotation-example-mouse-supported.196741/
    /// </summary>
    public class UISteeringWheel : MonoBehaviour
    {
        [Header("References")]
        /// <summary>
        /// Image that will act as the steering wheel.
        /// </summary>
        [Tooltip("Image that will act as the steering wheel")]
        public Graphic UI_Element;

        [Header("Settings")]
        /// <summary>
        /// Maximum steering wheel angle in degrees from center rotation.
        /// </summary>
        [Tooltip("Maximum steering wheel angle in degrees from center rotation")]
        public float MaximumSteeringAngle = 180f;
        /// <summary>
        /// Wheel angle at which the motor starts rotating.
        /// </summary>
        [Tooltip("Wheel angle at which the motor starts rotating")]
        public float WheelReleasedSpeed = 200f;

        [Header("Events")]
        /// <summary>
        /// Event invoked when the clamped value of the steering wheel changes.
        /// </summary>
        [Tooltip("Event invoked when the clamped value of the steering wheel changes")]
        public UnityEvent<float> OnSteeringWheelClampedValueChanged;
        /// <summary>
        /// Event invoked when the angle of the steering wheel changes.
        /// </summary>
        [Tooltip("Event invoked when the angle of the steering wheel changes")]
        public UnityEvent<float> OnSteeringWheelAngleChanged;
        /// <summary>
        /// Event invoked when the steering wheel is being held.
        /// </summary>
        [Tooltip("Event invoked when the steering wheel is being held")]
        public UnityEvent OnSteeringWheelHeld;
        /// <summary>
        /// Event invoked when the steering wheel is released.
        /// </summary>
        [Tooltip("Event invoked when the steering wheel is released")]
        public UnityEvent OnSteeringWheelReleased;

        private RectTransform _rectT;
        private Vector2 _centerPoint;

        private float _wheelAngle = 0f;
        private float _wheelPrevAngle = 0f;

        private bool _wheelBeingHeld = false;

        public float WheelAngle
        {
            get { return _wheelAngle; }
            set
            {
                if (_wheelAngle == value) return;
                _wheelAngle = value;
                OnSteeringWheelAngleChanged?.Invoke(GetAngle());
                OnSteeringWheelClampedValueChanged?.Invoke(GetClampedValue());
            }
        }


        #region Unity Messages
        void Start()
        {
            _rectT = UI_Element.rectTransform;
            InitEventsSystem();
        }

        void Update()
        {
            ApplyReleaseRotation();
            ApplyRotation();
        }
        #endregion


        #region Public Methods
        /// <summary>
        /// Gets the clamped value of the steering wheel.
        /// </summary>
        /// <returns>The clamped value in the range [-1, 1].</returns>
        public float GetClampedValue()
        {
            // returns a value in range [-1,1] similar to GetAxis("Horizontal")
            return WheelAngle / MaximumSteeringAngle;
        }

        /// <summary>
        /// Gets the angle of the steering wheel.
        /// </summary>
        /// <returns>The angle of the steering wheel.</returns>
        public float GetAngle()
        {
            // returns the wheel angle itself without clamp operation
            return WheelAngle;
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Initializes the event system for the steering wheel.
        /// </summary>
        private void InitEventsSystem()
        {
            // Warning: Be ready to see some extremely boring code here :-/
            // You are warned!

            if (!UI_Element.gameObject.TryGetComponent<EventTrigger>(out var events))
                events = UI_Element.gameObject.AddComponent<EventTrigger>();

            if (events.triggers == null)
                events.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();

            // PointerDown event
            AddEventTrigger(EventTriggerType.PointerDown, PressEvent);

            // Drag event
            AddEventTrigger(EventTriggerType.Drag, DragEvent);

            // PointerUp event
            AddEventTrigger(EventTriggerType.PointerUp, ReleaseEvent);
        }

        /// <summary>
        /// Adds an event trigger to the UI element.
        /// </summary>
        /// <param name="eventID">The type of event to trigger.</param>
        /// <param name="callback">The callback method for the event.</param>
        private void AddEventTrigger(EventTriggerType eventID, UnityAction<BaseEventData> callback)
        {
            EventTrigger.Entry entry = new();
            EventTrigger.TriggerEvent triggerEvent = new();
            triggerEvent.AddListener(callback);
            entry.eventID = eventID;
            entry.callback = triggerEvent;
            if (UI_Element.gameObject.TryGetComponent(out EventTrigger events))
                events.triggers.Add(entry);
        }

        /// <summary>
        /// Applies rotation when the wheel is released.
        /// </summary>
        private void ApplyReleaseRotation()
        {
            // If the wheel is released, reset the rotation
            // to initial (zero) rotation by wheelReleasedSpeed degrees per second
            if (!_wheelBeingHeld && !Mathf.Approximately(0f, WheelAngle))
            {
                float deltaAngle = WheelReleasedSpeed * Time.deltaTime;
                if (Mathf.Abs(deltaAngle) > Mathf.Abs(WheelAngle))
                    WheelAngle = 0f;
                else if (WheelAngle > 0f)
                    WheelAngle -= deltaAngle;
                else
                    WheelAngle += deltaAngle;
            }
        }

        /// <summary>
        /// Applies rotation to the UI element.
        /// </summary>
        private void ApplyRotation()
        {
            // Rotate the wheel image
            _rectT.localEulerAngles = Vector3.back * WheelAngle;
        }
        #endregion


        #region Event System
        /// <summary>
        /// Handles the pointer down event.
        /// </summary>
        /// <param name="eventData">The event data.</param>
        public void PressEvent(BaseEventData eventData)
        {
            // Executed when mouse/finger starts touching the steering wheel
            Vector2 pointerPos = ((PointerEventData)eventData).position;

            _wheelBeingHeld = true;
            OnSteeringWheelHeld?.Invoke();
            _centerPoint = RectTransformUtility.WorldToScreenPoint(((PointerEventData)eventData).pressEventCamera, _rectT.position);
            _wheelPrevAngle = Vector2.Angle(Vector2.up, pointerPos - _centerPoint);
        }

        /// <summary>
        /// Handles the drag event.
        /// </summary>
        /// <param name="eventData">The event data.</param>
        public void DragEvent(BaseEventData eventData)
        {
            // Executed when mouse/finger is dragged over the steering wheel
            Vector2 pointerPos = ((PointerEventData)eventData).position;

            float wheelNewAngle = Vector2.Angle(Vector2.up, pointerPos - _centerPoint);
            // Do nothing if the pointer is too close to the center of the wheel
            if (Vector2.Distance(pointerPos, _centerPoint) > 20f)
            {
                if (pointerPos.x > _centerPoint.x)
                    WheelAngle += wheelNewAngle - _wheelPrevAngle;
                else
                    WheelAngle -= wheelNewAngle - _wheelPrevAngle;
            }
            // Make sure wheel angle never exceeds maximumSteeringAngle
            WheelAngle = Mathf.Clamp(WheelAngle, -MaximumSteeringAngle, MaximumSteeringAngle);
            _wheelPrevAngle = wheelNewAngle;
        }

        /// <summary>
        /// Handles the pointer up event.
        /// </summary>
        /// <param name="eventData">The event data.</param>
        public void ReleaseEvent(BaseEventData eventData)
        {
            // Executed when mouse/finger stops touching the steering wheel
            // Performs one last DragEvent, just in case
            DragEvent(eventData);

            _wheelBeingHeld = false;
            OnSteeringWheelReleased?.Invoke();
        }
        #endregion
    }
}
