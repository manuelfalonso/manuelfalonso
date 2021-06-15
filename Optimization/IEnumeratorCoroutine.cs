using UnityEngine;
using System.Collections; // Required for IEnumerator

/// <summary>
/// Using Coroutines with IEnumerator to allow the use of WaitForSeconds
/// </summary>
public class IEnumeratorCoroutine : MonoBehaviour
{
    private IEnumerator coroutine;

    IEnumerator Start()
    {
        print("Starting " + Time.time);

        coroutine = WaitAndPrint(5);
        // Start function WaitAndPrint as a coroutine
        yield return StartCoroutine(coroutine);
        print("Done " + Time.time);
    }

    IEnumerator WaitAndPrint(float timeToWait)
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(timeToWait);
        print("WaitAndPrint " + Time.time);
    }
}
