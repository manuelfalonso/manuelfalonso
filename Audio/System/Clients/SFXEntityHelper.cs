#if A_YELLOWPAPER_SERIALIZED_COLLECTIONS
using UnityEngine;
using UnityEngine.Serialization;

namespace SombraStudios.Shared.Audio.System
{
    /// <summary>
    /// Helper class for managing SFX and voice playback for entities.
    /// </summary>
    public class SFXEntityHelper : MonoBehaviour
    {
        /// <summary>
        /// The scriptable data containing SFX and voice racks.
        /// </summary>
        [FormerlySerializedAs("_audioData")]
        [Tooltip("The scriptable data containing SFX and voice racks.")]
        [SerializeField] private SFXScriptableSO _audioSo;


        #region Unity Messages
        private void Awake()
        {
            if (_audioSo == null) { return; }

            // Creating a runtime version so it wont be overwritten for each entity.
            _audioSo = Instantiate(_audioSo);
        }

        private void Start()
        {
            if (_audioSo == null) return;

            _audioSo.SFXRack.InstantiateAll(transform);
            _audioSo.VoiceRack.InstantiateAll(transform);
        }
        #endregion


        #region Public Methods
        /// <summary>
        /// Plays the audio specified by the AnimationEvent.
        /// </summary>
        /// <param name="animationEvent">The AnimationEvent containing the audio key.</param>
        public void SFX_PlayAudio(AnimationEvent animationEvent)
        {
            PlayAudio(animationEvent.stringParameter);
        }

        /// <summary>
        /// Plays the audio specified by the key.
        /// </summary>
        /// <param name="key">The key of the audio to play.</param>
        public void SFX_PlayAudio(string key)
        {
            PlayAudio(key);
        }

        /// <summary>
        /// Plays the voice specified by the AnimationEvent.
        /// </summary>
        /// <param name="animationEvent">The AnimationEvent containing the voice key.</param>
        public void SFX_PlayVoice(AnimationEvent animationEvent)
        {
            PlayVoice(animationEvent.stringParameter);
        }

        /// <summary>
        /// Plays the voice specified by the key.
        /// </summary>
        /// <param name="key">The key of the voice to play.</param>
        public void SFX_PlayVoice(string key)
        {
            PlayVoice(key);
        }

        /// <summary>
        /// Plays the audio specified by the key in a loop.
        /// </summary>
        /// <param name="key">The key of the audio to play.</param>
        public void SFX_PlayLoopAudio(string key)
        {
            PlayLoopAudio(key);
        }

        /// <summary>
        /// Stops the looped audio specified by the key.
        /// </summary>
        /// <param name="key">The key of the audio to stop.</param>
        public void SFX_StopLoopAudio(string key)
        {
            StopLoopAudio(key);
        }
        #endregion


        #region Private Methods
        private void PlayAudio(string audio)
        {
            if (_audioSo == null) return;

            _audioSo.SFXRack.Play(audio);
        }

        private void PlayVoice(string voice)
        {
            if (_audioSo == null) return;

            _audioSo.VoiceRack.Play(voice);
        }

        private void PlayLoopAudio(string audio)
        {
            if (_audioSo == null) return;

            _audioSo.SFXRack[audio].AudioInstance.loop = true;
            _audioSo.SFXRack.Play(audio);
        }

        private void StopLoopAudio(string audio)
        {
            if (_audioSo == null) return;

            _audioSo.SFXRack.Stop(audio);
        }
        #endregion
    }
}
#endif