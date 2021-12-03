using UnityEngine;

/// <summary>
/// Quit script that works in Unity Editor
/// </summary>
public class QuitScript : MonoBehaviour
{
	public void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.ExitPlaymode();
#else
		Application.Quit();
#endif
	}
}
