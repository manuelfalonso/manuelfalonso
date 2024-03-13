using System;
using UnityEngine;

namespace SombraStudios.Shared.Audio.System
{
    /// <summary>
    /// Represents data for configuring an audio rack.
    /// </summary>
    [Serializable]
    public struct RackData
    {
        /// <summary>
        /// The AudioClip to be played by the audio rack.
        /// </summary>
        public AudioClip Clip;
        /// <summary>
        /// The volume of the audio clip.
        /// </summary>
        [Range(0, 1)] public float Volume;
        /// <summary>
        /// The pitch of the audio clip.
        /// </summary>
        [Range(-3, 3)] public float Pitch;
        /// <summary>
        /// The spatial blend of the audio clip.
        /// </summary>
        [Range(0, 1)] public float SpatialBlend;
        /// <summary>
        /// Determines whether the audio clip should play on awake.
        /// </summary>
        public bool PlayOnAwake;
        /// <summary>
        /// Determines whether the audio clip should loop.
        /// </summary>
        public bool Loop;
    }
}
