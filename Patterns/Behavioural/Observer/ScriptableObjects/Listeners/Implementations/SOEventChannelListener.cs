using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// A listener class for ScriptableObject event channels.
    /// Inherits from <see cref="GenericEventChannelListener{T}"/> with a ScriptableObject type parameter.
    /// </summary>
    public class SOEventChannelListener : GenericEventChannelListener<ScriptableObject>
    {
    }
}