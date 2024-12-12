using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos.Editor
{
    /// <summary>
    /// Custom editor for IGizmoSphere objects.
    /// </summary>
    [CustomEditor(typeof(IGizmoSphere))]
    public class GizmoSphereEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Draws gizmos for IGizmoSphere objects.
        /// </summary>
        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        private static void DrawGizmos(IGizmoSphere gizmo, GizmoType gizmoType)
        {
            if (!gizmo.GizmosEnabled) { return; }
            if (gizmo.ShowOnlyInPlayMode && !Application.isPlaying) { return; }
            SetupGizmo(gizmo);
            DrawGizmo(gizmo);
        }

        /// <summary>
        /// Sets up the gizmo for drawing.
        /// </summary>
        private static void SetupGizmo(IGizmoSphere gizmo)
        {
            if (gizmo.IsLocalPosition) { Gizmos.matrix = gizmo.MonoBehaviour.transform.localToWorldMatrix; }
            Gizmos.color = gizmo.GizmosColor;
        }

        /// <summary>
        /// Draws the gizmo.
        /// </summary>
        private static void DrawGizmo(IGizmoSphere gizmo)
        {
            DrawSphere(gizmo);
        }

        /// <summary>
        /// Draws the sphere gizmo.
        /// </summary>
        private static void DrawSphere(IGizmoSphere gizmo)
        {
            if (gizmo.IsWireSphere)
            {
                Gizmos.DrawWireSphere(gizmo.Center, gizmo.Radius);
            }
            else
            {
                Gizmos.DrawSphere(gizmo.Center, gizmo.Radius);
            }
        }
    }
}
