using SombraStudios.Shared.Interfaces;
using SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects;
using UnityEngine;

namespace SombraStudios.Shared.ScriptableObjects.RuntimeSets
{
    /// <summary>
    /// This is an example Runtime Set used for tracking one or more GameObjects or components at runtime.
    /// This can replace using singleton instances (which tend to make testing more difficult).
    /// </summary>
    [CreateAssetMenu(fileName = "NewGameObjectRuntimeSet", menuName = "Sombra Studios/Runtime Sets/GameObject Runtime Set")]
    public class GameObjectRuntimeSetSO : RuntimeSetSO<GameObject>, IDescribable
    {
        // Event for the Editor script 
        public System.Action ItemsChanged;

        [Header("Optional")]
        [Tooltip("Notify other objects this Runtime Set has changed")]
        [SerializeField] private VoidEventChannelSO m_RuntimeSetUpdated;

        [SerializeField] private string _description;
        public string Description { get => _description; set => _description = value; }

        private void OnEnable()
        {
            if (ItemsChanged != null)
                ItemsChanged();
        }

        // Adds one GameObject to the Items
        public override bool Add(GameObject thingToAdd)
        {
            bool success = false;

            if (!Items.Contains(thingToAdd))
            {
                Items.Add(thingToAdd);
                
                if (m_RuntimeSetUpdated != null)
                    m_RuntimeSetUpdated.RaiseEvent();

                if (ItemsChanged != null)
                    ItemsChanged();
                
                success = true;
            }
            return success;
        }

        // Removes one GameObject from the Items
        public override bool Remove(GameObject thingToRemove)
        {
            bool success = false;

            if (Items.Contains(thingToRemove))
            {
                Items.Remove(thingToRemove);
                
                if (m_RuntimeSetUpdated != null)
                    m_RuntimeSetUpdated.RaiseEvent();

                if (ItemsChanged != null)
                    ItemsChanged();
                
                success = true;
            }
            return success;
        }

        /// <summary>
        /// Reset the list of items in the runtime set.
        /// </summary>
        public void Clear()
        {
            Items.Clear();

            if (m_RuntimeSetUpdated != null)
                m_RuntimeSetUpdated.RaiseEvent();

            if (ItemsChanged != null)
                ItemsChanged();
        }

        /// <summary>
        /// Clean up any items after the list is cleared.
        /// </summary>
        public void DestroyItems()
        {
            foreach (GameObject item in Items)
            {
                Destroy(item);
            }
        }
    }
}