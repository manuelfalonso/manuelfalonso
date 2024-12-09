using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>  
    /// MonoBehaviour-based listener for a ScriptableObject event using a Unity Event.  
    /// Add necessary actions to the response UnityEvent.  
    /// Limitations:  
    /// - UnityEvents in the Editor don't support multi-parameters unless they are dynamic.  
    /// - If the object is disabled, it will stop listening.  
    /// </summary>  
    public class VoidEventChannelListener : MonoBehaviour
    {
        [Header("Listen to Event Channels")]

        /// <summary>  
        /// The ScriptableObject event to listen to.  
        /// </summary>  
        [Tooltip("The ScriptableObject event to listen to.")]
        [SerializeField] private VoidEventChannelSO _voidEventChannel;

        [Space]

        /// <summary>  
        /// The Unity Event response to invoke when the ScriptableObject event is raised.  
        /// </summary>  
        [Tooltip("The Unity Event response to invoke when the ScriptableObject event is raised.")]
        [SerializeField] private UnityEvent _response;

        /// <summary>  
        /// The delay before invoking the response UnityEvent.  
        /// </summary>
        [Tooltip("The delay before invoking the response UnityEvent.")]
        [SerializeField] private float _delay;

        private void OnEnable() => RegisterEvent();

        private void OnDisable() => UnregisterEvent();

        /// <summary>  
        /// Registers the event listener to the event channel.  
        /// </summary>  
        private void RegisterEvent()
        {
            if (_voidEventChannel == null)
            {
                Debug.LogError($"Missing Event Channel reference for {GetType().Name} on {name}. Disabling script.", this);
                enabled = false;
                return;
            }

            _voidEventChannel.OnEventRaised -= OnEventRaised;
            _voidEventChannel.OnEventRaised += OnEventRaised;
        }

        /// <summary>  
        /// Unregisters the event listener from the event channel.  
        /// </summary>  
        private void UnregisterEvent()
        {
            if (_voidEventChannel == null) return;

            _voidEventChannel.OnEventRaised -= OnEventRaised;
        }

        /// <summary>  
        /// Invokes the response UnityEvent with a delay.  
        /// </summary>  
        private void OnEventRaised()
        {
            Invoke(nameof(RaiseEventDelayed), _delay);
        }

        /// <summary>  
        /// Invokes the response UnityEvent.  
        /// </summary>  
        private void RaiseEventDelayed()
        {
            _response?.Invoke();
        }
    }
}
