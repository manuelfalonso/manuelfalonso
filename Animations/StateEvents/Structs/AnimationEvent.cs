using SombraStudios.Shared.ScriptableObjects.Values;
using System;
using UnityEngine.Events;

namespace SombraStudios.Shared.Animations.StateEvents
{
    /// <summary>  
    /// Struct representing an animation event.  
    /// </summary>  
    [Serializable]
    public struct AnimationEvent
    {
        /// <summary>  
        /// The name of the event.  
        /// </summary>  
        public StringValueSO EventName;

        /// <summary>  
        /// The action to be invoked when the event is triggered.  
        /// </summary>  
        public UnityEvent<AnimationEventData> EventAction;
    }
}
