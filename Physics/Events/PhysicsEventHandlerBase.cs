using SombraStudios.Shared.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Physics.Events
{
    /// <summary>
    /// Base class for handling physics events.
    /// </summary>
    /// <typeparam name="T">The type of the component that will trigger the events.</typeparam>
    public abstract class PhysicsEventHandlerBase<T> : MonoBehaviour
    {
        [Header("Base Settings")]
        /// <summary>
        /// The LayerMask used to filter the interaction that will trigger the events.
        /// Only the interaction with objects whose layer is included in this LayerMask will trigger the events.
        /// </summary>
        [Tooltip("The LayerMask used to filter the interaction that will trigger the events." +
            "Only the interaction with objects whose layer is included in this LayerMask will trigger the events.")]
        [SerializeField]
        protected LayerMask _layerMask = -1;
        /// <summary>
        /// If set, this interaction will only fire if the other gameobject has this tag.
        /// </summary>
        [Tooltip("If set, this interaction will only fire if the other gameobject has this tag.")]
        [SerializeField, Tag]
        protected string _requiredTag;
        /// <summary>
        /// Type of interaction event to handle (Enter, Stay, Exit).
        /// </summary>
        [Tooltip("Type of interaction event to handle (Enter, Stay, Exit).")]
        [SerializeField] protected PhysicInteractionEventType _eventType = PhysicInteractionEventType.Enter;

        [Header("Events")]
        /// <summary>
        /// Event triggered when interaction is met on enter.
        /// </summary>
        [Tooltip("Event triggered when interaction is met on enter.")]
        public UnityEvent<T> InteractionOnEnter = new();
        /// <summary>
        /// Event triggered when interaction is met on stay.
        /// </summary>
        [Tooltip("Event triggered when interaction is met on stay.")]
        public UnityEvent<T> InteractionOnStay = new();
        /// <summary>
        /// Event triggered when interaction is met on exit.
        /// </summary>
        [Tooltip("Event triggered when interaction is met on exit.")]
        public UnityEvent<T> InteractionOnExit = new();

        [Header("Base Debug")]
        /// <summary>
        /// Wheter the component is calculating physics interactions or not.
        /// </summary>        
        [Tooltip("Wheter the component is calculating physics interactions or not.")]
        [SerializeField] protected bool _isActive = true;
        /// <summary>
        /// The Transform of the last object that interact with this one.
        /// </summary>
        [Tooltip("The Transform of the last object that interact with this one.")]
        [SerializeField] protected Transform _lastInteraction;

        public bool IsActive { get => _isActive; set => _isActive = value; }
        public Transform LastInteraction => _lastInteraction;


        #region Public Methods
        /// <summary>
        /// Adds interaction listeners to the corresponding UnityEvents.
        /// </summary>
        /// <param name="onEnter">Action to be performed on interaction enter event.</param>
        /// <param name="onStay">Action to be performed on interaction stay event.</param>
        /// <param name="onExit">Action to be performed on interaction exit event.</param>
        public void AddInteractionListeners(
            UnityAction<T> onEnter, UnityAction<T> onStay, UnityAction<T> onExit)
        {
            if (onEnter != null) { InteractionOnEnter.AddListener(onEnter); }
            if (onStay != null) { InteractionOnStay.AddListener(onStay); }
            if (onExit != null) { InteractionOnExit.AddListener(onExit); }
        }

        /// <summary>
        /// Removes interaction listeners from the corresponding UnityEvents.
        /// </summary>
        /// <param name="onEnter">Action to be removed from interaction enter event.</param>
        /// <param name="onStay">Action to be removed from interaction stay event.</param>
        /// <param name="onExit">Action to be removed from interaction exit event.</param>
        public void RemoveInteractionListeners(
            UnityAction<T> onEnter, UnityAction<T> onStay, UnityAction<T> onExit)
        {
            if (onEnter != null) { InteractionOnEnter.RemoveListener(onEnter); }
            if (onStay != null) { InteractionOnStay.RemoveListener(onStay); }
            if (onExit != null) { InteractionOnExit.RemoveListener(onExit); }
        }

        /// <summary>
        /// Clears all interaction listeners from the UnityEvents.
        /// </summary>
        public void RemoveAllInteractionListeners()
        {
            InteractionOnEnter.RemoveAllListeners();
            InteractionOnStay.RemoveAllListeners();
            InteractionOnExit.RemoveAllListeners();
        }
        #endregion


        #region Protected Methods
        /// <summary>
        /// Checks if the specified interaction event type should be processed.
        /// </summary>
        /// <param name="interactionEventType">The interaction event type to check.</param>
        /// <returns>True if the event type should be processed, false otherwise.</returns>
        protected bool IsCollisionEventType(PhysicInteractionEventType interactionEventType)
        {
            return _eventType != PhysicInteractionEventType.None && (_eventType & interactionEventType) != 0;
        }
        #endregion

    }
}
