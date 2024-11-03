using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.AutoMove
{
    /// <summary>
    /// Controls the automatic movement of a GameObject based on the provided settings.
    /// </summary>
    public class AutoMoveController : MonoBehaviour, IBehaviour
    {
        /// <summary>
        /// The settings for the automatic movement.
        /// </summary>
        [Tooltip("The settings for the automatic movement.")]
        [SerializeField] private AutoMoveSettings _settings = null;

        /// <summary>
        /// This Transform overrides the Space settings. Pairs the relative to movement to another transform.
        /// </summary>
        [Tooltip("This Transform overrides the Space settings. Pairs the relative to movement to another transform.")]
        [SerializeField] private Transform _relativeToOverride = null;

        /// <summary>
        /// Indicates whether the automatic movement is enabled.
        /// </summary>
        [Tooltip("Indicates whether the automatic movement is enabled.")]
        [SerializeField] private bool _isEnabled = false;

        /// <summary>
        /// Gets or sets the auto-move settings.
        /// </summary>
        public AutoMoveSettings Settings { get => _settings; set => _settings = value; }

        /// <summary>
        /// Gets or sets the transform to use as a override to the Space settings.
        /// </summary>
        public Transform RelativeToOverride { get => _relativeToOverride; set => _relativeToOverride = value; }

        /// <summary>
        /// Gets or sets a value indicating whether the automatic movement is enabled.
        /// </summary>
        public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }

        /// <summary>
        /// Toggles the automatic movement on or off.
        /// </summary>
        public void ToggleBehaviour() => _isEnabled = !_isEnabled;


        void Update()
        {
            if (_isEnabled)
                ExecuteBehaviour();
        }


        /// <summary>
        /// Applies movement to the GameObject based on the settings.
        /// </summary>
        public void ExecuteBehaviour()
        {
            if (_settings == null)
            {
                Debug.LogError("AutoMoveSettings is not assigned.", gameObject);
                _isEnabled = false;
                return;
            }

            if (_relativeToOverride != null)
            {
                transform.Translate(
                    _settings.MovementSpeedX * Time.deltaTime,
                    _settings.MovementSpeedY * Time.deltaTime,
                    _settings.MovementSpeedZ * Time.deltaTime,
                    _relativeToOverride);
            }
            else
            {
                transform.Translate(
                    _settings.MovementSpeedX * Time.deltaTime,
                    _settings.MovementSpeedY * Time.deltaTime,
                    _settings.MovementSpeedZ * Time.deltaTime,
                    _settings.Space);
            }
        }
    }
}
