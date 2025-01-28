using System.Collections.Generic;
using SombraStudios.Shared.ScriptableObjects.RuntimeSets;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Unlock
{
    [CreateAssetMenu(fileName = "UnlockableSet", menuName = "Sombra Studios/Unlockable/Unlockable Runtime Set")]
    public class UnlockableRuntimeSetSO : RuntimeSetSO<UnlockableSO>
    {
        /// <summary>
        /// Returns all currently unlocked objects.
        /// </summary>
        public IEnumerable<UnlockableSO> GetUnlockedObjects()
        {
            foreach (var unlockable in Items)
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
        public bool IsUnlocked(UnlockableSO unlockable)
        {
            return unlockable.IsUnlocked;
        }

        /// <summary>
        /// Forcefully unlocks an object.
        /// </summary>
        /// <param name="unlockable">The object to unlock.</param>
        public void Unlock(UnlockableSO unlockable)
        {
            if (Items.Contains(unlockable))
            {
                unlockable.SetUnlocked(true);
            }
        }

        /// <summary>
        /// Forcefully locks an object.
        /// </summary>
        public void Lock(UnlockableSO unlockable)
        {
            if (Items.Contains(unlockable))
            {
                unlockable.SetUnlocked(false);
            }
        }

        /// <summary>
        /// Refresh the unlock states of all unlockable objects.
        /// </summary>
        public void RefreshUnlockStates()
        {
            foreach (var unlockable in Items)
            {
                // Check and update the unlock state by accessing IsUnlocked
                _ = unlockable.IsUnlocked;
            }
        }
    }
}
