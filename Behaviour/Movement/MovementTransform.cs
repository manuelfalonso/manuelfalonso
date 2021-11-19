using UnityEngine;

/// <summary>
/// Apply movement to a game object with keyboard input and Transform position
/// </summary>
public class MovementTransform : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movementInput = Vector3.zero;

        // Forward and backward movement
        if (Input.GetKey(KeyCode.W))
        {
            movementInput.z = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movementInput.z = -1;
        }

        // Lateral movement
        if (Input.GetKey(KeyCode.A))
        {
            movementInput.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movementInput.x = 1;
        }

        // Vertical movement
        if (Input.GetKey(KeyCode.E))
        {
            movementInput.y = 1;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            movementInput.y = -1;
        }

        // Apply movement with Trasnform
        transform.Translate(movementInput.normalized * speed * Time.deltaTime);
    }
}
