using System.Collections.Generic;
using SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Objectives
{
    /// <summary>
    /// Manages gameplay objectives and tracks their completion status.
    /// Notifies the <see cref="_allObjectivesCompleted"/> event channel when all objectives are completed.
    /// Listens for the game start event and updates objectives accordingly.
    /// </summary>
    public class ObjectiveManager : MonoBehaviour
    {
        private const string PROPERTIES_TITLE = "Properties";
        private const string BROADCAST_TITLE = "Broadcast on Event Channels";
        private const string LISTEN_TITLE = "Listen to Event Channels";

        [Header(PROPERTIES_TITLE)]
        [Tooltip("List of Objectives needed for win condition.")]
        [SerializeField] private List<ObjectiveSO> _objectives = new();

        [Header(BROADCAST_TITLE)]
        [Tooltip("Signal that all objectives are complete.")]
        [SerializeField] private VoidEventChannelSO _allObjectivesCompleted;

        [Header(LISTEN_TITLE)]
        [Tooltip("Gameplay has begun.")]
        [SerializeField] private VoidEventChannelSO _gameStarted;

        [Tooltip("Signal to update every time a single objective completes.")]
        [SerializeField] private VoidEventChannelSO _objectiveCompleted;

        #region Unity Messages

        /// <summary>
        /// Subscribes to event channels when the object is enabled.
        /// </summary>
        private void OnEnable()
        {
            SubscribeEvents();
        }

        /// <summary>
        /// Unsubscribes from event channels when the object is disabled to prevent errors.
        /// </summary>
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks if all objectives in the list are completed.
        /// </summary>
        /// <returns>True if all objectives are completed, otherwise false.</returns>
        public bool IsObjectiveListComplete()
        {
            foreach (ObjectiveSO objective in _objectives)
            {
                if (!objective.IsCompleted)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Subscribes to relevant event channels.
        /// </summary>
        private void SubscribeEvents()
        {
            if (_gameStarted != null)
                _gameStarted.OnEventRaised += OnGameStarted;

            if (_objectiveCompleted != null)
                _objectiveCompleted.OnEventRaised += OnCompleteObjective;
        }

        /// <summary>
        /// Unsubscribes from event channels to prevent errors.
        /// </summary>
        private void UnsubscribeEvents()
        {
            if (_gameStarted != null)
                _gameStarted.OnEventRaised -= OnGameStarted;

            if (_objectiveCompleted != null)
                _objectiveCompleted.OnEventRaised -= OnCompleteObjective;
        }

        /// <summary>
        /// Resets all objectives when the game starts.
        /// </summary>
        private void OnGameStarted()
        {
            foreach (ObjectiveSO objective in _objectives)
            {
                objective.ResetObjective();
            }
        }

        /// <summary>
        /// Checks if all objectives are complete when an objective is finished.
        /// Raises the <see cref="_allObjectivesCompleted"/> event if all objectives are completed.
        /// </summary>
        private void OnCompleteObjective()
        {
            if (IsObjectiveListComplete())
            {
                if (_allObjectivesCompleted != null)
                    _allObjectivesCompleted.RaiseEvent();
            }
        }

        #endregion
    }
}
