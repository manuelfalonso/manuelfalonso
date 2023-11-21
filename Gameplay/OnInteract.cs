using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace SombraStudios.Shared.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class OnInteract : MonoBehaviour
    {
        public bool isOneShot;
        public float cooldown;
        [FormerlySerializedAs("onTriggerEnterEvent")]
        public UnityEvent onInteractEvent;

        bool m_HasBeenTriggered;
        float m_Timer;


        void Start()
        {
            m_Timer = cooldown;
        }

        void Update()
        {
            if (m_HasBeenTriggered)
                m_Timer += Time.deltaTime;
        }


        public void Interact()
        {
            if(isOneShot && m_HasBeenTriggered)
                return;

            if(cooldown > m_Timer)
                return;

            onInteractEvent.Invoke();
            m_HasBeenTriggered = true;
            m_Timer = 0f;
        }
    }
}