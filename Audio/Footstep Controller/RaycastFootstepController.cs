using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Audio.Footsteps
{
    /// <summary>
    /// Select an audio clip list with a RayCast to the floor
    /// </summary>
    public class RaycastFootstepController : FootstepController
    {
        [SerializeField, Range(1, 50)] private float _updatesPerSeconds = 10f;

        [Header("Raycast")]
        [SerializeField] private Vector3 _origin = Vector3.zero;
        [SerializeField] private Vector3 _direction = Vector3.down;
        [SerializeField] private float _maxDistance = Mathf.Infinity;
        [SerializeField] private LayerMask _floorLayerMask;

        private RaycastHit _hit;
        private WaitForSeconds _interval;


        protected void Start()
        {
            base.Start();
            _interval = new WaitForSeconds(1f / _updatesPerSeconds);
        }

        private void OnEnable()
        {
            StartCoroutine(RayCastFloor());
        }


        private IEnumerator RayCastFloor()
        {
            while (true)
            {
                yield return _interval;

                if (Physics.Raycast(_origin, _direction, out _hit, _maxDistance, _floorLayerMask))
                {
                    SelectAudioClipList(_hit.transform);                
                }
            }
        }
    }
}
