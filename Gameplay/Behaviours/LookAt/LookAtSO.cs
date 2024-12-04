using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.LookAt
{
    /// <summary>  
    /// ScriptableObject to store settings for the "Look At" functionality.  
    /// </summary>  
    [CreateAssetMenu(fileName = "LookAtSettings", menuName = "Sombra Studios/Behaviours/LookAtSettings")]
    public class LookAtSO : ScriptableObject
    {
        [Header("LookAt Settings")]

        /// <summary>  
        /// The world position to look at.  
        /// </summary>  
        public Vector3 WorldPosition;

        /// <summary>  
        /// The world up direction.  
        /// </summary>  
        public Vector3 WorldUp;
    }
}
