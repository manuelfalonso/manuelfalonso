using SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Objectives
{
    /// <summary>
    /// ScriptableObject event channel for events with a ObjectiveSO parameter.
    /// </summary>
    [CreateAssetMenu(fileName = "NewObjectiveEventChannel", menuName = "Sombra Studios/Events/Objective Event Channel")]
    public class ObjectiveEventChannelSO : GenericEventChannelSO<ObjectiveSO>
    {
        // Inherits functionality from GenericEventChannelSO<T>
    }
}
