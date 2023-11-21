using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.VFX
{
    /// <summary>
    /// 
    /// </summary>
    public class ParticleSystemStoppedAction : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        [Header("Events")]
        public UnityEvent OnParticleSystemStoppedEvent = new UnityEvent();


        void Start()
        {
            var main = _particleSystem.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }


        private void OnParticleSystemStopped()
        {
            OnParticleSystemStoppedEvent?.Invoke();
        }
    }
}
