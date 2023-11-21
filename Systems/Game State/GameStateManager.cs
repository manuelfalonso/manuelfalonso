using SombraStudios.Shared.Patterns.Creational.Singleton;
using System;
using UnityEngine;

namespace SombraStudios.Shared.Systems.GameState
{
    /// <summary>
    /// Store reference to the actual game state and triggers an event when the game state changed
    /// </summary>
    public class GameStateManager : Singleton<GameStateManager>
    {
        [SerializeField] private GameStateSO _initialGameState;

        [Header("Debug")]
        [SerializeField] private bool _showLogs = false;

        public static event Action<GameStateSO> GameStateChanged;

        private GameStateSO currentState;

        public GameStateSO CurrentState
        {
            get { return currentState; }
            set
            {
                if (currentState != value)
                {
                    currentState = value;
                    Tools.Logger.Log(_showLogs, $"Game State Changed: {value.name}", this);
                    GameStateChanged?.Invoke(currentState);
                }
            }
        }


        private void Start()
        {
            CurrentState = _initialGameState;
        }
    }
}
