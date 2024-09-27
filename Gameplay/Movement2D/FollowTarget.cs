using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Movement2D
{
    /// <summary>
    /// Follot target script with look at target option.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class FollowTarget : MonoBehaviour
    {
	    [Tooltip("This is the target the object is going to move towards")]
	    [SerializeField] private Transform _target;

	    [Header("Movement")]
	    [Tooltip("Speed used to move towards the target")]
        [SerializeField] private float _speed = 1f;

	    [Tooltip("Used to decide if the object will look at the target while pursuing it")]
        [SerializeField] private bool _lookAtTarget = false;

	    [Tooltip("The direction that will face the target")]
        //[SerializeField] private Enums.Directions _useSide = Enums.Directions.Up;

	    private Rigidbody2D _rigidbody2D;

	    void Awake()
	    {
		    _rigidbody2D = GetComponent<Rigidbody2D>();
	    }

	    // FixedUpdate is called once per frame
	    void FixedUpdate()
        {
            Follow();
        }

        private void Follow()
        {
            //do nothing if the target hasn't been assigned or it was detroyed for some reason
            if (_target == null)
                return;

            //look towards the target
            if (_lookAtTarget)
            {
                //Utils.SetAxisTowards(_useSide, transform, _target.position - transform.position);
            }

            //Move towards the target
            _rigidbody2D.MovePosition(Vector2.Lerp(transform.position, _target.position, Time.fixedDeltaTime * _speed));
        }
    }
}
