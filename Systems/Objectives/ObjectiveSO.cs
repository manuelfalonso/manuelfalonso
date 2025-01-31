using SombraStudios.Shared.Interfaces;
using SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Objectives
{
    /// <summary>
    /// Base class for a ScriptableObject-based game objective.
    /// Implement the logic within each concrete class and customize the win/lose conditions.
    /// Combine them in the ObjectiveManager for more interesting results (e.g., pick up x number of items within n seconds).
    /// </summary>
    public class ObjectiveSO : ScriptableObject, IDescribable
    {
        private const string PROPERTIES_TITLE = "Properties";
        private const string EVENT_TITLE = "Events";

        [Space]
        [Header(PROPERTIES_TITLE)]

        [Tooltip("On-screen name of the objective.")]
        [SerializeField] private string _title;

        [Tooltip("Description of the objective, explaining its purpose or goal.")]
        [SerializeField] private string _description;

        [Tooltip("Indicates whether the objective is optional. If true, it is not required to win.")]
        [SerializeField] private bool _isOptional;

        [Header(EVENT_TITLE)]

        [Tooltip("Event raised when the objective is completed successfully.")]
        [SerializeField] private VoidEventChannelSO _objectiveCompleted;

        [Tooltip("Event raised when the objective cannot be completed (optional).")]
        [SerializeField] private ObjectiveEventChannelSO _objectiveFailed;

        /// <summary>
        /// Gets or sets the description of the objective.
        /// </summary>
        string IDescribable.Description
        {
            get => _description;
            set => _description = value;
        }

        /// <summary>
        /// Indicates whether the objective has been completed.
        /// </summary>
        public bool IsCompleted => _isCompleted;

        /// <summary>
        /// Reference to the event channel that triggers when the objective is completed.
        /// </summary>
        public VoidEventChannelSO ObjectiveComplete => _objectiveCompleted;
        
        private bool _isCompleted;

        /// <summary>
        /// Resets the objective to its initial state (not completed).
        /// </summary>
        public void ResetObjective()
        {
            _isCompleted = false;
        }

        /// <summary>
        /// Marks the objective as completed and triggers the completion event.
        /// </summary>
        protected virtual void CompleteObjective()
        {
            _isCompleted = true;
            if (_objectiveCompleted != null)
                _objectiveCompleted.RaiseEvent();
        }

        /// <summary>
        /// Marks the objective as failed and triggers the failure event if assigned.
        /// </summary>
        protected virtual void FailObjective()
        {
            _isCompleted = false;
            if (_objectiveFailed != null)
                _objectiveFailed.RaiseEvent(this);
        }
    }
}
