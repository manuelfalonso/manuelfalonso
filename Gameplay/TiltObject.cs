using UnityEngine;

namespace SombraStudios.Gameplay
{

    /// <summary>
    /// Tilt an object sideways up to tiltAngle
    /// </summary>
    public class TiltObject : MonoBehaviour
    {
        [SerializeField]
        private float _smooth = 5.0f;
        [SerializeField]
        private float _tiltAngle = 60.0f;


        void FixedUpdate()
        {
            Tilt();
        }


        private void Tilt()
        {
            // Smoothly tilts a transform towards a target rotation.
            float tiltAroundZ = Input.GetAxis("Horizontal") * _tiltAngle;
            float tiltAroundX = Input.GetAxis("Vertical") * _tiltAngle;

            Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * _smooth);
        }
    }
}
