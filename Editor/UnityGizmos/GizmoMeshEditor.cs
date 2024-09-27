using SombraStudios.Shared.Utility.UnityGizmos.Interfaces;
using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Editor.UnityGizmos
{
    /// <summary>
    /// Custom editor for IGizmoMesh objects.
    /// </summary>
    [CustomEditor(typeof(IGizmoMesh))]
    public class GizmoMeshEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Draws gizmos for IGizmoMesh objects.
        /// </summary>
        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        private static void DrawGizmos(IGizmoMesh gizmo, GizmoType gizmoType)
        {
            if (!gizmo.GizmosEnabled) { return; }
            if (gizmo.ShowOnlyInPlayMode && !Application.isPlaying) { return; }
            SetupGizmo(gizmo);
            DrawGizmo(gizmo);
        }

        /// <summary>
        /// Sets up the gizmo for drawing.
        /// </summary>
        private static void SetupGizmo(IGizmoMesh gizmo)
        {
            if (gizmo.IsLocalPosition) { Gizmos.matrix = gizmo.MonoBehaviour.transform.localToWorldMatrix; }
            Gizmos.color = gizmo.GizmosColor;
        }

        /// <summary>
        /// Draws the gizmo.
        /// </summary>
        private static void DrawGizmo(IGizmoMesh gizmo)
        {
            DrawMesh(gizmo);
        }

        /// <summary>
        /// Draws the mesh gizmo.
        /// </summary>
        private static void DrawMesh(IGizmoMesh gizmo)
        {
            if (gizmo.IsWireMesh)
            {
                Gizmos.DrawWireMesh(gizmo.Mesh, gizmo.Position, gizmo.Rotation, gizmo.Scale);
            }
            else
            {
                Gizmos.DrawMesh(gizmo.Mesh, gizmo.Position, gizmo.Rotation, gizmo.Scale);
            }
        }
    }
}
