using System;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.EventBus
{
    /// <summary>
    /// Represents a binding for events in the event bus system.
    /// </summary>
    /// <typeparam name="T">The type of the event data.</typeparam>
    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        /// <summary>
        /// The action to be invoked when the event with parameters is raised.
        /// </summary>
        private Action<T> _onEventRaised = delegate { };

        /// <summary>
        /// The action to be invoked when the event without parameters is raised.
        /// </summary>
        private Action _onEventRaisedNoParams = delegate { };

        /// <summary>
        /// Gets or sets the action to be invoked when the event with parameters is raised.
        /// </summary>
        public Action<T> OnEventRaised
        {
            get => _onEventRaised;
            set => _onEventRaised = value;
        }

        /// <summary>
        /// Gets or sets the action to be invoked when the event without parameters is raised.
        /// </summary>
        public Action OnEventRaisedNoParams
        {
            get => _onEventRaisedNoParams;
            set => _onEventRaisedNoParams = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventBinding{T}"/> class with the specified action
        /// for events with parameters.
        /// </summary>
        /// <param name="onEventRaised">The action to be invoked when the event with parameters is raised.</param>
        public EventBinding(Action<T> onEventRaised) => _onEventRaised = onEventRaised;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventBinding{T}"/> class with the specified action
        /// for events without parameters.
        /// </summary>
        /// <param name="onEventRaisedNoParams">The action to be invoked when the event without parameters
        /// is raised.</param>
        public EventBinding(Action onEventRaisedNoParams) => _onEventRaisedNoParams = onEventRaisedNoParams;

        /// <summary>
        /// Adds the specified action to the invocation list for events with parameters.
        /// </summary>
        /// <param name="onEventRaised">The action to add.</param>
        public void Add(Action<T> onEventRaised) => _onEventRaised += onEventRaised;

        /// <summary>
        /// Removes the specified action from the invocation list for events with parameters.
        /// </summary>
        /// <param name="onEventRaised">The action to remove.</param>
        public void Remove(Action<T> onEventRaised) => _onEventRaised -= onEventRaised;

        /// <summary>
        /// Adds the specified action to the invocation list for events without parameters.
        /// </summary>
        /// <param name="onEventRaisedNoParams">The action to add.</param>
        public void Add(Action onEventRaisedNoParams) => _onEventRaisedNoParams += onEventRaisedNoParams;

        /// <summary>
        /// Removes the specified action from the invocation list for events without parameters.
        /// </summary>
        /// <param name="onEventRaisedNoParams">The action to remove.</param>
        public void Remove(Action onEventRaisedNoParams) => _onEventRaisedNoParams -= onEventRaisedNoParams;
    }
}