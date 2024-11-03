using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.AutoRotate
{
    /// <summary>
    /// Controls the automatic rotation of a GameObject based on the provided settings.
    /// </summary>
    public class AutoRotateController : MonoBehaviour, IBehaviour
    {
        /// <summary>
        /// The settings for the automatic rotation.
        /// </summary>
        [Tooltip("The settings for the automatic rotation.")]
        [SerializeField] private AutoRotateSettings _settings;

        /// <summary>
        /// Indicates whether the rotation is enabled.
        /// </summary>
        [Tooltip("Indicates whether the rotation is enabled.")]
        [SerializeField] private bool _isEnabled = false;

        /// <summary>
        /// Gets or sets the auto-rotate settings.
        /// </summary>
        public AutoRotateSettings Settings { get => _settings; set => _settings = value; }

        /// <summary>
        /// Gets or sets a value indicating whether the rotation is enabled.
        /// </summary>
        public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }

        /// <summary>
        /// Toggles the rotation on or off.
        /// </summary>
        public void ToggleBehaviour() => _isEnabled = !_isEnabled;


        void Update()
        {
            if (_isEnabled)
                ExecuteBehaviour();
        }



        /// <summary>
        /// Rotates the GameObject based on the settings.
        /// </summary>
        public void ExecuteBehaviour()
        {
            if (_settings == null)
            {
                Debug.LogError("AutoRotateSettings is not assigned.", gameObject);
                _isEnabled = false;
                return;
            }

            transform.Rotate(
                _settings.RotationSpeedX * Time.deltaTime,
                _settings.RotationSpeedY * Time.deltaTime,
                _settings.RotationSpeedZ * Time.deltaTime,
                _settings.Space);
        }
    }
}