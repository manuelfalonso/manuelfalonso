using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.AutoRotate
{
    /// <summary>
    /// Settings for automatic rotation of a GameObject.
    /// </summary>
    [CreateAssetMenu(fileName = "AutoRotateSettings", menuName = "Sombra Studios/Behaviours/AutoRotateSettings")]
    public class AutoRotateSO : ScriptableObject
    {
        [Header("Rotation Settings")]

        /// <summary>
        /// The speed of rotation around the X axis.
        /// </summary>
        [Range(-1000f, 1000f)]
        public float RotationSpeedX = 0f;

        /// <summary>
        /// The speed of rotation around the Y axis.
        /// </summary>
        [Range(-1000f, 1000f)]
        public float RotationSpeedY = 0f;

        /// <summary>
        /// The speed of rotation around the Z axis.
        /// </summary>
        [Range(-1000f, 1000f)]
        public float RotationSpeedZ = 0f;

        /// <summary>
        /// The space in which the rotation is applied (Self or World).
        /// </summary>
        public Space Space = Space.Self;
    }
}
