using System;
using UnityEngine;

namespace SombraStudios.Patterns.Behavioural.Observer
{
    /// <summary>
    /// Observer Pattern
    /// 1- Create an event Action (or any other type of event)
    /// 2- Create a method to invoke the event.
    /// 3- In Listener classes create the neceesary methods
    /// 4- In Listener classes Subscribe this methods
    /// 5- In Listener classes Unsubscribe this methods
    /// 6- The subscribed methods will be called when the event is invoked.
    /// </summary>
    public class Observer : MonoBehaviour
    {
        /// <summary>
        /// Example event and invoke method.
        /// </summary>
        public static event Action OnTriggerEvent;

        private void Start() => TriggerEvent();

        // Check if there are methods subscribed to the event and invoke the event
        public void TriggerEvent() => OnTriggerEvent?.Invoke();
    }
}
