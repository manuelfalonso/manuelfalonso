using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// A listener class for Vector3 event channels.
    /// Inherits from <see cref="GenericEventChannelListener{T}"/> with a Vector3 type parameter.
    /// </summary>
    public class Vector3EventChannelListener : GenericEventChannelListener<Vector3>
    {
    }
}