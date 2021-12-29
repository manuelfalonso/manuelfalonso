using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// 
/// </summary>
public abstract class ConditionBase : MonoBehaviour
{

	//actionitems can be connected to GameplayAction scripts, and execute their one action (the method ExecuteAction implemented in each child class)
	[SerializeField]
	public List<Action> actions = new List<Action>();

	//custom actions are more complicated to setup but more powerful, and appear only if useCustomActions is enabled
	public bool useCustomActions = false;
	public UnityEvent customActions;

	//to perform the actions only once
	public bool happenOnlyOnce = false;
	private bool alreadyHappened = false;

	public bool filterByTag = false;
	public string filterTag = "Player";

	/// <summary>
	/// DataObject is usually the other object in the collision
	/// </summary>
	/// <param name="dataObject"></param>
	public void ExecuteAllActions(GameObject dataObject)
	{
		// First check if the action has already been executed
		if (happenOnlyOnce && alreadyHappened)
			return;


		// First execute the simple GameplayActions, if present
		bool actionResult;
		foreach (Action ga in actions)
		{
			if (ga != null)
			{
				actionResult = ga.ExecuteAction(dataObject);
				if (actionResult == false)
				{
					Debug.LogWarning("An action failed and interrupted the chain of Actions");
					return;
				}
			}
		}

		// Execute the custom actions, if present and enabled
		if (useCustomActions)
		{
			customActions.Invoke();
		}

		// Will prevent re-executing the actions if happenOnlyOnce is true
		alreadyHappened = true; 
	}
}