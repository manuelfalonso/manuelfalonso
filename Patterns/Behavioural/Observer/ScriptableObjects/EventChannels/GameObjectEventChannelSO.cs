using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject event channel for events with a GameObject parameter.
    /// </summary>
    [CreateAssetMenu(fileName = "NewGameObjectEventChannel", menuName = "Sombra Studios/Events/GameObject Event Channel")]
    public class GameObjectEventChannelSO : GenericEventChannelSO<GameObject>
    {
        // Inherits functionality from GenericEventChannelSO<T>
    }
}
