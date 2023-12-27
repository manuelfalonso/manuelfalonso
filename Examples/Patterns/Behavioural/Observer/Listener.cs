using UnityEngine;

namespace SombraStudios.Shared.Examples.Patterns.Behavioural.Observer
{
    /// <summary>
    /// Listen to Observer event
    /// </summary>
    public class Listener : MonoBehaviour
    {
        private void OnEnable() => Observer.OnTriggerEvent += OnTriggerEventHandler;

        private void OnDisable() => Observer.OnTriggerEvent -= OnTriggerEventHandler;

        private void OnTriggerEventHandler() => Debug.Log("OnTriggerEvent Event Invoked");
    }
}
