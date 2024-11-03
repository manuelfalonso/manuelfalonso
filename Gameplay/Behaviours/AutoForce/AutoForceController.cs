using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.AutoForce
{
    /// <summary>
    /// Controls the automatic application of force to a Rigidbody based on the provided settings.
    /// </summary>
    public class AutoForceController : MonoBehaviour, IBehaviour
    {
        /// <summary>
        /// The settings for the automatic force.
        /// </summary>
        [Tooltip("The settings for the automatic force.")]
        [SerializeField] private AutoForceSettings _settings;

        /// <summary>
        /// The Rigidbody to which the force will be applied.
        /// </summary>
        [Tooltip("The Rigidbody to which the force will be applied.")]
        [SerializeField] private Rigidbody _rigidbody;

        /// <summary>
        /// Indicates whether the automatic force is enabled.
        /// </summary>
        [Tooltip("Indicates whether the automatic force is enabled.")]
        [SerializeField] private bool _isEnabled = false;

        /// <summary>
        /// Gets or sets the auto-force settings.
        /// </summary>
        public AutoForceSettings Settings { get => _settings; set => _settings = value; }

        /// <summary>
        /// Gets or sets the Rigidbody to which the force will be applied.
        /// </summary>
        public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }

        /// <summary>
        /// Gets or sets a value indicating whether the automatic force is enabled.
        /// </summary>
        public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }

        /// <summary>
        /// Toggles the automatic force on or off.
        /// </summary>
        public void ToggleBehaviour() => _isEnabled = !_isEnabled;


        void FixedUpdate()
        {
            if (_isEnabled)
                ExecuteBehaviour();
        }


        /// <summary>
        /// Applies force to the Rigidbody based on the settings.
        /// </summary>
        public void ExecuteBehaviour()
        {
            if (_settings == null)
            {
                Debug.LogError("AutoForceSettings is not assigned.", gameObject);
                _isEnabled = false;
                return;
            }

            _rigidbody.AddForce(
                _settings.ForceSizeX * Time.deltaTime,
                _settings.ForceSizeY * Time.deltaTime,
                _settings.ForceSizeZ * Time.deltaTime,
                _settings.ForceMode);
        }
    }
}
