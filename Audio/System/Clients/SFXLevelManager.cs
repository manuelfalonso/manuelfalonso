#if NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
using SombraStudios.Shared.Patterns.Creational.Singleton;
using UnityEngine;

namespace SombraStudios.Shared.Audio.System.Clients
{
    /// <summary>
    /// Manages audio levels and playback in the game.
    /// </summary>
    public class SFXLevelManager : Singleton<SFXLevelManager>
    {
        [SerializeField] private SFXScriptableSO _audioSo;
        [Header("General Properties")]
        /// <summary>
        /// Determines whether level data configurations can be overwritten.
        /// </summary>
        [Tooltip("Determines whether level data configurations can be overwritten.")]
        [SerializeField] private bool _canLevelDataConfigOvewritten;
        /// <summary>
        /// The volume of the SFX audio.
        /// </summary>
        [Tooltip("The volume of the SFX audio.")]
        [SerializeField, Range(0, 1)] private float _volume = 1f;
        /// <summary>
        /// The volume of the dialogue audio.
        /// </summary>
        [Tooltip("The volume of the dialogue audio.")]
        [SerializeField, Range(0, 1)] private float _dialogueVolume = 1f;
        /// <summary>
        /// Determines whether audio is muted on start.
        /// </summary>
        [Tooltip("Determines whether audio is muted on start.")]
        [SerializeField] private bool _muteOnStart;
        /// <summary>
        /// Determines whether volumes are set on start.
        /// </summary>
        [Tooltip("Determines whether volumes are set on start.")]
        [SerializeField] private bool _setVolumesOnStart;


        #region Monobehaviour
        void Start()
        {
            InitializeRackInstances();

            if (_setVolumesOnStart)
            {
                SetSFXVolume(_volume);
                SetDialogueVolume(_dialogueVolume);
            }

            if (_muteOnStart)
            {
                SetSFXMute(_muteOnStart);
            }
        }
        #endregion


        #region Public
        /// <summary>
        /// Plays a one-shot audio clip at the specified position.
        /// </summary>
        /// <param name="audioEvent">The key of the audio event to play.</param>
        /// <param name="position">The position at which to play the audio.</param>
        public void PlayOneShotAudio(string audioEvent, Vector3 position)
        {
            AudioSource.PlayClipAtPoint(_audioSo.SFXRack[audioEvent].AudioData.Clip, position);
        }
        #endregion


        #region Private Methods
        private void SetSFXVolume(float volume)
        {
            if (_canLevelDataConfigOvewritten)
            {
                foreach (var rack in _audioSo.SFXRack)
                {
                    rack.Value.AudioInstance.volume = volume;
                }
            }
        }

        private void SetDialogueVolume(float volume)
        {
            if (_canLevelDataConfigOvewritten)
            {
                foreach (var rack in _audioSo.VoiceRack)
                {
                    rack.Value.AudioInstance.volume = volume;
                }
            }
        }

        private void SetSFXMute(bool mute)
        {
            if (_canLevelDataConfigOvewritten)
            {
                foreach (var rack in _audioSo.SFXRack)
                {
                    rack.Value.AudioInstance.mute = mute;
                }
            }
        }

        private void SetDialogueMute(bool mute)
        {
            if (_canLevelDataConfigOvewritten)
            {
                foreach (var rack in _audioSo.VoiceRack)
                {
                    rack.Value.AudioInstance.mute = mute;
                }
            }
        }

        private void InitializeRackInstances()
        {
            if (_audioSo == null) { return; }

            _audioSo.SFXRack.InstantiateAll(transform);
            _audioSo.VoiceRack.InstantiateAll(transform);
        }
        #endregion


        #region Buttons
        [Button]
        public void SetVolume() => SetSFXVolume(_volume);

        [Button]
        public void MuteVolume() => SetSFXMute(true);

        [Button]
        public void UnmuteVolume() => SetSFXMute(false);
        #endregion
    }
}
#endif