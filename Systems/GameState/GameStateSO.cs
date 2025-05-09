using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SombraStudios.Shared.Extensions;

namespace SombraStudios.Shared.Systems.GameState
{
    /// <summary>  
    /// Abstract base class for managing game states using a ScriptableObject.  
    /// </summary>  
    /// <typeparam name="T">An Enum type representing the game states.</typeparam>  
    public abstract class GameStateSO<T> : ScriptableObject where T : Enum
    {
        [Header("Properties")]
        [SerializeField] private T _gameState;

        [Header("Settings")]
        [SerializeField] private bool _resetOnLoad = true;

        [Header("Debug")]
        [SerializeField] private bool _showLogs;

        /// <summary>  
        /// Gets or sets the current game state.  
        /// Triggers the <see cref="GameStateChanged"/> event when the state changes.  
        /// </summary>  
        public T GameState
        {
            get => _gameState;
            protected set
            {
                if (EqualityComparer<T>.Default.Equals(_gameState, value)) return;

                _gameState = value;

                if (_showLogs)
                    Utility.Loggers.Logger.Log($"Game State Changed: {value}", this);

                GameStateChanged?.Invoke(value);
            }
        }

        /// <summary>  
        /// Event triggered when the game state changes.  
        /// </summary>  
        public event UnityAction<T> GameStateChanged;

        #region Unity Messages  
        /// <summary>  
        /// Unity callback invoked when the ScriptableObject is enabled.  
        /// Resets the game state if <see cref="_resetOnLoad"/> is true.  
        /// </summary>  
        private void OnEnable()
        {
            if (_resetOnLoad)
                ResetOnEnable();
        }
        #endregion

        #region Public Methods  
        /// <summary>  
        /// Sets the game state to a new value.  
        /// </summary>  
        /// <param name="newGameState">The new game state to set.</param>  
        public void SetGameState(T newGameState) => GameState = newGameState;

        /// <summary>  
        /// Resets the game state to its default value.  
        /// </summary>  
        public void ResetGameState() => GameState = default;

        /// <summary>  
        /// Adds a new game state to the current state.  
        /// </summary>  
        /// <param name="newGameState">The game state to add.</param>  
        public void AddGameState(T newGameState) => GameState = GameState.Add(newGameState);

        /// <summary>  
        /// Removes a specific game state from the current state.  
        /// </summary>  
        /// <param name="gameStateToRemove">The game state to remove.</param>  
        public void RemoveGameState(T gameStateToRemove) => GameState = GameState.Remove(gameStateToRemove);

        /// <summary>  
        /// Checks if the current game state contains a specific state.  
        /// </summary>  
        /// <param name="gameState">The game state to check.</param>  
        /// <returns>True if the current state contains the specified state; otherwise, false.</returns>  
        public bool HasGameState(T gameState) => GameState.Has(gameState);

        /// <summary>  
        /// Checks if the current game state contains any of the specified states.  
        /// </summary>  
        /// <param name="gameStates">The game states to check.</param>  
        /// <returns>True if the current state contains any of the specified states; otherwise, false.</returns>  
        public bool HasAnyGameState(T gameStates) => GameState.HasAny(gameStates);

        /// <summary>  
        /// Checks if the current game state is equal to a specific state.  
        /// </summary>  
        /// <param name="gameState">The game state to compare.</param>  
        /// <returns>True if the current state is equal to the specified state; otherwise, false.</returns>  
        public bool IsEqualGameState(T gameState) => GameState.Equals(gameState);
        #endregion

        #region Protected Methods  
        /// <summary>  
        /// Resets the game state when the ScriptableObject is enabled.  
        /// Must be implemented by derived classes.  
        /// </summary>  
        protected abstract void ResetOnEnable();
        #endregion
    }
}
