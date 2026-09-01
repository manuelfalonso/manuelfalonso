using UnityEngine;

namespace SombraStudios.Shared.AI
{
    public static partial class LineOfSight
    {
        /// <summary>
        /// Represents the data for a sight check.
        /// </summary>
        /// <remarks>
        /// The observer, the target and the obstacle mask are constructor arguments because a check
        /// without them cannot do anything; everything else is optional. Being a
        /// <see langword="readonly"/> struct means an instance cannot be half-built or mutated after
        /// the fact, and lets callers pass it by <see langword="in"/> without a defensive copy.
        /// </remarks>
        public readonly struct IsInSightData
        {
            /// <summary>
            /// Creates the data for a sight check.
            /// </summary>
            /// <param name="startPoint">The observer the line of sight starts from.</param>
            /// <param name="endPoint">The target the line of sight ends at.</param>
            /// <param name="obstaclesMask">The layers that count as obstacles.</param>
            /// <param name="startPointOffset">Offset applied to the observer's position.</param>
            /// <param name="endPointOffset">Offset applied to the target's position.</param>
            /// <param name="tagToIgnore">A tag whose colliders do not count as obstructions.</param>
            public IsInSightData(
                Transform startPoint,
                Transform endPoint,
                LayerMask obstaclesMask,
                Vector3 startPointOffset = default,
                Vector3 endPointOffset = default,
                string tagToIgnore = null)
            {
                StartPoint = startPoint;
                EndPoint = endPoint;
                ObstaclesMask = obstaclesMask;
                StartPointOffset = startPointOffset;
                EndPointOffset = endPointOffset;
                TagToIgnore = tagToIgnore;
            }

            /// <summary>
            /// The starting point of the line of sight, that is, the observer.
            /// </summary>
            public Transform StartPoint { get; }

            /// <summary>
            /// The offset of the starting point.
            /// </summary>
            public Vector3 StartPointOffset { get; }

            /// <summary>
            /// The ending point of the line of sight, that is, the target.
            /// </summary>
            public Transform EndPoint { get; }

            /// <summary>
            /// The offset of the ending point.
            /// </summary>
            public Vector3 EndPointOffset { get; }

            /// <summary>
            /// The layer mask representing obstacles to consider.
            /// </summary>
            public LayerMask ObstaclesMask { get; }

            /// <summary>
            /// A tag whose colliders do not count as obstructions.
            /// </summary>
            public string TagToIgnore { get; }
        }
    }
}
