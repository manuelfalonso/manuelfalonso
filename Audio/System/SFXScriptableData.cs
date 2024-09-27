using System;
using UnityEngine;

namespace SombraStudios.Shared.Audio.System
{
    /// <summary>
    /// Represents a ScriptableObject containing SFX and voice data.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "New SFX Data", menuName = "Sombra Studios/Audio/SFX Data")]
    public class SFXScriptableData : ScriptableObject
    {
        /// <summary>
        /// Dictionary mapping string keys to SFXRack instances.
        /// </summary>
        [Tooltip("Dictionary mapping string keys to SFXRack instances.")]
        [SerializeField] private SFXStringDictionary _sfxRack = new ();
        /// <summary>
        /// Dictionary mapping string keys to SFXInterruptable instances.
        /// </summary>
        [Tooltip("Dictionary mapping string keys to SFXInterruptable instances.")]
        [SerializeField] private SFXInterruptableDictionary _voiceRack = new ();

        /// <summary>
        /// Gets the dictionary containing SFXRack instances.
        /// </summary>
        public SFXStringDictionary SFXRack => _sfxRack;
        /// <summary>
        /// Gets the dictionary containing SFXInterruptable instances.
        /// </summary>
        public SFXInterruptableDictionary VoiceRack => _voiceRack;
    }
}
