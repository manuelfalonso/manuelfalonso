using System;
using UnityEngine;

namespace SombraStudios.Shared.Extensions
{
    /// <summary>
    /// Extension methods for the Transform class.
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Rotates the transform so that the given local direction points along a world-space vector.
        /// </summary>
        /// <remarks>
        /// This assigns <see cref="Transform.up"/>, <see cref="Transform.right"/> or
        /// <see cref="Transform.forward"/>, so it changes the transform's rotation, not its position.
        /// Aiming a single axis leaves the roll around that axis unspecified - Unity picks one. Use
        /// <see cref="Quaternion.LookRotation(Vector3, Vector3)"/> when the roll matters.
        /// </remarks>
        /// <param name="trans">The transform to rotate.</param>
        /// <param name="direction">Which of the transform's local directions to aim.</param>
        /// <param name="value">The world-space vector to aim that direction along.</param>
        public static void SetDirection(this Transform trans, Direction direction, Vector3 value)
        {
            switch (direction)
            {
                case Direction.Up:
                    trans.up = value;
                    break;
                case Direction.Down:
                    trans.up = -value;
                    break;
                case Direction.Left:
                    trans.right = -value;
                    break;
                case Direction.Right:
                    trans.right = value;
                    break;
                case Direction.Forward:
                    trans.forward = value;
                    break;
                case Direction.Backward:
                    trans.forward = -value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        /// <summary>
        /// Resets the transform's local position, rotation and scale to their identity values.
        /// </summary>
        /// <remarks>
        /// Operates entirely in local space, so a child returns to its parent's origin rather than the
        /// world origin.
        /// </remarks>
        /// <param name="trans">The transform to reset.</param>
        public static void ResetTransform(this Transform trans)
        {
            trans.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            trans.localScale = Vector3.one;
        }
    }
}
