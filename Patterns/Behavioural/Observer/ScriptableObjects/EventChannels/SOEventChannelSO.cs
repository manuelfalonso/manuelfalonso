using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject event channel for events with a ScriptableObject parameter.
    /// </summary>
    [CreateAssetMenu(fileName = "NewSOEventChannel", menuName = "Sombra Studios/Events/SO Event Channel")]
    public class SOEventChannelSO : GenericEventChannelSO<ScriptableObject>
    {
        // Inherits functionality from GenericEventChannelSO<T>
    }
}
