using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.MoveTowards
{
    /// <summary>  
    /// ScriptableObject to store settings for the Move Towards functionality.  
    /// </summary>  
    [CreateAssetMenu(fileName = "MoveTowardsSettings", menuName = "Sombra Studios/Behaviours/MoveTowardsSettings")]
    public class MoveTowardsSettings : ScriptableObject
    {
        [Header("Move Towards Settings")]

        /// <summary>  
        /// The target position to move towards.  
        /// </summary>  
        public Vector3 TargetPosition;

        /// <summary>  
        /// The maximum distance to move towards the target per frame. Can be negtive to move away in opposite 
        /// direction from target. 
        /// </summary>  
        public float MaxDistanceDelta;
    }
}
