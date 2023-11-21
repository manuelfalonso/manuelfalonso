using UnityEngine;

namespace SombraStudios.Shared.VFX
{
    /// <summary>
    /// Increase and decrease the light from 0 to a max during a time.
    /// </summary>
    [RequireComponent(typeof(Light))]
    public class PingPongLight : MonoBehaviour
    {
        [SerializeField]
        private float _maxLight = 8f;

        private Light _myLight;

        void Start()
        {
            _myLight = GetComponent<Light>();
        }

        void Update()
        {
            _myLight.intensity = Mathf.PingPong(Time.time, _maxLight);
        }
    }
}