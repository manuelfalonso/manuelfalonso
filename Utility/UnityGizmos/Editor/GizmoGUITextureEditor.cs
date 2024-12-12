using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos.Editor
{
    /// <summary>
    /// Custom editor for IGizmoGUITexture objects.
    /// </summary>
    [CustomEditor(typeof(IGizmoGUITexture))]
    public class GizmoGUITextureEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Draws gizmos for IGizmoGUITexture objects.
        /// </summary>
        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        private static void DrawGizmos(IGizmoGUITexture gizmo, GizmoType gizmoType)
        {
            if (!gizmo.GizmosEnabled) { return; }
            if (gizmo.ShowOnlyInPlayMode && !Application.isPlaying) { return; }
            SetupGizmo(gizmo);
            DrawGizmo(gizmo);
        }

        /// <summary>
        /// Sets up the gizmo for drawing.
        /// </summary>
        private static void SetupGizmo(IGizmoGUITexture gizmo)
        {
            if (gizmo.IsLocalPosition) { Gizmos.matrix = gizmo.MonoBehaviour.transform.localToWorldMatrix; }
            Gizmos.color = gizmo.GizmosColor;
        }

        /// <summary>
        /// Draws the gizmo.
        /// </summary>
        private static void DrawGizmo(IGizmoGUITexture gizmo)
        {
            DrawGUITexture(gizmo);
        }

        /// <summary>
        /// Draws the GUI texture gizmo.
        /// </summary>
        private static void DrawGUITexture(IGizmoGUITexture gizmo)
        {
            Gizmos.DrawGUITexture(
                gizmo.GUITextureScreenRect,
                gizmo.GUITextureTexture,
                gizmo.GUITextureLeftBorder,
                gizmo.GUITextureRightBorder,
                gizmo.GUITextureTopBorder,
                gizmo.GUITextureBottomBorder,
                gizmo.GUITextureMaterial);
        }
    }
}
