using UnityEngine;

namespace SombraStudios.Utility.UnityGizmos
{
    /// <summary>
    /// Draws a line starting at from towards to
    /// </summary>
    public class GizmosLineUtility : GizmosUtility
    {
        [Header("Line")]
        [SerializeField] private Vector3 _from = Vector3.zero;
        [SerializeField] private Vector3 _to = Vector3.zero;


        protected override void DrawGizmo()
        {
            DrawLine();
        }


        private void DrawLine()
        {
            Gizmos.DrawLine(_from, _to);
        }
    }
}
