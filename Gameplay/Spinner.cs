using UnityEngine;

namespace SombraStudios.Shared.Gameplay
{
    /// <summary>
    /// Spins the object in the defined x, y and/or z axis.
    /// </summary>
    public class Spinner : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeedX = 0f;
        [SerializeField]
        private float _rotationSpeedY = 1f;
        [SerializeField]
        private float _rotationSpeedZ = 0f;


        void Update()
        {
            Spin();
        }


        private void Spin()
        {
            transform.Rotate(_rotationSpeedX, _rotationSpeedY, _rotationSpeedZ, Space.Self);
        }
    }
}