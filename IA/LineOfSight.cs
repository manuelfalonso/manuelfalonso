using UnityEngine;

namespace SombraStudios.Shared.IA
{
    public static class LineOfSight
    {
        /// <summary>
        /// Checks if there is a direct line of sight between two points, considering obstacles on a 
        /// specified layer.
        /// Returns true if there is no obstacle (except the specified target), otherwise, false.
        /// </summary>
        /// <param name="startPoint">The starting point of the line of sight.</param>
        /// <param name="endPoint">The ending point of the line of sight.</param>
        /// <param name="obstaclesMask">The layer mask representing obstacles to consider.</param>
        /// <returns>True if there is a direct line of sight without obstacles (except the target), otherwise, false.
        /// </returns>
        public static bool IsInSight(Vector3 startPoint, Transform endPoint, Vector3 endPointOffset, LayerMask obstaclesMask)
        {
            Vector3 directionToTarget = (endPoint.position + endPointOffset) - startPoint;
            // Limits the result buffer to one.
            RaycastHit[] hits = new RaycastHit[1];
            int hitCount = Physics.RaycastNonAlloc(
                startPoint, 
                directionToTarget, 
                hits, 
                directionToTarget.magnitude, 
                obstaclesMask);

            if (hitCount > 0)
            {
#if UNITY_EDITOR
                //Debug.DrawLine(startPoint, hits[0].point, hits[0].collider.transform == endPoint ? Color.magenta : Color.cyan, 5);
#endif
                // Check if the hit object is the end point
                if (hits[0].collider.transform == endPoint)
                {
                    // Ignore this hit, as it's the specified target
                    return true;  
                }
                else
                {
                    // There is an obstacle (other than the target) in the line of sight
                    return false; 
                }
            }

            // No obstacles (other than the target) in the line of sight
            return true;
        }

        /// <summary>
        /// Checks if the target is inside the view of the entity.
        /// </summary>
        /// <param name="entity">The entity's transform.</param>
        /// <param name="target">The target's position.</param>
        /// <param name="obstaclesMask">The layer mask for obstacles.</param>
        /// <param name="viewAngle">The field of view angle.</param>
        /// <returns>True if the target is inside the specified angle and the "InSight" condition is met; 
        /// otherwise, false.</returns>
        public static bool IsInFieldOfView(Transform entity, Vector3 entityOffset, Transform target, Vector3 targetOffset, LayerMask obstaclesMask, float viewAngle)
        {
            if (target == null)
            {
                Debug.LogError("target is null");
                return false;
            }

            if (entity == null)
            {
                Debug.LogError("entity is null");
                return false;
            }

            if (obstaclesMask == default)
            {
                Debug.LogError("obstaclesMask is null/default");
                return false;
            }

            if (viewAngle == default)
            {
                Debug.LogError("viewAngle is null/default");
                return false;
            }

            Vector3 directionToTarget = target.position - entity.position;
            float angle = Vector3.Angle(entity.forward, directionToTarget);

            var entityPosition = entity.position + entityOffset;

            return angle < viewAngle / 2 && IsInSight(entityPosition, target, targetOffset, obstaclesMask);
        }
    }
}
