using System;
using UnityEngine;

/// <summary>
/// Singleton Class to manage all events
/// 1- Create an event Action
/// 2- Create a method to invoke the event.
/// 3- In Observers classes create the neceesary methods
/// 4- In Observers classes Subscribe this methods
/// 5- In Observers classes Unsubscribe this methods
/// 6- Call the asociated event invoke method from the place needed.
/// </summary>
public class Events : Singleton<Events>
{
    /// <summary>
    /// Example event and invoke method of a opening door.
    /// </summary>
    public event Action onTriggerEnterDoor;
    public void TriggerEnterDoor()
    {
        // Check if there are methods subscribed to the event
        if (onTriggerEnterDoor != null)
        {
            // Invoke the event
            onTriggerEnterDoor();
        }
    }
}
