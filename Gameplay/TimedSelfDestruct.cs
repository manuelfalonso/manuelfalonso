using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Gameplay
{
    /// <summary>
    /// Destroys the game object attached to, after certain time.
    /// </summary>
    public class TimedSelfDestruct : MonoBehaviour
    {
        /// <summary>
        /// Determines whether to destroy the object on Start message.
        /// </summary>
        [Tooltip("Determines whether to destroy the object on Start message.")]
        [SerializeField] private bool _destroyOnStart = true;
        /// <summary>
        /// After this time, the object will be destroyed
        /// </summary>
        [Tooltip("After this time, the object will be destroyed")]
        [SerializeField] private float _timeToDestruction;
        /// <summary>
        /// Enable/Disable OnDestroy Event
        /// </summary>
        [Tooltip("Enable/Disable OnDestroy Event")]
        [SerializeField] private bool _isOnDestroyEventEnable = true;
        /// <summary>
        /// Audio clip to be played when the gameObject gets destroyed
        /// </summary>
        [Tooltip("Audio clip to be played when the gameObject gets destroyed")]
        [SerializeField] private AudioClip _destroySound;

        [Space]

        [Tooltip("Unity Event invoked when the gameobject is destroyed")]
        public UnityEvent OnDestroy = new UnityEvent();

        /// <summary>
        /// Gets or sets the time until the object is destroyed.
        /// </summary>
        public float TimeToDestruction { get => _timeToDestruction; set => _timeToDestruction = value; }


        private void Start()
        {
            Destroy();
        }


        private void Destroy()
        {
            if (_destroyOnStart == false) { return; }
            // Invoke the DestroyMe method after a specified time
            Invoke(nameof(DestroyMe), _timeToDestruction);
        }

        /// <summary>
        /// Destroys the object and invokes the OnDestroy event.
        /// </summary>
        void DestroyMe()
        {
            // Invoke OnDestroy event if it's enabled
            if (OnDestroy != null && _isOnDestroyEventEnable)
            {
                OnDestroy.Invoke();
            }

            // Play destroy sound if provided
            if (_destroySound != null)
            {
                AudioSource.PlayClipAtPoint(_destroySound, transform.position);
            }

            // Destroy the game object
            Destroy(gameObject);
        }
    }
}
