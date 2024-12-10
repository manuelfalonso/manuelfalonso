using UnityEngine;
using UnityEditor;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects.Editor
{
    /// <summary>
    /// This event channel broadcasts and carries GameObject payload.
    /// </summary>
    [CustomEditor(typeof(GameObjectEventChannelSO))]
    public class GameObjectEventChannelSOEditor : GenericEventChannelSOEditor<GameObject>
    {
    }
}


