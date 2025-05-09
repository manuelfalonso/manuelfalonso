using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Systems.GameState
{
    /// <summary>  
    /// Base class for handling game state events.  
    /// Listens for changes in a specific game state and invokes a UnityEvent when the target state is reached.  
    /// </summary>  
    /// <typeparam name="T">An Enum type representing the game states.</typeparam>  
    public abstract class GameStateEvent<T> : GameStateListener<T> where T : Enum
    {
        [Header("Settings")]
        [SerializeField]
        [Tooltip("The target game state to listen for.")]
        private T _gameStateTarget;

        [Header("Events")]
        [Tooltip("Event triggered when the game state matches the target state.")]
        public UnityEvent GameStateChanged;

        /// <summary>  
        /// Called when the game state changes.  
        /// Invokes the <see cref="GameStateChanged"/> event if the new game state matches the target state.  
        /// </summary>  
        /// <param name="newGameState">The new game state.</param>  
        protected override void OnGameStateChanged(T newGameState)
        {
            if (EqualityComparer<T>.Default.Equals(newGameState, _gameStateTarget))
            {
                GameStateChanged?.Invoke();
            }
        }
    }
}
