using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject event channel for events with a GameObject parameter.
    /// </summary>
    [CreateAssetMenu(fileName = "New GameObject Event Channel", menuName = "Sombra Studios/Events/GameObject Event Channel")]
    public class GameObjectEventChannelSO : GenericEventChannelSO<GameObject>
    {
        // Inherits functionality from GenericEventChannelSO<T>
    }
}
