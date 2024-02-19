using SombraStudios.Shared.Utility.UnityGizmos;
using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Editor.UnityGizmos
{
    /// <summary>
    /// Custom editor for IGizmoLine objects.
    /// </summary>
    [CustomEditor(typeof(IGizmoLine))]
    public class GizmoLineEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Draws gizmos for IGizmoLine objects.
        /// </summary>
        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        private static void DrawGizmos(IGizmoLine gizmo, GizmoType gizmoType)
        {
            if (!gizmo.GizmosEnabled) { return; }
            if (gizmo.ShowOnlyInPlayMode && !Application.isPlaying) { return; }
            SetupGizmo(gizmo);
            DrawGizmo(gizmo);
        }

        /// <summary>
        /// Sets up the gizmo for drawing.
        /// </summary>
        private static void SetupGizmo(IGizmoLine gizmo)
        {
            if (gizmo.IsLocalPosition) { Gizmos.matrix = gizmo.MonoBehaviour.transform.localToWorldMatrix; }
            Gizmos.color = gizmo.GizmosColor;
        }

        /// <summary>
        /// Draws the gizmo.
        /// </summary>
        private static void DrawGizmo(IGizmoLine gizmo)
        {
            DrawLine(gizmo);
        }

        /// <summary>
        /// Draws the line gizmo.
        /// </summary>
        private static void DrawLine(IGizmoLine gizmo)
        {
            Gizmos.DrawLine(gizmo.From, gizmo.To);
        }
    }
}
