using System.Collections;
using System.Collections.Generic;
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
    private Vector3 initialMousePos;
    private Vector3 throwDirection;
    private Rigidbody2D rb2d;

    private float mZCord;
    [SerializeField] float speed = 1;
    [SerializeField] float timeGap = 0.5f;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
		// Invoke a repeating method to track the position of the mouse
        InvokeRepeating("LastMousePosition", 0, timeGap);
    }

    private void OnMouseDown()
    {
        mZCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

        //initialMousePos = Input.mousePosition;

        // Stop physics simulation because it causes weird things after OnMouseUp
        RestartPhysics(false);
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

    private void RestartPhysics(bool RestartIt)
    {
        // Check if there are Rigidbodies in the children objects
        var rigidbodies = gameObject.GetComponentsInChildren<Rigidbody2D>();

        if (RestartIt)
        {
            rb2d.simulated = true;
            rb2d.velocity = new Vector3(0f, 0f, 0f);
            rb2d.angularVelocity = 0f;

            foreach (var rb in rigidbodies)
            {
                rb.simulated = true;
                rb.velocity = new Vector3(0f, 0f, 0f);
                rb.angularVelocity = 0f;
            }
        }
        else
        {
            rb2d.simulated = false;

            foreach (var rb in rigidbodies)
            {
                rb.simulated = false;
            }
        }
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + mOffset;
    }

    private void OnMouseUp()
    {
        RestartPhysics(true);
        throwDirection = initialMousePos - Input.mousePosition;
        rb2d.AddForce(throwDirection * speed * -1, ForceMode2D.Force);
    }

    void LastMousePosition()
    {
        initialMousePos = Input.mousePosition;
    }
}
