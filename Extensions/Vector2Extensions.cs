using UnityEngine;

namespace SombraStudios.Shared.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="Vector2"/> struct.
    /// </summary>
    public static class Vector2Extensions
    {
        /// <summary>
        /// Converts a screen position from UI Toolkit (or other screen-space coordinates) to world space.
        /// </summary>
        /// <remarks>
        /// This method accounts for the fact that UI Toolkit's (0,0) is at the top-left, whereas
        /// Unity's world space uses a bottom-left origin.
        /// </remarks>
        /// <param name="screenPos">The screen position to convert.</param>
        /// <param name="camera">The camera used for conversion. If null, defaults to <see cref="Camera.main"/>.</param>
        /// <param name="zDepth">The depth at which the world position should be projected.</param>
        /// <returns>The world-space position corresponding to the given screen position.</returns>
        public static Vector3 ScreenPosToWorldPos(this Vector2 screenPos, Camera camera = null, float zDepth = 10f)
        {
            if (camera == null)
                camera = Camera.main;

            if (camera == null)
                return Vector2.zero; // Return a default value if no camera is available.

            if (float.IsNaN(screenPos.x) || float.IsNaN(screenPos.y))
                return Vector3.zero; // Return a default value if input is invalid.

            // Flip y-coordinate; in UI Toolkit, (0,0) is top-left instead of bottom-left.
            float yPos = camera.pixelHeight - screenPos.y;

            // Convert to world space position using Camera class.
            Vector3 screenCoord = new Vector3(screenPos.x, yPos, zDepth);
            return camera.ScreenToWorldPoint(screenCoord);
        }
    }
}
