using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace SombraStudios.Systems.Teleport
{
    /// <summary>
    /// Teleport behaviour to a SpawnPoint object inside the same scene or another one.
    /// </summary>
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private Object DestinationScene;
        [SerializeField] private string DestinationSpawnPointName;

        [Header("Events")]
        public UnityEvent Teleporting = new UnityEvent();
        public UnityEvent Teleported = new UnityEvent();


        public void Teleport(ITeleportable teleportable)
        {
            if (!teleportable.CanTeleport)
            {
                return;
            }

            teleportable.CanTeleport = false;

            OnTeleporting();

            if (SceneManager.GetActiveScene().name == DestinationScene.name)
            {
                TeleportToDestination(teleportable);
            }
            else
            {
                StartCoroutine(TeleportToNewScene(DestinationScene.name, teleportable));
            }
        }


        private IEnumerator TeleportToNewScene(string sceneName, ITeleportable teleportable)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            AsyncOperation newSceneAsyncLoad = SceneManager.LoadSceneAsync(DestinationScene.name, LoadSceneMode.Additive);

            while (!newSceneAsyncLoad.isDone)
            {
                yield return null;
            }

            SceneManager.MoveGameObjectToScene(teleportable.GameObject, SceneManager.GetSceneByName(sceneName));
            TeleportToDestination(teleportable);

            SceneManager.UnloadSceneAsync(currentScene);
        }

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

        private void OnTeleporting() => Teleporting?.Invoke();
        private void OnTeleported() => Teleported?.Invoke();
    }
}
