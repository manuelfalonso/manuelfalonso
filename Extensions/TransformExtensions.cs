using System;
using UnityEngine;

namespace SombraStudios.Shared.Extensions
{
    /// <summary>
    /// Extension methods for the Transform class.
    /// </summary>
    public static class TransformExtensions
    {
        [Serializable]
        /// <summary>
        /// Represents the directions along the axes.
        /// </summary>
        public enum Axis
        {
            /// <summary>
            /// Upward direction.
            /// </summary>
            Up,

            /// <summary>
            /// Downward direction.
            /// </summary>
            Down,

            /// <summary>
            /// Left direction.
            /// </summary>
            Left,

            /// <summary>
            /// Right direction.
            /// </summary>
            Right,

            /// <summary>
            /// Forward direction.
            /// </summary>
            Forward,

            /// <summary>
            /// Backward direction.
            /// </summary>
            Backward
        }

        /// <summary>
        /// Sets the local axis value of the transform.
        /// </summary>
        /// <param name="trans">The transform to set the axis value for.</param>
        /// <param name="axis">The axis to set.</param>
        /// <param name="value">The value to set for the axis.</param>
        public static void SetAxisValue(this Transform trans, Axis axis, Vector3 value)
        {
            switch (axis)
            {
                case Axis.Up:
                    trans.up = value;
                    break;
                case Axis.Down:
                    trans.up = -value;
                    break;
                case Axis.Left:
                    trans.right = -value;
                    break;
                case Axis.Right:
                    trans.right = value;
                    break;
                case Axis.Forward:
                    trans.forward = value;
                    break;
                case Axis.Backward:
                    trans.forward = -value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(axis), axis, null);
            }
        }

        /// <summary>
        /// Resets the position, rotation and scale of the transform to default values.
        /// </summary>
        /// <param name="trans">The transform to reset its position, rotation and scale.</param>
        public static void ResetTransform(this Transform trans)
        {
            trans.position = Vector3.zero;
            trans.localRotation = Quaternion.identity;
            trans.localScale = new Vector3(1, 1, 1);
        }
    }
}
