using UnityEngine;

/// <summary>
/// Uses Quaternion class to look at another Transform.
/// </summary>
public class LookAtTransform : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        Vector3 relativePos = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos);
    }
}
