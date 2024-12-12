using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos.Editor
{
    /// <summary>
    /// Custom editor for IGizmoIcon objects.
    /// </summary>
    [CustomEditor(typeof(IGizmoIcon))]
    public class GizmoIconEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Draws gizmos for IGizmoIcon objects.
        /// </summary>
        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        private static void DrawGizmos(IGizmoIcon gizmo, GizmoType gizmoType)
        {
            if (!gizmo.GizmosEnabled) { return; }
            if (gizmo.ShowOnlyInPlayMode && !Application.isPlaying) { return; }
            SetupGizmo(gizmo);
            DrawGizmo(gizmo);
        }

        /// <summary>
        /// Sets up the gizmo for drawing.
        /// </summary>
        private static void SetupGizmo(IGizmoIcon gizmo)
        {
            if (gizmo.IsLocalPosition) { Gizmos.matrix = gizmo.MonoBehaviour.transform.localToWorldMatrix; }
            Gizmos.color = gizmo.GizmosColor;
        }

        /// <summary>
        /// Draws the gizmo.
        /// </summary>
        private static void DrawGizmo(IGizmoIcon gizmo)
        {
            DrawIcon(gizmo);
        }

        /// <summary>
        /// Draws the icon gizmo.
        /// </summary>
        private static void DrawIcon(IGizmoIcon gizmo)
        {
            Gizmos.DrawIcon(gizmo.Center, gizmo.Name, gizmo.AllowScaling);
        }
    }
}
