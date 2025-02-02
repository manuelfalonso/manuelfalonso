using UnityEngine;
using UnityEngine.SceneManagement;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.Scenes.Strategies
{
    // The active Scene is the Scene which will be used as the target for new GameObjects instantiated by scripts
    // and from what Scene the lighting settings are used. When you add a Scene additively
    // (see LoadSceneMode.Additive), the first Scene is still kept as the active Scene. Use this to switch the
    // active Scene to the Scene you want as the target.
    // There must always be one Scene marked as the active Scene.
    [CreateAssetMenu(fileName = "NewSetActiveSceneStrategy", menuName = "Sombra Studios/Strategies/Scenes/Set Active Scene")]
    public class SetActiveSceneStrategy : SceneStrategy
    {
        public Scene Scene;

        public override void Execute()
        {
            var success = SceneManager.SetActiveScene(Scene);

            if (_showLogs)
            {
                if (success)
                {
                    Logger.Log(LOG_CATEGORY, $"Scene {Scene.name} set as active scene");
                }
                else
                {
                    Logger.LogError(
                        LOG_CATEGORY, 
                        $"Failed to set scene {Scene.name} as active scene. Scene is not loaded yet");
                }
            }
        }
    }
}
