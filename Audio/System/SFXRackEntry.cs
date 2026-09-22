#if !UNITY_6000_6_OR_NEWER
using System;
using UnityEngine;

namespace SombraStudios.Shared.Audio.System
{
    /// <summary>
    /// A single key/value pair of the SFX racks serialized by <see cref="SFXScriptableSO"/>.
    /// Unity 6.6 serializes <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"/> natively,
    /// so this list-backed entry only exists on older Editor versions.
    /// </summary>
    [Serializable]
    public class SFXRackEntry
    {
        [Tooltip("The key used to look the rack up at runtime.")]
        [SerializeField] private string _key;
        [Tooltip("The rack played for this key.")]
        [SerializeField] private SFXRack _value;

        /// <summary>
        /// Gets the key used to look the rack up at runtime.
        /// </summary>
        public string Key => _key;
        /// <summary>
        /// Gets the rack played for this key.
        /// </summary>
        public SFXRack Value => _value;
    }
}
#endif
