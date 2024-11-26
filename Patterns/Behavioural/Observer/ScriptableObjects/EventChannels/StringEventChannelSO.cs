using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject event channel for events with a string parameter.
    /// </summary>
    [CreateAssetMenu(fileName = "New String Event Channel", menuName = "Sombra Studios/Events/String Event Channel")]
    public class StringEventChannelSO : GenericEventChannelSO<string>
    {
        // Inherits functionality from GenericEventChannelSO<T>
    }
}
