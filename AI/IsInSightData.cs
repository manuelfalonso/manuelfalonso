using UnityEngine;

namespace SombraStudios.Shared.AI
{
    public static partial class LineOfSight
    {
        /// <summary>
        /// Represents the data for a sight check.
        /// </summary>
        public struct IsInSightData
        {
            /// <summary>
            /// The starting point of the line of sight.
            /// </summary>
            public Transform StartPoint;

            /// <summary>
            /// The offset of the starting point.
            /// </summary>
            public Vector3 StartPointOffset;

            /// <summary>
            /// The ending point of the line of sight.
            /// </summary>
            public Transform EndPoint;

            /// <summary>
            /// The offset of the ending point.
            /// </summary>
            public Vector3 EndPointOffset;

            /// <summary>
            /// The layer mask representing obstacles to consider.
            /// </summary>
            public LayerMask ObstaclesMask;

            /// <summary>
            /// The tag to ignore.
            /// </summary>
            public string TagToIgnore;
        }
    }
}
