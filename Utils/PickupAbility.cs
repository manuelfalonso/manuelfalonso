using UnityEngine;

/// <summary>
/// Any pickup ability script with Scriptable Objects
/// </summary>
public abstract class PickupAbility : ScriptableObject
{
	public int value;
	public abstract void ActivateOn(Player player);
}