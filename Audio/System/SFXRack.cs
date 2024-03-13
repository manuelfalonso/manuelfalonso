using System;
using UnityEngine;

namespace SombraStudios.Shared.Audio.System
{
    /// <summary>
    /// Represents a rack containing audio data and an AudioSource instance.
    /// </summary>
    [Serializable]
    public class SFXRack
    {
        /// <summary>
        /// Data for configuring the audio rack.
        /// </summary>
        [Tooltip("Data for configuring the audio rack.")]
        [SerializeField] private RackData _data;

        /// <summary>
        /// Gets the transform of the instantiated SFXRack instance.
        /// </summary>
        public Transform InstanceTransform => _instanceTransform;
        /// <summary>
        /// Gets the AudioSource component of the instantiated SFXRack instance.
        /// </summary>
        public AudioSource AudioInstance => _instance;
        /// <summary>
        /// Gets the audio data associated with this SFXRack.
        /// </summary>
        public RackData AudioData => _data;

        private Transform _instanceTransform;
        private AudioSource _instance;


        /// <summary>
        /// Initializes a new instance of the SFXRack class with the specified audio data.
        /// </summary>
        /// <param name="dataRef">The audio data to associate with the SFXRack.</param>
        public SFXRack(RackData dataRef)
        {
            _data = dataRef;
        }


        /// <summary>
        /// Instantiates an AudioSource at the specified transform with the audio data of this SFXRack.
        /// </summary>
        /// <param name="transform">The transform at which to instantiate the AudioSource.</param>
        public void InstantiateEventInstance(Transform transform)
        {
            _instanceTransform = transform;
            _instance = transform.gameObject.AddComponent<AudioSource>();
            _instance.clip = _data.Clip;
            _instance.volume = _data.Volume;
            _instance.playOnAwake = _data.PlayOnAwake;
            _instance.loop = _data.Loop;
            _instance.spatialBlend = _data.SpatialBlend;
            _instance.pitch = _data.Pitch;
            _instance.transform.SetParent(_instanceTransform);
        }
    }
}
