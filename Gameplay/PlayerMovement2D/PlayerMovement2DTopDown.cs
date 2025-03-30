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

        [Header("Boundaries")]
        [SerializeField] float padding = 1f;

        [Header("Configuration")]
        [SerializeField] private bool _applyBoundaries;
        [SerializeField] private bool _applyRotation = true;

        private float xMin;
        private float xMax;
        private float yMin;
        private float yMax;

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

            var newXPos = _applyBoundaries ? Mathf.Clamp(transform.position.x + deltaX, xMin, xMax) : 
                transform.position.x + deltaX;
            var newYPos = _applyBoundaries ? Mathf.Clamp(transform.position.y + deltaY, yMin, yMax) : 
                transform.position.y + deltaY;

            ApplyRotation(deltaX, deltaY);

            transform.position = new Vector2(newXPos, newYPos);
        }

        private void ApplyRotation(float deltaX, float deltaY)
        {
            if (!_applyRotation) return;

            Vector2 movement = new Vector2(deltaX, deltaY);
            if (movement.sqrMagnitude > 0.001f) // Avoid rotation when idle
            {
                float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        private void SetUpMoveBoundaries()
        {
            if (!_applyBoundaries) return;

            Camera gameCamera = Camera.main;
            xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
            xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
            yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
            yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
        }
    }
}
