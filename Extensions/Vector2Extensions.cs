using System;
using UnityEngine;

namespace SombraStudios.Shared.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="Vector2"/> struct.
    /// </summary>
    public static class Vector2Extensions
    {
        /// <summary>
        /// Converts a screen position expressed in top-left-origin coordinates, as UI Toolkit reports them,
        /// into a world-space position.
        /// </summary>
        /// <remarks>
        /// The y coordinate is flipped unconditionally, because UI Toolkit's (0,0) is the top-left corner
        /// while Unity's screen space starts at the bottom-left. Do not pass a value that already uses
        /// bottom-left origin, such as <c>Input.mousePosition</c> - it would be flipped a second time.
        /// </remarks>
        /// <param name="screenPos">The screen position to convert, with (0,0) at the top-left.</param>
        /// <param name="camera">The camera to project through.</param>
        /// <param name="zDepth">Distance from the camera, in world units, at which to place the result.</param>
        /// <returns>The world-space position corresponding to the given screen position.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="camera"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="screenPos"/> contains NaN.</exception>
        public static Vector3 ScreenPosToWorldPos(this Vector2 screenPos, Camera camera, float zDepth = 10f)
        {
            if (camera == null)
            {
                throw new ArgumentNullException(nameof(camera));
            }

            if (float.IsNaN(screenPos.x) || float.IsNaN(screenPos.y))
            {
                throw new ArgumentException($"Screen position contains NaN: {screenPos}.", nameof(screenPos));
            }

            // Flip y-coordinate; in UI Toolkit, (0,0) is top-left instead of bottom-left.
            var yPos = camera.pixelHeight - screenPos.y;

            return camera.ScreenToWorldPoint(new Vector3(screenPos.x, yPos, zDepth));
        }
    }
}