using System;
using UnityEngine;

namespace SombraStudios.Shared.Utility
{
    /// <summary>
    /// Detect collider being seen by the camera.
    /// Firstly calculates frustum camera planes and then
    /// calculates if a collider is inside the plane array
    /// </summary>
    public class ObjectInCameraFrustum : MonoBehaviour
{
    public event Action OnEnterFrustumEvent;
    public event Action OnExitFrustumEvent;


    [SerializeField]
    [Tooltip("The Camera in which the detection will be made")]
    private Camera _camera;
    [SerializeField]
    [Tooltip("The camera will not be moved during gameplay?")]
    private bool isCameraStatic = false;


    private Collider objCollider;

    private Plane[] planes;

    private bool isVisible = false;


    #region Unity Events
    void Start()
    {
        objCollider = GetComponent<Collider>();

        if (!_camera)
            Debug.LogWarning($"Missing Camera Reference");

        // If camera is static we can calculate planes just once
        if (isCameraStatic && _camera)
            planes = GeometryUtility.CalculateFrustumPlanes(_camera);
    }

    void Update()
    {
        if (!_camera)
            return;

        ReCalculatePlanes();
        DetectBoundsInPlanes();
    }
    #endregion

    private void ReCalculatePlanes()
    {
        // Re calculate planes if the camera is not static
        if (!isCameraStatic)
            planes = GeometryUtility.CalculateFrustumPlanes(_camera);
    }

    private void DetectBoundsInPlanes()
    {
        if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds) && !isVisible)
        {
            Debug.Log($"{gameObject.name} has been detected!");
            isVisible = true;
            OnEnterFrustumEvent?.Invoke();
        }
        else if (!GeometryUtility.TestPlanesAABB(planes, objCollider.bounds) && isVisible)
        {
            Debug.Log($"{gameObject.name} leaving camera limits...");
            isVisible = false;
            OnExitFrustumEvent?.Invoke();
        }
    }
}
}