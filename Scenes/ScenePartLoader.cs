using UnityEngine;
using UnityEngine.SceneManagement;

namespace SombraStudios.Shared.Scenes
{
    public enum CheckMethod
    {
        Distance,
        Trigger
    }

    public class ScenePartLoader : MonoBehaviour
    {
        public Transform Player;
        public CheckMethod CheckMethod;
        public float LoadRange;
        
        public string PlayerTag;

        //Scene state
        private bool _isLoaded;
        private bool _shouldLoad;


        #region UnityMessages
        private void Start()
        {
            //verify if the scene is already open to avoid opening a scene twice
            if (SceneManager.sceneCount > 0)
            {
                for (int i = 0; i < SceneManager.sceneCount; ++i)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if (scene.name == gameObject.name)
                    {
                        _isLoaded = true;
                    }
                }
            }
        }

        private void Update()
        {
            //Checking which method to use
            if (CheckMethod == CheckMethod.Distance)
            {
                DistanceCheck();
            }
            else if (CheckMethod == CheckMethod.Trigger)
            {
                TriggerCheck();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _shouldLoad = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _shouldLoad = false;
            }
        }
        #endregion


        #region Private Methods
        private void DistanceCheck()
        {
            //Checking if the player is within the range
            if (Vector3.Distance(Player.position, transform.position) < LoadRange)
            {
                LoadScene();
            }
            else
            {
                UnLoadScene();
            }
        }

        private void LoadScene()
        {
            if (!_isLoaded)
            {
                //Loading the scene, using the gameobject name as it's the same as the name of the scene to load
                SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
                //We set it to true to avoid loading the scene twice
                _isLoaded = true;
            }
        }

        private void UnLoadScene()
        {
            if (_isLoaded)
            {
                SceneManager.UnloadSceneAsync(gameObject.name);
                _isLoaded = false;
            }
        }

        private void TriggerCheck()
        {
            //shouldLoad is set from the Trigger methods
            if (_shouldLoad)
            {
                LoadScene();
            }
            else
            {
                UnLoadScene();
            }
        }
        #endregion
    }
}
