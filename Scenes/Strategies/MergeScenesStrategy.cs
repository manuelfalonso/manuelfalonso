using UnityEngine;
using UnityEngine.SceneManagement;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.Scenes.Strategies
{
    /// <summary>
    /// Strategy for merging the contents of a source scene into a destination scene.
    /// The source scene will be destroyed once the merge is completed.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMergeScenesStrategy", menuName = "Sombra Studios/Strategies/Scenes/Merge Scenes")]
    public class MergeScenesStrategy : SceneStrategy
    {
        /// <summary>
        /// The scene that will be merged into the destination scene.
        /// </summary>
        [HideInInspector] public Scene SourceScene;

        /// <summary>
        /// The scene that will receive the contents of the source scene.
        /// </summary>
        [HideInInspector] public Scene DestinationScene;

        /// <summary>
        /// Determines whether the merge can be executed by checking the validity of both scenes.
        /// </summary>
        /// <returns>True if both scenes are valid, otherwise false.</returns>
        public override bool CanExecute()
        {
            if (!SourceScene.IsValid())
            {
                Logger.LogError(LOG_CATEGORY, "Source scene is invalid.", this);
                return false;
            }

            if (!DestinationScene.IsValid())
            {
                Logger.LogError(LOG_CATEGORY, "Destination scene is invalid.", this);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Executes the scene merge operation, logging the process if logging is enabled.
        /// </summary>
        public override void Execute()
        {
            if (_showLogs)
            {
                Logger.Log(LOG_CATEGORY, $"Merging scenes: {SourceScene.name} -> {DestinationScene.name}");
            }

            SceneManager.MergeScenes(SourceScene, DestinationScene);
        }
    }
}
