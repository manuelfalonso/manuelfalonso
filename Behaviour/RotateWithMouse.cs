using UnityEngine;

/// <summary>
/// Rotate an object with mouse movement
/// </summary>
public class RotateWithMouse : MonoBehaviour
{
    [SerializeField]
    private int speed = 3;

    private void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * speed;
        float mouseY = Input.GetAxis("Mouse Y") * speed;
        transform.Rotate(mouseY, mouseX, 0);
    }
}
