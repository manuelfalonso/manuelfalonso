using SombraStudios.Shared.ScriptableObjects.Patterns.Behavioural.Strategy;
using SombraStudios.Shared.Structs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SombraStudios.Shared.Audio.Strategies
{
    [CreateAssetMenu(fileName ="AudioStrategy",menuName ="Sombra Studios/Strategies/Audio Strategy")]
    public class AudioStrategy : StrategySO<AudioSource>
    {
        private const float MIN_VOLUME = 0.0f;
        private const float MAX_VOLUME = 1.0f;
        private const float MIN_PITCH = -3.0f;
        private const float MAX_PITCH = 3.0f;
        
        [SerializeField] private AudioClip[] _clips;
        [SerializeField] private RangedFloat _volume;
        [SerializeField] private RangedFloat _pitch;

        private void Awake()
        {
            _volume = new RangedFloat(MIN_VOLUME, MAX_VOLUME);
            _pitch = new RangedFloat(MIN_PITCH, MAX_PITCH);
        }

        public override bool CanExecute(AudioSource data)
        {
            if (_clips.Length == 0 || data == null)
            {
                if (_showLogs)
                    Debug.LogWarning("No clips or audio source provided.", this);
                return false;
            }

            if (_volume.MinValue < MIN_VOLUME || _volume.MaxValue > MAX_VOLUME)
            {
                if (_showLogs)
                    Debug.LogWarning("Volume out of range.", this);
                return false;
            }

            if (_pitch.MinValue < MIN_PITCH || _pitch.MaxValue > MAX_PITCH)
            {
                if (_showLogs)
                    Debug.LogWarning("Pitch out of range.", this);
                return false;
            }
            
            return true;
        }

        // If we have a valid clip, select a random clip, volume, and pitch. Then, play the sound.
        public override void Execute(AudioSource source)
        {
            if (_clips.Length == 0 || source == null)
                return;

            source.clip = _clips[Random.Range(0, _clips.Length)];
            source.volume = _volume.RandomValue;
            source.pitch = _pitch.RandomValue;

            source.Play();
        }
    }
}
