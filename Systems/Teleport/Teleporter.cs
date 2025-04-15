using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using SombraStudios.Shared.Scenes.SceneAsset;

namespace SombraStudios.Shared.Systems.Teleport
{
    /// <summary>
    /// Teleport behaviour to a SpawnPoint object inside the same scene or another one.
    /// </summary>
    public class Teleporter : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private SceneAssetSO DestinationSceneAsset;
        [SerializeField] private string DestinationSpawnPointName;

        [Header("Events")]
        public UnityEvent Teleporting = new();
        public UnityEvent Teleported = new();

        /// <summary>
        /// Initiates the teleportation process.
        /// </summary>
        /// <param name="teleportable">The object to teleport.</param>
        public void Teleport(ITeleportable teleportable)
        {
            if (!teleportable.CanTeleport) return;

            teleportable.CanTeleport = false;
            OnTeleporting();

            if (SceneManager.GetActiveScene().name == DestinationSceneAsset.SceneName)
            {
                TeleportToDestination(teleportable);
            }
            else
            {
                StartCoroutine(TeleportToNewScene(DestinationSceneAsset.SceneName, teleportable));
            }
        }

        /// <summary>
        /// Teleports the object to a new scene.
        /// </summary>
        /// <param name="sceneName">The name of the new scene.</param>
        /// <param name="teleportable">The object to teleport.</param>
        private IEnumerator TeleportToNewScene(string sceneName, ITeleportable teleportable)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            AsyncOperation newSceneAsyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            yield return new WaitUntil(() => newSceneAsyncLoad.isDone);

            SceneManager.MoveGameObjectToScene(teleportable.GameObject, SceneManager.GetSceneByName(sceneName));
            TeleportToDestination(teleportable);

            SceneManager.UnloadSceneAsync(currentScene);
        }

        /// <summary>
        /// Teleports the object to the destination spawn point.
        /// </summary>
        /// <param name="teleportable">The object to teleport.</param>
        private void TeleportToDestination(ITeleportable teleportable)
        {
            SpawnPoint spawnPoint = TeleportManager.Instance.FindSpawnPoint(DestinationSpawnPointName);

            if (spawnPoint != null)
            {
                teleportable.TeleportTo(spawnPoint.transform);
            }

            teleportable.CanTeleport = true;
            OnTeleported();
        }

        /// <summary>
        /// Invokes the Teleporting event.
        /// </summary>
        private void OnTeleporting() => Teleporting?.Invoke();

        /// <summary>
        /// Invokes the Teleported event.
        /// </summary>
        private void OnTeleported() => Teleported?.Invoke();
    }
}
