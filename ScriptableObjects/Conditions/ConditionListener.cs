using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.ScriptableObjects.Conditions
{
    /// <summary>
    /// Listens to a ConditionSO and invokes UnityEvents based on its validity.
    /// </summary>
    public class ConditionListener : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The condition to evaluate.")]
        [SerializeField] private ConditionSO _condition;

        [Tooltip("Whether the listener is active.")]
        [SerializeField] private bool _isActive = true;

        [Tooltip("Check the condition on Start.")]
        [SerializeField] private bool _checkOnStart = false;
                
        [Tooltip("Check the condition on OnEnable.")]
        [SerializeField] private bool _checkOnEnable = false;

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

        /// <summary>
        /// Unity OnEnable callback. Checks the condition if enabled.
        /// </summary>
        private void OnEnable()
        {
            if (_checkOnEnable)
            {
                CheckCondition();
            }
        }

        /// <summary>
        /// Unity Start callback. Checks the condition if enabled.
        /// </summary>
        private void Start()
        {
            if (_checkOnStart)
            {
                CheckCondition();
            }
        }

        /// <summary>
        /// Checks the assigned condition and invokes the corresponding events.
        /// </summary>
        public void CheckCondition()
        {
            if (!_isActive)
                return;

            if (_condition == null)
            {
                Debug.LogError("ConditionSO is not assigned in ConditionListener.", this);
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
