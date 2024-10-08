using UnityEngine;

namespace SombraStudios.Shared.Gameplay.PlayerMovement2D
{
    /// <summary>
    /// Top down player movement 2d with boundaries limits
    /// </summary>
    public class PlayerMovement2DTopDown : MonoBehaviour
    {
        // configuration parameters
        [Header("Player")]
        [SerializeField] float moveSpeed = 10f;
        [SerializeField] float padding = 1f;

        float xMin;
        float xMax;
        float yMin;
        float yMax;

        // Use this for initialization
        void Start()
        {
            SetUpMoveBoundaries();
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        private void Move()
        {
            var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
            var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

            var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
            var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
            transform.position = new Vector2(newXPos, newYPos);
        }

        private void SetUpMoveBoundaries()
        {
            Camera gameCamera = Camera.main;
            xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
            xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
            yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
            yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
        }
    }
}
