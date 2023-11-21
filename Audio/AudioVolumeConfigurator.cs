using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace SombraStudios.Shared.Audio
{

    /// <summary>
    /// Singleton Class to manage AudioMixer volumes.
    /// Provides public functions that can be called when these values change.
    /// It must receive values from 0.0001 to 1. For example by a slider.
    /// Uses PlayerPrefs to save volume OnDisable and load them OnEnable.
    /// Requiered: In the Audio Mixer create two groups. One for Main and other
    /// for Music. Music is child of Main. Expose volumne by right clicking
    /// Match the name used by the script.
    /// One slider for each volume.
    /// </summary>
    public class AudioVolumeConfigurator : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer _Mixer;
        [SerializeField]
        private Slider _overallVolumeSlider;
        [SerializeField]
        private Slider _musicVolumeSlider;

        [SerializeField]
        private string _MixerVarMainVolume = "OverallVolume";
        [SerializeField]
        private string _MixerVarMusicVolume = "MusicVolume";

        public static AudioVolumeConfigurator Instance { get; private set; }

        /// <summary>
        /// We need audio sliders use a value between 0.0001 and 1, 
        /// but the mixer works in decibels -- by default, -80 to 0.
        /// To convert, we use log10(slider) multiplied by 20. 
        /// Why 20? because log10(.0001)*20=-80, 
        /// which is the bottom range for our mixer, meaning it's disabled.
        /// </summary>
        private const float _VolumeLog10Multiplier = 20;

        private float _overallVolume = 0f;
        private float _musicVolume = 0f;


        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void OnEnable()
        {
            _overallVolume = PlayerPrefs.GetFloat("OverallVolume");
            _musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }

        private void Start()
        {
            // Initial setup of loaded volume
            SetMainVolume(_overallVolume);
            SetMusicVolume(_musicVolume);
            _overallVolumeSlider.value = _overallVolume;
            _musicVolumeSlider.value = _musicVolume;
        }

        void OnDisable()
        {
            PlayerPrefs.SetFloat("OverallVolume", _overallVolume);
            PlayerPrefs.SetFloat("MusicVolume", _musicVolume);
            PlayerPrefs.Save();
        }


        public void SetMainVolume(float rawVolume)
        {
            _overallVolume = rawVolume;
            _Mixer.SetFloat(_MixerVarMainVolume, GetVolumeInDecibels(rawVolume));
        }

        public void SetMusicVolume(float rawVolume)
        {
            _musicVolume = rawVolume;
            _Mixer.SetFloat(_MixerVarMusicVolume, GetVolumeInDecibels(rawVolume));
        }


        private float GetVolumeInDecibels(float volume)
        {
            if (volume <= 0) // sanity-check
            {
                volume = 0.0001f;
            }
            return Mathf.Log10(volume) * _VolumeLog10Multiplier;
        }
    }
}
