using SombraStudios.Shared.Utility.UnityGizmos;
using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Editor.UnityGizmos
{
    /// <summary>
    /// Custom editor for IGizmoRay objects.
    /// </summary>
    [CustomEditor(typeof(IGizmoRay))]
    public class GizmoRayEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Draws gizmos for IGizmoRay objects.
        /// </summary>
        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        private static void DrawGizmos(IGizmoRay gizmo, GizmoType gizmoType)
        {
            if (!gizmo.GizmosEnabled) { return; }
            if (gizmo.ShowOnlyInPlayMode && !Application.isPlaying) { return; }
            SetupGizmo(gizmo);
            DrawGizmo(gizmo);
        }

        /// <summary>
        /// Sets up the gizmo for drawing.
        /// </summary>
        private static void SetupGizmo(IGizmoRay gizmo)
        {
            if (gizmo.IsLocalPosition) { Gizmos.matrix = gizmo.MonoBehaviour.transform.localToWorldMatrix; }
            Gizmos.color = gizmo.GizmosColor;
        }

        /// <summary>
        /// Draws the gizmo.
        /// </summary>
        private static void DrawGizmo(IGizmoRay gizmo)
        {
            DrawRay(gizmo);
        }

        /// <summary>
        /// Draws the ray gizmo.
        /// </summary>
        private static void DrawRay(IGizmoRay gizmo)
        {
            Gizmos.DrawRay(gizmo.From, gizmo.Direction);
        }
    }
}
