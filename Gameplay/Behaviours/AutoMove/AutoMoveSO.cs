using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.AutoMove
{
    /// <summary>
    /// Settings for automatic movement of a GameObject.
    /// </summary>
    [CreateAssetMenu(fileName = "AutoMoveSettings", menuName = "Sombra Studios/Behaviours/AutoMoveSettings")]
    public class AutoMoveSO : ScriptableObject
    {
        [Header("Movement Settings")]

        /// <summary>
        /// The speed of movement along the X axis.
        /// </summary>
        public float MovementSpeedX = 0f;

        /// <summary>
        /// The speed of movement along the Y axis.
        /// </summary>
        public float MovementSpeedY = 0f;

        /// <summary>
        /// The speed of movement along the Z axis.
        /// </summary>
        public float MovementSpeedZ = 0f;

        /// <summary>
        /// The space in which the movement is applied (Self or World).
        /// </summary>
        public Space Space = Space.Self;
    }
}
