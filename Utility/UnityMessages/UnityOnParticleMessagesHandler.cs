using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
    /// </summary>
    public class UnityOnParticleMessagesHandler : MonoBehaviour
    {
        [Header("Particle System Messages")]
        //[Tooltip("OnParticleCollision is called when a particle hits a Collider.")]
        //public UnityEvent OnParticleCollisionEvent = new ();
        [Tooltip("OnParticleSystemStopped is called when all particles in the system have died, and no new " +
            "particles will be born.New particles cease to be created either after Stop is called, or when the " +
            "duration property of a non-looping system has been exceeded.")]
        public UnityEvent OnParticleSystemStoppedEvent = new ();
        //[Tooltip("OnParticleTrigger is called when any particles in a Particle System meet the conditions " +
        //    "in the trigger module.")]
        //public UnityEvent OnParticleTriggerEvent = new ();
        //[Tooltip("OnParticleUpdateJobScheduled is called when a Particle System's built-in update job " +
        //    "has been scheduled.")]
        //public UnityEvent OnParticleUpdateJobScheduledEvent = new ();


        #region Unity Messages
        private void Start()
        {
            SetParticleSystemStopAction();
        }

        private void OnParticleSystemStopped()
        {
            OnParticleSystemStoppedEvent.Invoke();
        }
        #endregion


        private void SetParticleSystemStopAction()
        {
            if (TryGetComponent(out ParticleSystem particleSystem))
            {
                var main = particleSystem.main;
                main.stopAction = ParticleSystemStopAction.Callback;
            }
        }
    }
}
