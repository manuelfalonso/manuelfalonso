using UnityEngine;

namespace SombraStudios.Gameplay
{

    /// <summary>
    /// Raycast script for mouse object selection
    /// </summary>
    public class SelectionWithRaycast : MonoBehaviour
    {
        [Tooltip("Trigger selection with this key")]
        [SerializeField] private KeyCode _selectionKey = KeyCode.E;
        [Tooltip("Specify dimension to use Raycast correctely")]
        [SerializeField] private bool _is2D;

        private Camera _mainCamera;
        private bool _selectionKeyPressed = false;


        void Start()
        {
            _mainCamera = Camera.main;

            CheckCameraProjectionMode();
        }

        private void Update()
        {
            CheckInput();
        }

        void FixedUpdate()
        {
            if (_selectionKeyPressed)
            {
                if (_is2D)
                {
                    Raycast2D();
                }
                else
                {
                    Raycast3D();
                }
                _selectionKeyPressed = false;
            }
        }


        private void CheckCameraProjectionMode()
        {
            if (_is2D)
            {
                if (!_mainCamera.orthographic)
                {
                    Debug.LogError("Camera must be in Orthographic mode to Raycast correctly");
                }
            }
            else
            {
                if (_mainCamera.orthographic)
                {
                    Debug.LogError("Camera must be in Perspective mode to Raycast correctly");
                }
            }
        }

        private void CheckInput()
        {
            if (Input.GetKeyDown(_selectionKey))
            {
                _selectionKeyPressed = true;
            }
        }

        private void Raycast2D()
        {
            Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.transform != null)
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }

        private void Raycast3D()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }
}
