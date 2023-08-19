using System;
using UnityEngine;

namespace SombraStudios.Utility.UnityGizmos
{
    /// <summary>
    /// Draw a solid or wireframe box at center with size.
    /// </summary>
    public class GizmosCubeUtility : GizmosUtility
    {
        [Header("Cube")]
        [Tooltip("Wireframe cube")]
        [SerializeField] private bool _isWireCube = false;
        [SerializeField] private Vector3 _center = Vector3.zero;
        [SerializeField] private Vector3 _size = Vector3.zero;


        protected override void DrawGizmo()
        {
            DrawCube();
        }


        private void DrawCube()
        {
            if (_isWireCube)
            {
                Gizmos.DrawWireCube(_center, _size);
            }
            else
            {
                Gizmos.DrawCube(_center, _size);
            }
        }
    }
}
