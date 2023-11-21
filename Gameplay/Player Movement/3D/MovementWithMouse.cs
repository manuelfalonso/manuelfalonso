using UnityEngine;

namespace SombraStudios.Shared.Gameplay.PlayerMovement
{
    /// <summary>
    /// Send a position to a coroutine to move an object with lerp
    /// Object require MovementLerpCoroutine.cs script
    /// </summary>
    public class MovementWithMouse : MonoBehaviour
    {
        [SerializeField] private MovementLerpCoroutine coroutineScript;

        void OnMouseDown()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);

            Debug.Log("Raycast");


            if (hit.collider.gameObject == gameObject)
            {
                Vector3 newTarget = hit.point;
                coroutineScript.Target = newTarget;
                Debug.Log("Set target: " + newTarget);
            }
        }
    }
}