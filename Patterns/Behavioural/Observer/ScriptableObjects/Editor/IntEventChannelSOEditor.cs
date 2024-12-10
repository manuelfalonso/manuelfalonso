using UnityEditor;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects.Editor
{
    /// <summary>
    /// This event channel broadcasts and carries Integer payload.
    /// </summary>
    [CustomEditor(typeof(IntEventChannelSO))]
    public class IntEventChannelSOEditor : GenericEventChannelSOEditor<int>
    {
    }
}
