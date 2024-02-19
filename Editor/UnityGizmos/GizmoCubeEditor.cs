using SombraStudios.Shared.Utility.UnityGizmos;
using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Editor.UnityGizmos
{
    /// <summary>
    /// Custom editor for IGizmoCube objects.
    /// </summary>
    [CustomEditor(typeof(IGizmoCube))]
    public class GizmoCubeEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Draws gizmos for IGizmoCube objects.
        /// </summary>
        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        private static void DrawGizmos(IGizmoCube gizmo, GizmoType gizmoType)
        {
            if (!gizmo.GizmosEnabled) { return; }
            if (gizmo.ShowOnlyInPlayMode && !Application.isPlaying) { return; }
            SetupGizmo(gizmo);
            DrawGizmo(gizmo);
        }

        /// <summary>
        /// Sets up the gizmo for drawing.
        /// </summary>
        private static void SetupGizmo(IGizmoCube gizmo)
        {
            if (gizmo.IsLocalPosition) { Gizmos.matrix = gizmo.MonoBehaviour.transform.localToWorldMatrix; }
            Gizmos.color = gizmo.GizmosColor;
        }

        /// <summary>
        /// Draws the gizmo.
        /// </summary>
        private static void DrawGizmo(IGizmoCube gizmo)
        {
            DrawCube(gizmo);
        }

        /// <summary>
        /// Draws the cube gizmo.
        /// </summary>
        private static void DrawCube(IGizmoCube gizmo)
        {
            if (gizmo.IsWireCube)
            {
                Gizmos.DrawWireCube(gizmo.Center, gizmo.Size);
            }
            else
            {
                Gizmos.DrawCube(gizmo.Center, gizmo.Size);
            }
        }
    }
}
