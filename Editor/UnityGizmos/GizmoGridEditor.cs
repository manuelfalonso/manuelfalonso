using SombraStudios.Shared.Utility.UnityGizmos;
using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Editor.UnityGizmos
{
    /// <summary>
    /// Custom editor for IGizmoGrid objects.
    /// </summary>
    [CustomEditor(typeof(IGizmoGrid))]
    public class GizmoGridEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Draws gizmos for IGizmoGrid objects.
        /// </summary>
        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        private static void DrawGizmos(IGizmoGrid gizmo, GizmoType gizmoType)
        {
            if (!gizmo.GizmosEnabled) { return; }
            if (gizmo.ShowOnlyInPlayMode && !Application.isPlaying) { return; }
            SetupGizmo(gizmo);
            DrawGizmo(gizmo);
        }

        /// <summary>
        /// Sets up the gizmo for drawing.
        /// </summary>
        private static void SetupGizmo(IGizmoGrid gizmo)
        {
            if (gizmo.IsLocalPosition) { Gizmos.matrix = gizmo.MonoBehaviour.transform.localToWorldMatrix; }
            Gizmos.color = gizmo.GizmosColor;
        }

        /// <summary>
        /// Draws the gizmo.
        /// </summary>
        private static void DrawGizmo(IGizmoGrid gizmo)
        {
            DrawGrid(gizmo);
        }

        /// <summary>
        /// Draws the grid gizmo.
        /// </summary>
        private static void DrawGrid(IGizmoGrid gizmo)
        {
            for (int x = 0; x < gizmo.Width; ++x)
            {
                Gizmos.DrawLine(Vector3.right * x, Vector3.right * x + gizmo.Height * Vector3.forward);
            }

            Gizmos.DrawLine(Vector3.right * gizmo.Width, Vector3.right * gizmo.Width + gizmo.Height * Vector3.forward);

            for (int y = 0; y < gizmo.Height; ++y)
            {
                Gizmos.DrawLine(Vector3.forward * y, Vector3.forward * y + Vector3.right * gizmo.Width);
            }

            Gizmos.DrawLine(Vector3.forward * gizmo.Height, Vector3.forward * gizmo.Height + Vector3.right * gizmo.Width);
        }
    }
}
