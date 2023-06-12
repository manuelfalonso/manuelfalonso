using UnityEngine;

namespace SombraStudios.Gameplay.Drag
{

    /// <summary>
    /// Class to drag and throw and object with the mouse over the Z Camera axis.
    /// It freeze and draw a line as a charge shot.
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(LineRenderer))]
    public class ChargeThrow : MonoBehaviour
    {
        [SerializeField] float _speed = 100f;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private LineRenderer _lineRenderer;

        private Vector3 _lineStart;
        private Vector3 _lineEnd;
        private Vector3 _throwDirection;
        private Rigidbody[] _childRbs;

        private float _zCord;


        private void Start()
        {
            if (_rigidBody == null)
                _rigidBody = GetComponent<Rigidbody>();
            if (_lineRenderer == null)
                _lineRenderer = GetComponent<LineRenderer>();

            // Initialize in 0 points of the line renderer
            _lineRenderer.positionCount = 0;

            // Get if there are Rigidbodies in the children objects
            _childRbs = gameObject.GetComponentsInChildren<Rigidbody>();
        }

        private void OnMouseDown()
        {
            _lineRenderer.positionCount = 1;
            _lineStart = transform.position;
            _lineRenderer.SetPosition(0, _lineStart);

            _zCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

            // Stop physics simulation because it causes weird things after OnMouseUp
            StopPhysics();
        }

        private void OnMouseDrag()
        {
            _lineEnd = GetMouseAsWorldPoint();
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(1, _lineEnd);
        }

        private void OnMouseUp()
        {
            RestartPhysics();

            _throwDirection = _lineStart - _lineEnd;
            _rigidBody.AddForce(_throwDirection * _speed, ForceMode.Force);
            _lineRenderer.positionCount = 0;
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
            _rigidBody.isKinematic = true;

            foreach (var rb in _childRbs)
            {
                rb.isKinematic = true;
            }
        }

        private void RestartPhysics()
        {
            _rigidBody.isKinematic = false;
            _rigidBody.velocity = new Vector3(0f, 0f, 0f);
            _rigidBody.angularVelocity = Vector3.zero;

            foreach (var rb in _childRbs)
            {
                rb.isKinematic = false;
                rb.velocity = new Vector3(0f, 0f, 0f);
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
