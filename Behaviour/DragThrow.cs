using System;
using UnityEngine;

/// <summary>
/// Class to drag and drop and object, affected by physics, with the mouse over the Z axis.
/// Requiered: any collider or collider2D and rigidbody or rigidbody2D
/// Note: In case using 3D objects change "Rigidbody2D" for "Rigidbody"
/// </summary>
public class DragThrow : MonoBehaviour
{
    // Distance from the center of the object and the click.
    private Vector3 mOffset;
    private Vector3 lastMousePosition;
    private Vector3 throwDirection;
    private Rigidbody2D rb2d;
    private Rigidbody2D[] childRb2d;

    private float mZCord;
    [SerializeField] float speed = 1;
    [SerializeField] float timeGap = 0.5f;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // Get if there are Rigidbodies in the children objects
        childRb2d = gameObject.GetComponentsInChildren<Rigidbody2D>();

        InvokeRepeating("SetLastMousePosition", 0, timeGap);
    }

    private void OnMouseDown()
    {
        mZCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

        //initialMousePos = Input.mousePosition;

        // Stop physics simulation because it causes weird things after OnMouseUp
        StopPhysics();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + mOffset;
    }

    private void OnMouseUp()
    {
        RestartPhysics();

        throwDirection = lastMousePosition - Input.mousePosition;
        rb2d.AddForce(throwDirection * speed * -1, ForceMode2D.Force);
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // Z coordinate of game object on screen
        mousePoint.z = mZCord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void StopPhysics()
    {
        rb2d.simulated = false;

        foreach (var rb in childRb2d)
        {
            rb.simulated = false;
        }
    }

    private void RestartPhysics()
    {
        rb2d.simulated = true;
        rb2d.velocity = new Vector3(0f, 0f, 0f);
        rb2d.angularVelocity = 0f;

        foreach (var rb in childRb2d)
        {
            rb.simulated = true;
            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.angularVelocity = 0f;
        }
    }

    void SetLastMousePosition()
    {
        lastMousePosition = Input.mousePosition;
    }
}
