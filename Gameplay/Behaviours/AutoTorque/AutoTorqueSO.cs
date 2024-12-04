using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.AutoTorque
{
    /// <summary>
    /// Settings for automatic torque application.
    /// </summary>
    [CreateAssetMenu(fileName = "AutoTorqueSettings", menuName = "Sombra Studios/Behaviours/AutoTorqueSettings")]
    public class AutoTorqueSO : ScriptableObject
    {
        [Header("Torque Settings")]

        /// <summary>
        /// Size of torque along the world x-axis.
        /// </summary>
        [Range(-1000f, 1000f)]
        public float TorqueSizeX = 0f;

        /// <summary>
        /// Size of torque along the world y-axis.
        /// </summary>
        [Range(-1000f, 1000f)]
        public float TorqueSizeY = 0f;

        /// <summary>
        /// Size of torque along the world z-axis.
        /// </summary>
        [Range(-1000f, 1000f)]
        public float TorqueSizeZ = 0f;

        /// <summary>
        /// The type of torque to apply.
        /// </summary>
        public ForceMode ForceMode = ForceMode.Force;
    }
}
