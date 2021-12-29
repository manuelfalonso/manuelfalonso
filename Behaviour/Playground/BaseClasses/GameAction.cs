using UnityEngine;

/// <summary>
/// 
/// </summary>
public abstract class GameAction : MonoBehaviour
{
	public virtual bool ExecuteAction(GameObject other)
	{
		// The return value indicates if the action has been successful
		// some actions always return true
		return true;
	}
}
