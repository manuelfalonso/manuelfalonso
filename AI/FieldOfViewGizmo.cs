#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.AI
{
    /// <summary>
    /// Represents a gizmo for visualizing a field of view in the Unity editor.
    /// </summary>
    public class FieldOfViewGizmo : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] float _fieldOfViewAngle = 160f;
        [SerializeField] float _viewDistance = 12f;
        [SerializeField] Color _color = new Color(1f, 0f, 0f, 0.1f);

        /// <summary>
        /// Gets or sets the field of view angle.
        /// </summary>
        public float FieldOfViewAngle { get => _fieldOfViewAngle; set => _fieldOfViewAngle = value; }

        /// <summary>
        /// Gets or sets the view distance.
        /// </summary>
        public float ViewDistance { get => _viewDistance; set => _viewDistance = value; }


        /// <summary>
        /// Called by Unity to draw gizmos that are picked.
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying || Application.isEditor)
            {
                DrawCone(_fieldOfViewAngle, _viewDistance, _color);
            }
        }


        /// <summary>
        /// Draws a cone representing the field of view.
        /// </summary>
        /// <param name="angle">The angle of the cone.</param>
        /// <param name="radius">The radius of the cone.</param>
        /// <param name="color">The color of the cone.</param>
        private void DrawCone(float angle, float radius, Color color)
        {
            float startAngle = -angle / 2;
            float endAngle = angle / 2;

            Handles.color = color;
            Vector3 arcFrom = Quaternion.Euler(0f, startAngle, 0f) * transform.forward;
            Handles.DrawSolidArc(transform.localPosition, transform.up, arcFrom, angle, radius);

            Vector3 from = Quaternion.Euler(0f, startAngle, 0f) * (transform.forward * radius);
            Vector3 to = Quaternion.Euler(0f, endAngle, 0f) * (transform.forward * radius);

            Handles.color = color;
            Handles.DrawLine(transform.localPosition, from);
            Handles.DrawLine(transform.localPosition, to);
        }
    }
}
#endif