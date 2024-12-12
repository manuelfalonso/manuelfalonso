using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos
{
    public class GizmosFrustumUtility : GizmosUtility
    {
        [Header("Frustum")]
        [Tooltip("The apex of the truncated pyramid.")]
        [SerializeField] private Vector3 _frustumCenter = Vector3.zero;
        [Tooltip("Vertical field of view (ie, the angle at the apex in degrees).")]
        [SerializeField] private float _frustumFov = 0f;
        [Tooltip("Distance of the frustum's far plane.")]
        [SerializeField] private float _frustumMaxRange = 0f;
        [Tooltip("Distance of the frustum's near plane.")]
        [SerializeField] private float _frustumMinRange = 0f;
        [Tooltip("Width/height ratio.")]
        [SerializeField] private float _frustumAspect = 0f;


        protected override void DrawGizmo()
        {
            DrawFrustum();
        }


        private void DrawFrustum()
        {
            Gizmos.DrawFrustum(_frustumCenter, _frustumFov, _frustumMaxRange, _frustumMinRange, _frustumAspect);
        }
    }
}
