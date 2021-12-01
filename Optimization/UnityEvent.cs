using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Example Class using Unity events.
/// </summary>
public class UnityEvent : MonoBehaviour {

    public UnityEvent onSpacebar;
    public UnityEvent onReturn;


    public void OnTriggerStay()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onSpacebar != null)
            {
                onSpacebar.Invoke();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (onReturn != null)
            {
                onReturn.Invoke();
            }
        }
    }
}
