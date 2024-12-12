using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos
{
    /// <summary>
    /// DrawIcon can be used to allow important objects in your game to be selected quickly.
    /// </summary>
    public class GizmosIconUtility : GizmosUtility
    {
        [Header("Icon")]
        [SerializeField] private Vector3 _center = Vector3.zero;
        [Tooltip("File name for image stored in Assets/Gizmos")]
        [SerializeField] private string _name = string.Empty;
        [SerializeField] private bool _allowScaling = true;


        protected override void DrawGizmo()
        {
            DrawIcon();
        }


        private void DrawIcon()
        {
            Gizmos.DrawIcon(_center, _name, _allowScaling);
        }
    }
}
