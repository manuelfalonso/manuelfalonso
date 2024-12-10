using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects.Editor
{
    /// <summary>
    /// This event channel broadcasts and carries Scriptable Object payload.
    /// </summary>
    [CustomEditor(typeof(SOEventChannelSO))]
    public class SOEventChannelSOEditor : GenericEventChannelSOEditor<ScriptableObject>
    {
    }
}