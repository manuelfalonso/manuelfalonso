using SombraStudios.Shared.Enums;
using UnityEngine;
using UnityEngine.Events;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.ScriptableObjects.Conditions
{
    /// <summary>
    /// Listens to a ConditionSO and invokes UnityEvents based on its validity.
    /// </summary>
    public class ConditionChecker : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The condition to evaluate.")]
        [SerializeField] private ConditionSO _condition;

        [Tooltip("Whether the checker is active.")]
        [SerializeField] private bool _isActive = true;

        [Tooltip("Automatically check the condition on specified Unity MonoBehaviour messages.")]
        [SerializeField] private UnityMonobehaviourMessages _autoCheckMessages = UnityMonobehaviourMessages.None;

        [Header("Events")]
        /// <summary>
        /// Invoked with the result of the condition check.
        /// </summary>
        public UnityEvent<bool> OnCondition = new();

        /// <summary>
        /// Invoked when the condition is true.
        /// </summary>
        public UnityEvent OnConditionTrue = new();

        /// <summary>
        /// Invoked when the condition is false.
        /// </summary>
        public UnityEvent OnConditionFalse = new();

        /// <summary>
        /// Gets or sets whether the listener is active.
        /// </summary>
        public bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        #region Unity Messages
        private void Awake()
        {
            if (_autoCheckMessages.HasFlag(UnityMonobehaviourMessages.Awake))
            {
                CheckCondition();
            }
        }

        private void OnEnable()
        {
            if (_autoCheckMessages.HasFlag(UnityMonobehaviourMessages.OnEnable))
            {
                CheckCondition();
            }
        }

        private void Start()
        {
            if (_autoCheckMessages.HasFlag(UnityMonobehaviourMessages.Start))
            {
                CheckCondition();
            }
        }

        private void OnDisable()
        {
            if (_autoCheckMessages.HasFlag(UnityMonobehaviourMessages.OnDisable))
            {
                CheckCondition();
            }
        }

        private void OnDestroy()
        {
            if (_autoCheckMessages.HasFlag(UnityMonobehaviourMessages.OnDestroy))
            {
                CheckCondition();
            }
        }
        #endregion

        /// <summary>
        /// Checks the assigned condition and invokes the corresponding events.
        /// </summary>
        public void CheckCondition()
        {
            if (!_isActive)
                return;

            if (_condition == null)
            {
                Logger.LogError($"ConditionSO is not assigned in {typeof(ConditionChecker)}.", this);
                return;
            }

            bool conditionMet = _condition.IsValid();
            OnCondition?.Invoke(conditionMet);

            if (conditionMet)
            {
                OnConditionTrue?.Invoke();
            }
            else
            {
                OnConditionFalse?.Invoke();
            }
        }
    }
}
