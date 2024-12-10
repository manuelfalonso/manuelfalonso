using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects.Editor
{
    /// <summary>
    /// This event channel broadcasts and carries Vector3 payload.
    /// </summary>
    [CustomEditor(typeof(Vector3EventChannelSO))]
    public class Vector3EventChannelSOEditor : GenericEventChannelSOEditor<Vector3>
    {
    }
}