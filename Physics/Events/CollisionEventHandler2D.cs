using UnityEngine;

namespace SombraStudios.Shared.Physics.Events
{
    /// <summary>
    /// Handles 3D collision events when at least one involved object has a non-kinematic Rigidbody
    /// and both have Colliders with IsTrigger disabled.
    /// The required Rigidbody may be on the GameObject or any of its parents.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class CollisionEventHandler2D : CollisionEventHandlerBase<Collision2D>
    {
        #region Unity Messages
        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEnter(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            CollisionExit(collision);
        }
        #endregion


        #region Proteced Methods
        protected override bool IsStayInteractionValid(Collision2D component)
        {
            if (component == null) { return false; }
            if (!IsInteractionValid(component.gameObject)) { return false; }
            return true;
        }

        protected bool CollisionEnter(Collision2D collision)
        {
            if (!IsInteractionValid(collision.gameObject)) { return false; }
            if (!CalculateCollision(collision)) { return false; }

            HandleEnterInteractions(collision);
            if (!_eventType.HasFlag(PhysicInteractionEventType.Enter)) { return false; }

            InteractionOnEnter?.Invoke(collision);
            return true;
        }

        protected bool CollisionExit(Collision2D collision)
        {
            if (!IsInteractionValid(collision.gameObject)) { return false; }
            if (!CalculateCollision(collision)) { return false; }

            HandleExitInteractions(collision);
            if (!_eventType.HasFlag(PhysicInteractionEventType.Exit)) { return false; }

            InteractionOnExit?.Invoke(collision);
            return true;
        }

        protected override bool CalculateCollision(Collision2D collision)
        {
            _lastVelocity = collision.relativeVelocity.magnitude;

            _maxVelocity = Mathf.Max(_maxVelocity, _lastVelocity);

            var velocityMet = _lastVelocity > _velocityThreshold;

            if (velocityMet)
            {
                _lastInteraction = collision.transform;
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
