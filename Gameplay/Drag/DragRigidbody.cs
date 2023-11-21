using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Drag
{
    /// <summary>
    /// Class to drag and drop and object with the mouse over the Camera Z axis.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class DragRigidbody : MonoBehaviour
    {
        // Distance from the center of the object and the click.
        private Vector3 _offset;
        private Rigidbody _rb3d;

        private float _mZCord;


        private void Start()
        {
            _rb3d = GetComponent<Rigidbody>();
        }

        private void OnMouseDown()
        {
            _mZCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

            // Store offset = gameobject world pos - mouse world pos
            _offset = gameObject.transform.position - GetMouseAsWorldPoint();

            // Stop physics simulation because it causes weird things after OnMouseUp
            RestartPhysics(false);
        }

        private void OnMouseDrag()
        {
            transform.position = GetMouseAsWorldPoint() + _offset;
        }

        private void OnMouseUp()
        {
            RestartPhysics(true);
        }


        private Vector3 GetMouseAsWorldPoint()
        {
            // Pixel coordinates of mouse (x,y)
            Vector3 mousePoint = Input.mousePosition;

            // Z coordinate of game object on screen
            mousePoint.z = _mZCord;

            // Convert it to world points
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        private void RestartPhysics(bool restartIt)
        {
            if (restartIt)
            {
                _rb3d.isKinematic = false;
                _rb3d.velocity = new Vector3(0f, 0f, 0f);
                _rb3d.angularVelocity = Vector3.zero;
            }
            else
            {
                _rb3d.isKinematic = true;
            }

            // Check if there are Rigidbodies in the children objects
            if (gameObject.transform.childCount > 0)
            {
                var rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();

                if (restartIt)
                {
                    foreach (var rb in rigidbodies)
                    {
                        rb.isKinematic = false;
                        rb.velocity = new Vector3(0f, 0f, 0f);
                        rb.angularVelocity = Vector3.zero;
                    }
                }
                else
                {
                    foreach (var rb in rigidbodies)
                    {
                        rb.isKinematic = true;
                    }
                }
            }
        }
    }
}
