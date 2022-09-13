using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detects if the position of an object is inside Camera Viewport
/// </summary>
public class ObjectInCameraViewport : MonoBehaviour
{
    public event Action OnEnterViewportEvent;
    public event Action OnExitViewportEvent;


    [SerializeField]
    [Tooltip("The Camera in which the detection will be made")]
    private Camera camera;


    private Vector3 objectViewportPosition;
    
    private bool isInsideCameraView = false;
    private bool wasDetected = false;

    #region Unity Events
    private void Start()
    {
        if (!camera)
            Debug.LogWarning($"Missing Camera Reference");
    }

    void Update()
    {
        if (!camera)
            return;

        CalculateObjectInCameraViewport();
    }
    #endregion

    private void CalculateObjectInCameraViewport()
    {
        objectViewportPosition = camera.WorldToViewportPoint(transform.position);
        isInsideCameraView = IsInsideCameraView(objectViewportPosition);

        if (isInsideCameraView && !wasDetected)
        {
            Debug.Log($"{gameObject.name} has been detected by the camera!");
            wasDetected = true;
            OnEnterViewportEvent?.Invoke();
        }
        else if (!isInsideCameraView && wasDetected)
        {
            Debug.Log($"{gameObject.name} left the camera space!");
            wasDetected = false;
            OnExitViewportEvent?.Invoke();
        }
    }

    private bool IsInsideCameraView(Vector3 viewportPosition)
    {
        return viewportPosition.x > 0f && 
            viewportPosition.x < 1f && 
            viewportPosition.y > 0f && 
            viewportPosition.y < 1f && 
            viewportPosition.z > 0f;
    }
}
