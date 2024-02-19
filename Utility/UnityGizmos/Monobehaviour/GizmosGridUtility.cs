using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos
{
    public class GizmosGridUtility : GizmosUtility
    {
        [Header("Grid")]
        [SerializeField] private int _width = 0;
        [SerializeField] private int _height = 0;


        protected override void DrawGizmo()
        {
            DrawGrid();
        }


        private void DrawGrid()
        {
            for (int x = 0; x < _width; ++x)
            {
                Gizmos.DrawLine(Vector3.right * x, Vector3.right * x + _height * Vector3.forward);
            }

            Gizmos.DrawLine(Vector3.right * _width, Vector3.right * _width + _height * Vector3.forward);

            for (int y = 0; y < _height; ++y)
            {
                Gizmos.DrawLine(Vector3.forward * y, Vector3.forward * y + Vector3.right * _width);
            }

            Gizmos.DrawLine(Vector3.forward * _height, Vector3.forward * _height + Vector3.right * _width);
        }
    }
}
