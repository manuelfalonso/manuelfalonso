using UnityEngine;

/// <summary>
/// Any pickup management script with Scriptable Objects
/// </summary>

public class PickupWithSO : MonoBehaviour
{
	[SerializeField]
	private PickupAbility pickup;

	private void OnTriggerEnter(Collider other)
	{
        Player player = other.GetComponent<Player>();
        if (player == null)
            return;

        pickup.ActivateOn(player);
    }
}
