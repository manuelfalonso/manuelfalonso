using SombraStudios.Shared.Attributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

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
        [SerializeField] protected PhysicInteractionEventType _eventType = PhysicInteractionEventType.None;
        /// <summary>
        /// Frequency for Stay events (in FixedUpdate cycles). Higher values = less frequent calls.
        /// </summary>
        [Tooltip("Frequency for Stay events (in FixedUpdate cycles). Higher values = less frequent calls.")]
        [SerializeField] protected int _stayEventFrequency = 1;
        /// <summary>
        /// Wheter the component is calculating physics interactions or not.
        /// </summary>        
        [Tooltip("Wheter the component is calculating physics interactions or not.")]
        [SerializeField] protected bool _isActive = true;

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
        /// <summary>
        /// Invoked when the first interaction enter occurs while the interaction count goes from 0 to 1.
        /// </summary>
        [Tooltip("Invoked when the first interaction enter occurs while the interaction count goes from 0 to 1.")]
        public UnityEvent<T> InteractionOnFirstEnter = new();
        /// <summary>
        /// Invoked when the last interaction exit occurs while the interaction count goes from 1 to 0.
        /// </summary>
        [Tooltip("Invoked when the last interaction exit occurs while the interaction count goes from 1 to 0.")]
        public UnityEvent<T> InteractionOnLastExit = new();

        [Header("Base Debug")]
        [SerializeField] private bool _showLogs;
        /// <summary>
        /// The Transform of the last object that interact with this one.
        /// </summary>
        [Tooltip("The Transform of the last object that interact with this one.")]
        [SerializeField] protected Transform _lastInteraction;

        public bool IsActive { get => _isActive; set => _isActive = value; }
        public Transform LastInteraction => _lastInteraction;
        public int CurrentInteractionsCount => _currentInteractions.Count;

        // Optimized Stay event tracking
        protected readonly HashSet<T> _currentInteractions = new();
        protected int _stayEventCounter = 0;

        private bool _nullsInInteractions;

        #region Unity Messages
        protected virtual void FixedUpdate()
        {
            HandleStayInteractions();
        }
        #endregion

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
            InteractionOnEnter?.RemoveAllListeners();
            InteractionOnStay?.RemoveAllListeners();
            InteractionOnExit?.RemoveAllListeners();
        }
        #endregion


        #region Protected Methods
        /// <summary>
        /// Determines whether the stay interaction is valid for the specified component.
        /// </summary>
        /// <param name="component">The component to evaluate for the validity of the stay interaction.</param>
        /// <returns><see langword="true"/> if the stay interaction is valid for the specified component; otherwise, <see
        /// langword="false"/>.</returns>
        protected abstract bool IsStayInteractionValid(T component);

        /// <summary>
        /// Checks if the interaction should be processed based on various conditions.
        /// </summary>
        /// <param name="component">The component to check.</param>
        /// <returns>True if the interaction should be processed, false otherwise.</returns>
        protected virtual bool IsInteractionValid(GameObject component)
        {
            if (!_isActive) { return false; }
            if (!IsLayerMaskValid(component)) { return false; }
            if (!IsTagValid(component)) { return false; }
            _lastInteraction = component.transform;
            return true;
        }

        /// <summary>
        /// Handles Enter events and adds to tracking for Stay events.
        /// </summary>
        /// <param name="component">The component that entered.</param>
        protected virtual void HandleEnterInteractions(T component)
        {
            if (_eventType == PhysicInteractionEventType.None) { return; }
            _currentInteractions.Add(component);

            if (_showLogs)
                Logger.Log($"Added {component} to current interactions. Total now: {_currentInteractions.Count}");
        }

        /// <summary>
        /// Handles Exit events and removes from tracking for Stay events.
        /// </summary>
        /// <param name="component">The component that exited.</param>
        protected virtual void HandleExitInteractions(T component)
        {
            if (_eventType == PhysicInteractionEventType.None) { return; }
            _currentInteractions.Remove(component);

            if (_showLogs)
                Logger.Log($"Removed {component} from current interactions. Total now: {_currentInteractions.Count}");
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Handles "Stay" interactions by processing active interactions and invoking the associated event.
        /// </summary>
        /// <remarks>This method processes "Stay" events only if they are enabled, there are active
        /// interactions,  and the associated event has subscribers. The frequency of event invocation is throttled
        /// based  on the configured stay event frequency. Null interactions are removed from the collection after
        /// processing.</remarks>
        private void HandleStayInteractions()
        {
            // Only process if Stay events are enabled and there are active interactions
            if (!_eventType.HasFlag(PhysicInteractionEventType.Stay) ||
                _currentInteractions.Count == 0 ||
                InteractionOnStay.GetPersistentEventCount() == 0)
                return;

            // Throttle Stay events based on frequency setting
            if (++_stayEventCounter < _stayEventFrequency)
                return;

            _stayEventCounter = 0;

            // Process Stay events for all current interactions
            foreach (var interaction in _currentInteractions)
            {
                if (interaction != null)
                {
                    if (!IsStayInteractionValid(interaction)) { return; }
                    InteractionOnStay?.Invoke(interaction);
                }
                else
                {
                    _nullsInInteractions = true;
                }
            }

            if (_nullsInInteractions)
            {
                _currentInteractions.RemoveWhere(interaction => interaction == null);
                _nullsInInteractions = false;
            }
        }

        /// <summary>
        /// Checks if the layer of the component is valid.
        /// </summary>
        /// <param name="component">The component to check.</param>
        /// <returns>True if the layer is valid, false otherwise.</returns>
        private bool IsLayerMaskValid(GameObject component)
        {
            return (_layerMask == (_layerMask | (1 << component.layer)));
        }

        /// <summary>
        /// Checks if the tag of the collider is valid.
        /// </summary>
        /// <param name="component">The component to check.</param>
        /// <returns>True if the tag is valid, false otherwise.</returns>
        private bool IsTagValid(GameObject component)
        {
            return string.IsNullOrEmpty(_requiredTag) || component.CompareTag(_requiredTag);
        }
        #endregion
    }
}
