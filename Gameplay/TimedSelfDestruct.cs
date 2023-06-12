using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Gameplay
{

	/// <summary>
	/// Destroys the game object attached to, after certain time.
	/// </summary>
	public class TimedSelfDestruct : MonoBehaviour
	{
		[Tooltip("After this time, the object will be destroyed")]
		[SerializeField] private float _timeToDestruction;
		[Tooltip("Enable/Disable OnDestroy Event")]
		[SerializeField] private bool _isOnDestroyEventEnable = true;

		[Space]

		[Tooltip("Unity Event invoked when the gameobject is destroyed")]
		public UnityEvent OnDestroy = new UnityEvent();

		void Start()
		{
			Invoke("DestroyMe", _timeToDestruction);
		}

		// This function will destroy this object
		void DestroyMe()
		{
			if (OnDestroy != null && _isOnDestroyEventEnable)
				OnDestroy.Invoke();

			Destroy(gameObject);
		}
	}
}
