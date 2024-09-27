using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos.MonoBehaviours
{
    /// <summary>
    /// Draw a solid or wireframe Mesh.
    /// </summary>
    public class GizmosMeshUtility : GizmosUtility
    {
        [Header("Mesh")]
        [Tooltip("Wireframe Mesh")]
        [SerializeField] private bool _isWireMesh = false;
        [Tooltip("Mesh to draw as a gizmo.")]
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Vector3 _position = Vector3.zero;
        [SerializeField] private Quaternion _rotation = Quaternion.identity;
        [SerializeField] private Vector3 _scale = Vector3.one;


        protected override void DrawGizmo()
        {
            DrawMesh();
        }


        private void DrawMesh()
        {
            if (_isWireMesh)
            {
                Gizmos.DrawWireMesh(_mesh, _position, _rotation, _scale);
            }
            else
            {
                Gizmos.DrawMesh(_mesh, _position, _rotation, _scale);
            }
        }
    }
}
