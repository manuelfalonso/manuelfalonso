using UnityEngine;

namespace SombraStudios.Shared.Systems.GameState
{
    public abstract class GameStateListener : MonoBehaviour
    {
        protected void OnEnable()
        {
            GameStateManager.GameStateChanged -= OnGameStateChanged;
            GameStateManager.GameStateChanged += OnGameStateChanged;
        }

        protected void OnDisable()
        {
            GameStateManager.GameStateChanged -= OnGameStateChanged;
        }

        protected abstract void OnGameStateChanged(GameStateSO newGameState);
    }
}
