using SombraStudios.Shared.ScriptableObjects.Conditions;
using SombraStudios.Shared.ScriptableObjects.RuntimeSets;
using UnityEngine;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.Systems.Unlock
{
    /// <summary>
    /// Represents an unlockable object in the system.
    /// Each unlockable object can have a condition to determine its unlocked state.
    /// </summary>
    [CreateAssetMenu(fileName = "UnlockableItem", menuName = "Sombra Studios/Unlockable/Unlockable")]
    public class UnlockableSO : ScriptableObject, IRuntimeSetItem<UnlockableSO>
    {
        protected const string LOG_CATEGORY = "[UnlockableSO]";
        protected const string DEBUG_TITLE = "Debug";
        protected const string PROPERTIES_TITLE = "Properties";
        
        [Header(PROPERTIES_TITLE)]
        /// <summary>
        /// Condition required to unlock this object.
        /// </summary>
        [Tooltip("Condition required to unlock this object.")]
        [SerializeField] private ConditionSO _unlockCondition;
        /// <summary>
        /// Stores whether the object is currently unlocked.
        /// </summary>
        [Tooltip("Tracks whether the object is currently unlocked.")]
        [SerializeField] private bool _isUnlocked;
        /// <summary>
        /// The runtime set this object belongs to.
        /// </summary>
        [Tooltip("The runtime set this object belongs to.")]
        [SerializeField] private RuntimeSetSO<UnlockableSO> _runtimeSet;

        [Header(DEBUG_TITLE)]
        [Tooltip("Show debug logs.")]
        /// <summary>
        /// Show debug logs.
        /// </summary>
        [SerializeField] private bool _showLogs;
        
        /// <summary>
        /// Gets or sets the runtime set this object belongs to.
        /// </summary>
        public RuntimeSetSO<UnlockableSO> RuntimeSet
        {
            get => _runtimeSet;
            set => _runtimeSet = value;
        }

        /// <summary>
        /// Checks if the object is unlocked.
        /// </summary>
        public bool IsUnlocked
        {
            get
            {
                if (_isUnlocked)
                    return _isUnlocked;
                if (_unlockCondition == null)
                {
                    Logger.LogError(LOG_CATEGORY, "Unlock condition is null.", this);
                    return _isUnlocked;
                }
                _isUnlocked = _unlockCondition.IsValid();
                
                if (_showLogs)
                {
                    Logger.Log(LOG_CATEGORY, $"{_unlockCondition.name} is valid: {_isUnlocked}", this);
                }
                
                return _isUnlocked;
            }
            protected set => _isUnlocked = value;
        }

        private void OnEnable() => AddToRuntimeSet();

        private void OnDisable() => RemoveFromRuntimeSet();

        /// <summary>
        /// Adds the object to the runtime set.
        /// </summary>
        public void AddToRuntimeSet() => RuntimeSet?.Add(this);

        /// <summary>
        /// Removes the object from the runtime set.
        /// </summary>
        public void RemoveFromRuntimeSet() => RuntimeSet?.Remove(this);

        /// <summary>
        /// Forcefully sets the unlock state of the object.
        /// </summary>
        /// <param name="isUnlocked">Whether the object should be marked as unlocked.</param>
        public void SetUnlocked(bool isUnlocked) => _isUnlocked = isUnlocked;
    }
}

