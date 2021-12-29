using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// SO Event Listener using a Unity Event.
/// </summary>
public class GameEventSOListener : MonoBehaviour
{
    [SerializeField]
    private GameEventSO gameEvent;
    [SerializeField]
    private UnityEvent response;

    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }
}
