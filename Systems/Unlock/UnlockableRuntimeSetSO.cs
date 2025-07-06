using System.Collections.Generic;
using SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects;
using SombraStudios.Shared.ScriptableObjects.RuntimeSets;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Unlock
{
    [CreateAssetMenu(fileName = "UnlockableSet", menuName = "Sombra Studios/Unlockable/Unlockable Runtime Set")]
    public class UnlockableRuntimeSetSO : RuntimeSetSO<UnlockableSO>
    {
        protected const string EVENT_TITLE = "Events";

        [Header(EVENT_TITLE)]
        /// <summary>
        /// Event listener when the unlock condition of the unlockable collection should be checked.
        /// </summary>
        [Tooltip("Event listener when the unlock condition of the unlockable collection should be checked.")]
        public VoidEventChannelSO CheckUnlockConditionListener;
        /// <summary>
        /// Event triggered when the unlockable collection objects are unlocked.
        /// </summary>
        [Tooltip("Event triggered when the unlockable collection objects are unlocked.")]
        public VoidEventChannelSO OnUnlockedTrigger;

        
        #region Unity Messages

        private void OnEnable() => SubscribeToEvents();

        private void OnDisable() => UnsubscribeFromEvents();

        #endregion
        

        #region Public Methods

        /// <summary>
        /// Returns all currently unlocked objects.
        /// </summary>
        public IEnumerable<UnlockableSO> GetUnlockedObjects()
        {
            foreach (var unlockable in _items)
            {
                if (unlockable.IsUnlocked)
                    yield return unlockable;
            }
        }

        /// <summary>
        /// Checks if a specific unlockable is unlocked.
        /// </summary>
        /// <param name="unlockable">The unlockable object to check.</param>
        /// <returns>True if unlocked, false otherwise.</returns>
        public bool IsUnlocked(UnlockableSO unlockable) => unlockable.IsUnlocked;

        /// <summary>
        /// Checks if all unlockables are unlocked.
        /// </summary>
        /// <returns>True if all he unlockables are unlocked, false otherwise.</returns>
        public bool AreAllUnlocked()
        {
            foreach (var unlockable in _items)
            {
                if (!unlockable.IsUnlocked)
                    return false;
            }
            
            OnUnlockedTrigger?.RaiseEvent();

            return true;
        }
        
        /// <summary>
        /// Checks if all unlockables are unlocked.
        /// </summary>
        public void EvaluateAreAllUnlocked() => AreAllUnlocked();

        /// <summary>
        /// Forcefully unlocks an object.
        /// </summary>
        /// <param name="unlockable">The object to unlock.</param>
        public void Unlock(UnlockableSO unlockable)
        {
            if (_items.Contains(unlockable))
            {
                unlockable.SetUnlocked(true);
            }
        }

        /// <summary>
        /// Forcefully locks an object.
        /// </summary>
        public void Lock(UnlockableSO unlockable)
        {
            if (_items.Contains(unlockable))
            {
                unlockable.SetUnlocked(false);
            }
        }

        #endregion
        
        #region Private Methods

        private void SubscribeToEvents()
        {
            if (CheckUnlockConditionListener != null)
            {
                CheckUnlockConditionListener.OnEventRaised -= EvaluateAreAllUnlocked;
                CheckUnlockConditionListener.OnEventRaised += EvaluateAreAllUnlocked;
            }
        }

        private void UnsubscribeFromEvents()
        {
            if (CheckUnlockConditionListener != null)
                CheckUnlockConditionListener.OnEventRaised -= EvaluateAreAllUnlocked;
        }

        #endregion
    }
}