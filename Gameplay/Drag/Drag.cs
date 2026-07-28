using System;
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
        [Header("Drag Settings")]
        [SerializeField] private bool _useSmoothMovement = true;
        [SerializeField] private float _smoothSpeed = 10f;
        [SerializeField] private float _snapDistance = 0.01f;

        // Distance from the center of the object and the click.
        private Vector3 _offset;
        private Camera _mainCamera;
        private Transform _transform;
        private float _zCord;
        private Vector3 _targetPosition;
        private bool _isDragging = false;

        #region Unity Messages
        private void Awake()
        {
            CheckObjectCollider();
            _mainCamera = Camera.main;
            _transform = transform;
        }

        private void OnMouseDown()
        {
            CaptureDragOffset();
            _isDragging = true;
        }

        private void OnMouseDrag()
        {
            UpdateTargetPosition();
        }

        private void OnMouseUp()
        {
            _isDragging = false;
        }

        private void Update()
        {
            if (_isDragging)
            {
                UpdateDraggedObjectPosition();
            }
        }
        #endregion

        #region Private Methods
        private void CheckObjectCollider()
        {
            if (GetComponent<Collider>() == null && GetComponent<Collider2D>() == null)
            {
                throw new Exception($"The object {gameObject.name} requires a Collider or Collider2D to use Drag component.");
            }
        }

        private void CaptureDragOffset()
        {
            _zCord = _mainCamera.WorldToScreenPoint(_transform.position).z;
            _offset = _transform.position - GetMouseAsWorldPoint();
            _targetPosition = _transform.position;
        }

        private void UpdateTargetPosition()
        {
            _targetPosition = GetMouseAsWorldPoint() + _offset;
        }

        private void UpdateDraggedObjectPosition()
        {
            if (_useSmoothMovement)
            {
                _transform.position = Vector3.Lerp(_transform.position, _targetPosition, _smoothSpeed * Time.deltaTime);
                
                // Snap to target when close enough
                if (Vector3.Distance(_transform.position, _targetPosition) < _snapDistance)
                {
                    _transform.position = _targetPosition;
                }
            }
            else
            {
                _transform.position = _targetPosition;
            }
        }

        private Vector3 GetMouseAsWorldPoint()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = _zCord;
            return _mainCamera.ScreenToWorldPoint(mousePoint);
        }
        #endregion
    }
}
