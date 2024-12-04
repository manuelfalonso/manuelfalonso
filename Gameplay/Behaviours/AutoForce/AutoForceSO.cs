using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.AutoForce
{
    /// <summary>
    /// Settings for automatic force application.
    /// </summary>
    [CreateAssetMenu(fileName = "AutoForceSettings", menuName = "Sombra Studios/Behaviours/AutoForceSettings")]
    public class AutoForceSO : ScriptableObject
    {
        [Header("Force Settings")]

        /// <summary>
        /// Size of force along the world x-axis.
        /// </summary>
        [Range(-1000f, 1000f)]
        public float ForceSizeX = 0f;

        /// <summary>
        /// Size of force along the world y-axis.
        /// </summary>
        [Range(-1000f, 1000f)]
        public float ForceSizeY = 0f;

        /// <summary>
        /// Size of force along the world z-axis.
        /// </summary>
        [Range(-1000f, 1000f)]
        public float ForceSizeZ = 0f;

        /// <summary>
        /// The type of force to apply.
        /// </summary>
        public ForceMode ForceMode = ForceMode.Force;
    }
}
