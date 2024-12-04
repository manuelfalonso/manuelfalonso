using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.LookAt
{
    /// <summary>
    /// Controller to handle the "Look At" functionality on an object.
    /// </summary>
    public class LookAtController : MonoBehaviour, IBehaviour
    {
        [Tooltip("Settings for the 'Look At' behavior.")]
        [SerializeField] private LookAtSO _settings = null;
        [Tooltip("Override target for the 'Look At' target.")]
        [SerializeField] private Transform _targetOverride = null;
        [Tooltip("Indicates whether the 'Look At' functionality is enabled.")]
        [SerializeField] private bool _isEnabled = false;

        /// <summary>
        /// Settings for the "Look At" behavior.
        /// </summary>
        public LookAtSO Settings { get => _settings; set => _settings = value; }

        /// <summary>  
        /// Override target for the "Look At" target. 
        /// </summary> 
        public Transform TargetOverride { get => _targetOverride; set => _targetOverride = value; }
        /// <summary>
        /// Indicates whether the "Look At" functionality is enabled.
        /// </summary>
        public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }

        /// <summary>
        /// Toggles the state of the "Look At" functionality.
        /// </summary>
        public void ToggleBehaviour() => _isEnabled = !_isEnabled;


        void Update()
        {
            if (_isEnabled)
                ExecuteBehaviour();
        }


        /// <summary>
        /// Applies the "Look At" functionality to the object.
        /// </summary>
        public void ExecuteBehaviour()
        {
            if (_settings == null)
            {
                Debug.LogError("LookAtSettings is not assigned.", gameObject);
                _isEnabled = false;
                return;
            }

            if (_targetOverride != null)
                transform.LookAt(_targetOverride, _settings.WorldUp);
            else
                transform.LookAt(_settings.WorldPosition, _settings.WorldUp);
        }
    }
}
