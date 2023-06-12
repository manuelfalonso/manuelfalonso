using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Systems.LootBox
{
    [RequireComponent(typeof(Renderer))]
    public abstract class LootBox : MonoBehaviour
    {
        [Header("Common Parameters")]

        [SerializeField] private List<Item> _items;    
        [SerializeField] private Animation _openAnimation;

        [Tooltip("Enables modified Items Rarety and Items in LootBoxes during gameplay")]
        [SerializeField] private bool _testMode;
        [Tooltip("Enables hover to show possible loot")]
        [SerializeField] private bool _enableShowPosibleLoot = false;
        [Tooltip("Enable click to open box")]
        [SerializeField] private bool _enableOpenBox = false;

        private float _totalRarety = 0f;

        protected virtual void Start()
        {
            CalculateTotalRarety();
        }

        void Update()
        {
        
        }

        private void OnMouseDown()
        {
            if (_enableOpenBox)
            {
                OpenBox();
            }
        }

        private void OnMouseEnter()
        {
            if (_enableShowPosibleLoot)
            {
                ShowPosibleLoot();
            }
        }

        private void CalculateTotalRarety()
        {
            foreach (Item item in _items)
            {
                _totalRarety += item.Rarety;
            }
        }

        private Item OpenBox()
        {
            if (_testMode)
            {
                CalculateTotalRarety();
            }
                
            Item itemToReturn = null;

            float raretyItemToReturn = Random.Range(0, _totalRarety);        

            float accumulatedRarety = 0f;

            foreach (Item item in _items)
            {
                accumulatedRarety += item.Rarety;

                if (raretyItemToReturn <= accumulatedRarety && itemToReturn == null)
                {
                    itemToReturn = item;
                    break;
                }
            }

            return itemToReturn;
        }

        private void ShowPosibleLoot()
        {
            Debug.Log("Show Items");
            //int itemIndex = 0;
            for (int i = 0; i < _items.Count; i++)
            {
                Debug.Log(_items[i].Name + " " + 
                    _items[i].Rarety + " " + 
                    _items[i].Description);
            }
        }
    }
}
