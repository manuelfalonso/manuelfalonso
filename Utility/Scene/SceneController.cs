using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SombraStudios.Utility
{
    /// <summary>
    /// Singleton Scene Manager Script for some common SceneManager uses.
    /// </summary>
    public class SceneController : Singleton<SceneController>
    {
        /// <summary>
        /// Load next scene Index
        /// </summary>
        public void LoadNextScene(float timeDelay = 0)
        {
            StartCoroutine(WaitForTime(
                timeDelay,
                () => SceneManager.LoadScene(SceneManager.GetActiveScene().
                    buildIndex + 1)));
        }

        // Load scene by index
        public void LoadSceneByIndex(int sceneIndex, float timeDelay = 0)
        {
            StartCoroutine(WaitForTime(
                timeDelay,
                () => SceneManager.LoadScene(sceneIndex)));
        }

        // Load scene by name
        public void LoadSceneByName(string sceneName, float timeDelay = 0)
        {
            StartCoroutine(WaitForTime(
                timeDelay,
                () => SceneManager.LoadScene(sceneName)));
        }

        /// <summary>
        /// Load scene with 0 index
        /// </summary>
        public void Restart(float timeDelay = 0)
        {
            StartCoroutine(WaitForTime(
                timeDelay,
                () => SceneManager.LoadScene(0)));
        }

        // Reload scene
        public void ReloadScene(float timeDelay = 0)
        {
            StartCoroutine(WaitForTime(
                timeDelay,
                () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex)));
        }

        // Quit
        public void Quit(float timeDelay = 0)
        {
            StartCoroutine(WaitForTime(
                timeDelay,
                () =>
                {
    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.ExitPlaymode();
    #else
		            Application.Quit();
    #endif
                }));
        }

        // Wait for time Coroutine
        private IEnumerator WaitForTime(float timeDelay, Action callback)
        {
            yield return new WaitForSeconds(timeDelay);
            callback();
        }
    }
}
