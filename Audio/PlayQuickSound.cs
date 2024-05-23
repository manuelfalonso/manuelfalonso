using UnityEngine;

namespace SombraStudios.Shared.Audio
{
    /// <summary>
    /// Play a simple sound using <c>PlayOneShot</c> with volume and randomized pitch.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class PlayQuickSound : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The sound that is played.")]
        private AudioClip _sound;

        [SerializeField]
        [Tooltip("The volume of the sound.")]
        private float _volume = 1f;

        [SerializeField]
        [Tooltip("The range of pitch the sound is played at (-pitch, pitch).")]
        [Range(0, 1)]
        private float _randomPitchVariance;

        private AudioSource _audioSource;

        private const float _defaultPitch = 1f;


        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }


        public void Play()
        {
            var randomVariance = Random.Range(-_randomPitchVariance, _randomPitchVariance);
            randomVariance += _defaultPitch;

            _audioSource.pitch = randomVariance;
            _audioSource.PlayOneShot(_sound, _volume);
            _audioSource.pitch = _defaultPitch;
        }
    }
}
