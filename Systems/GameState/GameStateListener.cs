using System;
using UnityEngine;

namespace SombraStudios.Shared.Systems.GameState
{
    /// <summary>  
    /// Abstract base class for listening to changes in a GameStateSO.  
    /// </summary>  
    /// <typeparam name="T">An Enum type representing the game states.</typeparam>  
    public abstract class GameStateListener<T> : MonoBehaviour where T : Enum
    {
        [Header("References")]
        [SerializeField] private GameStateSO<T> _gameStateSOListening;

        /// <summary>  
        /// Unity callback invoked when the GameObject is enabled.  
        /// Subscribes to the GameStateChanged event of the assigned GameStateSO.  
        /// </summary>  
        protected virtual void OnEnable()
        {
            if (_gameStateSOListening == null)
            {
                Debug.LogError($"GameStateSO is not assigned in {gameObject.name}. Disabling the listener.", this);
                enabled = false;
                return;
            }

            // Ensure no duplicate subscriptions.  
            _gameStateSOListening.GameStateChanged -= OnGameStateChanged;
            _gameStateSOListening.GameStateChanged += OnGameStateChanged;
        }

        /// <summary>  
        /// Unity callback invoked when the GameObject is disabled.  
        /// Unsubscribes from the GameStateChanged event of the assigned GameStateSO.  
        /// </summary>  
        protected virtual void OnDisable()
        {
            if (_gameStateSOListening != null)
            {
                _gameStateSOListening.GameStateChanged -= OnGameStateChanged;
            }
        }

        /// <summary>  
        /// Abstract method to handle changes in the game state.  
        /// Must be implemented by derived classes.  
        /// </summary>  
        /// <param name="newGameState">The new game state.</param>  
        protected abstract void OnGameStateChanged(T newGameState);
    }
}
