using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.EventBus
{
    // Credits to git-ammend
    /// <summary>
    /// A static class that represents an event bus for managing event bindings and raising events.
    /// </summary>
    /// <typeparam name="T">The type of the event data, which must implement the IEvent interface.</typeparam>
    public static class EventBus<T> where T : IEvent
    {
        /// <summary>
        /// A set of event bindings registered with the event bus.
        /// </summary>
        static readonly HashSet<IEventBinding<T>> _eventBindings = new();

        /// <summary>
        /// Registers an event binding with the event bus.
        /// </summary>
        /// <param name="binding">The event binding to register.</param>
        public static void Register(IEventBinding<T> binding) => _eventBindings.Add(binding);

        /// <summary>
        /// Unregisters an event binding from the event bus.
        /// </summary>
        /// <param name="binding">The event binding to unregister.</param>
        public static void Unregister(IEventBinding<T> binding) => _eventBindings.Remove(binding);

        /// <summary>
        /// Raises an event, invoking all registered event bindings.
        /// </summary>
        /// <param name="event">The event to raise.</param>
        public static void Raise(T @event)
        {
            var snapshot = new HashSet<IEventBinding<T>>(_eventBindings);

            foreach (var binding in snapshot)
            {
                if (_eventBindings.Contains(binding))
                {
                    binding.OnEventRaised.Invoke(@event);
                    binding.OnEventRaisedNoParams.Invoke();
                }
            }
        }

        // Do not change the name of this method, as it is used in the EventBusUtil.
        /// <summary>
        /// Clears all event bindings from the event bus.
        /// </summary>
        private static void Clear()
        {
            //Debug.Log($"Clearing {typeof(T).Name} bindings");
            _eventBindings.Clear();
        }
    }
}