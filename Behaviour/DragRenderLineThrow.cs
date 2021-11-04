using UnityEngine;

/// <summary>
/// Class to drag and throw and object with the mouse over the Z Camera axis.
/// It freeze and draw a line as a charge shot.
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(LineRenderer))]
public class DragRenderLineThrow : MonoBehaviour
{
    [SerializeField] float _speed = 3;

    private Vector3 _throwDirection;
    private Rigidbody _rb;
    private Rigidbody[] _childRb2d;

    private Vector3 _initialMousePosition;
    private LineRenderer _lr;
    private Vector3 _lineEnd;

    private float mZCord;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _lr = GetComponent<LineRenderer>();

        // Get if there are Rigidbodies in the children objects
        _childRb2d = gameObject.GetComponentsInChildren<Rigidbody>();
    }

    private void OnMouseDown()
    {
        _lr.positionCount = 1;
        _lr.SetPosition(0, transform.position);
        _initialMousePosition = Input.mousePosition;

        mZCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Stop physics simulation because it causes weird things after OnMouseUp
        StopPhysics();
    }

    private void OnMouseDrag()
    {
        _lineEnd = GetMouseAsWorldPoint();
        _lr.positionCount = 2;
        _lr.SetPosition(1, _lineEnd);
    }

    private void OnMouseUp()
    {
        RestartPhysics();

        _throwDirection = Input.mousePosition - _initialMousePosition;

        _rb.AddForce(_throwDirection * _speed * -1, ForceMode.Force);

        _lr.positionCount = 0;
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
        _rb.velocity = new Vector3(0f, 0f, 0f);
        _rb.angularVelocity = Vector3.zero;

        foreach (var rb in _childRb2d)
        {
            rb.isKinematic = false;
            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.angularVelocity = Vector3.zero;
        }
    }
}
