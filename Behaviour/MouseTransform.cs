using UnityEngine;
using System.Collections;

/// <summary>
/// Send a position to a coroutine to move an object with lerp
/// Object require CoroutineLerp.cs script
/// </summary>
public class MoveWithMouse : MonoBehaviour
{
    public CoroutineLerp coroutineScript;

    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);

        if (hit.collider.gameObject == gameObject)
        {
            Vector3 newTarget = hit.point + new Vector3(0, 0.5f, 0);
            coroutineScript.Target = newTarget;
        }
    }
}