using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.LookWithLerp
{
    /// <summary>  
    /// Controller to handle the look to a rotation with lerp functionality on an object.  
    /// </summary>  
    public class LookWithLerpController : MonoBehaviour, IBehaviour
    {
        [Tooltip("Settings for the behavior.")]
        [SerializeField] private LookWithLerpSettings _settings = null;
        [Tooltip("Override the Forward Vector with a target Tranform.")]
        [SerializeField] private Transform _targetOverride = null;
        [Tooltip("Offset for the target override position.")]
        [SerializeField] private Vector3 _targetOverrideOffset = Vector3.zero;
        [Tooltip("Indicates whether the functionality is enabled.")]
        [SerializeField] private bool _isEnabled = false;

        /// <summary>  
        /// Settings for the behavior.  
        /// </summary>  
        public LookWithLerpSettings Settings { get => _settings; set => _settings = value; }

        /// <summary>  
        /// Override the Forward Vector with a target Tranform.
        /// </summary>  
        public Transform TargetOverride { get => _targetOverride; set => _targetOverride = value; }

        /// <summary>  
        /// Offset for the target override position.
        /// </summary> 
        public Vector3 TargetOverrideOffset { get => _targetOverrideOffset; set => _targetOverrideOffset = value; }

        /// <summary>  
        /// Indicates whether the functionality is enabled.  
        /// </summary>  
        public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }

        /// <summary>  
        /// Toggles the state of the functionality.  
        /// </summary>  
        public void ToggleBehaviour() => _isEnabled = !_isEnabled;

        private Vector3 _relativePosition;
        private Quaternion _newRotation;


        void Update()
        {
            if (_isEnabled)
                ExecuteBehaviour();
        }


        /// <summary>  
        /// Look to a rotation/object with lerp based on the settings.
        /// </summary>  
        public void ExecuteBehaviour()
        {
            if (_settings == null)
            {
                Debug.LogError("LookWithLerpSettings is not assigned.", gameObject);
                _isEnabled = false;
                return;
            }

            if (_targetOverride != null)
            {
                _relativePosition = _targetOverride.position + _targetOverrideOffset - transform.position;
                _newRotation = Quaternion.LookRotation(_relativePosition, _settings.UpVector);
            }
            else
            {
                _newRotation = Quaternion.LookRotation(_settings.ForwardVector, _settings.UpVector);
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, _newRotation, _settings.Speed * Time.deltaTime);
        }
    }
}
