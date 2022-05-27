using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Simple class to be attached to a gameobject 
/// that should be destroyed after certain time
/// </summary>
public class TimedSelfDestruct : MonoBehaviour
{
	[Tooltip("After this time, the object will be destroyed")]
	public float timeToDestruction;
	[Tooltip("Unity Event invoked when the gameobject is destroyed")]
	public static UnityEvent OnDestroy = new UnityEvent();
	[Tooltip("Enable/Disable OnDestroy Event")]
	public bool isOnDestroyEventEnable = true;

	void Start()
	{
		Invoke("DestroyMe", timeToDestruction);
	}

	// This function will destroy this object
	void DestroyMe()
	{
        if (OnDestroy != null && isOnDestroyEventEnable)
			OnDestroy.Invoke();

		Destroy(gameObject);
	}
}
