using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject event channel for events with a float parameter.
    /// </summary>
    [CreateAssetMenu(fileName = "New Float Event Channel", menuName = "Sombra Studios/Events/Float Event Channel")]
    public class FloatEventChannelSO : GenericEventChannelSO<float>
    {
        // Inherits functionality from GenericEventChannelSO<T>
    }
}
