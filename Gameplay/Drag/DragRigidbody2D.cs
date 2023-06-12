using UnityEngine;

namespace SombraStudios.Gameplay.Drag
{

    /// <summary>
    /// Class to drag and drop and object, affected by physics, with the mouse over the Z axis.
    /// Requiered: any collider or collider2D and rigidbody or rigidbody2D
    /// Note: In case using 3D objects change "Rigidbody2D" for "Rigidbody"
    /// </summary>
    public class DragRigidbody2D : MonoBehaviour
    {
        // Distance from the center of the object and the click.
        private Vector3 _offset;
        [SerializeField]
        private Rigidbody2D _rb2d;

        private float _zCord;


        private void Start()
        {
            if (_rb2d == null)
            {
                _rb2d = GetComponent<Rigidbody2D>();
            }
        }

        private void OnMouseDown()
        {
            _zCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

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
            mousePoint.z = _zCord;

            // Convert it to world points
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        private void RestartPhysics(bool RestartIt)
        {
            // Check if there are Rigidbodies in the children objects
            var rigidbodies = gameObject.GetComponentsInChildren<Rigidbody2D>();

            if (RestartIt)
            {
                _rb2d.simulated = true;
                _rb2d.velocity = new Vector3(0f, 0f, 0f);
                _rb2d.angularVelocity = 0f;

                foreach (var rb in rigidbodies)
                {
                    rb.simulated = true;
                    rb.velocity = new Vector3(0f, 0f, 0f);
                    rb.angularVelocity = 0f;
                }
            }
            else
            {
                _rb2d.simulated = false;

                foreach (var rb in rigidbodies)
                {
                    rb.simulated = false;
                }
            }
        }
    }
}
