using UnityEditor;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects.Editor
{
    /// <summary>
    /// This event channel broadcasts and carries String payload.
    /// </summary>
    [CustomEditor(typeof(StringEventChannelSO))]
    public class StringEventChannelSOEditor : GenericEventChannelSOEditor<string>
    {
    }
}
