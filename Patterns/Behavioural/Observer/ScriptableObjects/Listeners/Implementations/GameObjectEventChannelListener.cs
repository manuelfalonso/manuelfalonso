using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// A listener class for GameObject event channels.
    /// Inherits from <see cref="GenericEventChannelListener{T}"/> with a GameObject type parameter.
    /// </summary>
    public class GameObjectEventChannelListener : GenericEventChannelListener<GameObject>
    {
    }
}