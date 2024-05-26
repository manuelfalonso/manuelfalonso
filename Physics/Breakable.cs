using SombraStudios.Shared.Attributes;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Physics
{
    /// <summary>
    /// Detects a collision with a tagged collider, replacing this object with a 'broken' version
    /// </summary>
    public class Breakable : MonoBehaviour
    {
        [Serializable] public class BreakEvent : UnityEvent<GameObject, GameObject> { }

        [SerializeField]
        [Tooltip("The 'broken' version of this object.")]
        private GameObject _brokenVersion;

        [SerializeField, Tag]
        [Tooltip("The tag a collider must have to cause this object to break.")]
        private string _colliderTag = "Destroyer";

        [SerializeField]
        [Tooltip("Events to fire when a matching object collides and break this object. " +
            "The first parameter is the colliding object, the second parameter is the 'broken' version.")]
        private BreakEvent _onBreak = new BreakEvent();

        private bool _destroyed = false;

        /// <summary>
        /// Events to fire when a matching object collides and break this object.
        /// The first parameter is the colliding object, the second parameter is the 'broken' version.
        /// </summary>
        public BreakEvent OnBreak => _onBreak;


        void OnCollisionEnter(Collision collision)
        {
            CheckCollision(collision);
        }


        /// <summary>
        /// Check if the collision is with a matching object, and if so, break this object.
        /// </summary>
        /// <param name="collision">The object that collide with</param>
        private void CheckCollision(Collision collision)
        {
            if (_destroyed)
                return;

            if (collision.gameObject.CompareTag(_colliderTag))
            {
                _destroyed = true;
                var brokenVersion = Instantiate(_brokenVersion, transform.position, transform.rotation);
                _onBreak.Invoke(collision.gameObject, brokenVersion);
                Destroy(gameObject);
            }
        }
    }
}
