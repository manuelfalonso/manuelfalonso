using System;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.XR.KeyLockSystem
{
    /// <summary>
    /// Use this object as a generic way to validate if an object can perform some action.
    /// The check is done in the <see cref="CanUnlock"/> method.
    /// This class is used in combination with a <see cref="Keychain"/> component.
    /// </summary>
    /// <seealso cref="SocketInteractor.XRLockSocketInteractor"/>
    /// <seealso cref="SocketInteractor.XRLockGridSocketInteractor"/>
    [Serializable]
    public class Lock
    {        
        [Tooltip("The required keys to unlock this lock" +
            "Create new keys by selecting \"Assets/Create/XR/Key Lock System/Key\"")]
        [SerializeField] private List<KeySO> m_RequiredKeys;

        /// <summary>
        /// Returns the required keys to unlock this lock.
        /// </summary>
        public List<KeySO> RequiredKeys => m_RequiredKeys;


        /// <summary>
        /// Checks if the supplied keychain has all the required keys to open this lock.
        /// </summary>
        /// <param name="keychain">The keychain to be checked.</param>
        /// <returns>True if the supplied keychain has all the required keys; false otherwise.</returns>
        public bool CanUnlock(IKeychain keychain)
        {
            if (keychain == null) { return m_RequiredKeys.Count == 0; }                

            foreach (var requiredKey in m_RequiredKeys)
            {
                if (requiredKey == null) { continue; }

                if (!keychain.Contains(requiredKey)) { return false; }
            }

            return true;
        }
    }
}
