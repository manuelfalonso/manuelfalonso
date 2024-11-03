using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.AutoTorque
{
    /// <summary>
    /// Controls the automatic application of torque to a Rigidbody based on the provided settings.
    /// </summary>
    public class AutoTorqueController : MonoBehaviour, IBehaviour
    {
        /// <summary>
        /// The settings for the automatic torque.
        /// </summary>
        [Tooltip("The settings for the automatic torque.")]
        [SerializeField] private AutoTorqueSettings _settings;

        /// <summary>
        /// The Rigidbody to which the torque will be applied.
        /// </summary>
        [Tooltip("The Rigidbody to which the torque will be applied.")]
        [SerializeField] private Rigidbody _rigidbody;

        /// <summary>
        /// Indicates whether the automatic torque is enabled.
        /// </summary>
        [Tooltip("Indicates whether the automatic torque is enabled.")]
        [SerializeField] private bool _isEnabled = false;

        /// <summary>
        /// Gets or sets the auto-torque settings.
        /// </summary>
        public AutoTorqueSettings Settings { get => _settings; set => _settings = value; }

        /// <summary>
        /// Gets or sets the Rigidbody to which the torque will be applied.
        /// </summary>
        public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }

        /// <summary>
        /// Gets or sets a value indicating whether the automatic torque is enabled.
        /// </summary>
        public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }

        /// <summary>
        /// Toggles the automatic torque on or off.
        /// </summary>
        public void ToggleBehaviour() => _isEnabled = !_isEnabled;


        void FixedUpdate()
        {
            if (_isEnabled)
                ExecuteBehaviour();
        }


        /// <summary>
        /// Applies torque to the Rigidbody based on the settings.
        /// </summary>
        public void ExecuteBehaviour()
        {
            if (_settings == null)
            {
                Debug.LogError("AutoTorqueSettings is not assigned.", gameObject);
                _isEnabled = false;
                return;
            }

            _rigidbody.AddTorque(
                _settings.TorqueSizeX * Time.deltaTime,
                _settings.TorqueSizeY * Time.deltaTime,
                _settings.TorqueSizeZ * Time.deltaTime,
                _settings.ForceMode);
        }
    }
}
