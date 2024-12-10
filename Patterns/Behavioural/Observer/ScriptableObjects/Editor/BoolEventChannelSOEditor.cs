using UnityEditor;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects.Editor
{
    /// <summary>
    /// This event channel broadcasts and carries boolean payload.
    /// </summary>
    [CustomEditor(typeof(BoolEventChannelSO))]
    public class BoolEventChannelSOEditor : GenericEventChannelSOEditor<bool>
    {
    }
}
