using UnityEngine;

namespace SombraStudios.Utility.UnityGizmos
{
    /// <summary>
    /// Decorator/Wrapper over MonoBehaviour OnDrawGizmos and OnDrawGizmosSelected Unity messages.
    /// </summary>
    public abstract class GizmosUtility : MonoBehaviour
    {
        [SerializeField] private bool _gizmosEnabled = false;
        [SerializeField] private Color _gizmosColor = Color.white;
        [Tooltip("Show only if the object the script is attached to is selected on Hierarchy")]
        [SerializeField] private bool _isSelectedGizmo = true;
        [SerializeField] private bool _isLocalPosition = true;


        private void OnDrawGizmos()
        {
            if (!_gizmosEnabled) { return; }
            if (_isSelectedGizmo) { return; }
            SetupGizmo();
            DrawGizmo();
        }

        private void OnDrawGizmosSelected()
        {
            if (!_gizmosEnabled) { return; }
            if (!_isSelectedGizmo) { return; }
            SetupGizmo();
            DrawGizmo();
        }


        private void SetupGizmo()
        {
            if (_isLocalPosition) { Gizmos.matrix = transform.localToWorldMatrix; }
            Gizmos.color = _gizmosColor;
        }

        protected abstract void DrawGizmo();
    }
}
