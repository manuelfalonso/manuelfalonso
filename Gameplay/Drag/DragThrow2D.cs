using UnityEngine;

namespace SombraStudios.Gameplay.Drag
{

    /// <summary>
    /// Class to drag and throw and object with the mouse over the Z Camera axis.
    /// It consider the distance of the las time gap to calculate the velocity.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class DragThrow2D : MonoBehaviour
    {
        // Distance from the center of the object and the click.
        private Vector3 _Offset;
        private Vector3 _lastMousePosition;
        private Vector3 _throwDirection;
        [SerializeField]
        private Rigidbody2D _rb2d;
        [SerializeField]
        private Rigidbody2D[] _childRb2d;

        private float _zCord;
        [SerializeField] private float _speed = 1;
        [SerializeField] float _timeGap = 0.5f;


        private void Start()
        {
            if (_rb2d == null)
                _rb2d = GetComponent<Rigidbody2D>();

            // Get if there are Rigidbodies in the children objects
            if (_childRb2d == null)
                _childRb2d = gameObject.GetComponentsInChildren<Rigidbody2D>();

            InvokeRepeating("SetLastMousePosition", 0, _timeGap);
        }

        private void OnMouseDown()
        {
            _zCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

            // Store offset = gameobject world pos - mouse world pos
            _Offset = gameObject.transform.position - GetMouseAsWorldPoint();

            // Stop physics simulation because it causes weird things after OnMouseUp
            StopPhysics();
        }

        private void OnMouseDrag()
        {
            transform.position = GetMouseAsWorldPoint() + _Offset;
        }

        private void OnMouseUp()
        {
            RestartPhysics();

            _throwDirection = _lastMousePosition - Input.mousePosition;
            _rb2d.AddForce(_throwDirection * _speed * -1, ForceMode2D.Force);
        }


        private Vector3 GetMouseAsWorldPoint()
        {
            // Pixel coordinates of mouse (x,y)
            Vector3 mousePoint = Input.mousePosition;

            // Z coordinate of game object on screen
            mousePoint.z = _zCord;

            // Convert it to world points
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        private void StopPhysics()
        {
            _rb2d.simulated = false;

            foreach (var rb in _childRb2d)
            {
                rb.simulated = false;
            }
        }

        private void RestartPhysics()
        {
            _rb2d.simulated = true;
            _rb2d.velocity = new Vector3(0f, 0f, 0f);
            _rb2d.angularVelocity = 0f;

            foreach (var rb in _childRb2d)
            {
                rb.simulated = true;
                rb.velocity = new Vector3(0f, 0f, 0f);
                rb.angularVelocity = 0f;
            }
        }

        private void SetLastMousePosition()
        {
            _lastMousePosition = Input.mousePosition;
        }
    }
}
