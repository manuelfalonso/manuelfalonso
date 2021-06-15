using UnityEngine;

/// <summary>
/// Tilt an object sideways up to tiltAngle
/// </summary>
public class TiltObject : MonoBehaviour
{
    [SerializeField]
    float smooth = 5.0f;
    [SerializeField]
    float tiltAngle = 60.0f;

    void FixedUpdate()
    {
        // Smoothly tilts a transform towards a target rotation.
        float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
}
