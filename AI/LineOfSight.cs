using UnityEngine;

namespace SombraStudios.Shared.AI
{
    /// <summary>
    /// Field of view and obstruction checks between two transforms.
    /// </summary>
    /// <remarks>
    /// 2D and 3D physics are separate systems with separate hit types, so each obstruction check has an
    /// explicit 2D counterpart.
    /// </remarks>
    public static partial class LineOfSight
    {
        /// <summary>
        /// How long, in seconds, the Editor-only debug line stays visible.
        /// </summary>
        private const float DebugLineDuration = 5f;

        /// <summary>
        /// Checks if the target is inside the view of the entity and if it is in sight of the entity,
        /// using 3D physics.
        /// </summary>
        /// <param name="data">The data for the sight check.</param>
        /// <param name="viewAngle">The full field of view angle, in degrees.</param>
        /// <param name="hit">The RaycastHit information if an obstacle is detected.</param>
        /// <returns>True if the target is inside the specified angle and the "InSight" condition is met;
        /// otherwise, false.</returns>
        public static bool IsInFieldOfViewAndInSight(
            in IsInSightData data,
            float viewAngle,
            out RaycastHit hit)
        {
            hit = default;

            if (!ValidateFieldOfViewAndInSight(data, viewAngle)) { return false; }

            return IsInFieldOfView(data.StartPoint, data.EndPoint, viewAngle, is2D: false)
                && IsInSight(data, out hit);
        }

        /// <summary>
        /// Checks if the target is inside the view of the entity and if it is in sight of the entity,
        /// using 2D physics.
        /// </summary>
        /// <param name="data">The data for the sight check.</param>
        /// <param name="viewAngle">The full field of view angle, in degrees.</param>
        /// <param name="hit">The RaycastHit2D information if an obstacle is detected.</param>
        /// <returns>True if the target is inside the specified angle and the "InSight" condition is met;
        /// otherwise, false.</returns>
        public static bool IsInFieldOfViewAndInSight2D(
            in IsInSightData data,
            float viewAngle,
            out RaycastHit2D hit)
        {
            hit = default;

            if (!ValidateFieldOfViewAndInSight(data, viewAngle)) { return false; }

            return IsInFieldOfView(data.StartPoint, data.EndPoint, viewAngle, is2D: true)
                && IsInSight2D(data, out hit);
        }

        /// <summary>
        /// Checks if a target is within the field of view of an entity based on their positions and the
        /// specified field of view angle.
        /// </summary>
        /// <remarks>
        /// Pure angle math - no physics is queried, so this is the one check shared by the 2D and 3D
        /// paths. <paramref name="is2D"/> only selects which local axis counts as "forward".
        /// </remarks>
        /// <param name="entity">The transform of the entity (observer).</param>
        /// <param name="target">The transform of the target.</param>
        /// <param name="viewAngle">The full field of view angle, in degrees; the check uses half of it
        /// either side of the forward axis.</param>
        /// <param name="is2D">True to treat the entity's local up axis as forward, as 2D sprites do.</param>
        /// <returns>Returns true if the target is within the field of view, otherwise false.</returns>
        public static bool IsInFieldOfView(Transform entity, Transform target, float viewAngle, bool is2D = false)
        {
            if (target == null || entity == null)
            {
                Debug.LogError("Target or entity is null");
                return false;
            }

            return IsInFieldOfView(
                entity.position,
                is2D ? entity.up : entity.forward,
                target.position,
                viewAngle);
        }

        /// <summary>
        /// Checks if a position is within a field of view, given the observer's position and facing.
        /// </summary>
        /// <remarks>
        /// The vector form of the check: no Transforms, no scene, no physics, so it can be exercised
        /// directly. The Transform overload delegates here after resolving which axis is forward.
        /// <para>
        /// Degenerate inputs report true, because <see cref="Vector3.Angle"/> returns 0 when either
        /// vector has no length: a zero <paramref name="forward"/>, or a
        /// <paramref name="targetPosition"/> equal to <paramref name="origin"/>. Pass a real facing
        /// direction; it does not need to be normalized.
        /// </para>
        /// </remarks>
        /// <param name="origin">The observer's position.</param>
        /// <param name="forward">The direction the observer is facing. Need not be normalized.</param>
        /// <param name="targetPosition">The position being tested.</param>
        /// <param name="viewAngle">The full field of view angle, in degrees; the check uses half of it
        /// either side of <paramref name="forward"/>.</param>
        /// <returns>Returns true if the position is within the field of view, otherwise false.</returns>
        public static bool IsInFieldOfView(Vector3 origin, Vector3 forward, Vector3 targetPosition, float viewAngle)
        {
            return Vector3.Angle(forward, targetPosition - origin) < viewAngle / 2;
        }

