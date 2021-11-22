using UnityEngine;

/// <summary>
/// Constrain movement delimited by a box Collider
/// NOTE: Don`t work with CharacterController movement
/// </summary>
public class ConstrainToBounds: MonoBehaviour
{
    [SerializeField] private BoxCollider bounds;

    private Rigidbody _playerRb;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        PositionConstraint();
    }

    private void PositionConstraint()
    {
        if (!bounds.bounds.Contains(transform.position)) 
        {
            transform.position = new Vector3(
                Mathf.Clamp(
                    transform.position.x,
                    bounds.transform.position.x - bounds.size.x / 2,
                    bounds.transform.position.x + bounds.size.x / 2),
                Mathf.Clamp(
                    transform.position.y,
                    bounds.transform.position.y - bounds.size.y / 2,
                    bounds.transform.position.y + bounds.size.y / 2),
                Mathf.Clamp(
                    transform.position.z,
                    bounds.transform.position.z - bounds.size.z / 2,
                    bounds.transform.position.z + bounds.size.z / 2)
                );
            if (_playerRb != null)
            {
                _playerRb.velocity = Vector3.zero;
            }
        }
    }
}
