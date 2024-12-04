using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.LookWithLerp
{
    /// <summary>
    /// ScriptableObject to store settings for the "Look With Lerp" functionality.
    /// </summary>
    [CreateAssetMenu(fileName = "LookWithLerpSettings", menuName = "Sombra Studios/Behaviours/LookWithLerpSettings")]
    public class LookWithLerpSO : ScriptableObject
    {
        [Header("LookWithLerp Settings")]

        /// <summary>
        /// The direction to look in.
        /// </summary>
        public Vector3 ForwardVector;

        /// <summary>
        /// The vector that defines in which direction up is.
        /// </summary>
        public Vector3 UpVector;

        /// <summary>
        /// The velocity applied to the Lerp.
        /// </summary>
        [Min(0f)]
        public float Speed;
    }
}
