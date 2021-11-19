using UnityEngine;
using System.Collections;
/*#if UNITY_EDITOR
using UnityEditor;
#endif*/

public class QuitScript : MonoBehaviour {
	

	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.ExitPlaymode();
		#else
		Application.Quit();
		#endif
	}
}
