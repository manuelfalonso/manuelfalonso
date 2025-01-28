using SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects;
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
        protected const string PROPERTIES_TITLE = "Properties";
        protected const string EVENT_TITLE = "Events";
        protected const string DEBUG_TITLE = "Debug";
        
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
        [SerializeField] private RuntimeSetSO<UnlockableSO> _unlockableCollection;

        [Header(EVENT_TITLE)]
        /// <summary>
        /// Event listener when the unlock condition should be checked.
        /// </summary>
        [Tooltip("Event listener when the unlock condition should be checked.")]
        public VoidEventChannelSO CheckUnlockConditionListener;
        /// <summary>
        /// Event triggered when the object is unlocked.
        /// </summary>
        [Tooltip("Event triggered when the object is unlocked.")]
        public VoidEventChannelSO OnUnlockedTrigger;
        
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
            get => _unlockableCollection;
            set => _unlockableCollection = value;
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
                
                // Check if the object is unlocked and trigger the event if it hasn't been triggered yet
                if (_isUnlocked) 
                {
                    if (!_unlockedEventTriggered)
                    {
                        OnUnlockedTrigger?.RaiseEvent();
                        _unlockedEventTriggered = true;
                    }
                }
                else
                {
                    _unlockedEventTriggered = false;
                }
                
                if (_showLogs)
                {
                    Logger.Log(LOG_CATEGORY, $"{_unlockCondition.name} is valid: {_isUnlocked}", this);
                }
                
                return _isUnlocked;
            }
            protected set => _isUnlocked = value;
        }
        
        private bool _unlockedEventTriggered;

        
        #region Unity Messages

        private void OnEnable()
        {
            AddToRuntimeSet();
            AddListener();
            // Check if the object is unlocked when enabled to update the state if it was modified in inspector
            if (!_isUnlocked) { _unlockedEventTriggered = false; }
        }

        private void OnDisable()
        {
            RemoveFromRuntimeSet();
            RemoveListener();
        }
        
        #endregion

        
        #region Public Methods

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
        public void SetUnlocked(bool isUnlocked)
        {
            if (!isUnlocked) { _unlockedEventTriggered = false; }
            _isUnlocked = isUnlocked;
        }

        #endregion
        

        #region Private Methods

        private void AddListener()
        {
            if (CheckUnlockConditionListener == null) return;
            CheckUnlockConditionListener.OnEventRaised -= EvaluateUnlockCondition;
            CheckUnlockConditionListener.OnEventRaised += EvaluateUnlockCondition;
        }
        
        private void RemoveListener()
        {
            if (CheckUnlockConditionListener == null) return;
            CheckUnlockConditionListener.OnEventRaised -= EvaluateUnlockCondition;
        }
        
        private void EvaluateUnlockCondition() => _ = IsUnlocked;

        #endregion
    }
}