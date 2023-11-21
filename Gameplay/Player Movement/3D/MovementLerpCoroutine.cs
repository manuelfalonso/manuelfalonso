using UnityEngine;
using System.Collections;

namespace SombraStudios.Shared.Gameplay.PlayerMovement
{
    /// <summary>
    /// When receive a Target move the gameObject with lerp 
    /// </summary>
    public class MovementLerpCoroutine : MonoBehaviour
    {
        [SerializeField] private float smoothing = 1f;

        private Collider col;

        private void Start()
        {
            col = GetComponent<Collider>();
        }

        public Vector3 Target
        {
            get { return target; }
            set
            {
                target = value + new Vector3(0f, col.bounds.size.y / 2, 0f);

                StopCoroutine("Movement");
                StartCoroutine("Movement", target);
            }
        }
        private Vector3 target;

        IEnumerator Movement(Vector3 target)
        {
            while (Vector3.Distance(transform.position, target) > 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, target, smoothing * Time.deltaTime);

                yield return null;
            }
        }
    }
}