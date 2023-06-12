using UnityEngine;

namespace SombraStudios.Gameplay.PickUp
{

    /// <summary>
    /// Any pickup management script
    /// </summary>

    public class PickupWithoutSO : MonoBehaviour
    {
	    public enum PickupType
        {
		    Gold,
		    Health,
		    Mana
        };

	    [SerializeField]
	    private PickupType pickupType;
	    [SerializeField]
	    private int value;

        private void OnTriggerEnter(Collider other)
        {
		    Player player = other.GetComponent<Player>();
            if (player == null)
			    return;

            switch (pickupType)
            {
                case PickupType.Gold:
                    player.gold += value;
                    break;
                case PickupType.Health:
                    player.health += value;
                    break;
                case PickupType.Mana:
                    player.mana += value;
                    break;
                default:
                    break;
            }
        }
    }
}
