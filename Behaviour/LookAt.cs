using UnityEngine;

/// <summary>
/// transform.LookAt: Simple instruction to rotate an object to look at another.
/// Quaternion.LookRotation to look at another Transform.
/// </summary>
public class LookAt : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    void Update()
    {
        //TransformLookAt();
        //QuaternionLookRotation();
    }

    private void TransformLookAt()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

    private void QuaternionLookRotation()
    {
        Vector3 relativePos = _target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos);
    }
}
