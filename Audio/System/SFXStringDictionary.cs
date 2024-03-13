#if A_YELLOWPAPER_SERIALIZED_COLLECTIONS
using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

namespace SombraStudios.Shared.Audio.System
{
    /// <summary>
    /// Represents a dictionary mapping string keys to SFXRack values.
    /// </summary>
    [Serializable]
    public class SFXStringDictionary : SerializedDictionary<string, SFXRack>
    {
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
#endif