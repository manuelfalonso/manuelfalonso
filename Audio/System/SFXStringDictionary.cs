using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Audio.System
{
    /// <summary>
    /// Runtime dictionary mapping string keys to SFXRack values.
    /// Built from the serialized data held by <see cref="SFXScriptableSO"/>.
    /// </summary>
    public class SFXStringDictionary : Dictionary<string, SFXRack>
    {
        public SFXStringDictionary() { }

        public SFXStringDictionary(IDictionary<string, SFXRack> source) : base(source) { }

        /// <summary>
        /// Instantiates all SFXRack instances associated with this dictionary at the specified transform.
        /// </summary>
        /// <param name="t">The transform at which to instantiate the SFXRack instances.</param>
        public virtual void InstantiateAll(Transform t)
        {
            foreach (var rack in this)
            {
                rack.Value.InstantiateEventInstance(t);
                if (rack.Value.AudioInstance.playOnAwake)
                {
                    rack.Value.AudioInstance.Play();
                }
            }
        }

        /// <summary>
        /// Plays the audio associated with the specified key.
        /// </summary>
        /// <param name="audio">The key associated with the audio to play.</param>
        public virtual void Play(string audio)
        {
            if (TryGetValue(audio, out var rack))
            {
                rack.AudioInstance.Play();
            }
        }

        /// <summary>
        /// Stops the audio associated with the specified key.
        /// </summary>
        /// <param name="audio">The key associated with the audio to stop.</param>
        public virtual void Stop(string audio)
        {
            if (TryGetValue(audio, out var rack))
            {
                rack.AudioInstance.Stop();
            }
        }

        /// <summary>
        /// Stops all audio instances associated with this dictionary.
        /// </summary>
        public virtual void StopAllInstances()
        {
            foreach (var rack in this)
            {
                rack.Value.AudioInstance.Stop();
            }
        }
    }
}
