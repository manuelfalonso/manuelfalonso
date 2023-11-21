using UnityEngine;
using UnityEngine.UI;

namespace SombraStudios.Shared.UI
{
    /// <summary>
    /// Example script to use as an item in a UI inventory System.
    /// </summary>
    public class InventoryItemButton : MonoBehaviour
    {
        private Text buttonText;
        private string[] itemTypes = {"Armor", "Weapon", "Spell"};

        public int typeIndex;

        // Start is called before the first frame update
        void Awake()
        {
            typeIndex = Random.Range(0, 3);
            buttonText = GetComponentInChildren<Text>();
            buttonText.text = itemTypes[typeIndex];
        }
    }
}
