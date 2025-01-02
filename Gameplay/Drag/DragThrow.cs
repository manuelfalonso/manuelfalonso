using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Drag
{
    /// <summary>
    /// Class to drag and throw and object with the mouse over the Z Camera axis.
    /// It consider the distance of the las time gap to calculate the velocity.
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class DragThrow : MonoBehaviour
    {
        [SerializeField] float _speed = 3;
        [SerializeField] float _timeGap = 0.5f;

        // Distance from the center of the object and the click.
        private Vector3 _offset;
        private Vector3 _lastMousePosition;
        private Vector3 _throwDirection;
        private Rigidbody _rb;
        private Rigidbody[] _childRb2d;

        private float _zCord;


        private void Start()
        {
            _rb = GetComponent<Rigidbody>();

            // Get if there are Rigidbodies in the children objects
            _childRb2d = gameObject.GetComponentsInChildren<Rigidbody>();

            InvokeRepeating("SetLastMousePosition", 0, _timeGap);
        }

        private void OnMouseDown()
        {
            _zCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

            // Store offset = gameobject world pos - mouse world pos
            _offset = gameObject.transform.position - GetMouseAsWorldPoint();

            // Stop physics simulation because it causes weird things after OnMouseUp
            StopPhysics();
        }

        private void OnMouseDrag()
        {
            transform.position = GetMouseAsWorldPoint() + _offset;
        }

        private void OnMouseUp()
        {
            RestartPhysics();

            _throwDirection = _lastMousePosition - Input.mousePosition;
            _rb.AddForce(_throwDirection * _speed * -1, ForceMode.Force);
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
            _rb.isKinematic = true;

            foreach (var rb in _childRb2d)
            {
                rb.isKinematic = true;
            }
        }

        private void RestartPhysics()
        {
            _rb.isKinematic = false;
#if UNITY_6000_0_OR_NEWER
            _rb.linearVelocity = new Vector3(0f, 0f, 0f);
#else
            _rb.velocity = new Vector3(0f, 0f, 0f);
#endif
            _rb.angularVelocity = Vector3.zero;

            foreach (var rb in _childRb2d)
            {
                rb.isKinematic = false;
#if UNITY_6000_0_OR_NEWER
                rb.linearVelocity = new Vector3(0f, 0f, 0f);
#else
                rb.velocity = new Vector3(0f, 0f, 0f);
#endif
                rb.angularVelocity = Vector3.zero;
            }
        }

        private void SetLastMousePosition()
        {
            _lastMousePosition = Input.mousePosition;
        }
    }
}
