using UnityEngine.SceneManagement;

/// <summary>
/// Singleton Scene Manager Script for some common SceneManager uses.
/// </summary>
public class SceneController : Singleton<SceneController>
{
    /// <summary>
    /// Load next scene Index
    /// </summary>
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Load scene by index
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Load scene by name
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Load scene with 0 index
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    // Reload scene
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Quit
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
		Application.Quit();
#endif
    }
}
