using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Systems.GameState
{
    public class GameStateEvent : GameStateListener
    {
        [SerializeField] private GameStateSO _gameStateListening;
        
        public UnityEvent GameStateChanged;


        protected override void OnGameStateChanged(GameStateSO newGameState)
        {
            if (_gameStateListening != newGameState) { return; }
            GameStateChanged?.Invoke();
        }
    }
}
