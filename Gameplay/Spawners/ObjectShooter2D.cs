using UnityEngine;

namespace SombraStudios.Gameplay.Spawners
{

    /// <summary>
    /// Shooter script
    /// Required:
    /// Must exists a "Bullet" tag
    /// and Player tag must be used
    /// </summary>
    public class ObjectShooter2D : MonoBehaviour
    {
	    [Header("Object creation")]

	    [SerializeField] private GameObject _prefabToSpawn;
	    [Tooltip("The key to press to create the objects/projectiles")]
	    [SerializeField] private KeyCode _keyToPress = KeyCode.Space;

	    [Header("Other options")]

	    [Tooltip("The rate of creation, as long as the key is pressed")]
	    [SerializeField] private float _creationRate = .5f;
	    [Tooltip("The speed at which the object are shot along the Y axis")]
	    [SerializeField] private float _shootSpeed = 5f;
	    [SerializeField] private Vector2 _shootDirection = new Vector2(1f, 1f);
	    [SerializeField] private bool _relativeToRotation = true;

	    private float _timeOfLastSpawn = default;

	    // Use this for initialization
	    void Start()
	    {
		    _timeOfLastSpawn = -_creationRate;
	    }

	    // Update is called once per frame
	    void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            if (Input.GetKey(_keyToPress)
               && Time.time >= _timeOfLastSpawn + _creationRate)
            {
                Vector2 actualBulletDirection = _relativeToRotation ?
                    (Vector2)(Quaternion.Euler(0, 0, transform.eulerAngles.z) * _shootDirection)
                    : _shootDirection;

                GameObject newObject = Instantiate(_prefabToSpawn);
                newObject.transform.position = transform.position;
                newObject.transform.eulerAngles =
                    new Vector3(0f, 0f, GetAngle(actualBulletDirection));
                newObject.tag = "Bullet";

                // push the created objects, but only if they have a Rigidbody2D
                Rigidbody2D rigidbody2D = newObject.GetComponent<Rigidbody2D>();
                if (rigidbody2D != null)
                {
                    rigidbody2D.AddForce(actualBulletDirection * _shootSpeed, ForceMode2D.Impulse);
                }

                _timeOfLastSpawn = Time.time;
            }
        }

        private float GetAngle(Vector2 inputVector)
        {
            if (inputVector.x < 0)
                return (Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg * -1) - 360;
            else
                return -Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg;
        }
    }
}
