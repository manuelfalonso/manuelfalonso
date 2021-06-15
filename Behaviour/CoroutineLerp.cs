using UnityEngine;
using System.Collections;

/// <summary>
/// When receive a Target move with lerp the gameObject attach to it
/// </summary>
public class CoroutineLerp : MonoBehaviour
{
    public float smoothing = 7f;
    public Vector3 Target
    {
        get { return target; }
        set
        {
            target = value;

            StopCoroutine("Movement");
            StartCoroutine("Movement", target);
        }
    }

    private Vector3 target;

    IEnumerator Movement(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, target, smoothing * Time.deltaTime);

            yield return null;
        }
    }
}