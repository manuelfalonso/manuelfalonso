using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// A generic listener for an event channel of type T.
    /// This listener registers itself to an event channel and invokes a UnityEvent in response to the event.
    /// </summary>
    /// <typeparam name="T">The type of the event data.</typeparam>
    public abstract class GenericEventChannelListener<T> : MonoBehaviour
    {
        [Header("Listen to Event Channels")]
        /// <summary>
        /// The event channel that this listener will subscribe to.
        /// </summary>
        [Tooltip("The event channel to listen to.")]
        [SerializeField]
        private GenericEventChannelSO<T> _eventChannel;

        [Space]
        /// <summary>
        /// The UnityEvent response to invoke when the event channel raises an event.
        /// </summary>
        [Tooltip("The UnityEvent to invoke when the event channel raises an event.")]
        [SerializeField]
        private UnityEvent<T> _response;

        /// <summary>
        /// The delay (in seconds) before invoking the UnityEvent response.
        /// </summary>
        [Tooltip("The delay (in seconds) before invoking the UnityEvent response.")]
        [SerializeField]
        private float _delay;

        /// <summary>
        /// Registers the listener to the event channel when the GameObject is enabled.
        /// </summary>
        private void OnEnable() => RegisterEvent();

        /// <summary>
        /// Unregisters the listener from the event channel when the GameObject is disabled.
        /// </summary>
        private void OnDisable() => UnregisterEvent();

        /// <summary>
        /// Subscribes the listener to the event channel.
        /// Logs an error and disables the script if the event channel is not assigned.
        /// </summary>
        private void RegisterEvent()
        {
            if (_eventChannel == null)
            {
                Debug.LogError($"Missing Event Channel reference for {GetType().Name} on {name}. Disabling script.",
                    this);
                enabled = false;
                return;
            }

            _eventChannel.OnEventRaised -= OnEventRaised;
            _eventChannel.OnEventRaised += OnEventRaised;
        }

        /// <summary>
        /// Unsubscribes the listener from the event channel.
        /// </summary>
        private void UnregisterEvent()
        {
            if (_eventChannel == null) return;

            _eventChannel.OnEventRaised -= OnEventRaised;
        }

        /// <summary>
        /// Handles the event raised by the event channel.
        /// Starts a coroutine to invoke the UnityEvent after the specified delay.
        /// </summary>
        /// <param name="value">The event data.</param>
        private void OnEventRaised(T value)
        {
            if (_delay <= 0f)
            {
                _response?.Invoke(value);
                return;
            }
            StartCoroutine(RaiseEventDelayed(value));
        }

        /// <summary>
        /// Invokes the UnityEvent response after a delay.
        /// </summary>
        /// <param name="value">The event data to pass to the UnityEvent.</param>
        /// <returns>An IEnumerator for the delay.</returns>
        private IEnumerator RaiseEventDelayed(T value)
        {
            yield return new WaitForSeconds(_delay);
            _response?.Invoke(value);
        }
    }
}
