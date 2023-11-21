using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Drag
{
    /// <summary>
    /// Class to drag and drop and object with the mouse over the Z axis.
    /// Requiered: any collider or collider2D
    /// Note: only works without Rigidbody attached
    ///     why? Beacouse the physics engine continue working during the drag. 
    ///     After the drag the engine process all the data "cached" during that time.
    ///     This make weired acceleration and rotation.
    /// </summary>
    public class Drag : MonoBehaviour
    {
        // Distance from the center of the object and the click.
        private Vector3 _offset;

        private float _zCord;


        private void OnMouseDown()
        {
            _zCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

            // Store offset = gameobject world pos - mouse world pos
            _offset = gameObject.transform.position - GetMouseAsWorldPoint();
        }

        private void OnMouseDrag()
        {
            transform.position = GetMouseAsWorldPoint() + _offset;
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
    }
}
