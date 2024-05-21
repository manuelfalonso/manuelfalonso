using UnityEngine;

namespace SombraStudios.Shared.IA
{
    public static partial class LineOfSight
    {
        /// <summary>
        /// Checks if the target is inside the view of the entity and if it is in sight of the entity.
        /// </summary>
        /// <param name="data">The data for the sight check.</param>
        /// <param name="viewAngle">The field of view angle.</param>
        /// <returns>True if the target is inside the specified angle and the "InSight" condition is met; 
        /// otherwise, false.</returns>
        public static bool IsInFieldOfViewAndInSight(
            IsInSightData data,
            float viewAngle)
        {
            if (data.StartPoint == null)
            {
                Debug.LogError("Target is null");
                return false;
            }

            if (data.EndPoint == null)
            {
                Debug.LogError("Entity is null");
                return false;
            }

            if (viewAngle == default)
            {
                Debug.LogError("ViewAngle is null/default");
                return false;
            }

            return IsInFieldOfView(data.StartPoint, data.EndPoint, viewAngle)
                && IsInSight(data);
        }

        /// <summary>
        /// Checks if a target is within the field of view of an entity based on their positions and the 
        /// specified half-field of view angle.
        /// </summary>
        /// <param name="entity">The transform of the entity (observer).</param>
        /// <param name="target">The transform of the target.</param>
        /// <param name="viewAngle">The half-field of view angle (in degrees).</param>
        /// <returns>Returns true if the target is within the field of view, otherwise false.</returns>
        public static bool IsInFieldOfView(Transform entity, Transform target, float viewAngle)
        {
            if (target == null || entity == null)
            {
                Debug.LogError("Target or entity is null");
                return false;
            }

            Vector3 directionToTarget = target.position - entity.position;
            return Vector3.Angle(entity.forward, directionToTarget) < viewAngle / 2;
        }

        /// <summary>
        /// Checks if there is a direct line of sight between two points, considering obstacles on a specified layer.
        /// Returns true if there is no obstacle (except the specified target), otherwise, false.
        /// </summary>
        /// <param name="data">The data for the sight check.</param>
        /// <returns>True if there is a direct line of sight without obstacles (except the target), otherwise, false.
        /// </returns>
        public static bool IsInSight(
            IsInSightData data)
        {
            if (data.StartPoint == null || data.EndPoint == null) { return false; }

            var start = data.StartPoint.position + data.StartPointOffset;
            var end = data.EndPoint.position + data.EndPointOffset;
            Vector3 directionToTarget = end - start;

            // Limits the result buffer to one.
            RaycastHit[] hits = new RaycastHit[1];
            int hitCount = Physics.RaycastNonAlloc(
                start,
                directionToTarget,
                hits,
                directionToTarget.magnitude,
                data.ObstaclesMask);

            if (hitCount > 0)
            {
#if UNITY_EDITOR
                Debug.DrawLine(start,
                    hits[0].point,
                    hits[0].collider.transform == data.EndPoint ? Color.cyan : Color.magenta,
                    5);
#endif
                if (data.TagToIgnore != null
                    && data.TagToIgnore != string.Empty
                    && hits[0].collider.gameObject.CompareTag(data.TagToIgnore))
                {
                    return false;
                }

                // Check if the hit object is the end point
                if (hits[0].collider.transform == data.EndPoint)
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
    }
}
