using System;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Audio.System
{
    /// <summary>
    /// Represents a ScriptableObject containing SFX and voice data.
    /// Unity 6.6 and newer serialize the racks as native dictionaries; older Editor
    /// versions fall back to a list of <see cref="SFXRackEntry"/>. Both paths expose
    /// the same runtime API.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "New SFX Data", menuName = "Sombra Studios/Audio/SFX Data")]
    public class SFXScriptableSO : ScriptableObject
    {
#if UNITY_6000_6_OR_NEWER
        /// <summary>
        /// Dictionary mapping string keys to SFXRack instances.
        /// </summary>
        [Tooltip("Dictionary mapping string keys to SFXRack instances.")]
        [SerializeField] private Dictionary<string, SFXRack> _sfxRack = new();
        /// <summary>
        /// Dictionary mapping string keys to SFX Interruptable instances.
        /// </summary>
        [Tooltip("Dictionary mapping string keys to SFX Interruptable instances.")]
        [SerializeField] private Dictionary<string, SFXRack> _voiceRack = new();
#else
        /// <summary>
        /// Entries mapping string keys to SFXRack instances.
        /// </summary>
        [Tooltip("Entries mapping string keys to SFXRack instances.")]
        [SerializeField] private List<SFXRackEntry> _sfxRack = new();
        /// <summary>
        /// Entries mapping string keys to SFX Interruptable instances.
        /// </summary>
        [Tooltip("Entries mapping string keys to SFX Interruptable instances.")]
        [SerializeField] private List<SFXRackEntry> _voiceRack = new();
#endif

        private SFXStringDictionary _sfxRuntimeRack;
        private SFXInterruptableDictionary _voiceRuntimeRack;

        /// <summary>
        /// Gets the dictionary containing SFXRack instances.
        /// </summary>
        public SFXStringDictionary SFXRack =>
            _sfxRuntimeRack ??= new SFXStringDictionary(BuildLookup(_sfxRack));
        /// <summary>
        /// Gets the dictionary containing SFXInterruptable instances.
        /// </summary>
        public SFXInterruptableDictionary VoiceRack =>
            _voiceRuntimeRack ??= new SFXInterruptableDictionary(BuildLookup(_voiceRack));

#if UNITY_6000_6_OR_NEWER
        private static IDictionary<string, SFXRack> BuildLookup(Dictionary<string, SFXRack> source) => source;
#else
        private IDictionary<string, SFXRack> BuildLookup(List<SFXRackEntry> source)
        {
            var lookup = new Dictionary<string, SFXRack>(source.Count);

            foreach (var entry in source)
            {
                if (entry == null || string.IsNullOrEmpty(entry.Key))
                {
                    Debug.LogWarning($"{name} contains an SFX entry with no key; it was skipped.", this);
                    continue;
                }

                if (!lookup.TryAdd(entry.Key, entry.Value))
                {
                    Debug.LogWarning($"{name} contains duplicate SFX key '{entry.Key}'; " +
                        $"only the first one is used.", this);
                }
            }

            return lookup;
        }
#endif
    }
}
