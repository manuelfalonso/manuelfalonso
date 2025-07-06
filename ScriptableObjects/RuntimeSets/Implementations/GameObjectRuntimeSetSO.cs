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
        [SerializeField] private VoidEventChannelSO _runtimeSetUpdated;

        [SerializeField] private string _description;
        public string Description { get => _description; set => _description = value; }
        /// <summary>
        /// A read-only list of items in the runtime set.
        /// </summary>
        public System.Collections.Generic.IReadOnlyList<GameObject> Items => _items;

        private void OnEnable()
        {
            if (ItemsChanged != null)
                ItemsChanged();
        }

        public override bool TryAdd(GameObject thingToAdd)
        {
            if (base.TryAdd(thingToAdd) == false)
            {
                if (_runtimeSetUpdated != null)
                    _runtimeSetUpdated.RaiseEvent();

                if (ItemsChanged != null)
                    ItemsChanged();

                return true;
            }
            return false;
        }

        public override bool TryRemove(GameObject thingToRemove)
        {
            if (base.TryRemove(thingToRemove))
            {
                if (_runtimeSetUpdated != null)
                    _runtimeSetUpdated.RaiseEvent();

                if (ItemsChanged != null)
                    ItemsChanged();

                return true;
            }
            return false;
        }

        /// <summary>
        /// Reset the list of items in the runtime set.
        /// </summary>
        public override void Clear()
        {
            base.Clear();

            if (_runtimeSetUpdated != null)
                _runtimeSetUpdated.RaiseEvent();

            if (ItemsChanged != null)
                ItemsChanged();
        }

        /// <summary>
        /// Clean up any items after the list is cleared.
        /// </summary>
        public void DestroyItems()
        {
            foreach (GameObject item in _items)
            {
                Destroy(item);
            }
        }
    }
}