        /// <summary>
        /// Checks if there is a direct line of sight between two points using 3D physics, considering
        /// obstacles on a specified layer.
        /// </summary>
        /// <remarks>
        /// Only the nearest collider on the ray is examined. A collider carrying
        /// <see cref="IsInSightData.TagToIgnore"/> is treated as not obstructing, so an ignored object
        /// standing in front of a real obstacle will hide it - exclude whole categories with
        /// <see cref="IsInSightData.ObstaclesMask"/> when that matters.
        /// </remarks>
        /// <param name="data">The data for the sight check.</param>
        /// <param name="hit">The RaycastHit information if an obstacle is detected.</param>
        /// <returns>True if there is a direct line of sight without obstacles (except the target), otherwise, false.
        /// </returns>
        public static bool IsInSight(in IsInSightData data, out RaycastHit hit)
        {
            hit = default;
            if (data.StartPoint == null || data.EndPoint == null) { return false; }

            var start = data.StartPoint.position + data.StartPointOffset;
            var end = data.EndPoint.position + data.EndPointOffset;
            Vector3 directionToTarget = end - start;

            // Physics.Raycast reports the *nearest* hit and allocates nothing. RaycastNonAlloc with a
            // one-element buffer returns an arbitrary hit instead, and allocated the buffer per call.
            if (!Physics.Raycast(
                    start,
                    directionToTarget,
                    out hit,
                    directionToTarget.magnitude,
                    data.ObstaclesMask))
            {
                // Nothing at all between the two points.
                return true;
            }

            DrawSightLine(start, hit.point, hit.collider.transform == data.EndPoint);

            return IsClearOfObstruction(hit.collider, data);
        }

        /// <summary>
        /// Checks if there is a direct line of sight between two points using 2D physics, considering
        /// obstacles on a specified layer.
        /// </summary>
        /// <remarks>
        /// Only the nearest collider on the ray is examined. A collider carrying
        /// <see cref="IsInSightData.TagToIgnore"/> is treated as not obstructing, so an ignored object
        /// standing in front of a real obstacle will hide it - exclude whole categories with
        /// <see cref="IsInSightData.ObstaclesMask"/> when that matters.
        /// </remarks>
        /// <param name="data">The data for the sight check.</param>
        /// <param name="hit">The RaycastHit2D information if an obstacle is detected.</param>
        /// <returns>True if there is a direct line of sight without obstacles (except the target), otherwise, false.
        /// </returns>
        public static bool IsInSight2D(in IsInSightData data, out RaycastHit2D hit)
        {
            hit = default;
            if (data.StartPoint == null || data.EndPoint == null) { return false; }

            var start = data.StartPoint.position + data.StartPointOffset;
            var end = data.EndPoint.position + data.EndPointOffset;
            Vector3 directionToTarget = end - start;

            hit = Physics2D.Raycast(
                start,
                directionToTarget,
                directionToTarget.magnitude,
                data.ObstaclesMask);

            if (hit.collider == null)
            {
                // Nothing at all between the two points.
                return true;
            }

            DrawSightLine(start, hit.point, hit.collider.transform == data.EndPoint);

            return IsClearOfObstruction(hit.collider, data);
        }


        /// <summary>
        /// Decides whether the nearest collider counts as blocking the line of sight.
        /// </summary>
        /// <param name="collider">The nearest collider on the ray.</param>
        /// <param name="data">The data for the sight check.</param>
        /// <returns>True if the line is considered clear, otherwise false.</returns>
        private static bool IsClearOfObstruction(Component collider, in IsInSightData data)
        {
            if (!string.IsNullOrEmpty(data.TagToIgnore) && collider.CompareTag(data.TagToIgnore))
            {
                return true;
            }

            // The target itself is not an obstruction either; anything else is.
            return collider.transform == data.EndPoint;
        }

        /// <summary>
        /// Draws the sight line in the Editor: cyan when it reached the target, magenta when blocked.
        /// </summary>
        /// <param name="start">Where the ray started.</param>
        /// <param name="end">Where the ray stopped.</param>
        /// <param name="reachedTarget">True when the nearest collider was the target itself.</param>
        private static void DrawSightLine(Vector3 start, Vector3 end, bool reachedTarget)
        {
#if UNITY_EDITOR
            Debug.DrawLine(start, end, reachedTarget ? Color.cyan : Color.magenta, DebugLineDuration);
#endif
        }

        /// <summary>
        /// Reports the arguments a combined field-of-view and sight check needs.
        /// </summary>
        /// <param name="data">The data for the sight check.</param>
        /// <param name="viewAngle">The full field of view angle, in degrees.</param>
        /// <returns>True when the arguments are usable, otherwise false.</returns>
        private static bool ValidateFieldOfViewAndInSight(in IsInSightData data, float viewAngle)
        {
            if (data.StartPoint == null)
            {
                Debug.LogError("StartPoint (the observer) is null");
                return false;
            }

            if (data.EndPoint == null)
            {
                Debug.LogError("EndPoint (the target) is null");
                return false;
            }

            if (viewAngle <= 0f)
            {
                Debug.LogError("ViewAngle must be greater than zero");
                return false;
            }

            return true;
        }
    }
}
