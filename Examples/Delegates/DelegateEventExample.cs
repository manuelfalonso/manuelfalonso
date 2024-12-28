using UnityEngine;

namespace SombraStudios.Shared.Examples.Delegates
{
    /// <summary>
    /// Example Class using delegate and event.
    /// The static event - Subscription, Unsubscription and ChangeColor method can be on any other script.
    /// Also, events support multiple subscriptions.
    /// </summary>
    [RequireComponent(typeof(MeshRenderer))]
    public class DelegateEventExample : MonoBehaviour
    {
        // Delegate
        public delegate void ChangeRandomColor(Color color);
        // Static event field to be accessed from other scripts
        public static event ChangeRandomColor OnClick;

        private MeshRenderer _meshRenderer;

        private void Start()
        {
            // Subscribe to event
            OnClick += ChangeColor;
		
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var randomColor = new Color(Random.value, Random.value, Random.value, Random.value);
                // Execution of methods subscribe to the event with the send parameter
                OnClick?.Invoke(randomColor);
            }
        }

        private void ChangeColor(Color newColor)
        {
            _meshRenderer.material.color = newColor;
        }

        private void OnDestroy()
        {
            // Unsubscribe from event
            OnClick -= ChangeColor;
        }
    }
}
