using SombraStudios.Shared.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility
{
    /// <summary>
    /// Handles trigger events and invokes corresponding UnityEvents based on specified layers.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class TriggerEventHandler : MonoBehaviour
    {
        [Header("Settings")]
        /// <summary>
        /// The layers that trigger events should respond to.
        /// </summary>
        [Tooltip("The layers that trigger events should respond to.")]
        [SerializeField] private LayerMask _triggerLayer;

        [Header("Debug")]
        /// <summary>
        /// Indicates whether the object is currently inside a trigger.
        /// </summary>
        [Tooltip("Indicates whether the object is currently inside a trigger.")]
        [SerializeField, ReadOnly] private bool _isTriggered = false;

        /// <summary>
        /// Event invoked when an object enters the trigger and belongs to the specified layers.
        /// </summary>
        [Tooltip("Event invoked when an object enters the trigger and belongs to the specified layers.")]
        public UnityEvent<Collider> TriggerEntered;
        /// <summary>
        /// Event invoked while an object stays inside the trigger and belongs to the specified layers.
        /// </summary>
        [Tooltip("Event invoked while an object stays inside the trigger and belongs to the specified layers.")]
        public UnityEvent<Collider> TriggerStayed;
        /// <summary>
        /// Event invoked when an object exits the trigger and belongs to the specified layers.
        /// </summary>
        [Tooltip("Event invoked when an object exits the trigger and belongs to the specified layers.")]
        public UnityEvent<Collider> TriggerExited;

        /// <summary>
        /// Indicates whether the object is currently inside a trigger.
        /// </summary>
        public bool IsTriggered { get => _isTriggered; private set => _isTriggered = value; }


        private void OnTriggerEnter(Collider other)
        {
            // Only consider colliders with the defined TriggerLayer
            if ((_triggerLayer & other.gameObject.layer) == 0) { return; }
            TriggerEntered?.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            // Only consider colliders with the defined TriggerLayer
            if ((_triggerLayer & other.gameObject.layer) == 0) { return; }
            _isTriggered = true;
            TriggerStayed?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            // Only consider colliders with the defined TriggerLayer
            if ((_triggerLayer & other.gameObject.layer) == 0) { return; }
            _isTriggered = false;
            TriggerExited?.Invoke(other);
        }
    }
}
