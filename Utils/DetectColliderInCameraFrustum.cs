using UnityEngine;

/// <summary>
/// Detect collider being seen by the camera
/// Firstly calculate frustum camera planes and then
/// calculate if a collider is inside the plane array
/// </summary>
public class DetectColliderInCameraFrustum : MonoBehaviour
{
    private Collider objCollider;
    
    private bool isVisible = false;

    void Start()
    {
        objCollider = GetComponent<Collider>();
    }

    void Update()
    {
        // Re calculate planes if the camera is not static
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds) && !isVisible)
        {
            Debug.Log(gameObject.name + " has been detected!");
            isVisible = true;
        }
        else if (!GeometryUtility.TestPlanesAABB(planes, objCollider.bounds) && isVisible)
        {
            Debug.Log("Nothing has been detected");
            isVisible = false;
        }
    }
}