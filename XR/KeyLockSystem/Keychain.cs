using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.XR.KeyLockSystem
{
    /// <summary>
    /// A generic Keychain component that holds the <see cref="KeySO"/>s to open a <see cref="Lock"/>.
    /// Attach a Keychain component to an Interactable and assign to it the same Keys of an <see cref="SocketInteractor.XRLockSocketInteractor"/>
    /// or an <see cref="SocketInteractor.XRLockGridSocketInteractor"/> to open (or interact with) them.
    /// </summary>
    [DisallowMultipleComponent]
    public class Keychain : MonoBehaviour, IKeychain
    {
        [Tooltip("The keys on this keychain" +
            "Create new keys by selecting \"Assets/Create/XR/Key Lock System/Key\"")]
        [SerializeField] private List<KeySO> _keys;

        private HashSet<int> _keysHashSet = new HashSet<int>();


        void Awake()
        {
            RepopulateHashSet();
        }

        void OnValidate()
        {
            // A key was added through the inspector while the game was running?
            if (Application.isPlaying && _keys.Count != _keysHashSet.Count) { RepopulateHashSet(); }
        }


        private void RepopulateHashSet()
        {
            _keysHashSet.Clear();
            foreach (var key in _keys)
            {
                if (key != null) { _keysHashSet.Add(key.GetInstanceID()); }
            }
        }

        /// <summary>
        /// Adds the supplied key to this keychain
        /// </summary>
        /// <param name="key">The key to be added to the keychain</param>
        public void AddKey(KeySO key)
        {
            if (key == null || Contains(key)) { return; }

            _keys.Add(key);
            _keysHashSet.Add(key.GetInstanceID());
        }

        /// <summary>
        /// Adds the supplied key from this keychain
        /// </summary>
        /// <param name="key">The key to be removed from the keychain</param>
        public void RemoveKey(KeySO key)
        {
            _keys.Remove(key);

            if (key != null) { _keysHashSet.Remove(key.GetInstanceID()); }
        }

        /// <inheritdoc />
        public bool Contains(KeySO key)
        {
            return key != null && _keysHashSet.Contains(key.GetInstanceID());
        }
    }
}
