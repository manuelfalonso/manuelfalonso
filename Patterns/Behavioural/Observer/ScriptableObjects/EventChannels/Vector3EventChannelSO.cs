using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>  
    /// ScriptableObject event channel for events with a Vector3 parameter.  
    /// </summary>  
    [CreateAssetMenu(fileName = "New Vector3 Event Channel", menuName = "Sombra Studios/Events/Vector3 Event Channel")]
    public class Vector3EventChannelSO : GenericEventChannelSO<Vector3>
    {
        // Inherits functionality from GenericEventChannelSO<T>  
    }
}
