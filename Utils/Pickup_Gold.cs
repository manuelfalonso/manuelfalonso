using UnityEngine;

/// <summary>
/// Pickup gold implementation of abstract scriptable object class
/// </summary>
public class Pickup_Gold : PickupAbility
{
    /// <summary>
    /// Beahaviour trigger when activating the pickup
    /// </summary>
    /// <param name="player">Player controller script</param>
    public override void ActivateOn(Player player)
    {
        player.gold += value;
    }
}
