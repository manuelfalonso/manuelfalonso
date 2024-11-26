using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject event channel for events with a boolean parameter.
    /// </summary>
    [CreateAssetMenu(fileName = "New Bool Event Channel", menuName = "Sombra Studios/Events/Bool Event Channel")]
    public class BoolEventChannelSO : GenericEventChannelSO<bool>
    {
        // Inherits functionality from GenericEventChannelSO<T>
    }
}
