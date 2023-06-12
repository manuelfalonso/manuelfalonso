using UnityEngine;

namespace SombraStudios.UI
{
    /// <summary>
    /// Example script to filter and display items in a UI Inventory System.
    /// </summary>
    public class InventoryDisplay : MonoBehaviour
    {
        /// <summary>
	    /// Filter to show only the Item Types defined by the index
	    /// </summary>
	    /// <param name="itemType">Item type index</param>
        public void ShowOnly(int itemType) {
            for (int i = 0; i < transform.childCount; i++)
            {
                InventoryItemButton thisItem = transform.GetChild(i).GetComponent<InventoryItemButton>();
                thisItem.gameObject.SetActive(thisItem.typeIndex == itemType);
            }
        }

        /// <summary>
	    /// Show all Items
	    /// </summary>
        public void ShowAll() {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
