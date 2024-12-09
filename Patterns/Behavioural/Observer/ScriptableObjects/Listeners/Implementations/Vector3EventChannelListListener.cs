using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// A listener class for Vector3 event channels.
    /// Inherits from <see cref="GenericEventChannelListListener{T}"/> with a Vector3 type parameter.
    /// </summary>
    public class Vector3EventChannelListListener : GenericEventChannelListListener<Vector3>
    {
    }
}