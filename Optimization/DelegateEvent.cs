using UnityEngine;

/// <summary>
/// Example Class using delegate and event.
/// The static event - Subscription, Unsubscription and ChangeColor method
/// can be on any other script.
/// Also events support multiple subscriptions
/// </summary>
public class DelegateEvent : MonoBehaviour
{
    public delegate void ChangeRandomColor(Color color);
    // Static event to be accesed from other scripts
    public static event ChangeRandomColor OnClick;

    private MeshRenderer ms;

    private void Start()
    {
        // Subscribe to event
        OnClick += ChangeColor;
        ms = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value, Random.value);
            // Execution of methods subscribe to the event with the send parameter
            OnClick(randomColor);
        }
    }

    void ChangeColor(Color newColor)
    {
        ms.material.color = newColor;
    }

    private void OnDisable()
    {
        // Unsubscribe from event
        OnClick -= ChangeColor;
    }
}
