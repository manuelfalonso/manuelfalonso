using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject event channel for events with a Vector2 parameter.
    /// </summary>
    [CreateAssetMenu(fileName = "NewVector2EventChannel", menuName = "Sombra Studios/Events/Vector2 Event Channel")]
    public class Vector2EventChannelSO : GenericEventChannelSO<Vector2>
    {
        // Inherits functionality from GenericEventChannelSO<T>
    }
}
