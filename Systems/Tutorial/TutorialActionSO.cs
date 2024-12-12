using System;
using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Tutorial
{
    /// <summary>
    /// Base class for all tutorial actions
    /// </summary>
    public abstract class TutorialActionSO : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] protected bool _active = true;

        [NonSerialized] protected bool _isCompletedInitialValue = false;

        public bool IsCompleted { get; protected set; }


        public abstract IEnumerator ExecuteAction();


        // Manages initial value restoration
        public virtual void OnAfterDeserialize()
        {
            IsCompleted = _isCompletedInitialValue;
        }

        public void OnBeforeSerialize() { }
    }
}
