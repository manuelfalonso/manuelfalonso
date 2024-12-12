using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos.Editor
{
    /// <summary>
    /// Custom editor for IGizmoFrustum objects.
    /// </summary>
    [CustomEditor(typeof(IGizmoFrustum))]
    public class GizmoFrustumEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Draws gizmos for IGizmoFrustum objects.
        /// </summary>
        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        private static void DrawGizmos(IGizmoFrustum gizmo, GizmoType gizmoType)
        {
            if (!gizmo.GizmosEnabled) { return; }
            if (gizmo.ShowOnlyInPlayMode && !Application.isPlaying) { return; }
            SetupGizmo(gizmo);
            DrawGizmo(gizmo);
        }

        /// <summary>
        /// Sets up the gizmo for drawing.
        /// </summary>
        private static void SetupGizmo(IGizmoFrustum gizmo)
        {
            if (gizmo.IsLocalPosition) { Gizmos.matrix = gizmo.MonoBehaviour.transform.localToWorldMatrix; }
            Gizmos.color = gizmo.GizmosColor;
        }

        /// <summary>
        /// Draws the gizmo.
        /// </summary>
        private static void DrawGizmo(IGizmoFrustum gizmo)
        {
            DrawFrustum(gizmo);
        }

        /// <summary>
        /// Draws the frustum gizmo.
        /// </summary>
        private static void DrawFrustum(IGizmoFrustum gizmo)
        {
            Gizmos.DrawFrustum(gizmo.FrustumCenter, gizmo.FrustumFov, gizmo.FrustumMaxRange, gizmo.FrustumMinRange, gizmo.FrustumAspect);
        }
    }
}
