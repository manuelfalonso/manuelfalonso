using UnityEngine;

namespace SombraStudios.Utility.UnityGizmos
{
    /// <summary>
    /// Draws a solid or wireframe sphere with center and radius.
    /// </summary>
    public class GizmosSphereUtility : GizmosUtility
    {
        [Header("Sphere")]
        [Tooltip("Wireframe Sphere")]
        [SerializeField] private bool _isWireSphere = false;
        [SerializeField] private Vector3 _center = Vector3.zero;
        [SerializeField] private float _radius;


        protected override void DrawGizmo()
        {
            DrawSphere();
        }


        private void DrawSphere()
        {
            if (_isWireSphere)
            {
                Gizmos.DrawWireSphere(_center, _radius);
            }
            else
            {
                Gizmos.DrawSphere(_center, _radius);
            }
        }
    }
}
