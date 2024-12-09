using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject event channel for events with an integer parameter.
    /// </summary>
    [CreateAssetMenu(fileName = "NewIntEventChannel", menuName = "Sombra Studios/Events/Int Event Channel")]
    public class IntEventChannelSO : GenericEventChannelSO<int>
    {
        // Inherits functionality from GenericEventChannelSO<T>
    }
}
