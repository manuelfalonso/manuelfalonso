using UnityEngine;

namespace SombraStudios.Utility.UnityGizmos
{
    /// <summary>
    /// Draws a ray starting at from to from + direction.
    /// </summary>
    public class GizmosRayUtility : GizmosUtility
    {
        [Header("Ray")]
        [SerializeField] private Vector3 _from;
        [SerializeField] private Vector3 _direction;


        protected override void DrawGizmo()
        {
            DrawRay();
        }


        private void DrawRay()
        {
            Gizmos.DrawRay(_from, _direction);
        }
    }
}
