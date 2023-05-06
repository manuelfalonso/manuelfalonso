using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// SO Event Listener using a Unity Event.
/// Add to the response UnityEvent every neccesary action.
/// Limitations: 
/// Must define GameEventSO for each type of data structure needed.
/// UnityEvents in Editor dont support multi parameter unless they are dynamic.
/// If the object is disabled will stop listening.
/// </summary>
public class GameEventSOListener : MonoBehaviour
{
    [SerializeField]
    private GameEventSO _gameEvent;
    [SerializeField]
    private UnityEvent _response;


    private void OnEnable()
    {
        _gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        _gameEvent.UnregisterListener(this);
    }


    // Called from Game Event Scriptable Object
    public void OnEventRaised()
    {
        _response.Invoke();
    }
}
