using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.Destructible
{
    /// <summary>
    /// Helper component that spawns a prefab game object when the Destroy function is called.
    /// If the spawned game object has a rigidbodies then they will have force added to them based on the
    /// fields provided.
    /// </summary>
    public class DestructibleBehaviour : MonoBehaviour, IBehaviour
    {
        [Header("Prefab")]
        /// <summary>
        /// The prefab to spawn when the object is destroyed.
        /// </summary>
        [Tooltip("The prefab to spawn when the object is destroyed.")]
        [SerializeField] private GameObject _destroyedVersion;

        [Header("Data")]
        /// <summary>
        /// The data defining the destructible behavior.
        /// </summary>
        [Tooltip("The data defining the destructible behavior.")]
        [SerializeField] private DestructibleBehaviourSO _data;

        [Header("Debug")]
        [SerializeField] private bool _isEnabled;

        /// <summary>
        /// Gets or sets a value indicating whether the object is destroyed.
        /// </summary>
        public bool Destroyed { get; protected set; }
        public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }

        public void ToggleBehaviour() => _isEnabled = !_isEnabled;


        /// <summary>
        /// Destroys the object and spawns the destroyed version.
        /// </summary>
        public virtual void ExecuteBehaviour()
        {
            if (Destroyed) { return; }
            if (_destroyedVersion == null) { return; }

            // Instantiate the destroyed version of the object
            var destroyedPrefab = Instantiate(_destroyedVersion, transform.position, transform.rotation);

            // Apply explosion force to debris
            ApplyExplosion(destroyedPrefab);

            // Set a delay for removing debris
            var delay = _data.RemoveDebrisTimerUpper;
            RemoveDebris(destroyedPrefab, delay);

            Destroyed = true;
            Destroy(gameObject);
        }


        /// <summary>
        /// Applies explosion force to the debris.
        /// </summary>
        /// <param name="destroyed">The destroyed version of the object.</param>
        private void ApplyExplosion(GameObject destroyed)
        {
            if (destroyed == null) { return; }

            foreach (var rigidBody in destroyed.GetComponentsInChildren<Rigidbody>())
            {
                // Create a random force to apply to each debris
                var randomXForce = GetRandomForce();
                var randomYForce = GetRandomForce();
                var randomZForce = GetRandomForce();
                var forceVector = new Vector3(randomXForce, randomYForce, randomZForce);
                rigidBody.AddForce(forceVector * _data.ExplosionPowerMultiplier, ForceMode.VelocityChange);

                // Set a delay for removing debris
                var delay = Random.Range(_data.RemoveDebrisTimerLower, _data.RemoveDebrisTimerUpper);
                RemoveDebris(rigidBody.gameObject, delay);

                // Unparent debris
                rigidBody.transform.parent = null;
            }
        }

        /// <summary>
        /// Gets a random force value within the specified range.
        /// </summary>
        /// <returns>The random force value.</returns>
        private float GetRandomForce()
        {
            return Random.Range(_data.ExplosionPowerLower, _data.ExplosionPowerUpper);
        }

        /// <summary>
        /// Removes debris with a delay.
        /// </summary>
        /// <param name="debris">The debris game object to remove.</param>
        /// <param name="delayTimeInSeconds">The delay in seconds before removing the debris.</param>
        private void RemoveDebris(GameObject debris, float delayTimeInSeconds)
        {
            if (_data.RemoveDebris == false) { return; }

            // Add a timer for self-destructing debris
            var timer = debris.AddComponent<TimedSelfDestruct>();
            timer.TimeToDestruction = delayTimeInSeconds;
        }
    }
}
