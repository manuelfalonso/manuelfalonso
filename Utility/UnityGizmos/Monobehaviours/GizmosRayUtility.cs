using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos
{
    /// <summary>
    /// Draws a ray starting at from to from + direction.
    /// </summary>
    public class GizmosRayUtility : GizmosUtility
    {
        [Header("Ray")]
        [SerializeField] private Vector3 _from = Vector3.zero;
        [SerializeField] private Vector3 _direction = Vector3.zero;


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
