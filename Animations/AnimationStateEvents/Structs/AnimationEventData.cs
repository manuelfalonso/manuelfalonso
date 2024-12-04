using SombraStudios.ScriptableObjects.Values;
using System;
using UnityEngine;

namespace SombraStudios.Shared.Animations.AnimationStateEvents
{
    /// <summary>
    /// Struct representing the data associated with an animation event.
    /// </summary>
    [Serializable]
    public struct AnimationEventData
    {
        /// <summary>
        /// The name of the event.
        /// </summary>
        public StringValueSO EventName;

        /// <summary>
        /// A float parameter associated with the event.
        /// </summary>
        public float FloatParameter;

        /// <summary>
        /// An integer parameter associated with the event.
        /// </summary>
        public int IntParameter;

        /// <summary>
        /// A string parameter associated with the event.
        /// </summary>
        public string StringParameter;

        /// <summary>
        /// A ScriptableObject reference parameter associated with the event.
        /// </summary>
        public ScriptableObject ObjectReferenceParameter;
    }
}
