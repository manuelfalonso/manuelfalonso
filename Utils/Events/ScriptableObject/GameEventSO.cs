using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SO Event used to decoupled code instead a normal class.
/// </summary>
[CreateAssetMenu(fileName = "New Game Event", menuName = "Game Event", order = 51)]
public class GameEventSO : ScriptableObject
{
    private List<GameEventSOListener> listeners = new List<GameEventSOListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventSOListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventSOListener listener)
    {
        listeners.Remove(listener);
    }
}
