using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos
{
    /// <summary>
    /// Decorator/Wrapper over MonoBehaviour OnDrawGizmos and OnDrawGizmosSelected Unity messages.
    /// </summary>
    public abstract class GizmosUtility : MonoBehaviour
    {
        [SerializeField] private bool _gizmosEnabled = false;
        [SerializeField] private Color _gizmosColor = Color.white;
        [Tooltip("Show only if the object the script is attached to is selected on Hierarchy")]
        [SerializeField] private bool _showIfSelected = true;
        [SerializeField] private bool _showOnlyInPlayMode = true;
        [SerializeField] private bool _isLocalPosition = true;

        public bool GizmosEnabled { get => _gizmosEnabled; internal set => _gizmosEnabled = value; }
        public Color GizmosColor { get => _gizmosColor; set => _gizmosColor = value; }
        public bool ShowIfSelected { get => _showIfSelected; set => _showIfSelected = value; }
        public bool ShowOnlyInPlayMode { get => _showOnlyInPlayMode; set => _showOnlyInPlayMode = value; }


        private void OnDrawGizmos()
        {
            if (!_gizmosEnabled) { return; }
            if (_showIfSelected) { return; }
            if (_showOnlyInPlayMode && !Application.isPlaying) { return; }
            SetupGizmo();
            DrawGizmo();
        }

        private void OnDrawGizmosSelected()
        {
            if (!_gizmosEnabled) { return; }
            if (!_showIfSelected) { return; }
            if (_showOnlyInPlayMode && !Application.isPlaying) { return; }
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
