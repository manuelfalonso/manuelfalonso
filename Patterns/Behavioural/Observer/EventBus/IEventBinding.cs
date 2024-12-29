using System;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.EventBus
{
    /// <summary>
    /// Interface for event bindings in the event bus system.
    /// </summary>
    /// <typeparam name="T">The type of the event data.</typeparam>
    public interface IEventBinding<T>
    {
        /// <summary>
        /// Gets or sets the action to be invoked when the event with parameters is raised.
        /// </summary>
        public Action<T> OnEventRaised { get; set; }

        /// <summary>
        /// Gets or sets the action to be invoked when the event without parameters is raised.
        /// </summary>
        public Action OnEventRaisedNoParams { get; set; }
    }
}