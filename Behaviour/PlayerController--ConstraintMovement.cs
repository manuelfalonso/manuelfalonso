using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 2.5f;
    private float upZBound = 10f;
    private float downZBound = 0.0f;

    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerPosition();
    }

    // Create movement with arrow keys
    void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        playerRb.AddForce(speed * horizontalInput, 0, speed * verticalInput);
    }

    // Limit player movement in screen
    void ConstrainPlayerPosition()
    {
        if (transform.position.z > upZBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, upZBound);
            playerRb.velocity = new Vector3(playerRb.velocity.x, playerRb.velocity.y, 0);
        }

        if (transform.position.z < downZBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, downZBound);
            playerRb.velocity = new Vector3(playerRb.velocity.x, playerRb.velocity.y, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enter collision with enemy");
        }
    }
}
