using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.MoveTowards
{
    /// <summary>
    /// Controller to handle the Move Towards functionality on an object.
    /// </summary>
    public class MoveTowardsController : MonoBehaviour
    {
        [SerializeField] private MoveTowardsSettings _settings;
        [SerializeField] private Transform _targetOverride = null;
        [SerializeField] private bool _isEnabled = false;

        /// <summary>
        /// Settings for the behavior.
        /// </summary>
        public MoveTowardsSettings Settings { get => _settings; set => _settings = value; }

        /// <summary>
        /// Override target for the functionality.
        /// </summary>
        public Transform TargetOverride { get => _targetOverride; set => _targetOverride = value; }

        /// <summary>
        /// Indicates whether the functionality is enabled.
        /// </summary>
        public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }

        /// <summary>
        /// Toggles the state of the functionality.
        /// </summary>
        public void ToggleBehaviour() => _isEnabled = !_isEnabled;


        void Update()
        {
            if (_isEnabled)
                ExecuteBehaviour();
        }


        /// <summary>
        /// Applies the functionality to the object.
        /// </summary>
        public void ExecuteBehaviour()
        {
            if (_settings == null)
            {
                Debug.LogError("MoveTowardsSettings is not assigned.", gameObject);
                _isEnabled = false;
                return;
            }

            if (_targetOverride != null)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    _targetOverride.position,
                    _settings.MaxDistanceDelta * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    _settings.TargetPosition,
                    _settings.MaxDistanceDelta * Time.deltaTime);
            }

        }
    }
}
